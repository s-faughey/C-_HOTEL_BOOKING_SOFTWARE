using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace assessment2
{
    public class SerializeData
    {
        FileStream stream = null;                                                                               //open a new FileStream
        BinaryFormatter bformatter = null;                                                                      //open a new BinaryFormatter
        String txtFileName = "";                                                                                //create a blank filename to be input
        ArrayList objectArray = new ArrayList();                                                                //create a blank list which will contain all objects
        public SerializeData(String fileName)
        {
            txtFileName = fileName;                                                                             //supply the file name
            bformatter = new BinaryFormatter();
        }

        //method to check whether a file exists and if not create one
        public void createFile()
        {
            if (!File.Exists(txtFileName)) {                                                                    //check if the file exists, if not
                stream = File.Open(txtFileName, FileMode.Create);                                               //create the file
                closeStream();
            }
        }

        //this method writes an object to the file
        public void serializeObject(Object objectToSerialize)
        {
            createFile();                                                                                       //check if the file exists
            objectArray = deserializeArray();                                                                   //read in all of the objects to an array
            objectArray.Add(objectToSerialize);                                                                 //add this object to the array
            using (var stream = File.OpenWrite(txtFileName))
            {
                bformatter.Serialize(stream, objectArray);                                                      //write the new array to the file
            }
        }

        //method to find the first available number for unique auto-incrementing identifiers
        public int findFirstAvailableNumber(string objectToCount)
        {
            int counter = 0;
            deserializeArray();                                                                                 //read in all objects
            for (int i = 0; i < objectArray.Count; i++ )                                                        //for every object
            {
                if (objectToCount == "customer") 
                {                                                                                               //if the object we're looking to find the first available number for is a customer
                    if (objectArray[i] is Customer)                                                             //and the object in the array is a customer
                    {
                        counter++;                                                                              //count of customers in the array
                    }
                }

                if (objectToCount == "booking")                                                                 //if the object we're looking to find the first available number for is a booking
                {                                                                                               //and the object in the array is a booking
                    if (objectArray[i] is Booking)
                    {
                        counter++;                                                                              //count of bookings in the array
                    }
                }

                if (objectToCount == "guest")                                                                   //if the object we're looking to find the first available number for is a guest
                {                                                                                               //and the object in the arra is a guest
                    if (objectArray[i] is Guest) 
                    {
                        counter++;                                                                              //count of guests in the array
                    }
                }
            }
            return counter+1;                                                                                   //return the next number up from the number of objects in the array
        }

        //method to read in the entire file
        public ArrayList deserializeArray()
        {
            createFile();                                                                                       //check if the file exists
            var list = new ArrayList();                                                                                 
            using (var stream = File.OpenRead(txtFileName))
            {
                if (stream.Length != 0) {                                                                       //if the file is not blank
                    list = (ArrayList)bformatter.Deserialize(stream);                                           //read the file and make the list = the contents
                }
            }
            objectArray = list;
            return list;                                                                                        //return a list representing the list stored in the file
        }

        //method to read a specific object from file
        public Object deserializeObject(int objectIdentifier, string objectType, string passportNumber)
        {
            createFile();                                                                                       //check to see if the file exists
            Object objectToReturn = null;
            objectArray = deserializeArray();                                                                   //read in the entire file
            try
            {
                for (int i = 0; i < objectArray.Count; i++)                                                     //for every object in the list
                {
                    if (objectType == "customer")                                                               //if we're reading a customer
                    {
                        if (objectArray[i] is Customer)                                                         //and the object in the array is a customer
                        {
                            Customer customer = (Customer)objectArray[i];                                       //read the customer
                            if (customer.CustomerReferenceNumber == objectIdentifier)                           //if its the customer we're looking for
                            {
                                objectToReturn = customer;                                                      //return the customer
                                break;                                                                          //exit loop
                            }
                        }
                    }

                    if (objectType == "booking")                                                                //if we're read a booking
                    {
                        if (objectArray[i] is Booking)                                                          //and the object in the array is a booking
                        {
                            Booking booking = (Booking)objectArray[i];                                          //read the booking
                            if (booking.BookingReferenceNumber == objectIdentifier)                             //if its the booking we're looking for
                            {
                                objectToReturn = booking;                                                       //return the booking
                                break;                                                                          //exit the loop
                            }
                        }
                    }

                    if (objectType == "guest")                                                                  //if we're reading a guest
                    {
                        if (objectArray[i] is Guest)                                                            //and the object in the array is a guest
                        {
                            Guest guest = (Guest)objectArray[i];                                                //read the guest
                            if (guest.PassportNumber == passportNumber)                                         //if its the guest we're looking for
                            {
                                objectToReturn = guest;                                                         //return the guest
                                break;                                                                          //exit loop
                            }
                        }
                    }
                }
            }

            catch(SerializationException e)
            {
                Console.WriteLine(e);
            }
            
            return objectToReturn;                                                                              //return the customer/booking/guest
        }

        //method to delete an object from file
        public bool deleteObject(int objectIdentifier, string objectType, string passportNumber)
        {
            createFile();                                                                                       //check to see if the file exists
            deserializeArray();                                                                                 //read in the entire file to an array
            bool foundObject = false;                                                                           //set boolean foundobject to be false
            for (int i = 0; i < objectArray.Count; i++)                                                         //for every object in the array
            {
                if (objectType == "customer" && objectArray[i] is Customer) {                                   //if we're looking for a customer and the object in the array is a customer
                    Customer objectToCheck = (Customer) objectArray[i];                                         //read the customer
                    if (objectToCheck.CustomerReferenceNumber == objectIdentifier)                              //if its the customer we're looking for
                    {
                        bool customerHasBookings = false;                                                       //set boolean to false
                        foreach (Object thing in objectArray)                                                   //for every object in the array
                        {
                            if (thing is Booking)                                                               //if it is a booking
                            {
                                Booking booking = (Booking)thing;                                               //read the booking
                                if (booking.CustomerReferenceNumber == objectIdentifier)                        //if the booking is related to the customer we are looking to delete
                                {
                                    MessageBox.Show("This customer has bookings, cannot delete.");              //message the user that we cannot delete the customer
                                    customerHasBookings = true;                                                 //set customer has bookings to be true
                                    break;                                                                      //exi the loop
                                }
                            }
                        }
                        if (!customerHasBookings)                                                               //if the customer doesnt have any bookings
                        {
                            objectArray.RemoveAt(i);                                                            //remove the customer from the array
                            foundObject = true;                                                                 //set foundObject to true
                            break;                                                                              //exit the loop
                        }
                    }
                }
                if (objectType == "booking" && objectArray[i] is Booking)                                       //if we're looking to delete a booking and the object in the array is a booking
                {
                    Booking objectToCheck = (Booking)objectArray[i];                                            //read the booking
                    if (objectToCheck.BookingReferenceNumber == objectIdentifier)                               //if its the specific booking we're looking for
                    {
                        objectArray.RemoveAt(i);                                                                //remove the booking
                        foundObject =  true;                                                                    //set found object to be true
                        break;                                                                                  //exit the loop
                    }
                }
                if (objectType == "guest" && objectArray[i] is Guest)                                           //if we're looking to delete a guest and the object in the array is a guest
                {
                    Guest objectToCheck = (Guest)objectArray[i];                                                //read the guest
                    if (objectToCheck.PassportNumber == passportNumber)                                         //if its the specific guest we're looking for
                    {
                        objectArray.RemoveAt(i);                                                                //remove the guest form the array
                        foundObject =  true;                                                                    //set found object to be true
                        break;                                                                                  //exit the loop
                    }
                }
            }
            stream = File.Open(txtFileName, FileMode.Create);                                                   //open stream to the file, ready to be overwritten
            bformatter.Serialize(stream, objectArray);                                                          //overwrite the file
            closeStream();                                                                                      //close the stream to the file, we're finished writing
            return foundObject;                                                                                 //return whether we deleted the file
        }

        //metohd to amend a specific customer
        public bool amendCustomer(int objectIdentifier, string name, string address)
        {
            bool amendedCustomer = false;                                                                       //boolean whether we have amended the customer
            ArrayList objectArray = deserializeArray();                                                         //read in the entire file
            for (int i = 0; i < objectArray.Count; i++)                                                         //for every object in the file
            {
                if (objectArray[i] is Customer)                                                                 //if the object is a customer
                {
                    Customer objectToCheck = (Customer)objectArray[i];                                          //read in the customer
                    if (objectToCheck.CustomerReferenceNumber == objectIdentifier)                              //if its the customer we're looking for
                    {
                        objectToCheck.Name = name;                                                              //amend the name
                        objectToCheck.Address = address;                                                        //amend the address
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);                                          //overwrite the new array to the file
                            amendedCustomer = true;                                                             //boolean = true, we have amended the customer
                        }
                    }
                }
            }
            return amendedCustomer;                                                                             //return whether we ahve amended the customer
        }

        //method to amend a specific booking
        public bool amendBooking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, string eveningDiet, string breakfastDiet, DateTime from, DateTime until, int customerReferenceNumber, string passportNumber, string driver)
        {
            bool amendedBooking = false;                                                                        //boolean stating whethe rwe have amended the booking
            ArrayList objectArray = deserializeArray();                                                         //read in the entire file
            for (int i = 0; i < objectArray.Count; i++)                                                         //for every object in the file
            {
                if (objectArray[i] is Booking)                                                                  //if the object is a booking
                {
                    Booking objectToCheck = (Booking)objectArray[i];                                            //read in the booking
                    if (objectToCheck.BookingReferenceNumber == bookingReferenceNumber)                         //if its the specific booking we were looking for
                    {
                        objectToCheck.ArrivalDate = arrivalDate;                                                //ammend the booking's information
                        objectToCheck.DepartureDate = departureDate;                                            
                        objectToCheck.EveningMeals = eveningMeals;
                        objectToCheck.Breakfast = breakfast;
                        objectToCheck.CarHire = carHire;
                        objectToCheck.EveningDietaryRequirements = eveningDiet;
                        objectToCheck.BreakfastDietaryRequirements = breakfastDiet;
                        objectToCheck.CarHireStart = from;
                        objectToCheck.CarHireEnd = until;
                        objectToCheck.Driver = driver;
                        if (customerReferenceNumber != 0)                                                       //if we have provided a customer reference number - we dont want to set this to 0!
                        {
                            objectToCheck.CustomerReferenceNumber = customerReferenceNumber;                    //amend the customer reference number
                        }
                        if (passportNumber != "")                                                               //if we have provided a guest which we want to add
                        {
                            if (objectToCheck.GuestArray.Count == 0) {                                          //if there are no guests on this booking
                                objectToCheck.GuestArray.Add(passportNumber);                                   //add the guest
                            }
                            else                                                                                //if there are guests on this booking
                            {
                                bool foundIt = false;                                                           //boolean to see if this guest already exists
                                for (int count = 0; count < objectToCheck.GuestArray.Count; count++)            //for every guest on the booking
                                {
                                    if (objectToCheck.GuestArray[count] == passportNumber)                      //if the guest we're looking to add is on the booking
                                    {
                                        foundIt = true;                                                         //boolean = true
                                    }
                                }
                                if (!foundIt)                                                                   //if boolean = false - we never found the guest
                                {
                                    objectToCheck.GuestArray.Add(passportNumber);                               //add the guest
                                }
                            }
                        }
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);                                          //overwrite the array to the file
                            amendedBooking = true;                                                              //return will = true
                        }
                    }
                }
            }
            return amendedBooking;                                                                              //return whether the booking was amended
        }

        //method to amend the booking when deleting a guest
        public void amendBookingRemovePassport(Booking booking)
        {
            ArrayList objectArray = deserializeArray();                                                         //read in the entire file
            for (int i = 0; i < objectArray.Count; i++)                                                         //for every object in the file
            {
                if (objectArray[i] is Booking)                                                                  //if the object is a booking
                {
                    Booking objectToCheck = (Booking) objectArray[i];                                           //read the booking
                    if (objectToCheck.BookingReferenceNumber == booking.BookingReferenceNumber )                //if its the booking we're looking for
                    {
                        objectArray[i] = booking;                                                               //this booking = the booking provided
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);                                          //overwrite this booking to file
                        }
                    }
                }
            }
        }

        //method to amend a guest
        public bool amendGuest(string name, int age, string objectIdentifier)
        {
            bool amendedGuest = false;                                                                          //boolean whether we have amended the guest
            ArrayList objectArray = deserializeArray();                                                         //read in the entire file
            for (int i = 0; i < objectArray.Count; i++)                                                         //for every object in the file
            {
                if (objectArray[i] is Guest)                                                                    //if the object is a guest
                {
                    Guest objectToCheck = (Guest)objectArray[i];                                                //read in the guest
                    if (objectToCheck.PassportNumber == objectIdentifier)                                       //if the guest is the specific guest we are looking for
                    {
                        objectToCheck.Name = name;                                                              //amend the guest name
                        objectToCheck.Age = age;                                                                //amend the guest age
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);                                          //overwrite the guest to the file
                            amendedGuest = true;                                                                //boolean stating whether we have amended the guest is true
                        }
                    }
                }
            }
            return amendedGuest;                                                                                //return the boolean
        }

        //method to close the file stream, allowing it to be accessible later
        public void closeStream()
        {
            stream.Close(); //close the stream
        }
    }
}
