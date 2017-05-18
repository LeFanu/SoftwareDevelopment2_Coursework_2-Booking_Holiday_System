using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;


namespace NapierHolidayVillage
{
    /// <summary>
    /// Interaction logic for BookingWin.xaml
    /// </summary>


    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This window alows interactions with Booking class and related classess.
     * It uses canvases for new booking and editing, so there is no need for additional window.
     * It is probably most complicated window due to multiple interactions with various components.
     *
     * Design Patterns Used:
     * DECORATOR PATTERN is used for Extras added to the booking. Booking window is a GUI component for Booking class and allow all of its interactions.
     * With the Decorator pattern in use, booking is now an inherited class from the Booking Component.
     * The same goes for the Abstract Decorator and all Concrete Decorators, which are actual Extras.
     * Whenever user is adding any extra to the Booking we're using reference to the Booking Component so all the things can be wrapped up together.
     * As an addition we use decorators references to check for extras upon loading a window and deciding what the booking includes.
     *
     * Last Update: 07/12/2016
     */
    public partial class BookingWin : Window
    {

        //--------SETTING UP THE WINDOW, VARIABLES AND REFERENCES------------

        //reference to our DB
        NHV_DatabaseAccessSingleton dbAccess;
        //this is the reference to the object that will be used at any given point
        Booking bookingInUse;
        //creating reference for a customer who owns this booking
        CustomerWin customerWindow;
        MainWindow mainWindow;

