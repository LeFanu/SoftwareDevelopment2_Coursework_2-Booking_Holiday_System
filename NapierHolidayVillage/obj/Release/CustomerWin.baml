   M S B A M L   `   `   `  ����   NapierHolidayVillage&     NapierHolidayVillage.CustomerWin   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  CustomerWindow�� � Title$ CustomerWin��$	��620��$	��365�����	#FFCBCBCB.��� .+��� -    ��Name$ canvNewCustomer��$
��Left=�$	��620��$	��360��$��-3,0,0,0q�$	��Top=�$��Hidden=�.+��� -    ��Name$ lblCustomerTitle_Canvas��$��Napier Holiday Village��$��Center=�$	��Top=� J� RenderTransformOrigin$ 4.469,0.609&�$��Pericles)�$��24(�$
��Bold"�$
��10,5q� � IsSharedSizeScope . $	��338��
 �� Top$ 10�� �� Left$ 12���� -   $ lblCustReference_Canvas��$��Reference Number:��$
��Left=�$	��Top=�$ 0.204,0.461&�$
��Bold"�$ 37��$ 58���� -   $ lblCustName_Canvas��$
��Name��$
��Left=�$	��Top=�$ 37��$ 84���� -    ��Name$ txtCustName_Canvas��$
��Left=�$��23��	 �� TextWrapping$
	 Wrap=�$	��Top=�$	��157��$ 37��$	 110��$��Please enter the name���� -   $ lblAddress_Canvas��$��Address��$
��Left=�$	��Top=�$ 37��$	 147���� -   $ txtAddress_Canvas��$
��Left=�$��57��$
	 Wrap=�$	��Top=�$	��288��$ 37��$	 173��$"��Please enter address details���� -	   
 ��Name$"
 txtBlkReferenceNumber_Canvas��$
��Left=�$
��Wrap=�$��Will be Generated��$	��Top=�$ 	0.4,0.146&�$	 161��$ 63��$	��152���� -
    ��Name$ btnSaveCustomer��$
��Save��$
��Left=�$	��Top=�$��75��$ 37��$	 235���� -   $ btnCancelCustomer��$��Cancel��$
��Left=�$	��Top=�$��75��$	 143��$	 235���� -   $ canvCustomerWindowMain��$
��Left=�$	��620��$	��Top=�$	��357��$��0,-1,0,0q�.+��� -   $ lblCustomerTitle��$��Napier Holiday Village��$��Center=�$	��Top=�$ 4.469,0.609&�$��Pericles)�$��24(�$
��Bold"�$
��10,5q� . $	��338��$ 10��$ 12���� -   $ lblCustReference��$��Reference Number:��$
��Left=�$	��Top=�$ 0.204,0.461&�$
��Bold"�$ 34��$ 58���� -   $ lblCustName��$
��Name��$
��Left=�$	��Top=�$ 34��$ 84���� -   $ txtCustName��$
��Left=�$��23��$
	 Wrap=�$	��Top=�$	��157��$ 34��$	 110���� -   $ 
lblAddress��$��Address��$
��Left=�$	��Top=�$ 34��$	 148���� -   $ 
txtAddress��$
��Left=�$��57��$
	 Wrap=�$	��Top=�$	��288��$ 34��$	 174���� -   $
 txtBlkReferenceNumber��$
��Left=�$
��Wrap=�$�� Reference Number��$	��Top=�$ 	0.4,0.146&�$	 155��$ 63���� -   $ 
btnNewCust��$	��New��$
��Left=�$	��Top=�$��75��$ 34��$	 236���� -   $ 	btnUpdate��$��Update��$
��Left=�$	��Top=�$��75��$	 140��$	 236���� -   $ 	btnDelete��$��Delete��$
��Left=�$	��Top=�$��75��$	 247��$	 236���� -   $ lblBookingsTitle��$��Bookings��$
��Left=�$	��Top=�$��Pericles)�$��14(�$
��Bold"�$��84��$ 34��$	 268���� -   $ lblPastBookings��$��Past Bookings��$
��Left=�$	��Top=�$ 34��$	 296���� -    ��Name$ lstPastBookings��$
��Left=�$��90��$	��Top=�$	��288��$ 34��$	 322���� -   $ lblFutureBookings��$��Future Bookings��$
��Left=�$	��Top=�$ 34��$	 417���� -   $ lstFutureBookings��$
��Left=�$��90��$	��Top=�$	��288��$ 34��$	 443���� -   $ btnAddBooking��$��Add Booking��$
��Left=�$	��Top=�$��92��$ 34��$	 538��