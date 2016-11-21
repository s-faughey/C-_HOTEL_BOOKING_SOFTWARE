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
        public Booking(string arrivalDate, string departureDate, int bookingReferenceNumber, bool eveningMeals, bool breakfast, bool carHire)
        {
            this.arrivalDate = arrivalDate;
            this.bookingReferenceNumber = bookingReferenceNumber;
            this.departureDate = departureDate;
            this.eveningMeals = eveningMeals;
            this.breakfast = breakfast;
            this.carHire = carHire;
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

    
        public string ArrivalDate
        {
            get
            {
                return this.arrivalDate;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("This is wrong!");
                }
                this.arrivalDate = value;
            }
        }

        public string DepartureDate
        {
            get
            {
                return this.departureDate;
            }
            set
            {
                if (value == null) 
                {
                    throw new Exception("This is wrong");
                }
                this.arrivalDate = value;
            }
        }

        public bool Breakfast
        {
            get
            {
                return this.breakfast;
            }
            set
            {
                if (value == null) {
                    throw new Exception("this is wrong");
                }
                this.breakfast = value;
            }
        }

        public bool CarHire
        {
            get
            {
                return this.carHire;
            }
            set
            {
                if (value == null) {
                    throw new Exception("wrnog");
                }
                this.carHire = value;
            }
        }

        public bool EveningMeals
        {
            get
            {
                return this.eveningMeals;
            }
            set
            {
                if (value == null) {
                    throw new Exception("wrong");
                }
                this.carHire = value;
            }
        }
    }
}
