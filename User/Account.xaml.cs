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
using BE;
using DL.Model;
using DL.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : UserControl
    { 
        VMTests LastResult = new VMTests(SpesificUser.ThisEntry.Entity.Name);
        UserCollection use = new UserCollection();
        BE.User setuser = SpesificUser.ThisEntry.Entity;

        public Account()
        {
            InitializeComponent();
           
            this.DataContext = setuser;
            detailse.DataContext = SpesificUser.ThisEntry.Entity.ToString();
            testDay.ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            statusf.ItemsSource = Enum.GetValues(typeof(FamilyStatus));
            LastW.Content = "last wheigh: " + use.TheLastWeigh(SpesificUser.ThisEntry.Entity.Name);
            List1.DataContext = LastResult;
         }
     
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            use.UpdateUser(SpesificUser.ThisEntry.Entity,setuser);
        }
    }
}
