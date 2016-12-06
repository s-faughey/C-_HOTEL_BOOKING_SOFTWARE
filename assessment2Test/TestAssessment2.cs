using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using assessment2;
using System.IO;

//Author name: Sean Faughey
//Description: This is a test class which tests all of the methods within the solution allowing you to add, change or delete a customer, booking or guest
//Last modified: 06/12/2016
namespace assessment2Test
{
    [TestClass]
    public class TestAssessment2
    {

        //this method test the functionality of the createCustomer method in the facade by adding a customer,
        //reading in the customer using the customerReferenceNumber and comparing the properties
        [TestMethod]
        public void TestAddCustomer()
        {
            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure the file is empty
            Facade facade = new Facade();                                                                           //create a new facade
            Customer customer = new Customer("john", "address", 1);                                                 //expected customer

            facade.createCustomer("john", "address");                                                               //create a customer
            Assert.AreEqual(customer.Name, facade.readCustomer(1).Name);                                            //check that the properties of the expected customer and the written customer are the same
            Assert.AreEqual(customer.Address, facade.readCustomer(1).Address);
            Assert.AreEqual(customer.CustomerReferenceNumber, facade.readCustomer(1).CustomerReferenceNumber);
        }

        //this method tests the functionality of the createBooking method in the facade by adding a booking,
        //reading in the booking using the bookingReferenceNumber and comparing the properties
        [TestMethod]
        public void TestAddBooking()
        {
            DateTime arrivalDate = DateTime.Parse("01/01/2001");
            DateTime departureDate = DateTime.Parse("02/02/2001");
            DateTime carHireStart = DateTime.Parse("01/01/2001");
            DateTime carHireEnd = DateTime.Parse("02/02/2001");

            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //check the the file is empty when we start
            Facade facade = new Facade();                                                                           //create a new facade
            Customer customer = new Customer("john", "address", 1);                                                 //test customer
            Booking booking = new Booking(arrivalDate, departureDate, 1, true, true, true, 1, "", "", carHireStart, carHireEnd, "john");    //expected booking
            facade.createBooking(arrivalDate, departureDate, true, true, true, 1, "", "", carHireStart, carHireEnd, "john");    //create the booking

            Assert.AreEqual(booking.ArrivalDate, facade.readBooking(1).ArrivalDate);                                //checks that all of the information is correct
            Assert.AreEqual(booking.DepartureDate, facade.readBooking(1).DepartureDate);
            Assert.AreEqual(booking.EveningMeals, facade.readBooking(1).EveningMeals);
            Assert.AreEqual(booking.Breakfast, facade.readBooking(1).Breakfast);
            Assert.AreEqual(booking.CarHire, facade.readBooking(1).CarHire);
            Assert.AreEqual(booking.CustomerReferenceNumber, facade.readBooking(1).CustomerReferenceNumber);
            Assert.AreEqual(booking.EveningDietaryRequirements, facade.readBooking(1).EveningDietaryRequirements);
            Assert.AreEqual(booking.BreakfastDietaryRequirements, facade.readBooking(1).BreakfastDietaryRequirements);
            Assert.AreEqual(booking.CarHireStart, facade.readBooking(1).CarHireStart);
            Assert.AreEqual(booking.CarHireEnd, facade.readBooking(1).CarHireEnd);
            Assert.AreEqual(booking.Driver, facade.readBooking(1).Driver);
        }

        //this method tests the functionality of the createGuest method in the facade by adding a guest,
        //reading in the guest using the passportNumber and comparing the properties
        [TestMethod]
        public void TestAddGuest()
        {
            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure the file is empty when we start
            Facade facade = new Facade();                                                                           //create a new facade
            Guest guest = new Guest("john", "123abc", 21);                                                          //expected guest                              
            facade.createGuest("john", "123abc", 21);                                                               //create the guest

            Assert.AreEqual(guest.Name, facade.readGuest("123abc").Name);                                           //checks that all the information is correct
            Assert.AreEqual(guest.Age, facade.readGuest("123abc").Age);
            Assert.AreEqual(guest.PassportNumber, facade.readGuest("123abc").PassportNumber);
        }

