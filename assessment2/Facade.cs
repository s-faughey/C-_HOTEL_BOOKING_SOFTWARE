using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//Author: Sean Faughey 
//Description: This class follows the facade design pattern in order to seperate the main functionality of the solution from the GUI
//Date last modified: 06/12/2016
//Design pattern: Facade
namespace assessment2
{
    public class Facade
    {
        CustomerFactory customerFactory = new CustomerFactory(); //produces a customer factory
        BookingFactory bookingFactory = new BookingFactory(); //produces a booking factory
        GuestFactory guestFactory = new GuestFactory(); //produces a guest factory
        SerializeData serializer = new SerializeData("testBinaryFile.txt"); //produces a serializer in order to store data
        public void createCustomer(string name, string address) //method to create a customer
        {
            int customerReferenceNumber = serializer.findFirstAvailableNumber("customer"); //finds the first available number for a customer reference number
            serializer.serializeObject(customerFactory.createCustomer(name, address, customerReferenceNumber)); //access the serializer and store the customer using the customer factory
        }

        //method to create a booking
        public void createBooking(DateTime arrivalDate, DateTime departureDate, bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber, string dietEvening, string dietBreakfast, DateTime from, DateTime until, string driver)
        {
            serializer.serializeObject(bookingFactory.createBooking(            //access the serializer and store the booking using the booking factory
                arrivalDate, departureDate, serializer.findFirstAvailableNumber("booking"), eveningMeals, breakfast, carHire, customerReferenceNumber, dietEvening, dietBreakfast, from, until, driver));
        }

        //method to create a guest
        public void createGuest(string guestName, string passportNumber, int age)
        {
            serializer.serializeObject(guestFactory.createGuest(guestName, passportNumber, age)); //access the serializer and store the guest using the guest factory
        }

        //method to retreive a customer
        public Customer readCustomer(int customerReferenceNumber)
        {
            Customer customer = null;
            if (customerReferenceNumber != 0) { //if given a customer reference number
                customer = (Customer) serializer.deserializeObject(customerReferenceNumber, "customer", "0"); //find the customer usnig the serializer
            }
            return customer;
        }

        //method to retreive a booking
        public Booking readBooking(int bookingReferenceNumber)
        {
            Booking booking = null;
            if (bookingReferenceNumber != 0) //if given a booking reference number
            {
                booking = (Booking)serializer.deserializeObject(bookingReferenceNumber, "booking", "0"); //find the booking using the serializer
            }
            return booking;
        }

        //method to retreieve a guest
        public Guest readGuest(string passportNumber)
        {
            Guest guest = null;
            if (passportNumber != "") { //if given a passport number
                guest = (Guest)serializer.deserializeObject(0, "guest", passportNumber); //find the guest using the passport number
            }
            return guest;
        }

        //method to delete an object from the file
        public bool deleteObject(int objectIdentifier, string objectType, string passportNumber)
        {
            return serializer.deleteObject(objectIdentifier, objectType, passportNumber); //delete an object from file using the serializer
        }

        //method to delete a guest from the relevant booking
        public void deleteGuest(string passportNumber, Booking booking)
        {
            booking.GuestArray.Remove(passportNumber); //remove the guest from the booking given
            serializer.amendBookingRemovePassport(booking); //use the serializer to amend the booking which the guest is attributed to
            serializer.deleteObject(0, "guest", passportNumber); //use the serializer to remove the guest from file
        }

        //method to amend a customer's information
        public bool amendCustomer(int objectIdentifier, string objectType, string name, string address)
        {
            return serializer.amendCustomer(objectIdentifier, name, address); //use the serializer to amend the information
        }

        //method to amend a booking's information
        public bool amendBooking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, string eveningDiet, string breakfastDiet, DateTime from, DateTime until, int customerReferenceNumber, string passportNumber, string driver)
        {
            //use the serializer to amend the booking information
            return serializer.amendBooking(arrivalDate, departureDate, bookingReferenceNumber, eveningMeals, breakfast, carHire, eveningDiet, breakfastDiet, from, until, customerReferenceNumber, passportNumber, driver);
        }

        //methoid to amend a guest's information
        public bool amendGuest(string name, int age, string passportNumber)
        {
            //use the serializer to amend the guest information
            return serializer.amendGuest(name, age, passportNumber);
        }

        //method to create an invoice of a booking
        public void createInvoice(int bookingReferenceNumber) {
            //create a new invoice object by reading the booking, passing it a serializer to read from
            Invoice invoice = new Invoice(readBooking(bookingReferenceNumber), serializer);
            invoice.Show(); //show a new Invoice window
        }
    }
}
