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
            facade.createBooking(tbArrivalDate.Text, tbDepartureDate.Text, Int32.Parse(tbBookingReferenceNumber.Text), (bool) cbEveningMeals.IsChecked, (bool) cbBreakfast.IsChecked, (bool) cbCarHire.IsChecked);
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.createGuest(tbGuestName.Text, Int32.Parse(tbPassportNumber.Text), Int32.Parse(tbAge.Text));
        }
    }
}
