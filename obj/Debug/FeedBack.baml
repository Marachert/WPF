   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.FeedBack   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  FeedBack��5   	   $	��150��6   $	��300��6'   
������� � 	Resources 5	       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value �� 
StartPoint	 �� EndPoint
 � Offset � Color :� RoutedEvent �� TargetProperty g� Duration W� From	 W� To �� Binding �� ElementName �� Path �� Value��%  '��       �� 5
   
   " ��6   �5      �� 5       � ��
 ��  �66   �� 5       � ��
 �����66   �� 5       � ��$ Verdana)�66   �� 5       � ��$ 3q�6;   �� 5       � ��
 �  ��67   �� 5       � ��$ 10q�62    5      P� 5       �@J�IsMouseOver . . �5      ��  � �� 5      �� 5      $ 0.5 0.5&�5      $	 0.7 0.9&�5      .��5   "   � $	
 0.2Q�5   !   $
 Gold��5   !   � 5   "   $	
 0.4Q�5   !   $ 	Gainsboro��5   !   � 5    "   $	
 0.6Q�5!   !   $ HotPink��5"   !   5#      5&      5'      :� 5)      $ Click��6   [�5*      �� 5+      .��5,      �� .��5-   "   W� $ Width�5.   %   $ 0:0:0.8D�5/   %   $ 20X�50   %   $	 220X�6/   W� 51   "   $ Opacity�52   %   $ 0:0:0.7D�53   %   $	 0.5X�54   %   $ 1X�60   55      56      58      �� 5:       5;      �� $ notify��$ 	IsChecked� True5<      .a�5=      ��  � ��
 � � �6;   5>      5A      5B      .��5D      �  � ColumnDefinitions 5E   
   �� 5F      6!   �� 5G      6!    � RowDefinitions 5I   
   �� 5J      $��* �6   �� 5K      $
��0.5* �6   �� 5L      $
��1.5* �6   .+�5O   
   �� $��0��5P      $��0��5Q      $��Center=�5R      $��Right=�5S      $��20(�5T      $��10q�5U      $��Ваше мнение: ��5V      �� 5X   
   $��1��5Y      $��2��5Z      $��Center=�5[      $��Center=�5\      $	��160��5]      $��50��5^      �� 5`   
   -   5         ��Name$ notify��5c      $��0��5a      $��1��5b      $��15��5d      $*��$Уведомить об ответе��5e      $��Center=�5f      $��Center=�5g      �� 5i   
   -   5         ��Name$ 
SendButton��5j      $��0��5k      $��2��5l      $��2��5m      $��Послать��5n      $��25(�5o      5q      5r      5s      