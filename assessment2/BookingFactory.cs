using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class BookingFactory
    {
        public Booking createBooking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber, string eveningDietaryRequirements,
            string breakfastDietaryRequirements, DateTime carHireStart, DateTime carHireEnd, string driver)
        {
            Booking booking = new Booking(arrivalDate, departureDate, bookingReferenceNumber,
            eveningMeals, breakfast, carHire, customerReferenceNumber, eveningDietaryRequirements, breakfastDietaryRequirements, carHireStart, carHireEnd, driver);
            return booking;
        }
    }
}
