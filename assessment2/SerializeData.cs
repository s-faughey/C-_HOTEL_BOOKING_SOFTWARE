using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    public class SerializeData
    {
        FileStream stream = null;
        BinaryFormatter bformatter = null;
        String txtFileName = "";
        ArrayList objectArray = new ArrayList();
        public SerializeData(String fileName)
        {
            txtFileName = fileName;
            bformatter = new BinaryFormatter();
        }

        public void createFile()
        {
            if (!File.Exists(txtFileName)) {
                stream = File.Open(txtFileName, FileMode.Create);
                closeStream();
            }
        }


        public void serializeObject(Object objectToSerialize)
        {
            createFile();
            objectArray = deserializeArray();
            objectArray.Add(objectToSerialize);
            using (var stream = File.OpenWrite(txtFileName))
            {
                bformatter.Serialize(stream, objectArray);
            }
        }

        public int findFirstAvailableNumber(string objectToCount)
        {
            int counter = 0;
            deserializeArray();
            for (int i = 0; i < objectArray.Count; i++ )
            {
                if (objectToCount == "customer") {
                    if (objectArray[i] is Customer)
                    {
                        counter++;
                    }
                }

                if (objectToCount == "booking")
                {
                    if (objectArray[i] is Booking)
                    {
                        counter++;
                    }
                }

                if (objectToCount == "guest") {
                    if (objectArray[i] is Guest) {
                        counter++;
                    }
                }
            }
            return counter+1;
        }

        public ArrayList deserializeArray()
        {
            createFile();
            var list = new ArrayList();
            using (var stream = File.OpenRead(txtFileName))
            {
                if (stream.Length != 0) {
                    list = (ArrayList)bformatter.Deserialize(stream);
                }
            }
            objectArray = list;
            Console.WriteLine(list);
            return list;
        }

        public Object deserializeObject(int objectIdentifier, string objectType, string passportNumber)
        {
            createFile();
            Object objectToReturn = null;
            objectArray = deserializeArray();
            try
            {
                for (int i = 0; i < objectArray.Count; i++)
                {
                    if (objectType == "customer")
                    {
                        if (objectArray[i] is Customer)
                        {
                            Customer customer = (Customer)objectArray[i];
                            if (customer.CustomerReferenceNumber == objectIdentifier)
                            {
                                objectToReturn = customer;
                                break;
                            }
                        }
                    }

                    if (objectType == "booking")
                    {
                        if (objectArray[i] is Booking)
                        {
                            Booking booking = (Booking)objectArray[i];
                            if (booking.BookingReferenceNumber == objectIdentifier)
                            {
                                objectToReturn = booking;
                                break;
                            }
                        }
                    }

                    if (objectType == "guest")
                    {
                        if (objectArray[i] is Guest)
                        {
                            Guest guest = (Guest)objectArray[i];
                            if (guest.PassportNumber == passportNumber)
                            {
                                objectToReturn = guest;
                                break;
                            }
                        }
                    }
                }
            }

            catch(SerializationException e)
            {
                Console.WriteLine(e);
            }
            
            return objectToReturn;
        }

        public bool deleteObject(int objectIdentifier, string objectType, string passportNumber)
        {
            createFile();
            deserializeArray();
            bool foundObject = false;
            for (int i = 0; i < objectArray.Count; i++)
            {
                if (objectType == "customer" && objectArray[i] is Customer) {
                    Customer objectToCheck = (Customer) objectArray[i];
                    if (objectToCheck.CustomerReferenceNumber == objectIdentifier)
                    {
                        objectArray.RemoveAt(i);
                        foundObject = true;
                        break;
                    }
                }
                if (objectType == "booking" && objectArray[i] is Booking)
                {
                    Booking objectToCheck = (Booking)objectArray[i];
                    if (objectToCheck.BookingReferenceNumber == objectIdentifier)
                    {
                        objectArray.RemoveAt(i);
                        foundObject =  true;
                        break;
                    }
                }
                if (objectType == "guest" && objectArray[i] is Guest)
                {
                    Guest objectToCheck = (Guest)objectArray[i];
                    if (objectToCheck.PassportNumber == passportNumber)
                    {
                        objectArray.RemoveAt(i);
                        foundObject =  true;
                        break;
                    }
                }
            }
            stream = File.Open(txtFileName, FileMode.Create);
            bformatter.Serialize(stream, objectArray);
            closeStream();
            return foundObject;
        }

        public void amendCustomer(int objectIdentifier, string name, string address)
        {
            ArrayList objectArray = deserializeArray();
            for (int i = 0; i < objectArray.Count; i++)
            {
                if (objectArray[i] is Customer)
                {
                    Customer objectToCheck = (Customer)objectArray[i];
                    if (objectToCheck.CustomerReferenceNumber == objectIdentifier)
                    {
                        objectToCheck.Name = name;
                        objectToCheck.Address = address;
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);
                        }
                    }
                }
            }
        }

        public void amendBooking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, string eveningDiet, string breakfastDiet, DateTime from, DateTime until, int customerReferenceNumber, string passportNumber, string driver)
        {
            ArrayList objectArray = deserializeArray();
            for (int i = 0; i < objectArray.Count; i++)
            {
                if (objectArray[i] is Booking)
                {
                    Booking objectToCheck = (Booking)objectArray[i];
                    if (objectToCheck.BookingReferenceNumber == bookingReferenceNumber)
                    {
                        objectToCheck.ArrivalDate = arrivalDate;
                        objectToCheck.DepartureDate = departureDate;
                        objectToCheck.EveningMeals = eveningMeals;
                        objectToCheck.Breakfast = breakfast;
                        objectToCheck.CarHire = carHire;
                        objectToCheck.EveningDietaryRequirements = eveningDiet;
                        objectToCheck.BreakfastDietaryRequirements = breakfastDiet;
                        objectToCheck.CarHireStart = from;
                        objectToCheck.CarHireEnd = until;
                        objectToCheck.Driver = driver;
                        if (customerReferenceNumber != 0)
                        {
                            objectToCheck.CustomerReferenceNumber = customerReferenceNumber;
                        }
                        if (passportNumber != "")
                        {
                            if (objectToCheck.GuestArray.Count == 0) {
                                objectToCheck.GuestArray.Add(passportNumber);
                            }
                            else
                            {
                                bool foundIt = false;
                                for (int count = 0; count < objectToCheck.GuestArray.Count; count++)
                                {
                                    if (objectToCheck.GuestArray[count] == passportNumber)
                                    {
                                        foundIt = true;
                                    }
                                }
                                if (!foundIt)
                                {
                                    objectToCheck.GuestArray.Add(passportNumber);
                                }
                            }
                        }
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);
                        }
                    }
                }
            }
        }

        public void amendGuest(string name, int age, string objectIdentifier)
        {
            ArrayList objectArray = deserializeArray();
            for (int i = 0; i < objectArray.Count; i++)
            {
                if (objectArray[i] is Guest)
                {
                    Guest objectToCheck = (Guest)objectArray[i];
                    if (objectToCheck.PassportNumber == objectIdentifier)
                    {
                        objectToCheck.Name = name;
                        objectToCheck.Age = age;
                        using (var stream = File.OpenWrite(txtFileName))
                        {
                            bformatter.Serialize(stream, objectArray);
                        }
                    }
                }
            }
        }

        public void closeStream()
        {
            stream.Close();
        }
    }
}
