using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace NapierHolidayVillage
{

    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class will read our database stored in a file at the start of the program.
     * We want to keep it separately as we also want to update the file after each interaction with the database to be sure our data is correct.
     * Therefore this class will be responsible for handling all file operations
     *
     * Design Patterns Used:
     * I used SINGLETON to build this class and to dissallow creating more than one instance of this class.
     * There is no need for that as only one object can handle all the file operations.
     * Instead of creating new object in every window and reading file again, we use the same instance with the same status across the whole application.
     * With the development of this class I realised it resembles Facade in some points and could be easily reorganised and split.
     *
     * Last Update: 07/12/2016
     */

    [Serializable]
    class NHV_DatabaseAccessSingleton
    {

        /* lists for storing our customers and bookings to be able to operate on them
        *  These lists are the ones for the whole application. The main ones.
        *  Each Customer has a list of its own bookings and each Booking has a list of its own Guests
        *  We require separatae lists for reference purposes. With help of the LINQ we can organise those lists on the startup
        */
        private List<Customer> customersDB = new List<Customer>();
        private List<Booking> bookingsDB;

        public List<Booking> BookingsDB
        {
            get { return bookingsDB; }
            set { bookingsDB = value; }
        }
        internal List<Customer> CustomersDB
        {
            get { return customersDB; }
            set { customersDB = value; }
        }

        //reference to itself for Singleton
        private static NHV_DatabaseAccessSingleton instance;


        //constructor
        private NHV_DatabaseAccessSingleton()
        {

        }

        //this property will create a new instance for Singleton if it is the first time when it's used. Otherwise it will return the instance of already created object
        public static NHV_DatabaseAccessSingleton Instance
        {
            get
            {
                if (instance == null)
                    /*
                     * If this is being called for the first time, instanciate a Logger object.
                     */
                {
                    instance = new NHV_DatabaseAccessSingleton();
                }
                return instance;
            }
        }




        //-----------READING FILES ON STARTUP AND CREATING LISTS----------------------------
        //fields and references for serialization
        [NonSerialized()]
        FileStream stream;
        [NonSerialized()]
        string filename;
        [NonSerialized()]
        BinaryFormatter formatter = new BinaryFormatter();

        //this method is going to read our customer DataBase at the beginning of every program
        public void readDB_OnStartup()
        {
            try
            {
                //we are accessing our file
                filename = "customers.dat";
                FileStream stream3 = File.OpenRead(filename);
                try
                {
                    customersDB = (List<Customer>)formatter.Deserialize(stream3);
                }
                catch (Exception)
                {
                    MessageBox.Show("File Empty. Please create new Database.", "Empty File", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                stream3.Close();

                Customer.ReferenceCount = customersDB.Last().CustomerReference;

            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Database for Customers not found! Please add new Customers to create new one.", "Missing File!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            readBookings();
        }

        //this method goes through all the customers and read their bookings to fill in our bookings DB to be able to operate on all of them
        private void readBookings()
        {
            //clearing the list to avoid duplicates and errors
            bookingsDB = null;
            bookingsDB = new List<Booking>();

            //populating list of all existing bookings
            try
            {
                foreach (Customer customer in customersDB)
                {
                    foreach (Booking booking in customer.Bookings)
                    {
                        bookingsDB.Add(booking);
                    }
                }

                //after we concluded we sort the list and assign static variable for generating further booking reference numbers
                bookingsDB = bookingsDB.OrderBy(b => b.BookingReference).ToList();
                Booking.ReferenceCount = bookingsDB.Last().BookingReference;
            }
            catch (Exception)
            {
                MessageBox.Show("Currently there are no bookings.");
            }
        }




        //-------------------ALTERING OUR DATABASE AND LISTS-------------------------------------
        //this method will save our DB after every update on customer or booking


        //method for deleting requested customer from the list
        public void deleteEntry(Customer toBeDeleted)
        {
            customersDB.Remove(customersDB.Find(x => x.CustomerReference == toBeDeleted.CustomerReference));
            //when deleting customer we want the reference number to match last customer.
            //having this wil allow us to count upwards again from the last highest number
            Customer.ReferenceCount = customersDB.Last().CustomerReference;
            //writing our list to a file after it was updated
            saveFiles();
        }

        public void deleteEntry(Booking toBeDeleted)
        {
            toBeDeleted.CustomerOwner.Bookings.Remove(toBeDeleted.CustomerOwner.Bookings.Find(b => b.BookingReference == toBeDeleted.BookingReference));
            bookingsDB.Remove(bookingsDB.Find(b => b.BookingReference == toBeDeleted.BookingReference));
            //writing our list to a file after it was updated
            saveFiles();
        }


        //DO WE NEED TRY CATCH IN HERE? POSSIBLY NOT
        //method for saving our current DB to a file
        public void saveFiles()
        {
            filename = "customers.dat";
            stream = File.Create(filename);
            formatter.Serialize(stream, customersDB);
            stream.Close();
            readBookings();
        }


        //this method is used with search box from the main window. It returns the list of filtered customers
        public List<Customer> customerSearch(String reference)
        {
            //creating variable to store filtered results
            IEnumerable<Customer> customersList;
            customersList = CustomersDB.AsQueryable();
            //creating list for filtered objects
            List<Customer> filteredResults = new List<Customer>();

            int referenceInt;
            bool isDigit = int.TryParse(reference, out referenceInt);
            //linq search query that looks for entries containing any part of the search box content
            //it is is a digit we filter results for the reference number
            if (isDigit == true)
            {
                customersList = customersList.Where(m => m.CustomerReference.ToString().Contains(reference));
            }
                //if it is a text we search name or the address
            else
            {
                customersList = customersList.Where(m => m.Name.ToLower().Contains(reference.ToLower()) || m.Address.ToLower().Contains(reference.ToLower()));
            }

            foreach (Customer c in customersList)
            {
                //if an object has matching reference number it is added to the result's list
                if (c.CustomerReference.ToString().Contains(reference) || c.Name.ToLower().Contains(reference.ToLower()) || c.Address.ToLower().Contains(reference.ToLower()))
                {
                    filteredResults.Add(c);
                }
            }
            //we return filtered objects
            return filteredResults;
        }

        //method for fitering bookings depending on the query
        public List<Booking> bookingSearch(String reference)
        {
            readBookings();
            IEnumerable<Booking> listOfBookings;
            listOfBookings = bookingsDB.AsQueryable();

            //creating list for filtered objects
            List<Booking> filteredResults = new List<Booking>();

            //linq search query that looks for entries containing any part of the search box content
            listOfBookings = listOfBookings.Where(m => m.BookingReference.ToString().Contains(reference));
            foreach (Booking c in listOfBookings)
            {
                //if an object has matching reference number it is added to the result's list
                if (c.BookingReference.ToString().Contains(reference))
                {
                    filteredResults.Add(c);
                }
            }
            //we return filtered objects
            return filteredResults;
        }
    }
}
