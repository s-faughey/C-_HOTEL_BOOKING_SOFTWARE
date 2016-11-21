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

        public void createBooking(string arrivalDate, string departureDate, bool eveningMeals, bool breakfast, bool carHire)
        {
            serializer.serializeObject(bookingFactory.createBooking(
                arrivalDate, departureDate, serializer.findFirstAvailableNumber("booking"), eveningMeals, breakfast, carHire));
        }

        public void createGuest(string guestName, int passportNumber, int age)
        {
            serializer.serializeObject(guestFactory.createGuest(guestName, passportNumber, age));
        }

        public Customer readCustomer(int customerReferenceNumber)
        {
            Customer customer = null;
            if (customerReferenceNumber != 0) {
                customer = (Customer) serializer.deserializeObject(customerReferenceNumber, "customer");
            }
            return customer;
        }

        public Booking readBooking(int bookingReferenceNumber)
        {
            Booking booking = null;
            if (bookingReferenceNumber != 0)
            {
                booking = (Booking)serializer.deserializeObject(bookingReferenceNumber, "booking");
            }
            return booking;
        }

        public Guest readGuest(int passportNumber)
        {
            Guest guest = null;
            if (passportNumber != 0) {
                guest = (Guest)serializer.deserializeObject(passportNumber, "guest");
            }
            return guest;
        }

        public void deleteObject(int objectIdentifier, string objectType)
        {
            serializer.deleteObject(objectIdentifier, objectType);
        }

        public void writeToFile(Object item)
        {
            serializer.serializeObject(item);
            serializer.closeStream();
        }
    }
}
