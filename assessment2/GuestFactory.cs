using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class GuestFactory
    {
        public void createGuest(string guestName, int passportNumber, int age)
        {
            Guest guest = new Guest(age, guestName, passportNumber);
        }

        private void deleteGuest()
        {

        }

        private void amendGuest()
        {

        }
    }
}
