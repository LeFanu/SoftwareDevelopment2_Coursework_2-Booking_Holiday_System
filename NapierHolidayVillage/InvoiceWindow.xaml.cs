using System;
using System.Windows;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class contains details about the Invoice. Its constructor uses boooking to have functionality for calculating costs.
     * All calculations and operations are processed in the constructor as this window simply provides information.
     * There is no additional functionality
     *
     * Last Update: 07/12/2016
     */


    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {

        public InvoiceWindow(Booking bookingInUse)
        {
            InitializeComponent();

            //calculating number of guests with and without discount
            int adult = 0, kid = 0;
            foreach (Guest g in bookingInUse.Guests)
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

            //printing cost information
            lblGuestStandard.Content = adult.ToString();
            lblGuestDiscount.Content = kid.ToString();
            lblStandard.Content = 50*adult*bookingInUse.DaysBooking;
            lblDiscount.Content = 30*kid*bookingInUse.DaysBooking;

            double eveningMeal = 0;
            try
            {
                eveningMeal = bookingInUse.EveningMeal.getCost();
            }
            catch (Exception)
            {
            }


            double breakfastMeal = 0;
            try
            {
                breakfastMeal = bookingInUse.Breakfast.getCost();
            }
            catch (Exception)
            {

            }
            lblMealsCost.Content = breakfastMeal + eveningMeal ;


            double carHire = 0;
            try
            {
                carHire = bookingInUse.CarHire.getCost();
            }
            catch (Exception)
            {
            }
            lblCarCost.Content = carHire;


            try
            {
                daysCar.Content = (bookingInUse.CarHire.EndDate - bookingInUse.CarHire.StartDate).TotalDays;
            }
            catch (Exception)
            {
                daysCar.Content = "0";
            }


            lbltotalCost.Content = bookingInUse.getCost() + breakfastMeal + eveningMeal + carHire;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
