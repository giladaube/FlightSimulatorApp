   M S B A M L   `   `   `  ����M  IFlightSimulatorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:FlightSimulatorApp.ViewsFlightSimulatorApp.Views  5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   %    FlightSimulatorApp.Views.Graphs   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:FlightSimulatorApp.Views     5   	   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  6>   Emah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   -   5          � Title$  Graphs��5
   	   $	��600��6   $	��800��6%    � WindowStartupLocation$ Manual=�6e   .��5      � 
����ح�.+�5   
   �� -   5        $��	MyListBox��5   $   
�������$��Visible=�6Y   $��10,20,544.6,70.4q�6�   ��65   �� *VM_ColNames+6�     OxyPlot.Wpf.Plot  5   
   
�������
�������$��2q�6A   $��274,20,282.6,344.4q�6      Title 6U   �� *VM_SelectedGraphFeature+   Series 5        OxyPlot.Wpf.LineSeries  5      ��6!   �� *#!VM_Last300PointsOfSelectedFeature+5        5   
   
�������
�������$��2q�6A   $��525,20,19.333,344.667q�6�    6U   �� *#!VM_SelectedGraphFeatureCorrelated+ 5        5      ��6!   �� *-+VM_Last300PointsOfSelectedFeatureCorrelated+5        5   
   
�������
�������$��2q�6@   $��274,244,19.333,29.667q�6�    6T   �� *#!VM_SelectedGraphFeatureCorrelated+ 5        5          	LineStyle$
 None=�6!      
MarkerFill$ Black��62      
MarkerSize$ 1Q�6E      
MarkerType$ Circle=�6T   ��6h   �� */-VM_Last300PointsOfSelectedFeatureAsCorrelated+  5!      $ Solid=�6!   $
 Aqua��6`   ��63   �� *VM_LinearRegression+6`   5#      �� 5%   
   -   5         ��Name$ 	DLLButton��5%   $   
�������$��10,505,544.6,26.4q�6P   $#��Load DLL Anomaly Detection...��6k   5(      5)      