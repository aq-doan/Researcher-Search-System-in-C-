   M S B A M L   `   `   `  ����Q  MAssigmentSpecification, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:AssigmentSpecification.ViewAssigmentSpecification.View  5      4    .AssigmentSpecification.View.ResearcherListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:AssigmentSpecification.View     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6F   .��5
      � -   5          �Name$  researcher_list_view��5
      .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��0q�6   $��Stretch=�6*   $��Stretch=�6H   $��0q�6d   .��5      � $��Stretch=�6   $��Stretch=�61    � RowDefinitions 5      �� 5      $
��0.2* �6$   64   �� 5      $��* �6$   61   .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��1* �6+   68   �� 5      $��1* �6+   68   �� 5      $
��0.5* �6+   6:    5      �� 5      $
��0.2* �6(   68   �� 5      $��1* �6(   66   �� 5      $
��0.2* �6(   68   .+�5      � $��1��6   $��0��6(    5      �� 5      $
��0.5* �6/   6>   �� 5      $��1* �6/   6<   .+�5!      � $��0��6   $��0��6/   .+�5#      �� $��
10,6,10,13q�6(   $��Level��6<   $��Center=�6I    �� TextAlignment$ Center=�6d   $��Center=�6{   $��13(�6�   ���#0000006�   5$      � 5%      $��1��6   $��1��6,   .+�5&      �� -   5        $��cboLevel��5&   h    �� Text$	 All��6'    �� 
IsEditable .  �� 
IsReadOnly . $��0,5,0,13q�6V   $��Center=�6x   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��20��6�   $	��110��6  
���   �$��1q�6#  .@�5'   "   �� $��Student��6/   $
��Left=�6A   6e   �� 5(   "   $��A��6/   $
��Left=�6;   6_   �� 5)   "   $��B��6/   $
��Left=�6;   6_   �� 5*   "   $��C��6/   $
��Left=�6;   6_   �� 5+   "   $��D��6/   $
��Left=�6;   6_   �� 5,   "   $��E��6/   $
��Left=�6;   6_   �� 5-   "   $��Other��6/   $
��Left=�6?   6c   5.      5/      50      � 51      $��1��6   $��1��6(    52      �� 53      $
��0.7* �6/   6>   �� 54      $��1* �6/   6<   .+�56      � $��0��6   $��0��6/   .+�57      �� $��
10,6,10,13q�6(   $��Name:��6<   $��Center=�6I   $ Center=�6d   $��Center=�6{   $��13(�6�   ���#0000006�   58      � 59      $��1��6   $��0��6/   .+�5:      �� -   5         ��Name$ txtName��5:   o   $��0,6,0,13q�6&    � ToolTip$
 Name��68   
���   �$��1q�6[   $��25��6�   $��Stretch=�6�   $��15(�6�   $��Normal"�6�   ���#000000���#FFFFFF$��Center=�6�   $��Center=�6  67  5;      5<      � 5=      $��1��6   $��2��6(   .+�5?      �� -   5        	 ��Name$	 	btnSearch��5?   !   ���#FFFFFF$��18��6I   $��5,6,5,13q�6U   
���   �$��1q�6{   .��5@      �� $��Bottom=�6(   $ Center=�6C   $��12(�6Z   ���#000000Search6�   5A      5B      5C      �� 5E      
�������$��1q�6.   $��1��6B   $��0��6O   ._�5F      �  5G      �� 5H      $
��0.2* �6,   6<   �� 5I      $��* �6,   69   .+�5K      � $��1��6    5L      �� 5M   "   $
��0.1* �63   6B   �� 5N   "   $��3* �63   6@   �� 5O   "   $
��0.1* �63   6B   .+�5Q      � $��1��6#   .+�5R   "   &   System.Windows.Controls.DataGrid  -   5        
  Name$
 dgDataNameLevel��5R   +   ���#FFFFFF
���   �$��1q�6m   $��3��6�      CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     GridLinesVisibility$ 
Horizontal=�6�      HeadersVisibility$ Column=�6     CanUserAddRows .     CanUserDeleteRows .     SelectionMode$ Extended=�6R     
IsReadOnly .    AutoGenerateColumns .  |�.  $   HorizontalScrollBarVisibility$ Hidden=�6�  ��.     Columns 5S   &   0  *System.Windows.Controls.DataGridTextColumn  5T   *   ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6=      
Foreground
 �   �   Header$
 Name��6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5U   .   `� 5V   2    �� StringFormat$ {0}, {1}��6?   .1�5W   6   ��  �� Path$ 	GivenName�6>   �� 5X   6   $ 
FamilyName�6>   5Y   3   5[   +     5\   *   $ 1* 6X   
 �   �$ Title��6w    6=   �� *Title+5]   +   5_   #   5`      5a      5b      5c      5d      5e      5f      5g      