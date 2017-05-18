using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierHolidayVillage;
using System.IO;
namespace NapierHolidayVillage.Tests
{
    [TestClass()]
    public class BookingClassTest
    {

        [TestMethod()]
        public void BookingCheckDates_positiveTest()
        {
            Booking booking = new Booking();
            DateTime arrival = new DateTime();
            DateTime departure = new DateTime();
            bool expectedResult = true;

            arrival = DateTime.Today.Date;
            departure = DateTime.Today.AddDays(10);

            //with the given dates the result should always be true
            bool actualResult = booking.checkDates(arrival, departure);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void BookingCheckDates_negativeTest()
        {
            Booking booking = new Booking();
            DateTime arrival = new DateTime();
            DateTime departure = new DateTime();
            bool expectedResult = false;

            arrival = DateTime.Today.AddDays(-10);
            departure = DateTime.Today.Date;

            //with the given dates the result should always be false
            bool actualResult = booking.checkDates(arrival, departure);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

namespace BookingTest
{
    [TestClass]
    public class BookingClassTest
    {
        [TestMethod]
        public void AddEditGuest_GuestOutOfRange()
        {
            //arrange
            Guest guestTest = new Guest();
            Booking booking = new Booking();
            for (int i = 0; i < 3; i++)
            {
                booking.Guests.Add(guestTest);
            }
            int guestPosition = 0;

            //after succesfull operation when the exception is not thrown we should have the same number of guests
            int guestCountExpected = 3;
            //act
 
                booking.addEditGuest(guestTest, guestPosition);

            //after checking our method we want to see if the number of guests have changed
            //if it changed it means the exceptions was thrown and new Guest was added to the list
            int guestCountActual = booking.Guests.Count;
            //assert
            Assert.AreEqual(guestCountExpected, guestCountActual);
        }

    }
}
