using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * This is the starting point of our program. It contains GUI for interactions with the system.
     * Main window allows few simple starting operations. Most operations happen in another classess and windows.
     *
     * Last Update: 07/12/2016
     */



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //creating reference for our Singleton file reader
        NHV_DatabaseAccessSingleton dbAccess;
        public MainWindow()
        {
            InitializeComponent();
            //creating file reader object and using it's method to read the Database and for file operations
            dbAccess = NHV_DatabaseAccessSingleton.Instance;
            //reading our database and filling in the data to operate on
            dbAccess.readDB_OnStartup();
        }


        //-------------ACTIONS FOR CUSTOMER-------------------------
        //actions for adding new customer
        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWin customerWindow = new CustomerWin();

            customerWindow.canvCustomerWindowMain.Visibility = Visibility.Hidden;
            customerWindow.canvNewCustomer.Visibility = Visibility.Visible;
            customerWindow.Owner = this;
            customerWindow.ShowDialog();
        }

        //actions for editing existing customer
        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openCustomer();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Customer from the list in order to edit it", "Make selection first.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //this method will search DB to look for matching customers and will put that into the listbox
        public void btnCustSearch_Click(object sender, RoutedEventArgs e)
        {
            //before every search we clear the list and previous results
            lstCustomers.Items.Clear();
            List<Customer> results = null;

            // search is based on the searchbox data
            results = dbAccess.customerSearch(txtCustSearch.Text);
            foreach (Customer c in results)
            {
                lstCustomers.Items.Add("Ref: " + c.CustomerReference + ", Name: " + c.Name + ", Address: " + c.Address);
            }
        }

        //this method will open customer's detail after double click on it
        private void lstCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                openCustomer();
            }
            catch (Exception)
            {
                //If customer is on the list but missing in DB we want to search again and inform user
                MessageBox.Show("This customer doesn't exist anymore.", "Missing Customer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.btnCustSearch_Click(sender, e);

            }
        }

        private void openCustomer()
        {
            //this line will look for reference number part in the whole line on the list to put the correct details in customer's window
            string reference = Regex.Match(lstCustomers.SelectedItem.ToString(), @"\d+").Value;

            //creating new customer window and passing the reference to get all the correct details
            CustomerWin customerWin = new CustomerWin();

            //this method uses reference to fill in the new window with correct data
            customerWin.setCustomerWindow(reference);
            customerWin.canvNewCustomer.Visibility = Visibility.Hidden;
            customerWin.canvCustomerWindowMain.Visibility = Visibility.Visible;
            customerWin.Owner = this;
            customerWin.ShowDialog();
        }


        //-----------ACTIONS FOR BOOKING------------
        //actions for adding new booking
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingWin bookingWindow = new BookingWin();

            bookingWindow.canvBookingMainWindow.Visibility = Visibility.Hidden;
            bookingWindow.canvNewBooking.Visibility = Visibility.Visible;
            bookingWindow.Owner = this;
            bookingWindow.newWindow();
            bookingWindow.ShowDialog();
        }

        //actions for editing existing booking
        private void btnEditBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this line will look for reference number in the selected item
                string reference = Regex.Match(lstBookings.SelectedItem.ToString(), @"\d+").Value;
                BookingWin bookingWin = new BookingWin();

                //this method uses reference number to setup the new window with proper data
                bookingWin.setBookingWindow(reference);
                bookingWin.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Booking from the list in order to edit it", "Make selection first.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //this method will search DB to look for matching booking and will put that into the listbox
        public void btnBookSearch_Click(object sender, RoutedEventArgs e)
        {
            lstBookings.Items.Clear();
            List<Booking> results = null;

            // search is based on the searchbox data
            results = dbAccess.bookingSearch(txtBookSearch.Text);
            foreach (Booking b in results)
            {
                lstBookings.Items.Add("Ref: " + b.BookingReference + ", Arrival: " + b.Arrival.Date.ToString("dd/MM/yyyy") + ", Departure " + b.Departure.Date.ToString("dd/MM/yyyy"));
            }
        }

        //this method will open booking window after doubleclick on the list
        private void lstBookings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //this line will look for reference number part in the whole line to put the correct details in booking window
                string reference = Regex.Match(lstBookings.SelectedItem.ToString(), @"\d+").Value;
                BookingWin bookingWin = new BookingWin();
                bookingWin.Owner = this;
                //this method uses reference number to setup the new window with proper data
                bookingWin.setBookingWindow(reference);
                bookingWin.ShowDialog();
            }
            catch (Exception)
            {
                //If booking is on the list but missing in DB we want to search again and inform user
                MessageBox.Show("This Booking no longer exists.", "Missing booking", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.btnBookSearch_Click(sender, e);
            }
        }
    }
}
