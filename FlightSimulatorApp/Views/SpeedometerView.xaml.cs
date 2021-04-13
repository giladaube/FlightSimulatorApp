﻿using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/***
 * SpeedometerView- View class, holds a VM to sends command and bind data with its properties.
 * ***/

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for SpeedometerView.xaml
    /// </summary>
    public partial class SpeedometerView : UserControl
    {
        private ViewModelControls vm;
        public SpeedometerView()
        {
            InitializeComponent();
            this.vm = new ViewModelControls(MainWindow.getModel());
            this.DataContext = this.vm;
        }
    }
}
