using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author name: Sean Faughey
//Description: this is a booking object meant ot represent a booking in the napire holiday village.
//  Holds information regarding the booking including who made the booking (customer) and who is in the booking (guests)
//This class is hidden from the GUI using the facade design pattern (Facade class), and is created using the BookingFactory class.
//Last modified: 06/12/2016
namespace assessment2
{
    [Serializable] //allows booking objects to be added to a file using a serializer
    public class Booking
    {
        private DateTime arrivalDate; //represents the date of arrival of the guests
        private int bookingReferenceNumber; //represents the booking reference number, a unique identifier of bookings
        private int customerReferenceNumber; //represents the customer reference number of the customer who made the booking
        private DateTime departureDate; //represents the date of departure of the guests
        private bool eveningMeals; //whether the customer chose to include evening meals
        private bool breakfast; //whether the customer chose to include breakfast
        private bool carHire; //whether the customer chose to include car hire
        private string eveningDietaryRequirements; //any dietary requirements of the evening meals
        private string breakfastDietaryRequirements; //any breakfast dietary requirements
        private DateTime carHireStart; //date of start of the car hire
        private DateTime carHireEnd; //date of the car hire end
        private string driver; //name of the driver of the car
        private List<string> guestArray; //list of guests on the booking


        //constructor
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

        //getters and setters for all of the properties

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
