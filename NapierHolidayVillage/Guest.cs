using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace NapierHolidayVillage
{
    /* Author: Karol Pasierb - Software Engineering - 40270305
     *
     * Description:
     * This class contains details about the Guests. Guests are the part of the booking.
     * There is also validation included for requested range for Age
     *
     * Last Update: 07/12/2016
     */

    [Serializable]
    public class Guest
    {
        //instance fields and get set methods for them
        private string name;
        private string passportNumber;
        private int age;

        public string Name
        {
            get { return name; }
            set
            {
                    name = value;
            }
        }

        public string PassportNumber
        {
            get { return passportNumber; }
            set {
                    //no need for pasport validation as it is included in teh textbox settings
                    passportNumber = value;
                }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        //constructor
        public Guest()
        {

        }

        //validation for proper age
        public bool ageInRange(String ageString)
        {
            try
            {
               int tempAge = Convert.ToInt32(ageString);
               if (tempAge > 0 && tempAge < 101)
                {
                    age = tempAge;
                    return true;
                }
                else
                {
                      MessageBox.Show("Age has to be between 0 and 101.", "Wrong age", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                      return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Age has to be a numeric vaue between 0 and 101.", "Wrong value", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
        }

        //validation for proper name
        public bool nameFormat(String name)
        {
            //alowing only letters and space in name
            if (Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
            {
                this.name = name;
                return true;
            }
            else
            {
                MessageBox.Show("Please enter proper name format.", "Wrong Characters", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
        }
    }
}
