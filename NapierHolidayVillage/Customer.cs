using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class contains details about the customer.
     * Reference number is calculated based on the most recent number.
     * When the DB is read on startup referenceCount variable is adjusted to the content of the DB.
     * When the new number is needed it is generated to follow up. Therefore there cannot be duplicated references, nor we cannot use used and deleted references again
     *
     * Last Update: 07/12/2016
     */

    [Serializable]
    public class Customer
    {
        //instance fields and get set methods for them
        private string name;
        private string address;
        private int customerReference;
        private static int referenceCount = 0;

        public static int ReferenceCount
        {
            get { return Customer.referenceCount; }
            set { Customer.referenceCount = value; }
        }
        private List<Booking> bookings = new List<Booking>();
        internal List<Booking> Bookings
        {
            get { return bookings; }
            set { bookings = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int CustomerReference
        {
            get { return customerReference; }
            set { customerReference = value; }
        }


        //adding current booking to the list of current customer's bookings
        public void addBooking(Booking b)
        {
            bookings.Add(b);
        }

        public int calculateReference()
        {
            return ++referenceCount;
        }

        //method for checking correct name format
        public bool nameFormat (String name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
            {
                this.name = name;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
