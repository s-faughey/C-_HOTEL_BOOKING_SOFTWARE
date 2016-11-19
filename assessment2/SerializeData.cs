using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class SerializeData
    {
        Stream stream = null;
        BinaryFormatter bformatter = null;
        String txtFileName = "";
        public SerializeData(String fileName)
        {
            txtFileName = fileName;
            stream = File.Open(txtFileName, FileMode.Create);
            bformatter = new BinaryFormatter();
            closeStream();
        }

        public void SerializeObject(Object objectToSerialize)
        {
            stream = File.Open(txtFileName, FileMode.Append);
            bformatter.Serialize(stream, objectToSerialize);
            closeStream();
        }

        public Object DeserializeObjects(int objectIdentifier)
        {
            Object objectFromStream = null;
            stream = File.Open(txtFileName, FileMode.Open);
            Object objectToReturn = null;
            try
            {
                while (stream.CanSeek)
                {
                    objectFromStream = (Object)bformatter.Deserialize(stream);

                    if(objectFromStream is Customer) {
                        Customer customer = (Customer)objectFromStream;
                        if (customer.CustomerReferenceNumber == objectIdentifier) {
                            objectToReturn = customer;
                        }
                    }

                    else if (objectFromStream is Booking) {
                        Booking booking = (Booking)objectFromStream;
                        if (booking.BookingReferenceNumber == objectIdentifier) {
                            objectToReturn = booking;
                        }
                    }

                    else if (objectFromStream is Guest) {
                        Guest guest = (Guest)objectFromStream;
                        if (guest.PassportNumber == objectIdentifier) {
                            objectToReturn = guest;
                        }
                    }
                }
            }
            catch(SerializationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            closeStream();
            return objectToReturn;
        }

        public void closeStream()
        {
            stream.Close();
        }
    }
}
