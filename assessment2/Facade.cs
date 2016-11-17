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
        public void createCustomer(string name, string address, int customerReferenceNumber)
        {
            customerFactory.createCustomer(name, address, customerReferenceNumber);
        }

        public void createBooking(string arrivalDate, string departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire)
        {
            bookingFactory.createBooking(arrivalDate, departureDate, bookingReferenceNumber, eveningMeals, breakfast, carHire);
        }

        public void createGuest(string guestName, int passportNumber, int age)
        {
            guestFactory.createGuest(guestName, passportNumber, age);
        }

        private void amendFile()
        {

        }

        private void readFile()
        {

        }
    }
}
