using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    [Serializable]
    class Guest
    {
        private int age;
        private string name;
        private int passportNumber;
        public Guest(int age, string name, int passportNumber)
        {
            this.age = age;
            this.name = name;
            this.passportNumber = passportNumber;
        }

        public int PassportNumber
        {
            get
            {
                return this.passportNumber;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("This is wrong");
                }
                this.passportNumber = value;
            }
        }
    }
}
