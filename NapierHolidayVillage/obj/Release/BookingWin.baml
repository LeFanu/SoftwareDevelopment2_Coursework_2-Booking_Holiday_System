   M S B A M L   `   `   `  ����   NapierHolidayVillage%    NapierHolidayVillage.BookingWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Booking Details��$	��749��$	��350�����	#FFCBCBCB.��� .+��� -    ��Name$ canvBookingMainWindow��$
��Left=�$	��709��$	��Top=�$	��300��$��10,0,0,0q�.+��� -    ��Name$ lblBookingTitle��$��Napier Holiday Village��$��Center=�$	��Top=� J� RenderTransformOrigin$ 4.469,0.609&�$��Pericles)�$��24(�$
��Bold"�$
��10,5q� � IsSharedSizeScope . $	��338��
 �� Top$ 20���� -   $ lblBookReference��$��Reference Number: ��$
��Left=�$	��Top=�$ 0.204,0.461&�$
��Bold"� �� Left$ 28��$ 66���� -    ��Name$ txtBlkReferenceNumber��$
��Left=�$
��Wrap=�$�� Reference Number��$	��Top=�$ 	0.4,0.146&�$	 152��$ 71���� -   $ lblArrivalDate��$��Arrival Date��$
��Left=�$	��Top=�$ 0.875,0.328&�$ 28��$ 96��$
��Bold"��� -   $ lblDepartureDate��$��DepartureDate��$
��Left=�$	��Top=�$ 28��$	 150��$
��Bold"��� -   $ lblCustomerDetails��$��Customer Details��$
��Left=�$	��Top=�$ 0.237,0.321&�$ 28��$	 211��$
��Bold"��� -   $ txtblkCustomerDetails��$
��Left=�$
��Wrap=�$B��<  Details of a customer who made                 the booking��$	��Top=�$��39��$	��196��$ 31��$	 242���� -	   $ 	lblGuests��$��Guests��$
��Left=�$	��Top=�$ 27��$	 286��$ 
0.63,6.192&�$
��Bold"��� -
    ��Name$ 	lstGuests��$
��Left=�$��68��$	��Top=�$	��227��$ 0.367,0.407&�$ 28��$	 317��.@��� $��
add new...���� $��
add new...���� $��
add new...���� $��
add new...���� -   	 ��Name$	 btnEdit��$
��Edit��$
��Left=�$	��Top=�$��54��$ 21��$	 677���� -   $	 	btnDelete��$��Delete��$
��Left=�$	��Top=�$��54��$ 94��$	 677��$ 1.444,0.455&�(  "System.Windows.Controls.DatePicker  -   
  Name$
 datePickArrival��$
��Left=�$	��Top=�$	��117��   FirstDayOfWeek$ Monday=�$ 31��$	 121��  -   $
 datePickDeparture��$
��Left=�$	��Top=�$	��117��$ Monday=�$ 31��$	 181���� -   $	 btnNewBooking��$	��New��$	 158��$	 677��$��54��$ 0.759,0.455&��� -   $ 	lblExtras��$��Extras��$ 31��$	 390��$
��Bold"��� -   $ lblBreakfast��$��	Breakfast��$ 28��$	 416���� -   $	 btnAddRemoveBreakfast��$	��Add��$	 198��$	 416��$��57���� -   $ lblEveningMeal��$��Evening Meal��$ 28��$	 469���� -   $	 btnAddRemoveEveningMeal��$	��Add��$	 198��$	 473��$��57���� -   $	 
btnInvoice��$��Invoice��$	 229��$	 677��$��54���� -   $ 
lblCarHire��$��Car Hire��$ 31��$	 529��$��75���� -   $ 	lblDriver��$��Driver Name��$ 57��$	 555��$ 0.184,0.308&��� -    ��Name$ txtDriverName��$��23��$	 139�� �� TextWrapping$
 Wrap=�$	 556��$	��116���� -   $ lblHireStart��$��
Start Date��$ 57��$	 581���� -   $ 
lblHireEnd��$��End Date��$ 57��$	 607��  -   $
 dateHireStart��$	 139��$	 581��$	��116��$ Monday=�  -   $
 dateHireEnd��$	 139��$	 607��$	��116��$ Monday=��� -   $	 btnAddRemocveCar��$	��Add��$	 198��$	 636��$��57���� -   $ lblEditConfirmed��$��Updated Successfully!��$ 21��$	 649��$	��120�����	#00000000$��10(����	#FFCB0505$y�Hidden=��� -   $ dietaryReuirements��$��Dietary 
Requirements��$ 31��$	 432��$��10(��� -    $% txtBreakfastDietaryRequirements��$��23��$	 107��$
 Wrap=�$	 441��$	��148���� -!   $ dietaryReuirements_Copy��$��Dietary 
Requirements��$ 31��$	 491��$��10(��� -"   $# txtEveningDietaryRequirements��$��23��$	 107��$
 Wrap=�$	 500��$	��148���� 
���   �$��1q�$	��277��$ 10��$	 390��$	��260���� -#   $ canvNewBooking��$
��Left=�$	��709��$	��Top=�$	��300��$��10,0,0,0q�.+��� -$   $ lblBookingTitleCanvas��$��Napier Holiday Village��$��Center=�$	��Top=�$ 4.469,0.609&�$��Pericles)�$��24(�$
��Bold"�$
��10,5q� . $	��338��$ 20���� -%   $ lblBookReferenceCanvas��$��Reference Number: ��$
��Left=�$	��Top=�$ 0.204,0.461&�$
��Bold"�$ 27��$ 66���� -&   $! txtBlkReferenceNumberCanvas��$
��Left=�$
��Wrap=�$��Will Be Generated��$	��Top=�$ 	0.4,0.146&�$	 152��$ 71���� -'   $ lblArrivalDateCanvas��$��Arrival Date��$
��Left=�$	��Top=�$ 0.875,0.328&�$ 28��$ 96��$
��Bold"��� -(   $ lblDepartureDateCanvas��$��DepartureDate��$
��Left=�$	��Top=�$ 28��$	 150��$
��Bold"��� -)   $  lblCustomerReferenceCanvas��$��	Customer:��$
��Left=�$	��Top=�$ 0.237,0.321&�$ 28��$	 211��$
��Bold"��� -*   $ lblGuestsCanvas��$��Guests��$
��Left=�$	��Top=�$ 27��$	 286��$
��Bold"��� -+   $ lstGuestsCanvas��$
��Left=�$��68��$	��Top=�$	��227��$ 0.367,0.407&�$ 28��$	 317��.@��� $��
add new...���� $��
add new...���� $��
add new...���� $��
add new...���� -,   $	 btnSave��$
��Save��$
��Left=�$	��Top=�$��54��$ 21��$	 677���� --   $	 	btnCancel��$��Cancel��$
��Left=�$	��Top=�$��60��$ 92��$	 677��  -.   $
 datePickArrivalCanvas��$
��Left=�$	��Top=�$	��117��$ Monday=�$ 31��$	 121��  -/   $
 datePickDepartureCanvas��$
��Left=�$	��Top=�$	��117��$ Monday=�$ 31��$	 181���� -0   $ lblExtras_Canvas��$��Extras��$ 31��$	 390��$
��Bold"��� -1   $ lblBreakfast_Copy��$��	Breakfast��$ 28��$	 416���� -2   $"	 btnAddRemoveBreakfast_Canvas��$	��Add��$	 198��$	 416��$��57���� -3   $ lblEveningMeal_Copy��$��Evening Meal��$ 28��$	 469���� -4   $$	 btnAddRemoveEveningMeal_Canvas��$	��Add��$	 198��$	 473��$��57���� -5   $ lblCarHire_Copy��$��Car Hire��$ 31��$	 528��$��75���� -6   $ lblDriver_Copy��$��Driver Name��$ 57��$	 554��$ 0.184,0.308&��� -7   $ txtDriverName_Canvas��$��23��$	 139��$
 Wrap=�$	 553��$	��116���� -8   $ lblHireStart_Copy��$��
Start Date��$ 57��$	 580���� -9   $ lblHireEnd_Copy��$��End Date��$ 57��$	 606��  -:   $
 dateHireStart_Canvas��$	 139��$	 579��$	��116��$ Monday=�  -;   $
 dateHireEnd_Canvas��$	 139��$	 605��$	��116��$ Monday=��� -<   $	 btnAddRemocveCar_Canvas��$	��Add��$	 198��$	 634��$��57���� -=   $ dietaryReuirements_Copy1��$��Dietary 
Requirements��$ 31��$	 432��$��10(��� ->   $, &txtBreakfastDietaryRequirements_Canvas��$��23��$	 107��$
 Wrap=�$	 441��$	��148���� -?   $ dietaryReuirements_Copy2��$��Dietary 
Requirements��$ 31��$	 491��$��10(��� -@   $* $txtEveningDietaryRequirements_Canvas��$��23��$	 107��$
 Wrap=�$	 500��$	��148���� -A    ��Name$ cmbCustomersList��$ 31��$	 237��$	��188�� �� Text$ Please select a customer�� �� 
IsReadOnly .  �� 
IsEditable . �� -B   $	 btnSelectCustomer��$��Select��$	 225��$	 237��$��65���� 
���   �$��1q�$	��277��$ 10��$	 390��$	��261��