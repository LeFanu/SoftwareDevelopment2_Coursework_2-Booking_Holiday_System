   M S B A M L   `   `   `  ����M  ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089_<clr-namespace:System.Text.RegularExpressions;assembly=SystemSystem.Text.RegularExpressions  5   	    NapierHolidayVillage#   NapierHolidayVillage.GuestWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   [RegularExpressions<clr-namespace:System.Text.RegularExpressions;assembly=System     5   	     � Title$  Guest��5   	   $��277.406��6   $��466.946��6(   ���	#FFCBCBCB.��5      � .+�5   
   �� -   5         ��Name$ lblMainTitle��5      $��Napier Holiday Village��6&   $��Center=�6G   $	��Top=�6d    J� RenderTransformOrigin$ 4.469,0.609&�6|   $��Pericles)�6�   $��24(�6�   $
��Bold"�6�   $
��10,5q�6�    � IsSharedSizeScope . $	��338��6  �� 5   
   -   5        $ lblGuestName��5      $��Guest's Name��6&   $
��Left=�6=   $��	21,53,0,0q�6X   $	��Top=�6k   �� 5	   
   -   5         ��Name$ txtGuestName��5	      $
��Left=�6(   $��23��6C   $��	21,79,0,0q�6O    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $	��179��6�   �� 5
   
   -   5        $ lblAge��5
      $��Guest's Age��6    $
��Left=�66   $��
240,53,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5        $ txtAge��5      $
��Left=�6"   $��23��6=   $��
240,79,0,0q�6I   $
 Wrap=�6]   $	��Top=�6q   $	��179��6�   �� 5   
   -   5        $ lblPassport��5      $��Guest's Passport��6%   $
��Left=�6@   $��
21,114,0,0q�6[   $	��Top=�6o   �� 5   
   -   5        $ txtPassport��5      $
��Left=�6'   $��23��6B   $��
21,145,0,0q�6N   $
 Wrap=�6b   $	��Top=�6v   $	��179��6�    �� 	MaxLength$ 10��6�    �� CharacterCasing$ Upper=�6�   .��6�    � DataContext 5      1   +System.Text.RegularExpressions.RegexOptions 5      None=�68   .��5   #   5      �� 5   
   -   5        	 ��Name$	 btnSaveGuest��5      $
��Save��6'   $
��Left=�66   $��
21,191,0,0q�6Q   $	��Top=�6e   $��75��6}   6�   �� 5   
   -	   5        $	 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��240,191,0,0q�6P   $	��Top=�6e   $��75��6}   6�   �� 5   
   -
   5        $	 	btnDelete��5      $��Remove��6$   $
��Left=�65   $��125,191,0,0q�6P   $	��Top=�6e   $��75��6}   6�   5      5      