using System;
using System.Linq;
using System.Windows;

namespace NapierHolidayVillage
{

    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This window alows interactions with Booking and Guest classess.
     * It is failry simply allowing to add/edit/remove Guest all in one window.
     * There is no more functionality.
     *
     * Last Update: 07/12/2016
     */

    /// <summary>
    /// Interaction logic for GuestWin.xaml
    /// </summary>
    partial class GuestWin : Window
    {
        //references and variables used in this instance
        int guestNumber;
        Guest guestInUse;
        Booking bookingInUse;
        NHV_DatabaseAccessSingleton dbAccess = NHV_DatabaseAccessSingleton.Instance;
        BookingWin bookingWindow;

        //altered constructor allows us to know which guest we're operating on
        public GuestWin(int guestNumber, Booking bookingInUse)
        {
            InitializeComponent();
            this.guestNumber = guestNumber;
            this.bookingInUse = bookingInUse;
        }

        //if we want to edit Guest this method is called before the constructor, so the window would contain correct data
        public void setWindow()
        {
            txtGuestName.Text = bookingInUse.Guests.ElementAt(guestNumber).Name;
            txtAge.Text = bookingInUse.Guests.ElementAt(guestNumber).Age.ToString();
            txtPassport.Text = bookingInUse.Guests.ElementAt(guestNumber).PassportNumber;
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //actions to take whie editing or adding new guest
        public void btnSaveGuest_Click(object sender, RoutedEventArgs e)
        {
            bookingWindow = (BookingWin)this.Owner;
            guestInUse = new Guest();

            //checking if all the fields are filled in
            if(txtAge.Text != "" && txtGuestName.Text != "" && txtPassport.Text !="")
            {
                bool correctAge = guestInUse.ageInRange(txtAge.Text);
                bool correctName = guestInUse.nameFormat(txtGuestName.Text);
                guestInUse.PassportNumber = txtPassport.Text.ToUpper();

                //if age and anme are in correct format we proceed further
                if (correctAge && correctName)
                {
                    //adding guest to the list of guests for this booking
                    bookingInUse.addEditGuest(guestInUse, guestNumber);

                    //updating the list of guests in the booking window.
                    bookingWindow.lstGuests.Items[guestNumber] = guestInUse.Name + ", " + guestInUse.PassportNumber + ", " + guestInUse.Age;
                    bookingWindow.lstGuestsCanvas.Items[guestNumber] = guestInUse.Name + ", " + guestInUse.PassportNumber + ", " + guestInUse.Age;

                    this.Close();
                    //bookingWindow = (BookingWin)this.Owner;
                    //bookingWindow.lstGuests.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields before saving.", "Missing Details.", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        //actions to take when removing Guest
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            bookingWindow = (BookingWin)this.Owner;
            //making sure you cannot delete a Guest if there's last one
            if (bookingInUse.Guests.Count > 1)
            {
                try
                {
                    bookingInUse.Guests.RemoveAt(guestNumber);
                    //replacing Guest's details with add new
                    bookingWindow.lstGuests.Items[guestNumber] = "add new...";
                }
                catch (ArgumentOutOfRangeException)
                {
                }
            }
            else
            {
                MessageBox.Show("Unable to delete this Guest. There has to be at least one Guest.", "Minumum Guests", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.Close();
            //bookingWindow = (BookingWin)this.Owner;
            //bookingWindow.lstGuests.Items.Refresh();
        }
    }
}
