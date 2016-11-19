using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    [Serializable]
    class Booking
    {
        private string arrivalDate;
        private int bookingReferenceNumber;
        private string departureDate;
        private bool eveningMeals;
        private bool breakfast;
        private bool carHire;
        private int customerReferenceNumber;
        public Booking(string arrivalDate, string departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber)
        {
            this.arrivalDate = arrivalDate;
            this.bookingReferenceNumber = bookingReferenceNumber;
            this.departureDate = departureDate;
            this.eveningMeals = eveningMeals;
            this.breakfast = breakfast;
            this.carHire = carHire;
            this.customerReferenceNumber = customerReferenceNumber;
        }

        public int BookingReferenceNumber
        {
            get
            {
                return this.bookingReferenceNumber;
            }
            set
            {
                if (value == null) 
                {
                    throw new Exception("This is wrong");
                }
                this.bookingReferenceNumber = value;
            }
        }

        public int CustomerReferenceNumber
        {
            get
            {
                return this.customerReferenceNumber;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("This is wrong");
                }
                this.bookingReferenceNumber = value;
            }
        }
    }
}
