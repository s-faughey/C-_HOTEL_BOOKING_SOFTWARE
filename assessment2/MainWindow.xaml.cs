using System;
using System.Collections.Generic;
using System.Globalization;
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
            tbPassportNumber.MaxLength = 10;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e) //method to add a customer
        {
            if (tbCustomerName.Text != "" && tbCustomerAddress.Text != "") { //Check that there is input in all necessary fields
                facade.createCustomer(tbCustomerName.Text, tbCustomerAddress.Text); //access the createCustomer method in facade
            }

            else
            {
                MessageBox.Show("You must input all necessary customer fields!");
            }
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e) //method to add a booking
        {
            try
           {
                DateTime arrivalDate = DateTime.ParseExact(tbArrivalDate.Text, "dd/MM/yyyy", null);
                DateTime departureDate = DateTime.ParseExact(tbDepartureDate.Text, "dd/MM/yyyy", null);
                bool carHire = (bool)cbCarHire.IsChecked; //checks if customer wants a car hire
                bool eveningMeals = (bool)cbEveningMeals.IsChecked; //checks if customer wants evening meals
                bool breakfast = (bool)cbBreakfast.IsChecked; //checks if customer wants breakfast
                string fromString = tbFrom.Text;
                DateTime expectedDate;
                if ((bool)cbCarHire.IsChecked)                                                              //if the car hire box is checked
                {
                    if (!DateTime.TryParseExact(tbFrom.Text, "dd/MM/yyyy", new CultureInfo("en-US"),        //if either form or until box is unfilled
                            DateTimeStyles.None, out expectedDate) || !DateTime.TryParseExact(tbUntil.Text, "dd/MM/yyyy", new CultureInfo("en-US"),
                                DateTimeStyles.None, out expectedDate))
                    {
                        MessageBox.Show("You must enter from and until dates for the car hire.");       //tell the user to fill them
                        return;                                                                         //exit method
                    }

                    else
                    {
                        if (tbDriver.Text != "") {
                            DateTime until = DateTime.ParseExact(tbUntil.Text, "dd/MM/yyyy", null);
                            DateTime from = DateTime.ParseExact(tbFrom.Text, "dd/MM/yyyy", null);
                            facade.createBooking(arrivalDate, departureDate,                                    //access the createBooking() method in facade
                                    eveningMeals, breakfast, carHire, Int32.Parse(tbCustomerReferenceNumber.Text), tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, from, until, tbDriver.Text);
                        }
                        else
                        {
                            MessageBox.Show("Please enter a driver name");
                        }
                    }
                }

                if (!(bool)cbCarHire.IsChecked)                                                                 //if the customer doesnt want a car
                {
                    string date = "01/01/0001";
                    DateTime from = DateTime.Parse(date);
                    DateTime until = DateTime.Parse(date);
                    facade.createBooking(arrivalDate, departureDate,                                    //access the createBooking() method in facade
                                eveningMeals, breakfast, carHire, Int32.Parse(tbCustomerReferenceNumber.Text), tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, from, until, tbDriver.Text);
                }
            }
            catch (FormatException exc) {
                MessageBox.Show(exc.Message);
                MessageBox.Show("Please make sure that you have entered all dates in the correct format");
            }
        }


        private void btnAddGuest_Click(object sender, RoutedEventArgs e) //method to add a guest
        {
            int bookingResult;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult); //parse the booking reference number, 0 if not valid
            if (bookingResult > 0)
            { //if the booking reference number is valid
                if (facade.readBooking(bookingResult) != null) //and the booking exists
                {
                    int age;
                    if (Int32.TryParse(tbAge.Text, out age)) {
                        if (0 <= age && age <= 101) {
                            Booking booking = facade.readBooking(bookingResult);
                            if (booking.GuestArray.Count < 4) {
                                facade.createGuest(tbGuestName.Text, tbPassportNumber.Text, Int32.Parse(tbAge.Text)); //access the createGuest method in facade
                                facade.amendBooking(booking.ArrivalDate, booking.DepartureDate, bookingResult, booking.EveningMeals, booking.Breakfast, booking.CarHire, booking.EveningDietaryRequirements, booking.BreakfastDietaryRequirements, booking.CarHireStart, booking.CarHireEnd,
                                    booking.CustomerReferenceNumber, tbPassportNumber.Text, booking.Driver); //only amends the passport array
                            }
                            else
                            {
                                MessageBox.Show("There are already 4 guests on this holiday.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please insert an age between 0 and 101");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please insert an age between 0 and 101");
                    }
                }
                else
                {
                    MessageBox.Show("This is not a valid booking reference number!");
                }
            }

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
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult);                               //parse the booking reference number, 0 if not valid
            if (bookingResult > 0) {                                                                        //if the booking reference number is valid
                if (facade.readBooking(bookingResult) != null)                                              //and the booking exists
                {
                    Booking booking = facade.readBooking(bookingResult);
                    tbArrivalDate.Text = booking.ArrivalDate.Date.ToShortDateString();                     //update arrival date
                    tbDepartureDate.Text = booking.DepartureDate.Date.ToShortDateString();                          //update departure date
                    cbEveningMeals.IsChecked = booking.EveningMeals;                                        //update evening meals
                    cbBreakfast.IsChecked = booking.Breakfast;                                              //update breakfast
                    cbCarHire.IsChecked = booking.CarHire;                                                  //update car hire
                    tbDietaryRequirements.Text = booking.EveningDietaryRequirements;                        //update Evening Meals dietary requirements
                    tbDietaryRequirements_Breakfast.Text = booking.BreakfastDietaryRequirements;            //update Breakfast dietary requirements
                    tbDriver.Text = booking.Driver;
                    tbCustomerReferenceNumber.Text = booking.CustomerReferenceNumber.ToString();

                    if (!booking.CarHire) {
                        tbFrom.Text = "";
                        tbUntil.Text = "";
                    }
                    else
                    {
                        tbFrom.Text = booking.CarHireStart.ToShortDateString();                                 //update car hire start
                        tbUntil.Text = booking.CarHireEnd.Date.ToShortDateString();                                   //update car hire end
                    }
                }
                else
                {
                    MessageBox.Show("This is not a valid booking reference number!");                       //inform the user this is not a valid booking
                }
            }
        }

        //finds the guest given the passport number
        private void btnReadGuest_Click(object sender, RoutedEventArgs e)
        {
            if (facade.readGuest(tbPassportNumber.Text) != null) //and the guest exists
            {
                tbGuestName.Text = facade.readGuest(tbPassportNumber.Text).Name; //update the guest name
                tbAge.Text = facade.readGuest(tbPassportNumber.Text).Age.ToString(); //update the guest age
            }
            else
            {
                MessageBox.Show("This is not a valid passport number!");
            }
        }

        //deletes the customer given the customerReferenceNumber
        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(Int32.Parse(tbCustomerReferenceNumber.Text), "customer", "0"); //access deleteObject telling it we are deleting a customer
                                                                                            //and the customer reference number
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(Int32.Parse(tbBookingReferenceNumber.Text), "booking", "0");
        }

        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.deleteObject(0, "guest", tbPassportNumber.Text);
        }

        private void btnAmendCustomer_Click(object sender, RoutedEventArgs e)
        {
            facade.amendCustomer(Int32.Parse(tbCustomerReferenceNumber.Text), "customer", tbCustomerName.Text, tbCustomerAddress.Text);
        }

        private void btnAmendBooking_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (Int32.TryParse(tbBookingReferenceNumber.Text, out result))
            {
                Booking booking = facade.readBooking(result);
                DateTime arrival;
                DateTime departure;
                DateTime from;
                DateTime until;
                if (DateTime.TryParseExact(tbArrivalDate.Text, "dd/MM/yyyy", new CultureInfo("en-US"),          //if the arrival date box is filled
                                        DateTimeStyles.None, out arrival))
                {
                    if (DateTime.TryParseExact(tbDepartureDate.Text, "dd/MM/yyyy", new CultureInfo("en-US"),    //and if the departure box is filled
                                        DateTimeStyles.None, out departure))
                    {
                        if (DateTime.TryParseExact(tbFrom.Text, "dd/MM/yyyy", new CultureInfo("en-US"),         //and if the from box is filled
                                        DateTimeStyles.None, out from))
                        {
                            if (DateTime.TryParseExact(tbUntil.Text, "dd/MM/yyyy", new CultureInfo("en-US"),     //and if the until box is filled
                                          DateTimeStyles.None, out until))
                            {
                                DateTime checkedArrival = DateTime.ParseExact(tbArrivalDate.Text, "dd/mm/yyyy", null);
                                DateTime checkedDeparture = DateTime.ParseExact(tbDepartureDate.Text, "dd/MM/yyyy", null);
                                DateTime checkedUntil = DateTime.ParseExact(tbUntil.Text, "dd/MM/yyyy", null);
                                DateTime checkedFrom = DateTime.ParseExact(tbFrom.Text, "dd/MM/yyyy", null);

                                facade.amendBooking(checkedArrival, checkedDeparture, booking.BookingReferenceNumber, (bool)cbEveningMeals.IsChecked, (bool)cbBreakfast.IsChecked, (bool)cbCarHire.IsChecked,
                                    tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, checkedFrom, checkedUntil, booking.CustomerReferenceNumber, "", tbDriver.Text);
                            }
                        }
                    }
                }
            }
        }

        private void btnAmendGuest_Click(object sender, RoutedEventArgs e)
        {
            facade.amendGuest(tbGuestName.Text, Int32.Parse(tbAge.Text), tbPassportNumber.Text);
        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            int bookingReferenceNumber = 0;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingReferenceNumber);
            facade.createInvoice(bookingReferenceNumber);
        }
    }
}