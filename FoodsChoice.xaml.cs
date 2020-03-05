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
using DL.ViewModel;
using DL.Model;
using DL.Command;
using BE;
using System.Collections.ObjectModel;


namespace UI
{
    /// <summary>
    /// Interaction logic for FoodsChoice.xaml
    /// </summary>
    /// 
    public partial class FoodsChoice : UserControl
    {
        UserCollection use = new UserCollection();
        string CurrentUsername = SpesificUser.ThisEntry.Entity.Name;
        List<Food> chosenF;

        public CollectionOfFruits Fruit = new CollectionOfFruits();
        public CollectionOfVeget Veg = new CollectionOfVeget();
        public CollectionOfElse Else = new CollectionOfElse();

        // public List<Food> Chosed { get; set; }
        Meal This_meal;
        Food ezer;
        public FoodsChoice()
        {
            InitializeComponent();
            Start();
        }
        private void Start()
        {
            This_meal = new Meal();//primiary id
            chosenF = new List<Food>();
            InitializeFoodLists();
            InitializeCombobox();
            this.choosMealgrid.DataContext = This_meal;
        }
        private void InitializeFoodLists()
        {
            fruitList.DataContext = null; vegList.DataContext = null; elseList.DataContext = null; //for refreshing
            fruitList.DataContext = Fruit; vegList.DataContext = Veg; elseList.DataContext = Else;
         }
        private void InitializeCombobox()
        {
            MealTime.ItemsSource = null;//to refresh
            MealTime.ItemsSource = Enum.GetValues(typeof(TimeOfMeal));
            feel.ItemsSource = null;//to refresh
            feel.ItemsSource = Enum.GetValues(typeof(Feelings));
            plase.ItemsSource = null;//to refresh
            plase.ItemsSource = Enum.GetValues(typeof(Plases));
        }
       
        private void Food_Click(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
             ezer = Fruit.SimpleSearch(B.Content.ToString());
            IDictionary<string, double> Ezernutrient = ezer.KandIron();
            sheilta.Text = "iron=" + Ezernutrient["Iron"] + "\t"
                + "vitamin K=" + Ezernutrient["phylloquinone"] + "\n";
        }
        private void Food_check_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check_sender = sender as CheckBox;
               ezer = Fruit.SimpleSearch(check_sender.FontFamily.ToString());
            
            if (check_sender.IsChecked == true)
                 chosenF.Add(ezer);
            if (check_sender.IsChecked == false)
                chosenF.Remove(ezer);
           
        }

        private void ApplayORremove_Click(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;

            if (B.Name == "ApplayButton")
            { 
                #region save the Meal
                use.AddMeal(This_meal,CurrentUsername);
                 
                foreach (Food f in chosenF)
                {
                    f.MealId = This_meal.id;
                    use.AddFoodToMeal(f,This_meal);
                 }
                MessageBox.Show("Meall added!", "succses");
                sheilta.Text = "the last meal giving you: " + use.SumOfVitaminkInMeal(This_meal) + " microgram vitamin-K";
                #endregion
            }

            Start();


        }

    }
}
