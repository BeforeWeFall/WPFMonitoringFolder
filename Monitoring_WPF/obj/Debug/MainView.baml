   M S B A M L   `   `   `  ����I  EMonitoring_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Monitoring_WPFMonitoring_WPF  5          Monitoring_WPF.MainView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Monitoring_WPF     5        N� Title$  MainView��5	       N� ShowsNavigationUI .  .��5      �  � ColumnDefinitions 5   
   �� 5      $��0.25* �6   �� 5      $
��0.5* �6   �� 5      $
��0.2* �6    � RowDefinitions 5   
   �� 5      $
��0.1* �6   6-   �� 5      $��* �6   6)   .+�5   
   �� -   5         ��Name$ settings��5      $��Настройки��6#   $��2��69   $
��auto��5      $��Center=�6   $��Center=�6=   $	��100��6X   $��20��6g   $��14(�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGridInfo��5      $��2��6)   $��3��67      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Disabled=�6�   $��14(�6  ��6K   �� *
Collection+	   Columns	 5      4  .System.Windows.Controls.DataGridTemplateColumn  5       ,  &System.Windows.Controls.DataGridColumn
   Header$
 Image��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 68      HeaderStyle 5!      �� 5"       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5#      ��  �� Property � �� �� Value$ Center=�6K   5$         CellTemplate 5&      �� 5'      .R�5(      �� $
��Auto��6>   $	��100��6L   ��6$   �� *Image+6L   5*      5,      0  *System.Windows.Controls.DataGridTextColumn  5.      $
 ФИО и дата��6%   $
 0.4* 6^    �� TextAlignment$ Center=�6k      
IsReadOnly .    FontSize$ 14Q�6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ��  �� Path$ NameAndDate� 5/      �� 50      "  6    .�51      ��  � ��$ Center=�6K   52         	CellStyle 54      �� 55      *  $System.Windows.Controls.DataGridCell"  6    �56      �� 57   "    �@��TextAlignment$ Center=�6L   �� 58   "    � ��$
 auto��6E   �� 59   "    � �� 5:   &   �� 5;   *    �� 
TargetType"  6:   .R�5<   .   � #��z��63   .+�5=   2   �� $��Center=�6C   5>   /   5?   +   5A   #   5C      5E        5G      $
 Температура��6%   $
 0.2* 6o    . $ 14Q�6�    6:   �� $
 Temp� �� StringFormat$ {0} °С�� 5H      �� 5I      "  6    .�5J      ��  � ��$ Center=�6K   5K       5M      �� 5N      "  6    �5O      �� 5P   "    � �� 5Q   &   �� 5R   *   "  6:   .R�5S   .   � #��z��63   .+�5T   2   �� $��Center=�6C   5U   /   5V   +   5X   #   �� 5Y   "    � ��$ Center=�6O   �� 5Z   "    �@��TextAlignment$ Center=�6L   5\      5^      5a      5b      5c      