        //this method tests the deleteCustomer method in the facade by creating a customer, checking that the customer is not null using the readCustomer method
        //then calling the deleteObject method with the customerReferenceNumber and customer object identifier
        //and checking whether the readCustomer method then returns null
        [TestMethod]
        public void TestDeleteCustomer()
        {
            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //check to see that the file is empty
            Facade facade = new Facade();                                                                           //create a new facade

            facade.createCustomer("john", "address");                                                               //create the customer
            Assert.IsNotNull(facade.readCustomer(1));                                                               //check the customer was created

            facade.deleteObject(1, "customer", "");                                                                 //delete the customer
            Assert.IsNull(facade.readCustomer(1));                                                                  //ccheck the customer was deleted
        }

        //this method tests the deleteBooking method in the facade by creating a booking, checking that the booking is not null using the readBooking method
        //then calling the deleteObject method with the bookingReferenceNumber and booking object identifier,
        //and checking whether the readBooking method then returns null
        [TestMethod]
        public void TestDeleteBooking()
        {
            DateTime arrivalDate = DateTime.Parse("01/01/2001");
            DateTime departureDate = DateTime.Parse("02/02/2001");
            DateTime carHireStart = DateTime.Parse("01/01/2001");
            DateTime carHireEnd = DateTime.Parse("02/02/2001");

            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure we are working with an empty file
            Facade facade = new Facade();                                                                           //create a facade

            facade.createBooking(arrivalDate, departureDate, true, true, true, 1, "", "", carHireStart, carHireEnd, "john");    //create a booking
            Assert.IsNotNull(facade.readBooking(1));                                                                //check the booking exists

            facade.deleteObject(1, "booking", "");                                                                  //delete the booking

            Assert.IsNull(facade.readBooking(1));                                                                   //check the booking was deleted
        }

        //this method tests the deleteGuest method in the facade by creating a guest, checking that the guest is not null using the readGuest method
        //and then calling the deleteGuest method with the passportNumber and object identifier,
        //then the amendBooking method is called to remove the guest from the booking
        //checks then occur to see whether that guest exists using the readGuest method and checking the number of guests the booking holds (should be 0)
        [TestMethod]
        public void TestDeleteGuest()
        {
            DateTime arrivalDate = DateTime.Parse("01/01/2001");
            DateTime departureDate = DateTime.Parse("02/02/2001");
            DateTime carHireStart = DateTime.Parse("01/01/2001");
            DateTime carHireEnd = DateTime.Parse("02/02/2001");

            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure we are working with an empty file
            Facade facade = new Facade();                                                                           //create a new facade

            facade.createBooking(arrivalDate, departureDate, true, true, true, 1, "", "", carHireStart, carHireEnd, "john");    //create a booking
            Booking booking = facade.readBooking(1);                                                                //read in the booking

            facade.createGuest("john", "123abc", 21);                                                               //create the guest
            facade.amendBooking(arrivalDate, departureDate, 1, true, true, true, "", "", carHireStart, carHireEnd, 1, "123abc", "john");    //amend the booking with the guest
            facade.deleteGuest("123abc", booking);                                                                  //delete the guest

            Assert.IsNull(facade.readGuest("123abc"));                                                              //check the guest does not exist on file
            Assert.IsTrue(facade.readBooking(1).GuestArray.Count == 0);                                             //check the reference to the guest in the booking has been deleted
        }

        //this method tests the amendCustomer method by creating a customer, and then amending the customer using the amendCustomer method
        //the properties are then checked to not be equal to what they were prior to the amendCustomer method being called
        [TestMethod]
        public void TestAmendCustomer()
        {
            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure we are working with an empty file
            Facade facade = new Facade();                                                                           //create a new facade

            facade.createCustomer("john", "address");                                                               //create a new customer
            facade.amendCustomer(1, "customer", "notjohn", "notaddress");                                           //amend the customer

            Assert.AreNotEqual("john", facade.readCustomer(1).Name);                                                //check the customer's information has changed from the original information
            Assert.AreNotEqual("address", facade.readCustomer(1).Address);                                          //^^
        }

