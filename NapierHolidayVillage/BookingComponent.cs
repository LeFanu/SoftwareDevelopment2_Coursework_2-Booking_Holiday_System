using System;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class is an abstract class for all classes related to bookings.
     * Its sole purpose is to provide the base and inheritance for the Decorator pattern
     *
     * Design Patterns Used:
     * DECORATOR PATTERN - Booking component is a parent class for booking and extras, which are decorators
     *
     * Last Update: 07/12/2016
     */


    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    [Serializable]
    public abstract class BookingComponent
    {
        public abstract double getCost();
    }
}
