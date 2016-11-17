using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class Booking
    {
        private string arrivalDate;
        private int bookingReferenceNumber;
        private string departureDate;
        private bool eveningMeals;
        private bool breakfast;
        private bool carHire;
        public Booking(string arrivalDate, string departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire)
        {
            arrivalDate = this.arrivalDate;
            bookingReferenceNumber = this.bookingReferenceNumber;
            departureDate = this.departureDate;
            eveningMeals = this.eveningMeals;
            breakfast = this.breakfast;
            carHire = this.carHire;
        }

    }
}
