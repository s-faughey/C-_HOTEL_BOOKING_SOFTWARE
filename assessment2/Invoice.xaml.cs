using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Author name: Sean Faughey
//Description: This class deals with the GUI for the Invoice window
//Last modified: 06/12/2016
namespace assessment2
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        private Booking booking;    //a booking object to be displayed on screen
        public Invoice(Booking booking, SerializeData serializer) //a constructor
        {
            InitializeComponent();
            this.booking = booking;
            InvoiceBooking invoice = new InvoiceBooking(booking, serializer); //create a new invoicebooking object which deals with calculations
            tbBookingReferenceNumber.Text = booking.BookingReferenceNumber.ToString(); //show the booking reference number on screen
            tbCostPerNight.Text = invoice.basicCost().ToString(); //show the cost per night
            tbExtrasCost.Text = invoice.extrasCost().ToString(); //show the extras cost
        }
    }
}
