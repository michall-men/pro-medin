using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity.Infrastructure;
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
    /// Interaction logic for Dosage.xaml
    /// </summary>
    public partial class ApplayBlood : UserControl
    {
        UserCollection use = new UserCollection();
 
        Test test { get; set; }
        public ApplayBlood()
        {
           
            InitializeComponent();
          
            Start();
        }
        public void Start()
        {
            test = new Test();
             test.UserName = SpesificUser.ThisEntry.Entity.Name;
             intAnsure.Text = "";
            dosagInput.Text = "";
            weighInput.Text = "";
         //  this.TestGrid.DataContext = test;
        }
        
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                test.Inr = double.Parse(intAnsure.Text);
                test.Dosage = double.Parse(dosagInput.Text);
               
            }
            catch(Exception ex)
            {
               
                MessageBox.Show("invalid input: letters insteed numbers");
                Start();
                return;
            }

            try
            {
                test.Weight = double.Parse(weighInput.Text);
            }
            catch (Exception ex)
            {
                test.Weight = 0;
            }
                string AddMessage =use.AddTest(test,SpesificUser.ThisEntry.Entity.Name);
            MessageBox.Show(AddMessage);
            if (test.Inr> SpesificUser.ThisEntry.Entity.EndtOfRange|| test.Inr < SpesificUser.ThisEntry.Entity.StartOfRange)
                ImSoTierd.Content = "tour last resuls is out of the range";
            Start();
             
        }

    }
}
