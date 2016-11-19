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
        public void createCustomer(string name, string address, int customerReferenceNumber)
        {
            serializer.SerializeObject(customerFactory.createCustomer(name, address, customerReferenceNumber));
            serializer.closeStream();
        }

        public void createBooking(string arrivalDate, string departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber)
        {
            serializer.SerializeObject(bookingFactory.createBooking(
                arrivalDate, departureDate, bookingReferenceNumber, eveningMeals, breakfast, carHire, customerReferenceNumber));
        }

        public void createGuest(string guestName, int passportNumber, int age, int bookingReferenceNumber)
        {
            serializer.SerializeObject(guestFactory.createGuest(guestName, passportNumber, age, bookingReferenceNumber));
        }

        public Customer readCustomer(int customerReferenceNumber, int bookingReferenceNumber)
        {
            Customer customer = null;
            if (customerReferenceNumber == 0) {
                if (bookingReferenceNumber != 0)
                {
                    Booking booking = (Booking)serializer.DeserializeObjects(bookingReferenceNumber);
                    customer = (Customer)serializer.DeserializeObjects(booking.CustomerReferenceNumber);
                }
            }
            else if (customerReferenceNumber != 0) {
                customer = (Customer) serializer.DeserializeObjects(customerReferenceNumber);
            }
            return customer;
        }

        public Booking readBooking(int bookingReferenceNumber)
        {
            Booking booking = null;
            if (bookingReferenceNumber != null)
            {
                booking = (Booking)serializer.DeserializeObjects(bookingReferenceNumber);
            }
            return booking;
        }

        public Guest readGuest(int passportNumber)
        {
            Guest guest = null;
            if (passportNumber != null) {
                guest = (Guest)serializer.DeserializeObjects(passportNumber);
            }
            return guest;
        }

        public void amendFile()
        {

        }

        public void writeToFile(Object item)
        {
            serializer.SerializeObject(item);
            serializer.closeStream();
        }
        public Object readObject(int objectIdentifier)
        {
            return serializer.DeserializeObjects(objectIdentifier);
        }
    }
}
