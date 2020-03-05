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
using System.Threading;
using DL.Model;
namespace UI
{
     /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    //   UserCollection usc = new UserCollection();
        public bool LManue=false;
        Exposite ex = new Exposite();
        bool leftmanuintalize = false;
        UserCollection use = new UserCollection();
        public MainWindow()
        {
         //   usc.ClearAll();
            InitializeComponent();
           
            exchange.Navigate(ex);
            ex.sighnIn.Click += ((sender, e) => exchange.Navigate(new NewAccount()));
            ex.login.Click += ((sender, e) => exchange.Navigate(new LogIn()));

            login.Click += ((sender, e) => exchange.Navigate(new LogIn()));
            sign.Click += ((sender, e) => exchange.Navigate(new NewAccount()));
          
        }
       
        private void MenueInitialize()
        {
             
            FoodsChoice fc = new FoodsChoice();
             
            statistic st = new statistic();
           
            Account ac = new Account();
            ApplayBlood ab = new ApplayBlood(); 
            Calendar cdr = new Calendar();
            SearchFoodNutrients sn = new SearchFoodNutrients();

           LeftMenue.AddMeal.Click+=((sender,e)=>exchange.Navigate(fc));
           
            LeftMenue.Stat.Click += ((sender, e) => exchange.Navigate(st));
          
            LeftMenue.Account.Click+= ((sender, e) => exchange.Navigate(ac));
           LeftMenue.Dosage.Click += ((sender, e) => exchange.Navigate(ab));
           LeftMenue.Calen.Click += ((sender, e) => exchange.Navigate(cdr));
           LeftMenue.search.Click+= ((sender, e) => exchange.Navigate(sn));
            //hear soppose to be cpt past of that for all button  in the left manue

        }
         private void Login_Click(object sender, RoutedEventArgs e)
        {
            exchange.Navigate(new LogIn());
            LeftMenue.Visibility = Visibility.Hidden;
        }
        private void ClickHiddenConvter(object sender, RoutedEventArgs e)
        {

            if (LeftMenue.Visibility == System.Windows.Visibility.Hidden)
            {
                if (SpesificUser.ThisEntry != null)
                {
                    LeftMenue.IsEnabled = true;
                    if (!leftmanuintalize)
                    { MenueInitialize(); leftmanuintalize = true; }
                }
                LeftMenue.Visibility = System.Windows.Visibility.Visible;
            }
            else
                LeftMenue.Visibility = System.Windows.Visibility.Hidden;
           
        }
 
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            exchange.Navigate(ex);
            SpesificUser.ThisEntry = null;
            LeftMenue.IsEnabled = false;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (SpesificUser.ThisEntry != null)
            {
                exchange.Refresh();

                MenueInitialize();
              /*  var thread = new Thread(() => );
                thread.Start();
                thread.Join();*/
            }
        }

        private void showDbcontext()
        {
            string all = "";
            var u = use.DbUsers.ToArray();
            var m = use.Meals.ToArray();
            var t = use.Testes.ToArray();
            var f = use.FoodSet.ToArray();
            var d = use.DbDailyDosage.ToArray();
            for (int i = 0; i < u.Length; i++)
                all += u[i].ToString() + " ";
            all += "\n meals:";
            for (int j = 0; j < m.Length; j++)
                all += m[j].id + "-" + m[j].UserName + " ";
            all += "\n test:";
            for (int j = 0; j < t.Length; j++)
                all += t[j].id + "-" + t[j].UserName + " ";
            all += "\n test:";
            for (int j = 0; j < f.Length; j++)
                all += f[j].name + "-" + f[j].MealId + " ";
            all += "\n Daily:";
            for (int j = 0; j < d.Length; j++)
                all += d[j].DayDate + "-" + d[j].UserName + " ";
            MessageBox.Show(all);
        }
    }
}
