   M S B A M L   `   `   `  ����M  IFlightSimulatorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:FlightSimulatorApp.ViewsFlightSimulatorApp.Views  5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   (    "FlightSimulatorApp.Views.Anomalies   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:FlightSimulatorApp.Views     5   	   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  6>   Emah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   -   5          � Title$  	Anomalies��5	   	   $	��750��6   $
��1000��6(   .��5
      � 
����ح�.+�5   
   �� $��Center=�6   $��Select a Feature:��61   $��105,14,802.6,686.8q�6J    J� RenderTransformOrigin$ 0.474,0.633&�6f   �� 5   
   $��Center=�6   $��Anomalous Timesteps��61   $��21,523,717.6,178.8q�6L   $ 0.474,0.633&�6h   �� 5   
   -   5        $��AnomalousPointsListBox��5   S   
�������
�������$��2q�6?   $��Visible=�6�   $��20.6,542.3,701.6,9.8q�6  $��1��6+  $��1��6;  ��6q   �� *VM_AnomaliesTimesteps+6;  �� 5   
   -   5        $��	MyListBox��5      
�������$��Visible=�6Y   $��21,34,701.6,212.8q�6�   ��65   �� *VM_ColNames+6�     OxyPlot.Wpf.PlotView  5   
   -   5          Name$ AnomalyPlot��5   X   
�������
�������$��2q�6D   $��303.6,10,9.6,9.8q�6�   $��1��6�   $��2��6�      Model 6m   �� *VM_AnomalyPlotModel+6�   5      5      