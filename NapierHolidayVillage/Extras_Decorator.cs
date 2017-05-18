using System;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class is an abstract class for all classes related to extras.
     * Its sole purpose is to provide the base and inheritance for extras within the Decorator pattern
     *
     * Design Patterns Used:
     * DECORATOR PATTERN - Extras Decorator is an abstract class for extras, which are decorators.
     * It has one constructors which takes booking as a parameter.
     *
     * Last Update: 07/12/2016
     */



    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    [Serializable]
    abstract class Extras_Decorator : BookingComponent
    {
        //protected BookingComponent booking_component;
        protected Booking booking_component;

        //constructor
        public Extras_Decorator(Booking booking_component)
        {
            this.booking_component = booking_component;
        }
    }
}
