using System;
using System.Windows;

namespace NapierHolidayVillage
{
    /// <summary>
    /// Interaction logic for CustomerWin.xaml
    /// </summary>

    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This window allows interactions with Customer class through various GUI components.
     * It can be called out only from the main window regardless if it will be an edit or new customer action.
     * This window uses canvases for editing and creating new customers in order to avoid having additional windows.
     *
     *
     * Last Update: 07/12/2016
     */




    public partial class CustomerWin : Window
    {
        //reference to our database
        NHV_DatabaseAccessSingleton dbAccess;
        MainWindow mainWindow;


        public CustomerWin()
        {
            InitializeComponent();
            this.Title = "Customer Details";
            dbAccess = NHV_DatabaseAccessSingleton.Instance;
        }

        //this object reference is used for all operations on a current or new customer
        Customer customerInUse;

        //this method queries our customer's list for the given reference and provides all the details for requested customer
        public void setCustomerWindow (String reference)
        {
            //looking for a customer in a list and adding this object to our reference
            customerInUse = dbAccess.CustomersDB.Find(c => c.CustomerReference.ToString() == reference);

            //customer reference is in textBlock so it would not be possible to edit it
            txtBlkReferenceNumber.Text = customerInUse.CustomerReference.ToString();
            txtCustName.Text = customerInUse.Name;
            txtAddress.Text = customerInUse.Address;

            //searching through bookings list to obtain this customer's bookings
           readBookings();
        }

        public void readBookings()
        {
            foreach (Booking booking in customerInUse.Bookings)
            {
                if (booking.Arrival.Date < DateTime.Today.Date)
                {
                    lstPastBookings.Items.Add("Booking Ref: " + booking.BookingReference + " - Dates: " + booking.Arrival.Date.ToString("dd/MM/yyyy") + "," + booking.Departure.Date.ToString("dd/MM/yyyy"));
                }
                else
                {
                    //Future bookings are the upcoming ones
                    lstFutureBookings.Items.Add("Booking Ref: " + booking.BookingReference + " - Dates: " + booking.Arrival.Date.ToString("dd/MM/yyyy") + "," + booking.Departure.Date.ToString("dd/MM/yyyy"));
                }
            }
        }

        //-------------EDITING CUSTOMER---------------------------
        //upon update we change the content of an object and send it to file write to update the list and save the file
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //checking correct format of teh name and only proceeding if it's right
            bool correctName = customerInUse.nameFormat(txtCustName.Text);
            if (correctName)
            {
                customerInUse.Address = txtAddress.Text;
                dbAccess.saveFiles();

                //refreshing customer search results to contain valid data
                mainWindow = (MainWindow)this.Owner;
                mainWindow.btnCustSearch_Click(sender, e);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter proper name format.", "Wrong Characters", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }

        //when we want to delete our customer file writer will do that for us
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //preventing delete customer if there are any bookings in future
                if (lstFutureBookings.Items.Count > 0)
                {
                    MessageBox.Show("Customer has planned booking. Unable to delete.", "Planned Bookings", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    dbAccess.deleteEntry(customerInUse);
                    //refreshing customer search results to contain valid data
                    mainWindow = (MainWindow)this.Owner;
                    mainWindow.btnCustSearch_Click(sender, e);
                    this.Close();
                }
            }

        }



        //------------ADDING NEW CUSTOMER-------------------------
        //when adding new customer our window is changing with the use of the canvas for new functionality
        private void btnNewCust_Click(object sender, RoutedEventArgs e)
        {
            canvNewCustomer.Visibility = Visibility.Visible;
            canvCustomerWindowMain.Visibility = Visibility.Hidden;
        }

        //when creating new customer is cancelled original window look is resotred
        private void btnCancelCustomer_Click(object sender, RoutedEventArgs e)
        {
            canvNewCustomer.Visibility = Visibility.Hidden;
            canvCustomerWindowMain.Visibility = Visibility.Visible;
            this.Close();
        }

        //actions while saving new customer
        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            customerInUse = new Customer();
            //this line will generate new reference number
            customerInUse.CustomerReference = customerInUse.calculateReference();

            //checking if the name's format is correct
            bool correctName = customerInUse.nameFormat(txtCustName_Canvas.Text);
            if (correctName)
            {
                customerInUse.Address = txtAddress_Canvas.Text;
                dbAccess.CustomersDB.Add(customerInUse);
                dbAccess.saveFiles();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter proper name format.", "Wrong Characters", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Customer.ReferenceCount--;
            }
        }



        //actions for adding new booking directly from customer's window
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingWin bookingWindow = new BookingWin();

            bookingWindow.canvBookingMainWindow.Visibility = Visibility.Hidden;
            bookingWindow.canvNewBooking.Visibility = Visibility.Visible;
            bookingWindow.setBookingWindow(customerInUse);
            bookingWindow.Owner = this;
            bookingWindow.ShowDialog();
        }
    }
}
