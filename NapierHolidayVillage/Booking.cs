using System;
using System.Collections.Generic;
using System.Windows;


namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class contains all the details about bookings.
     *
     * Design Patterns Used: DECORATOR PATTERN
     * Booking class is a part of the Decorator patter. It is a Booking Component that is decorated with Extras, which are concrete decorators.
     * It contains references to the Extras and it's functionality can be extended.
     *
     * Last Update: 07/12/2016
     */

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    [Serializable]
    public class Booking : BookingComponent
    {
        //instance fields and get set methods for them
        private int bookingReference;
        private Customer customerOwner;

        //reference count is required to allow us control on generating reference numbers based on what was created already
        private static int referenceCount = 0;

        private DateTime departure = new DateTime();
        private DateTime arrival = new DateTime();
        private List<Guest> guests = new List<Guest>();

        //this variable is needed for the cost calculations
        private double daysBooking;

        //reference to our DB reader
        NHV_DatabaseAccessSingleton dbAccess = NHV_DatabaseAccessSingleton.Instance;

        //references to our decorators
        private CarHire_Decorator carHire;
        private EveningMeal_Decorator eveningMeal;
        private Breakfast_Decorator breakfast;

        //setters and getters for all fields
        internal CarHire_Decorator CarHire
        {
            get { return carHire; }
            set { carHire = value; }
        }
        internal EveningMeal_Decorator EveningMeal
        {
            get { return eveningMeal; }
            set { eveningMeal = value; }
        }
        internal Breakfast_Decorator Breakfast
        {
            get { return breakfast; }
            set { breakfast = value; }
        }
        public double DaysBooking
        {
            get { return daysBooking; }
            set { daysBooking = value; }
        }
        public DateTime Arrival
        {
            get { return arrival; }
            set
            { arrival = value; }
        }
        public DateTime Departure
        {
            get { return departure; }
            set {departure = value; }
        }
        public int BookingReference
        {
            get { return bookingReference; }
            set { bookingReference = value; }
        }
        public static int ReferenceCount
        {
            get { return Booking.referenceCount; }
            set { Booking.referenceCount = value; }
        }
        public List<Guest> Guests
        {
            get { return guests; }
            set { guests = value; }
        }
        internal Customer CustomerOwner
        {
            get { return customerOwner; }
            set { customerOwner = value; }
        }


        //calculationg the cost for basic stay or with discount
        public override double getCost()
        {
            //calculating cost for adults and kids
            int adult = 0, kid = 0;
            foreach (Guest g in guests)
            {
                if (g.Age < 18)
                {
                    kid++;
                }
                else
                {
                    adult++;
                }
            }

            double cost = (adult * 50 + kid * 30) * DaysBooking;
            return cost;
        }

        public int calculateReference()
        {
            return ++referenceCount; ;
        }

        //checking if provided dates match proper criteria and informing user if they now
        public bool checkDates(DateTime arrival, DateTime departure)
        {
            if ((departure - arrival).TotalDays <= 0)
            {
                MessageBox.Show("Departure date has to be at least one day later than arrival. Please correct", "Wrong Date!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (arrival.Date < DateTime.Today)
            {
                MessageBox.Show("Arrival date cannot be earlier than today. Please correct", "Wrong Date!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else
            {
                DaysBooking = (departure - arrival).TotalDays;
                return true;
            }
        }

        //actions for adding or editing Guest, depending on how method was called
        public void addEditGuest(Guest guest, int guestNumber)
        {
            //if list is empty we simply add Guest to it
            if (Guests.Count == 0)
            {
                Guests.Add(guest);
            }
            else
            {
                //otherwise we replace objects within the lists
                try
                {
                    Guests.RemoveAt(guestNumber);
                    Guests.Insert(guestNumber, guest);
                }
                    //if we cannot find our object in the list we just add it
                catch (ArgumentOutOfRangeException)
                {
                    Guests.Add(guest);
                }
            }
        }
    }
}
