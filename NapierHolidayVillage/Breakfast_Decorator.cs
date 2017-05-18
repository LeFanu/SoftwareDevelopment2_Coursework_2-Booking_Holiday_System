using System;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class is an extras decorator for booking.
     * It provides additional functionality and features to the booking.
     * Decorator objects can be wrapped up in any order and it has no impact on fuctionality
     *
     * Design Patterns Used:
     * DECORATOR PATTERN - This extra extends booking functionality simply by adding meal's cost to the basic cost and dietary requirements for this meal
     *
     * Last Update: 07/12/2016
     */


    /// <summary>
    /// The 'ConcreteDecorator' class
    /// </summary>
    [Serializable]
    class Breakfast_Decorator : Extras_Decorator
    {
        //private BookingComponent currentExtra;
        private Booking booking_component;
        private string dietaryRequirements;

        public string DietaryRequirements
        {
            get { return dietaryRequirements; }
            set { dietaryRequirements = value; }
        }

        ////constructor
        public Breakfast_Decorator(Booking booking_component)
            : base(booking_component)
        {
            this.booking_component = booking_component;
        }

        public override double getCost()
        {
            return  (5 * booking_component.Guests.Count * booking_component.DaysBooking);
        }
    }
}