        //constructor
        public BookingWin()
        {
            InitializeComponent();
            dbAccess = NHV_DatabaseAccessSingleton.Instance;
            this.canvBookingMainWindow.Visibility = Visibility.Visible;
            this.canvNewBooking.Visibility = Visibility.Hidden;
            //disabling dates in the date picker for new bookings
            this.datePickArrivalCanvas.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue,
                DateTime.Now.AddDays(-1)));
            this.datePickDepartureCanvas.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Now));
        }

        //when we open the window from results list we want to fill in the data
        public void setBookingWindow(String reference)
        {
            //searching DB for requested Booking
            bookingInUse = dbAccess.BookingsDB.Find(b => b.BookingReference.ToString() == reference);

            //filling in all the data about this booking
            txtBlkReferenceNumber.Text = bookingInUse.BookingReference.ToString();
            datePickArrival.SelectedDate = bookingInUse.Arrival.Date;
            datePickDeparture.SelectedDate = bookingInUse.Departure.Date;
            txtblkCustomerDetails.Text = "Ref. " + bookingInUse.CustomerOwner.CustomerReference.ToString() + ", Name: " +
                                         bookingInUse.CustomerOwner.Name;

            //checking if selected booking has any extras
            //if yes, then we change buttons content and decorate objects accordingly
            if (bookingInUse.Breakfast != null)
            {
                btnAddRemoveBreakfast.Content = "Remove";
                txtBreakfastDietaryRequirements.Text = bookingInUse.Breakfast.DietaryRequirements;
            }
            if (bookingInUse.EveningMeal != null)
            {
                btnAddRemoveEveningMeal.Content = "Remove";
                txtEveningDietaryRequirements.Text = bookingInUse.EveningMeal.DietaryRequirements;
            }
            if (bookingInUse.CarHire != null)
            {
                btnAddRemocveCar.Content = "Remove";
                txtDriverName.Text = bookingInUse.CarHire.DriverName;
                dateHireStart.SelectedDate = bookingInUse.CarHire.StartDate;
                dateHireEnd.SelectedDate = bookingInUse.CarHire.EndDate;
            }

            //filling in the Guests window
            int index = 0;
            foreach (Guest guest in bookingInUse.Guests)
            {
                //first we try to remove the content of the line we want to fill in if we would come back to this window after editing booking
                try
                {
                    lstGuests.Items.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                lstGuests.Items.Insert(index, guest.Name + ", " + guest.Age + ", " + guest.PassportNumber);
                index++;
            }
        }

        //setting up the window for adding new booking
        public void setBookingWindow(Customer customerOwner)
        {
            newWindow();
            bookingInUse.CustomerOwner = customerOwner;
            this.cmbCustomersList.SelectedItem = "Ref: " + customerOwner.CustomerReference + ", Name: " +
                                                 customerOwner.Name;
        }

        public void newWindow()
        {
            bookingInUse = new Booking();
            bookingInUse.BookingReference = bookingInUse.calculateReference();
            txtBlkReferenceNumberCanvas.Text = "Generated number: " + bookingInUse.BookingReference;

            //checking if there is any customer in the DB
            if (dbAccess.CustomersDB.Count > 0)
            {
                foreach (Customer customer in dbAccess.CustomersDB)
                {
                    cmbCustomersList.Items.Add("Ref: " + customer.CustomerReference + ", Name: " + customer.Name);
                }
            }
            else
            {
                MessageBox.Show("Please add at least one customer before making any bookings.", "Missing Customer",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }



        //----------EDITING BOOKING----------------------

        //actions to take when editing booking
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //before editing we check if the selected dates are correct
            if (!bookingInUse.checkDates(datePickArrival.SelectedDate.Value, datePickDeparture.SelectedDate.Value))
            {
                //if not, then changing the dates to current
                datePickArrival.SelectedDate = DateTime.Today.Date;
                datePickDeparture.SelectedDate = DateTime.Today.Date.AddDays(1);
            }
            //if they do we proceed with saving changes
            else
            {
                bookingInUse.Arrival = datePickArrival.SelectedDate.Value;
                bookingInUse.Departure = datePickDeparture.SelectedDate.Value;
                bookingInUse.DaysBooking = (bookingInUse.Departure - bookingInUse.Arrival).TotalDays;

                //trying to store content of extras if this was updated
                try
                {
                    bookingInUse.Breakfast.DietaryRequirements = txtBreakfastDietaryRequirements.Text;
                    bookingInUse.EveningMeal.DietaryRequirements = txtEveningDietaryRequirements.Text;
                    bookingInUse.CarHire.DriverName = txtDriverName.Text;
                    bookingInUse.CarHire.StartDate = dateHireStart.SelectedDate.Value;
                    bookingInUse.CarHire.EndDate = dateHireEnd.SelectedDate.Value;
                }
                catch (Exception)
                {
                }
                dbAccess.saveFiles();
                //refreshing main window's result after this operation
                mainWindow = (MainWindow) this.Owner;
                mainWindow.btnBookSearch_Click(sender, e);
                    this.Close();
                }
          }

        //actions to take when deleting booking
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bookingInUse.CustomerOwner.Bookings.Remove(bookingInUse.CustomerOwner.Bookings.Find(booking => booking.Equals(bookingInUse)));
                dbAccess.deleteEntry(bookingInUse);
                //refreshing main window's result after this operation
                mainWindow = (MainWindow)this.Owner;
                mainWindow.btnBookSearch_Click(sender, e);
                this.Close();
            }
        }

        //actions for adding/removing/updating Guests
        private void lstGuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //using altered constructor to pass a number of the guest we're using
            int guestNumber = lstGuests.Items.IndexOf(lstGuests.SelectedItem);
            GuestWin guestWindow = new GuestWin(Convert.ToInt32(guestNumber), bookingInUse);
            guestWindow.Owner = this;

            //if we're editing Guest we use this method to fill in the Guests data
            if (!lstGuests.SelectedItem.ToString().Contains("add new..."))
            {
                guestWindow.setWindow();
            }

            guestWindow.ShowDialog();
        }

        //actions for adding/removing breakfast
        private void btnAddRemoveBreakfast_Click(object sender, RoutedEventArgs e)
        {
            if (bookingInUse.Breakfast == null)
            {
                btnAddRemoveBreakfast.Content = "Remove";
                bookingInUse.Breakfast = new Breakfast_Decorator(bookingInUse);
                bookingInUse.Breakfast.DietaryRequirements = txtBreakfastDietaryRequirements.Text;
            }
            else
            {
                btnAddRemoveBreakfast.Content = "Add";
                bookingInUse.Breakfast = null;
                try
                {
                    txtBreakfastDietaryRequirements.Text.Remove(0);
                }
                catch (Exception)
                {
                }
            }
        }

        //actions for adding/removing Evening meal
        private void btnAddRemoveEveningMeal_Click(object sender, RoutedEventArgs e)
        {
            if (bookingInUse.EveningMeal == null)
            {
                btnAddRemoveEveningMeal.Content = "Remove";
                bookingInUse.EveningMeal = new EveningMeal_Decorator(bookingInUse);
                bookingInUse.EveningMeal.DietaryRequirements = txtEveningDietaryRequirements.Text;
            }
            else
            {
                btnAddRemoveEveningMeal.Content = "Add";
                bookingInUse.EveningMeal = null;
                try
                {
                    txtEveningDietaryRequirements.Text.Remove(0);
                }
                catch (Exception)
                {
                }
            }
        }

        //actions for adding/removing Car
        private void btnAddRemocveCar_Click(object sender, RoutedEventArgs e)
        {
            if (bookingInUse.CarHire == null)
            {
                try
                {
                    btnAddRemocveCar.Content = "Remove";
                    bookingInUse.CarHire = new CarHire_Decorator(bookingInUse);
                    bookingInUse.CarHire.DriverName = txtDriverName.Text;
                    bookingInUse.CarHire.StartDate = dateHireStart.SelectedDate.Value;
                    bookingInUse.CarHire.EndDate = dateHireEnd.SelectedDate.Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select dates and fill in all the Car Hire fields correctly before saving.", "Missing Fields.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    btnAddRemocveCar.Content = "Add";
                }
            }
            else
            {
                btnAddRemocveCar.Content = "Add";
                bookingInUse.CarHire = null;
                try
                {
                    txtDriverName.Text.Remove(0);
                    dateHireStart.DisplayDate.ToString().Remove(0);
                    dateHireEnd.DisplayDate.ToString().Remove(0);
                }
                catch (Exception)
                {
                }
            }
        }

        //actions for opening Invoice form with the costs for the Booking
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceWindow invoiceWindow = new InvoiceWindow(bookingInUse);
            invoiceWindow.ShowDialog();
        }




        //---------NEW BOOKING--------------------


        //actions taken on cancelling new booking
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.canvBookingMainWindow.Visibility = Visibility.Visible;
            this.canvNewBooking.Visibility = Visibility.Hidden;
            //reducing static variable as the booking is cancelled
            Booking.ReferenceCount--;
            this.Close();
        }

        //adding new booking will change the canvas to show proper data
        private void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            this.canvBookingMainWindow.Visibility = Visibility.Hidden;
            this.canvNewBooking.Visibility = Visibility.Visible;

            //Until the save button is clicked this is a dummy booking that will be lost if cancel is pressed
            newWindow();
        }

        //actions for adding/editing/removing Guest for the new booking
        private void lstGuestsCanvas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //using altered constructor to pass a number of the guest we're using
            int guestNumber = lstGuestsCanvas.Items.IndexOf(lstGuestsCanvas.SelectedItem);
            GuestWin guestWindow = new GuestWin(guestNumber, bookingInUse);
            guestWindow.Owner = this;
            //if we're editing Guest we use this method to fill in the Guests data
            if (!lstGuestsCanvas.SelectedItem.ToString().Contains("add new..."))
            {
                guestWindow.setWindow();
            }

            guestWindow.ShowDialog();
        }

        private void btnSelectCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bookingInUse.CustomerOwner = dbAccess.CustomersDB.Find(x => x.CustomerReference == Convert.ToInt32(Regex.Match(cmbCustomersList.SelectedItem.ToString(), @"\d+").Value));
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a customer from the list.", "Customer not selected", MessageBoxButton.OK,
                    MessageBoxImage.Hand);
            }
        }

        //actions to take when adding new booking
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bookingInUse.Guests.Count>0 && (bookingInUse.CustomerOwner != null))
                {
                    //checking dates if they match proper criteria
                    if (!bookingInUse.checkDates(datePickArrivalCanvas.SelectedDate.Value, datePickDepartureCanvas.SelectedDate.Value))
                    {
                        //if not, then changing the dates to current
                        datePickArrivalCanvas.SelectedDate = DateTime.Today.Date;
                        datePickDepartureCanvas.SelectedDate = DateTime.Today.Date.AddDays(1);
                    }
                    //if they do we proceed with storing current booking
                    else
                    {
                        bookingInUse.Arrival = datePickArrivalCanvas.SelectedDate.Value;
                        bookingInUse.Departure = datePickDepartureCanvas.SelectedDate.Value;

                        //trying to store extras details if they were entered after adding extra
                        try
                        {
                            bookingInUse.Breakfast.DietaryRequirements = txtBreakfastDietaryRequirements_Canvas.Text;
                            bookingInUse.EveningMeal.DietaryRequirements = txtEveningDietaryRequirements_Canvas.Text;
                        }
                        catch (Exception)
                        {
                        }

                        //looking for desired customer to add this booking to it's list
                        bookingInUse.CustomerOwner.Bookings.Add(bookingInUse);
                        dbAccess.saveFiles();

                        //if this booking was added directly from customer's window we want to update his list of bookings
                        try
                        {
                            customerWindow = (CustomerWin)this.Owner;
                            customerWindow.lstFutureBookings.Items.Clear();
                            customerWindow.readBookings();
                        }
                        catch (Exception)
                        {
                        }
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("There has to be at least one guest and Customer must be selected.", "Missing Guest and/or Customer.", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception)
            {
                 MessageBox.Show("Please select dates and fill in all the fields correctly before saving.", "Missing Fields.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        private void btnAddRemoveBreakfast_Canvas_Click(object sender, RoutedEventArgs e)
        {
            if (bookingInUse.Breakfast == null)
            {
                btnAddRemoveBreakfast_Canvas.Content = "Remove";
                bookingInUse.Breakfast = new Breakfast_Decorator(bookingInUse);
                bookingInUse.Breakfast.DietaryRequirements = txtBreakfastDietaryRequirements_Canvas.Text;
            }
            else
            {
                btnAddRemoveBreakfast_Canvas.Content = "Add";
                bookingInUse.Breakfast = null;
                try
                {
                    txtBreakfastDietaryRequirements_Canvas.Text.Remove(0);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnAddRemoveEveningMeal_Canvas_Click(object sender, RoutedEventArgs e)
        {
            if (bookingInUse.EveningMeal == null)
            {
                btnAddRemoveEveningMeal_Canvas.Content = "Remove";
                bookingInUse.EveningMeal = new EveningMeal_Decorator(bookingInUse);
                bookingInUse.EveningMeal.DietaryRequirements = txtEveningDietaryRequirements_Canvas.Text;
            }
            else
            {
                btnAddRemoveEveningMeal_Canvas.Content = "Add";
                bookingInUse.EveningMeal = null;
                try
                {
                    txtEveningDietaryRequirements_Canvas.Text.Remove(0);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnAddRemocveCar_Canvas_Click(object sender, RoutedEventArgs e)
        {
            if (bookingInUse.CarHire == null)
            {
                try
                {
                    btnAddRemocveCar_Canvas.Content = "Remove";
                    bookingInUse.CarHire = new CarHire_Decorator(bookingInUse);
                    bookingInUse.CarHire.DriverName = txtDriverName_Canvas.Text;
                    bookingInUse.CarHire.StartDate = dateHireStart_Canvas.SelectedDate.Value;
                    bookingInUse.CarHire.EndDate = dateHireEnd_Canvas.SelectedDate.Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select dates and fill in all the Car Hire fields correctly before saving.", "Missing Fields.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    btnAddRemocveCar_Canvas.Content = "Add";
                }
            }
            else
            {
                btnAddRemocveCar_Canvas.Content = "Add";
                bookingInUse.CarHire = null;
                try
                {
                    txtDriverName_Canvas.Text.Remove(0);
                    dateHireStart_Canvas.DisplayDate.ToString().Remove(0);
                    dateHireEnd_Canvas.DisplayDate.ToString().Remove(0);
                }
                catch (Exception)
                {
                }
            }
        }





    }
}
