﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class BookingFactory
    {
        public void createBooking(string arrivalDate, string departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire)
        {
            Booking booking = new Booking(arrivalDate, departureDate, bookingReferenceNumber, eveningMeals, breakfast, carHire);
        }

        private void deleteBooking()
        {

        }

        private void amendBooking()
        {

        }
    }
}