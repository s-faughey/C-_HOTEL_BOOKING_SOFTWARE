using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    [Serializable]
    public class Booking
    {
        private DateTime arrivalDate;
        private int bookingReferenceNumber;
        private int customerReferenceNumber;
        private DateTime departureDate;
        private bool eveningMeals;
        private bool breakfast;
        private bool carHire;
        private string eveningDietaryRequirements;
        private string breakfastDietaryRequirements;
        private DateTime carHireStart;
        private DateTime carHireEnd;
        private string driver;
        private List<string> guestArray;

        public Booking(DateTime arrivalDate, DateTime departureDate, int bookingReferenceNumber,
            bool eveningMeals, bool breakfast, bool carHire, int customerReferenceNumber, string eveningDietaryRequirements,
            string breakfastDietaryRequirements, DateTime carHireStart, DateTime carHireEnd, string driver)
        {
            guestArray = new List<string>();
            this.arrivalDate = arrivalDate;
            this.bookingReferenceNumber = bookingReferenceNumber;
            this.departureDate = departureDate;
            this.eveningMeals = eveningMeals;
            this.breakfast = breakfast;
            this.carHire = carHire;
            this.eveningDietaryRequirements = eveningDietaryRequirements;
            this.breakfastDietaryRequirements = breakfastDietaryRequirements;
            this.carHireStart = carHireStart;
            this.carHireEnd = carHireEnd;
            this.customerReferenceNumber = customerReferenceNumber;
            this.driver = driver;
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


        public DateTime ArrivalDate
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

        public DateTime DepartureDate
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
                this.departureDate = value;
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
                if (value == null)
                {
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
                if (value == null)
                {
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
                if (value == null)
                {
                    throw new Exception("wrong");
                }
                this.eveningMeals = value;
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
                    throw new Exception("cant happen pal");
                }
                this.customerReferenceNumber = value;
            }
        }

        public List<string> GuestArray
        {
            get
            {
                return this.guestArray;
            }
        }

        public string EveningDietaryRequirements
        {
            get
            {
                return this.eveningDietaryRequirements;
            }
            set
            {
                this.eveningDietaryRequirements = value;
            }
        }

        public string BreakfastDietaryRequirements
        {
            get
            {
                return this.breakfastDietaryRequirements;
            }
            set
            {
                this.breakfastDietaryRequirements = value;
            }
        }

        public DateTime CarHireStart
        {
            get
            {
                return this.carHireStart;
            }
            set
            {
                this.carHireStart = value;
            }
        }

        public DateTime CarHireEnd
        {
            get
            {
                return this.carHireEnd;
            }
            set
            {
                this.carHireEnd = value;
            }
        }

        public string Driver
        {
            get
            {
                return this.driver;
            }
            set
            {
                this.driver = value;
            }
        }
    }
}
