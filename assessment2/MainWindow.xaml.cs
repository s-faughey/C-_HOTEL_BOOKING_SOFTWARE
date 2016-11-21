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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace assessment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Facade facade = new Facade();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            facade.createCustomer(tbCustomerName.Text, tbCustomerAddress.Text);
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            bool carHire = (bool) cbCarHire.IsChecked;
            bool eveningMeals = (bool)cbEveningMeals.IsChecked;
            bool breakfast = (bool)cbBreakfast.IsChecked;
            facade.createBooking(tbArrivalDate.Text, tbDepartureDate.Text, 
                eveningMeals, breakfast, carHire);
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.createGuest(tbGuestName.Text, Int32.Parse(tbPassportNumber.Text), Int32.Parse(tbAge.Text));
        }

        private void btnReadCustomer_Click(object sender, RoutedEventArgs e)
        {
            // finds the customer given only the customer reference number
            int customerResult;
            Int32.TryParse(tbCustomerReferenceNumber.Text, out customerResult);
            if (customerResult > 0)
            {
                if (facade.readCustomer(customerResult) != null) {
                    tbCustomerName.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text)).Name;
                    tbCustomerAddress.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text)).Address;
                }

                else
                {
                    MessageBox.Show("This is not a valid customer reference number.");
                }
            }   
        }


        //finds the booking given the booking reference number
        private void btnReadBooking_Click(object sender, RoutedEventArgs e)
        {
            int bookingResult;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult);
            if (bookingResult != 0) {
                if (facade.readBooking(bookingResult) != null)
                {
                    tbArrivalDate.Text = facade.readBooking(bookingResult).ArrivalDate;
                    tbDepartureDate.Text = facade.readBooking(bookingResult).DepartureDate;
                    cbEveningMeals.IsChecked = facade.readBooking(bookingResult).EveningMeals;
                    cbBreakfast.IsChecked = facade.readBooking(bookingResult).Breakfast;
                    cbCarHire.IsChecked = facade.readBooking(bookingResult).CarHire;
                }
            }
        }

        private void btnReadGuest_Click(object sender, RoutedEventArgs e)
        {
            int passportNumber;
            Int32.TryParse(tbPassportNumber.Text, out passportNumber);
            if (passportNumber != 0) {
                if (facade.readGuest(passportNumber) != null) {
                    tbGuestName.Text = facade.readGuest(passportNumber).Name;
                    tbAge.Text = facade.readGuest(passportNumber).Age.ToString();
                }
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(Int32.Parse(tbCustomerReferenceNumber.Text), "customer");
        }
    }
}