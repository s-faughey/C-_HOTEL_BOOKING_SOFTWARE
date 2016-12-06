using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author name: Sean Faughey
//Description: This is a factory which follows the factory design pattern in order to create bookings with unique identifiers
//This class is hidden from the GUI using the facade design pattern (Facade class)
//Last modified: 06/12/2016
namespace assessment2
{
    class BookingFactory
    {
        //creates a booking object and returns it
        public Booking createBooking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber, string eveningDietaryRequirements,
            string breakfastDietaryRequirements, DateTime carHireStart, DateTime carHireEnd, string driver)
        {
            Booking booking = new Booking(arrivalDate, departureDate, bookingReferenceNumber,
            eveningMeals, breakfast, carHire, customerReferenceNumber, eveningDietaryRequirements, breakfastDietaryRequirements, carHireStart, carHireEnd, driver);
            return booking;
        }
    }
}
