using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class Facade
    {
        CustomerFactory customerFactory = new CustomerFactory();
        BookingFactory bookingFactory = new BookingFactory();
        GuestFactory guestFactory = new GuestFactory();
        SerializeData serializer = new SerializeData("testBinaryFile.txt");
        public void createCustomer(string name, string address)
        {
            int customerReferenceNumber = serializer.findFirstAvailableNumber("customer");
            serializer.serializeObject(customerFactory.createCustomer(name, address, customerReferenceNumber));
        }

        public void createBooking(DateTime arrivalDate, DateTime departureDate, bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber, string dietEvening, string dietBreakfast, DateTime from, DateTime until, string driver)
        {
            serializer.serializeObject(bookingFactory.createBooking(
                arrivalDate, departureDate, serializer.findFirstAvailableNumber("booking"), eveningMeals, breakfast, carHire, customerReferenceNumber, dietEvening, dietBreakfast, from, until, driver));
        }

        public void createGuest(string guestName, string passportNumber, int age)
        {
            serializer.serializeObject(guestFactory.createGuest(guestName, passportNumber, age));
        }

        public Customer readCustomer(int customerReferenceNumber)
        {
            Customer customer = null;
            if (customerReferenceNumber != 0) {
                customer = (Customer) serializer.deserializeObject(customerReferenceNumber, "customer", "0");
            }
            return customer;
        }

        public Booking readBooking(int bookingReferenceNumber)
        {
            Booking booking = null;
            if (bookingReferenceNumber != 0)
            {
                booking = (Booking)serializer.deserializeObject(bookingReferenceNumber, "booking", "0");
            }
            return booking;
        }

        public Guest readGuest(string passportNumber)
        {
            Guest guest = null;
            if (passportNumber != "") {
                guest = (Guest)serializer.deserializeObject(0, "guest", passportNumber);
            }
            return guest;
        }

        public bool deleteObject(int objectIdentifier, string objectType, string passportNumber)
        {
            return serializer.deleteObject(objectIdentifier, objectType, passportNumber);
        }

        public void amendCustomer(int objectIdentifier, string objectType, string name, string address)
        {
            serializer.amendCustomer(objectIdentifier, name, address);
        }

        public void amendBooking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, string eveningDiet, string breakfastDiet, DateTime from, DateTime until, int customerReferenceNumber, string passportNumber, string driver)
        {
            serializer.amendBooking(arrivalDate, departureDate, bookingReferenceNumber, eveningMeals, breakfast, carHire, eveningDiet, breakfastDiet, from, until, customerReferenceNumber, passportNumber, driver);
        }

        public void amendGuest(string name, int age, string passportNumber)
        {
            serializer.amendGuest(name, age, passportNumber);
        }

        public void createInvoice(int bookingReferenceNumber) {

            Invoice invoice = new Invoice(readBooking(bookingReferenceNumber), serializer);
            invoice.Show();
        }
    }
}
