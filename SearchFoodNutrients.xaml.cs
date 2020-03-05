using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DL.ViewModel;
using DL.Model;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for SearchFoodNutrients.xaml
    /// </summary>
    public partial class SearchFoodNutrients : UserControl
    {
        FoodVM FoodCollectionObserver = new FoodVM();
        // static IDictionary<string, double> nutMeasur=null;
        
        
        public SearchFoodNutrients()
        {
            InitializeComponent();
             comboBoxFoodObserver.ItemsSource = FoodCollectionObserver.CurrentModel.Foods;

        }

        private void ComboBoxFoodObserver_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox  csender = sender as ComboBox ;
            string Sellected_food= csender.SelectedValue.ToString();
            Food f = FoodCollectionObserver.SimpleSearch(Sellected_food);
            VMDictonary vMDictonary = new VMDictonary(f.AllNutrients());
            r.Text = "Value per " + f.Measure;
            NutList.DataContext = vMDictonary;

        }

    }
}
