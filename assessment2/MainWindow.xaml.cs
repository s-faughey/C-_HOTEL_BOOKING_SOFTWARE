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
    /// Author: Sean Faughey
    /// GUI logic and verification of input
    /// Date last modified: 06/12/2016
    /// </summary>
    public partial class MainWindow : Window
    {
        Facade facade = new Facade();                                                                       //this program uses the facade design pattern, create a new facade
        //constructor
        public MainWindow()
        {
            InitializeComponent();
            tbPassportNumber.MaxLength = 10;                                                                //forces the user to type no more than 10 characters for the passport
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)                                 //method to add a customer
        {
            if (tbCustomerName.Text != "" && tbCustomerAddress.Text != "") {                                //Check that there is input in all necessary fields
                facade.createCustomer(tbCustomerName.Text, tbCustomerAddress.Text);                         //access the createCustomer method in facade
            }

            else
            {
                MessageBox.Show("You must input all necessary customer fields!");                           //message the user
            }
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)                                  //method to add a booking
        {
            try
           {
                DateTime arrivalDate = DateTime.ParseExact(tbArrivalDate.Text, "dd/MM/yyyy", null);         //change arrival date from string to time
                DateTime departureDate = DateTime.ParseExact(tbDepartureDate.Text, "dd/MM/yyyy", null);     //change departure date from string to time
                bool carHire = (bool)cbCarHire.IsChecked;                                                   //checks if customer wants a car hire
                bool eveningMeals = (bool)cbEveningMeals.IsChecked;                                         //checks if customer wants evening meals
                bool breakfast = (bool)cbBreakfast.IsChecked;                                               //checks if customer wants breakfast
                DateTime expectedDate;
                if ((bool)cbCarHire.IsChecked)                                                              //if the car hire box is checked
                {
                    if (!DateTime.TryParseExact(tbFrom.Text, "dd/MM/yyyy", new CultureInfo("en-US"),        //if either form or until box is unfilled
                            DateTimeStyles.None, out expectedDate) || !DateTime.TryParseExact(tbUntil.Text, "dd/MM/yyyy", new CultureInfo("en-US"),
                                DateTimeStyles.None, out expectedDate))
                    {
                        MessageBox.Show("You must enter from and until dates for the car hire.");           //tell the user to fill them
                        return;                                                                             //exit method
                    }

                    else
                    {
                        if (tbDriver.Text != "") {                                                          //if the user hasn given a driver name
                            DateTime until = DateTime.ParseExact(tbUntil.Text, "dd/MM/yyyy", null);
                            DateTime from = DateTime.ParseExact(tbFrom.Text, "dd/MM/yyyy", null);
                            facade.createBooking(arrivalDate, departureDate,                                //access the createBooking() method in facade
                                    eveningMeals, breakfast, carHire, Int32.Parse(tbCustomerReferenceNumber.Text), tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, from, until, tbDriver.Text);
                        }
                        else                                                                                //if the user hasnt entered a driver name
                        {
                            MessageBox.Show("Please enter a driver name");                                  //output error message
                        }
                    }
                }

                if (!(bool)cbCarHire.IsChecked)                                                             //if the customer doesnt want a car
                {
                    facade.createBooking(arrivalDate, departureDate,                                        //access the createBooking() method in facade
                                eveningMeals, breakfast, carHire, Int32.Parse(tbCustomerReferenceNumber.Text), tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, DateTime.MinValue, DateTime.MinValue, tbDriver.Text);
                }
            }
            catch (FormatException exc) {                                                                   //catches exception when user has incorrectly entered information and outputs error message
                MessageBox.Show(exc.Message);
                MessageBox.Show("Please make sure that you have entered all dates in the correct format");
            }
        }


        private void btnAddGuest_Click(object sender, RoutedEventArgs e)                                    //method to add a guest
        {
            int bookingResult;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult);                               //parse the booking reference number, 0 if not valid
            if (bookingResult > 0)
            {                                                                                               //if the booking reference number is valid
                if (facade.readBooking(bookingResult) != null)                                              //and the booking exists
                {
                    int age;
                    if (Int32.TryParse(tbAge.Text, out age)) {                                              //check that the age has been input correctly
                        if (0 <= age && age <= 101) {
                            Booking booking = facade.readBooking(bookingResult);                            //retrieve the current booking
                            if (booking.GuestArray.Count < 4) {                                             //check there is space on the booking for more guests
                                facade.createGuest(tbGuestName.Text, tbPassportNumber.Text, Int32.Parse(tbAge.Text)); //access the createGuest method in facade, adding the guest to the database
                                facade.amendBooking(booking.ArrivalDate, booking.DepartureDate, booking.BookingReferenceNumber, booking.EveningMeals, booking.Breakfast, booking.CarHire, booking.EveningDietaryRequirements,
                                    booking.BreakfastDietaryRequirements, booking.CarHireStart, booking.CarHireEnd, booking.CustomerReferenceNumber, tbPassportNumber.Text, booking.Driver); //add the guest to the booking
                            }
                            else
                            {
                                MessageBox.Show("There are already 4 guests on this holiday.");             //if there is no space on the booking
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please insert an age between 0 and 101");                      //if the age is out of bounds
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please insert an age between 0 and 101");                          //if the age is incorrectly input
                    }
                }
                else
                {
                    MessageBox.Show("This is not a valid booking reference number!");                       //if the bookingReferenceNumber doesnt exist
                }
            }
            else
            {
                MessageBox.Show("Please input a booking reference number!");                                //if the bookingReferenceNumber is incorrectly input
            }

        }

        //finds the customer given the customer reference number
        private void btnReadCustomer_Click(object sender, RoutedEventArgs e)
        {
            int customerResult;                                                                             //initialize customer result
            Int32.TryParse(tbCustomerReferenceNumber.Text, out customerResult);                             //parse customer reference number if "" then = 0
            if (customerResult > 0)                                                                         //if customer reference number > 0
            {
                if (facade.readCustomer(customerResult) != null) {                                          //read the customer using the customer reference number
                    tbCustomerName.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text)).Name; //access the facade.readCustomer method and update the GUI
                    tbCustomerAddress.Text = facade.readCustomer(Int32.Parse(tbCustomerReferenceNumber.Text)).Address; //^^
                }
                else
                {
                    MessageBox.Show("This is not a valid customer reference number.");                      //if the customerReferenceNumber doesnt exist
                }
            }

            else
            {
                MessageBox.Show("This is not a valid customer reference number.");                          //if the customerReferenceNumber doesnt is out of bounds or not an int
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
                    tbArrivalDate.Text = booking.ArrivalDate.Date.ToShortDateString();                      //update arrival date
                    tbDepartureDate.Text = booking.DepartureDate.Date.ToShortDateString();                  //update departure date
                    cbEveningMeals.IsChecked = booking.EveningMeals;                                        //update evening meals
                    cbBreakfast.IsChecked = booking.Breakfast;                                              //update breakfast
                    cbCarHire.IsChecked = booking.CarHire;                                                  //update car hire
                    tbDietaryRequirements.Text = booking.EveningDietaryRequirements;                        //update Evening Meals dietary requirements
                    tbDietaryRequirements_Breakfast.Text = booking.BreakfastDietaryRequirements;            //update Breakfast dietary requirements
                    tbDriver.Text = booking.Driver;
                    tbCustomerReferenceNumber.Text = booking.CustomerReferenceNumber.ToString();

                    if (!booking.CarHire) {                                                                 //if the customer doesnt want a car clear the car related fields
                        tbFrom.Text = "";                                                                   //set the fields to blank
                        tbUntil.Text = "";
                        tbDriver.Text = "";
                    }
                    else
                    {
                        tbFrom.Text = booking.CarHireStart.ToShortDateString();                             //update car hire start
                        tbUntil.Text = booking.CarHireEnd.Date.ToShortDateString();                         //update car hire end
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
            if (facade.readGuest(tbPassportNumber.Text) != null)                                            //and the guest exists
            {
                tbGuestName.Text = facade.readGuest(tbPassportNumber.Text).Name;                            //update the guest name
                tbAge.Text = facade.readGuest(tbPassportNumber.Text).Age.ToString();                        //update the guest age
            }
            else
            {
                MessageBox.Show("This is not a valid passport number!");
            }
        }

        //deletes the customer given the customerReferenceNumber
        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            bool objectDeleted = facade.deleteObject(Int32.Parse(tbCustomerReferenceNumber.Text), "customer", "0"); //access deleteObject telling it we are deleting a customer
            if (!objectDeleted) {                                                                                   //if the customer was not deleted
                MessageBox.Show("The customer was not deleted");                                                    //message to the user
            }
        }

        //deletes a booking
        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            bool objectDeleted = facade.deleteObject(Int32.Parse(tbBookingReferenceNumber.Text), "booking", "0");   //access deleteObject telling it we are deleting a booking
            if (!objectDeleted) {                                                                                   //if the booking was not deleted
                MessageBox.Show("The booking was not deleted!");                                                    //message to the user
            }
        }

        //deletes a guest
        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            int bookingResult;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingResult);                                       //parse the booking reference number, 0 if not valid
            if (bookingResult > 0)
            {                                                                                                       //if the booking reference number is valid
                Booking booking = facade.readBooking(bookingResult);                                                //read the booking with the valid booking reference number
                if (booking != null)                                                                                //and the booking exists
                {
                    facade.deleteGuest(tbPassportNumber.Text, booking);                                             //delete the guest
                }
                else
                {
                    MessageBox.Show("The guest was not deleted");                                                   //if the guest was not deleted, message the user
                }
            }
        }

        //amends the customer
        private void btnAmendCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbCustomerAddress.Text != "" && tbCustomerName.Text != "")                                      //if all necessary fields are input, except the customerReferenceNumber
                {
                    bool amendedCustomer = facade.amendCustomer(Int32.Parse(tbCustomerReferenceNumber.Text), "customer", tbCustomerName.Text, tbCustomerAddress.Text); //access amend customer method
                    if (!amendedCustomer)                                                                           //if the customer was not amended
                    {
                        MessageBox.Show("The customer was not amended!");                                           //message to the user
                    }
                }
                else                                                                                                //if the necessary fields are blank
                {
                    MessageBox.Show("Please make sure all data was entered correctly");                             //message the user
                }
            }
            catch (FormatException exc) {                                                                           //if the customerReferenceNumber is blank
                MessageBox.Show("You must enter all necessary fields");                                             //message the user
            }
        }

        //amends the booking
        private void btnAmendBooking_Click(object sender, RoutedEventArgs e)
        {
            bool amendedBooking = false;
            int result;
            if (Int32.TryParse(tbBookingReferenceNumber.Text, out result))                                          //if the booking reference number is a number
            {
                Booking booking = facade.readBooking(result);                                                       //read the booking
                DateTime arrival;
                DateTime departure;
                DateTime from;
                DateTime until;
                if (DateTime.TryParseExact(tbArrivalDate.Text, "dd/MM/yyyy", new CultureInfo("en-US"),              //if the arrival date box is filled
                                        DateTimeStyles.None, out arrival))
                {
                    if (DateTime.TryParseExact(tbDepartureDate.Text, "dd/MM/yyyy", new CultureInfo("en-US"),        //and if the departure box is filled
                                        DateTimeStyles.None, out departure))
                    {
                        DateTime.TryParseExact(tbUntil.Text, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out until);   //check if the car hire end date is filled in correctly
                        DateTime.TryParseExact(tbFrom.Text, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out from);     //check if the car hire start date is filled in correctly
                        if ((bool)cbCarHire.IsChecked)                                                              //if the customer wants a car hire
                        {
                            if (from > DateTime.MinValue && until > DateTime.MinValue && tbDriver.Text != "")       //if the car hire start and end dates were valid
                            {
                                amendedBooking = facade.amendBooking(arrival, departure, booking.BookingReferenceNumber, (bool)cbEveningMeals.IsChecked, (bool)cbBreakfast.IsChecked, (bool)cbCarHire.IsChecked,
                                    tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, from, until, booking.CustomerReferenceNumber, "", tbDriver.Text); //amend the booking
                            }
                        }
                        else                                                                                        //if the cutsomer doesnt want a car
                        {
                            amendedBooking = facade.amendBooking(arrival, departure, booking.BookingReferenceNumber, (bool)cbEveningMeals.IsChecked, (bool)cbBreakfast.IsChecked, (bool)cbCarHire.IsChecked,
    tbDietaryRequirements.Text, tbDietaryRequirements_Breakfast.Text, DateTime.MinValue, DateTime.MinValue, booking.CustomerReferenceNumber, "", ""); //amend the booking with minimum values and blank driver
                        }
                    }
                }
            }

            if (!amendedBooking) {                                                                                  //if the booking was not amended
                MessageBox.Show("The booking was not amended!");                                                    //message the user
            }
        }

        //amends the guest
        private void btnAmendGuest_Click(object sender, RoutedEventArgs e)
        {
            try                                                                                                     //check the information was input correctly
            {
                bool amendedGuest = facade.amendGuest(tbGuestName.Text, Int32.Parse(tbAge.Text), tbPassportNumber.Text);
                if (!amendedGuest)                                                                                  //if the guest was not amended
                {
                    MessageBox.Show("The guest was not amended.");                                                  //message the user
                }
            }
            catch (FormatException exc)
            {                                                                                                       //if the information was not input correctly
                MessageBox.Show("Make sure all data was correctly entered");                                        //message the user
            }
        }
        
        //produces an invoice given the booking reference number
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            int bookingReferenceNumber = 0;
            Int32.TryParse(tbBookingReferenceNumber.Text, out bookingReferenceNumber);                              //check the bookingReferenceNumber was input correctly, if not bookingRefernceNumber = 0
            facade.createInvoice(bookingReferenceNumber);                                                           //access the createInvoice method in the facade
        }
    }
}