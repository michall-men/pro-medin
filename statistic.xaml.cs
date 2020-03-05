using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms.DataVisualization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DL.ViewModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using OxyPlot;
using OxyPlot.Series;
using System.IO;
using System.Net;
using DL.Model;

using BE;
using OxyPlot.Wpf;
using System.ComponentModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>

    public partial class statistic : UserControl
    {
        public enum PartOfDate
        {
          יום ,שבוע,
                חודש,
                שנה
        }
        UserCollection usc = new UserCollection();


        //  public CollectionOfFruits Fruit { get; set; }
        public VMTests  TObserver { get; set; }
        OxyTestPlotModel OxyPlotModel { get; set; }
        DateTime d;
        //OxyTestPlotModel OxyPlotModel; 
        public statistic()
        {
            
            TObserver = new VMTests(SpesificUser.ThisEntry.Entity.Name);
           
            OxyPlotModel = new OxyTestPlotModel(TObserver.TestsObserver.ToList(), TObserver.KbuyAnyDay(),TObserver.IronbuyAnyDay());
           
            InitializeComponent();
          
            Last.ItemsSource= Enum.GetValues(typeof(PartOfDate));
            
            List1.DataContext = TObserver;
            //model
            model1.DataContext = OxyPlotModel;
            model2.DataContext = OxyPlotModel;
            model3.DataContext = OxyPlotModel;
           
            model1.InvalidatePlot(true);

        }
        private void Last_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            switch (Last.SelectedIndex)
            {
                case 0: d = DateTime.Now.AddDays(-1); break;
                case 1: d = DateTime.Now.AddDays(-7); break;
                case 2: d = DateTime.Now.AddMonths(-1); break;
                case 3: d = DateTime.Now.AddYears(-1); break;
                 
            }
        
            TObserver = new VMTests(SpesificUser.ThisEntry.Entity.Name,d);
           
            List1.DataContext = TObserver;
            OxyPlotModel = new OxyTestPlotModel(TObserver.TestsObserver.ToList(), TObserver.KbuyAnyDayFrom(d),TObserver.IronbuyAnyDayFrom(d));
            model1.DataContext = null;
            model2.DataContext = null;
            model3.DataContext = null;
            
            model1.DataContext = OxyPlotModel;
            model2.DataContext = OxyPlotModel;
            model3.DataContext = OxyPlotModel;

            model1.InvalidatePlot(true);
            model2.InvalidatePlot(true);
            model3.InvalidatePlot(true);

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox s = sender as ComboBox;
            switch (s.SelectedIndex)
            { 
                case 0: { model1.Visibility = Visibility.Visible; model2.Visibility = Visibility.Collapsed; model3.Visibility = Visibility.Collapsed;  } break;
                case 1: { model2.Visibility = Visibility.Visible; model1.Visibility = Visibility.Collapsed; model3.Visibility = Visibility.Collapsed; } break;
                case 2: { model3.Visibility = Visibility.Visible; model1.Visibility = Visibility.Collapsed; model2.Visibility = Visibility.Collapsed; } break;
            }
        }
    }
}