        //this method tests the amendBooking method by creating a booking, and then amending the booking using the amendBooking method
        //the properties are then checked to not be equal to what they were prior to the amendBooking method being called
        [TestMethod]
        public void TestAmendBooking()
        {
            DateTime arrivalDate = DateTime.Parse("01/01/2001");
            DateTime departureDate = DateTime.Parse("02/02/2001");
            DateTime carHireStart = DateTime.Parse("01/01/2001");
            DateTime carHireEnd = DateTime.Parse("02/02/2001");

            DateTime arrivalDateAmended = DateTime.Parse("02/01/2001");
            DateTime departureDateAmended = DateTime.Parse("03/01/2001");
            DateTime carHireStartAmended = DateTime.MinValue;
            DateTime carHireEndAmended = DateTime.MinValue;

            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure we are working with a new file
            Facade facade = new Facade();                                                                           //create a new facade 
            Customer customer = new Customer("john", "address", 1);                                                 //create a !expected customer
            Booking booking = new Booking(arrivalDate, departureDate, 1, true, true, true, 1, "", "", carHireStart, carHireEnd, "john");    //create a !expected booking

            facade.createBooking(arrivalDate, departureDate, true, true, true, 1, "", "", carHireStart, carHireEnd, "john");    //create a booking
            facade.amendBooking(arrivalDateAmended, departureDateAmended, 1, false, false, false, "dietary requirement", "dietary requirement", carHireStartAmended, carHireEndAmended, 1, "", "");     //amend the booking

            Assert.AreNotEqual(booking.ArrivalDate, facade.readBooking(1).ArrivalDate);                             //checks to see that the !expected booking and the amended booking are not equal
            Assert.AreNotEqual(booking.DepartureDate, facade.readBooking(1).DepartureDate);
            Assert.AreNotEqual(booking.EveningMeals, facade.readBooking(1).EveningMeals);
            Assert.AreNotEqual(booking.Breakfast, facade.readBooking(1).Breakfast);
            Assert.AreNotEqual(booking.CarHire, facade.readBooking(1).CarHire);
            Assert.AreEqual(booking.CustomerReferenceNumber, facade.readBooking(1).CustomerReferenceNumber);
            Assert.AreNotEqual(booking.EveningDietaryRequirements, facade.readBooking(1).EveningDietaryRequirements);
            Assert.AreNotEqual(booking.BreakfastDietaryRequirements, facade.readBooking(1).BreakfastDietaryRequirements);
            Assert.AreNotEqual(booking.CarHireStart, facade.readBooking(1).CarHireStart);
            Assert.AreNotEqual(booking.CarHireEnd, facade.readBooking(1).CarHireEnd);
            Assert.AreNotEqual(booking.Driver, facade.readBooking(1).Driver);
        }


        //this method tests the amendGuest method by creating a guest, and then amending the guest using the amendGuest method
        //the proiperties are then checked to not be equal to what they were prior to the amendGuest method being called
        [TestMethod]
        public void TestAmendGuest()
        {
            File.WriteAllText("testBinaryFile.txt", String.Empty);                                                  //make sure we are working with a new file
            Facade facade = new Facade();                                                                           //create a new facade                        
            Guest guest = new Guest("john", "123abc", 21);                                                          //create a !expected guest
            facade.createGuest("john", "123abc", 21);                                                               //create a guest to be amended
            facade.amendGuest("notjohn", 22, "123abc");                                                             //amend the guest

            Assert.AreNotEqual(guest.Name, facade.readGuest("123abc").Name);                                        //check the the !expected guest and the amended guest are not equal
            Assert.AreNotEqual(guest.Age, facade.readGuest("123abc").Age);
        }
    }
}
