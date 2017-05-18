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
     * DECORATOR PATTERN - This extra extends booking functionality by adding few new features that could be easily a class of its own.
     * It contains details about the car hire for the given booking
     *
     * Last Update: 07/12/2016
     */

    /// <summary>
    /// The 'ConcreteDecorator' class
    /// </summary>
    [Serializable]
    class CarHire_Decorator : Extras_Decorator
    {
        private Booking booking_component;

        private string driverName;
        private DateTime startDate;
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        //Constructor
        public CarHire_Decorator(Booking booking_component)
            : base(booking_component)
        {
            this.booking_component = booking_component;
        }

        //calculating the cost for totals
        public override double getCost()
        {
            return  50 * (endDate - startDate).TotalDays;
        }
    }
}
