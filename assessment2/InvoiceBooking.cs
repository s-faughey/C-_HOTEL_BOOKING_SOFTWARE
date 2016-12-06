using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//Author name: Sean Faughey
//Description: This class deals with calculations of a booking invoice
//Last modified: 06/12/2016
namespace assessment2
{
    class InvoiceBooking
    {
        private Booking booking; //represents a booking to be represented
        private SerializeData serializer; //a serializer in order to read from file
        private List<string> listOfGuests; //a list of all the guests of the booking

        public InvoiceBooking(Booking booking, SerializeData serializer) //constructor
        {
            this.booking = booking;
            this.serializer = serializer;
            listOfGuests = booking.GuestArray; //gets the guests from the booking
        }

        //a method to calculate the total cost of a booking
        public int totalCost()
        {
            int totalCost = basicCost() + extrasCost(); //cost per night per guest + extras per night
            return totalCost;
        }

        //a method to calculate the basic cost of a booking before extras
        public int basicCost()
        {
            int basicCost = 0;
            int totalDays = (booking.DepartureDate - booking.ArrivalDate).Days; //calculates the number of nights the booking si for
            foreach (String passport in listOfGuests) { //for every guest
                Guest guest = (Guest)serializer.deserializeObject(0, "guest", passport); //read the guest from file
                if (guest.Age < 18) { //if the guest is under 18 add 30 to the basic cost
                    basicCost = basicCost + 30;
                }

                if (guest.Age >= 18) { //if the guest is 18 or over add 50 to the basic cost
                    basicCost = basicCost + 50;
                }
            }
            return basicCost; //return the basic cost
        }


        //method to calculate the cos tof all the extras per night
        public int extrasCost()
        {
            int extrasCost = 0;
            if (booking.CarHire) { //if the booking has a car hire
                int carHireDays = (booking.CarHireEnd - booking.CarHireStart).Days; //calculate for how long
                extrasCost = carHireDays * 50; //multiply nights by price (50)
            }
            if (booking.Breakfast) { //if the customer chose to have breakfast included
                int breakfastCost = listOfGuests.Count * 5; //count the number of guests and multiply by the cost (5)
                extrasCost = extrasCost + breakfastCost; //add the breakfast cost to the total extras cost
            }

            if (booking.EveningMeals) { //if the customer chose to have evening meals included
                int eveningMealsCost = listOfGuests.Count * 15; //count the number of guests and multiply by the cost (15)
                extrasCost = extrasCost + eveningMealsCost; //add the evening meals cost to the total extras cost
            }
            return extrasCost; //return the overall cost of extras
        }
    }
}
