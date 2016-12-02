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
        private string passportNumber;
        public Guest(string name, string passportNumber, int age)
        {
            this.age = age;
            this.name = name;
            this.passportNumber = passportNumber;
        }

        public string PassportNumber
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

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null) {
                    throw new Exception("This is wrong");
                }
                this.name = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value == null) {
                    throw new Exception("This is wrong");
                }
                this.age = value;
            }
        }
    }
}
