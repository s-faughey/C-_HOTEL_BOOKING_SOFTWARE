using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace assessment2
{
    class InvoiceBooking
    {
        private Booking booking;
        private SerializeData serializer;
        private List<string> listOfGuests;

        public InvoiceBooking(Booking booking, SerializeData serializer)
        {
            this.booking = booking;
            this.serializer = serializer;
            listOfGuests = booking.GuestArray;
        }

        public int totalCost()
        {
            int totalCost = basicCost() + extrasCost();
            return totalCost;
        }

        public int basicCost()
        {
            int basicCost = 0;
            int totalDays = (booking.DepartureDate - booking.ArrivalDate).Days;
            foreach (String passport in listOfGuests) {
                Guest guest = (Guest)serializer.deserializeObject(0, "guest", passport);
                MessageBox.Show(guest.Age.ToString());
                if (guest.Age < 18) {
                    basicCost = basicCost + 30;
                }

                if (guest.Age >= 18) {
                    basicCost = basicCost + 50;
                }
            }
            return basicCost;
        }

        public int extrasCost()
        {
            int extrasCost = 0;
            if (booking.CarHire) {
                int carHireDays = (booking.CarHireEnd - booking.CarHireStart).Days;
                extrasCost = carHireDays * 50;
            }
            if (booking.Breakfast) {
                int breakfastCost = listOfGuests.Count * 5;
                extrasCost = extrasCost + breakfastCost;
            }

            if (booking.EveningMeals) {
                int eveningMealsCost = listOfGuests.Count * 15;
                extrasCost = extrasCost + eveningMealsCost;
            }
            return extrasCost;
        }
    }
}
