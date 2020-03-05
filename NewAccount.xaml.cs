using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
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
using DL;

namespace UI
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    /// 
   
    public partial class NewAccount : UserControl
    {

        UserCollection usc = new UserCollection();
        BE.User newuser;
        public NewAccount()
        {
            newuser = new BE.User();
            InitializeComponent();
            to.Text = "to";
            from.Text = "from";
            testDay.ItemsSource = Enum.GetValues(typeof(DayOfWeek));
            statusf.ItemsSource = Enum.GetValues(typeof(FamilyStatus));
            gender.ItemsSource =  Enum.GetValues(typeof(Gender));
            for (int i = 1900; i <= DateTime.Now.Year; i++)
                      BirthYear.Items.Add(i);
          /*  for (double i = 1; i <= 5; i += 0.5) אני עדיין לא שוללת את האפשרות של גלילה.. זה נראה הרבה יותר טוב
            {
                from.Items.Add(i);
                to.Items.Add(i);
            }*/

                //   BE.User newuser = new BE.User();
                this.DataContext = newuser;
            
        }

        private void ApplayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                newuser.Heigh = double.Parse(hiegBox.Text);
                newuser.StartOfRange = double.Parse(from.Text);
                newuser.EndtOfRange = double.Parse(to.Text);
            }
            catch(Exception ex) {
              
                MessageBox.Show("Errorr!! letter used  insteed numbers, or null filed");
                return;
            }
            if (newuser.gender == Gender.אשה) newuser.UpdateImage(@"\icons\Fuser.png");
            else newuser.UpdateImage(@"\icons\Muser.png");
            
            MessageBox.Show(usc.AddUserToDB(newuser).ToString());
            SpesificUser.ThisEntry = usc.SearchAndGetEntity(newuser.Name, newuser.Password);
        }
    }
}
