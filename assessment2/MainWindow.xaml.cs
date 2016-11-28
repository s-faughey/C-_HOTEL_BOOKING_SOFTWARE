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
        Facade facade = new Facade(); //this program uses the facade design pattern, create a new facade
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e) //method to add a customer
        {
            facade.createCustomer(tbCustomerName.Text, tbCustomerAddress.Text); //access the createCustomer method in facade
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e) //method to add a booking
        {
            bool carHire = (bool) cbCarHire.IsChecked; //checks if customer wants a car hire
            bool eveningMeals = (bool)cbEveningMeals.IsChecked; //checks if customer wants evening meals
            bool breakfast = (bool)cbBreakfast.IsChecked; //checks if customer wants breakfast
            facade.createBooking(tbArrivalDate.Text, tbDepartureDate.Text, //acess the createBooking method in facade
                eveningMeals, breakfast, carHire);
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e) //method to add a guest
        {
            facade.createGuest(tbGuestName.Text, Int32.Parse(tbPassportNumber.Text), Int32.Parse(tbAge.Text)); //access the createGuest method in facade
        }

        //finds the customer given the customer reference number
        private void btnReadCustomer_Click(object sender, RoutedEventArgs e) //method to read customer
        {
            int customerResult; //initialize customer result
            Int32.TryParse(tbCustomerReferenceNumber.Text, out customerResult); //parse customer reference number if "" then = 0
            if (customerResult > 0) //if customer reference number > 0
            {
                if (facade.readCustomer(customerResult) != null) { //read the customer using the customer reference number
                    tbCustomerName.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text)).Name; //access the facade.readCustomer method and update the GUI
                    tbCustomerAddress.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text)).Address; //^^
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
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult); //parse the booking reference number, 0 if not valid
            if (bookingResult > 0) { //if the booking reference number is valid
                if (facade.readBooking(bookingResult) != null) //and the booking exists
                {
                    tbArrivalDate.Text = facade.readBooking(bookingResult).ArrivalDate; //update arrival date
                    tbDepartureDate.Text = facade.readBooking(bookingResult).DepartureDate; //update departure date
                    cbEveningMeals.IsChecked = facade.readBooking(bookingResult).EveningMeals; //update evening meals
                    cbBreakfast.IsChecked = facade.readBooking(bookingResult).Breakfast;//update breakfast
                    cbCarHire.IsChecked = facade.readBooking(bookingResult).CarHire;//update car hire
                }
                else
                {
                    MessageBox.Show("This is not a valid booking reference number!");
                }
            }
        }

        //finds the guest given the passport number
        private void btnReadGuest_Click(object sender, RoutedEventArgs e)
        {
            int passportNumber;
            Int32.TryParse(tbPassportNumber.Text, out passportNumber); //parse tbPassportNumber.Text, 0 if invalid
            if (passportNumber != 0) { //if valid
                if (facade.readGuest(passportNumber) != null) { //and the guest exists
                    tbGuestName.Text = facade.readGuest(passportNumber).Name; //update the guest name
                    tbAge.Text = facade.readGuest(passportNumber).Age.ToString(); //update the guest age
                }
            }
            else
            {
                MessageBox.Show("This is not a valid passport number!");
            }
        }

        //deletes the customer given the customerReferenceNumber
        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(Int32.Parse(tbCustomerReferenceNumber.Text), "customer"); //access deleteObject telling it we are deleting a customer
                                                                                            //and the customer reference number
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(Int32.Parse(tbBookingReferenceNumber.Text), "booking");
        }

        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(Int32.Parse(tbPassportNumber.Text), "guest");
        }

        private void btnAmendCustomer_Click(object sender, RoutedEventArgs e)
        {
            facade.amendCustomer(Int32.Parse(tbCustomerReferenceNumber.Text), "customer", tbCustomerName.Text, tbCustomerAddress.Text);
        }

        private void btnAmendBooking_Click(object sender, RoutedEventArgs e)
        {
            facade.amendBooking(tbArrivalDate.Text, tbDepartureDate.Text, Int32.Parse(tbBookingReferenceNumber.Text), 
                (bool) cbEveningMeals.IsChecked,(bool) cbBreakfast.IsChecked, (bool) cbCarHire.IsChecked);
        }

        private void btnAmendGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.amendGuest(tbGuestName.Text, Int32.Parse(tbAge.Text), Int32.Parse(tbPassportNumber.Text));
        }
    }
}