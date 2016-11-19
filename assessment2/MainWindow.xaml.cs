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
            facade.createCustomer(tbCustomerName.Text, tbCustomerAddress.Text, Int32.Parse(tbCustomerReferenceNumber.Text));
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            facade.createBooking(tbArrivalDate.Text, tbDepartureDate.Text, Int32.Parse(tbBookingReferenceNumber.Text), (bool) cbEveningMeals.IsChecked, (bool) cbBreakfast.IsChecked, (bool) cbCarHire.IsChecked, Int32.Parse(tbCustomerReferenceNumber.Text));
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.createGuest(tbGuestName.Text, Int32.Parse(tbPassportNumber.Text), Int32.Parse(tbAge.Text), Int32.Parse(tbBookingReferenceNumber.Text));
        }

        private void btnReadCustomer_Click(object sender, RoutedEventArgs e)
        {
            int customerResult;
            Int32.TryParse(tbCustomerReferenceNumber.Text, out customerResult);
            if (customerResult > 0) {
                int result;
                Int32.TryParse(tbBookingReferenceNumber.Text, out result);
                if (result > 0) {
                    tbCustomerReferenceNumber.Text = facade.readCustomer(customerResult, result).CustomerReferenceNumber.ToString();
                    tbCustomerName.Text = facade.readCustomer(customerResult, result).Name;
                    tbCustomerAddress.Text = facade.readCustomer(customerResult, result).Address;
                }
            }
            int customerResult2;
            Int32.TryParse(tbCustomerReferenceNumber.Text, out customerResult2);
            if (customerResult2 == 0)
            {
                if (Int32.Parse(tbBookingReferenceNumber.Text) > 0)
                {
                    int customerReferenceNumber = 0;
                    tbCustomerName.Text = facade.readCustomer(customerReferenceNumber, Int32.Parse(tbBookingReferenceNumber.Text)).Name;
                    tbCustomerAddress.Text = facade.readCustomer(customerReferenceNumber, Int32.Parse(tbBookingReferenceNumber.Text)).Address;
                    tbCustomerReferenceNumber.Text = facade.readCustomer(customerReferenceNumber, Int32.Parse(tbBookingReferenceNumber.Text)).CustomerReferenceNumber.ToString();
                }
            }

            int bookingResult;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult);
            if (bookingResult == 0)
            {
                if (Int32.Parse(tbCustomerReferenceNumber.Text) > 0)
                {
                    int bookingReferenceNumber = 0;
                    tbCustomerName.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text), bookingReferenceNumber).Name;
                    tbCustomerAddress.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text), bookingReferenceNumber).Address;
                    tbCustomerReferenceNumber.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text), bookingReferenceNumber).CustomerReferenceNumber.ToString();
                }
            }
        }
    }
}