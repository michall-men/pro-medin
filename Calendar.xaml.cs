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
using DL.Model;
using BE;
using System.Data.Entity.Infrastructure;

namespace UI
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        UserCollection use = new UserCollection();
        string name = SpesificUser.ThisEntry.Entity.Name;
        DateTime chosen;
        DailyDosage DD;
        DbEntityEntry<DailyDosage> entry;
        public Calendar()
        {
            InitializeComponent();
 
            if (SpesificUser.ThisEntry!=null)
            MonthView.FirstDayOfWeek = SpesificUser.ThisEntry.Entity.TestDay;
        }

        private void MonthView_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
 
            mashov.Visibility = Visibility.Hidden;
            Tackit.IsChecked = false;
            chosen = DateTime.Parse(MonthView.SelectedDate.ToString()).Date;

            if (use.TestInDay(chosen, name) == null)
            {
                blood.Text = "לא נרשמו בדיקות";
            }
            else
            {
                double InrResult = use.TestInDay(chosen, name).Inr;
                blood.Text = InrResult.ToString();
                if (InrResult < SpesificUser.ThisEntry.Entity.StartOfRange) { mashov.BorderBrush = Brushes.Yellow; mashov.Visibility = Visibility.Visible; }
                if (InrResult > SpesificUser.ThisEntry.Entity.EndtOfRange) {mashov.BorderBrush = Brushes.Red; mashov.Visibility = Visibility.Visible; }
            }
            try
            {
                agent.Text = use.LastTestfromDay(chosen, name).Dosage.ToString();
            }
            catch(Exception ex) { agent.Text = "לא מוגדר מינון"; }
            Iron.Text = use.DailyIron(chosen, name).ToString();
            KVitamin.Text = use.DailyK(chosen, name).ToString();
           
            if (use.FaindDailyDosageInDay(chosen, name) != null)
            {
                entry = use.Entry(use.FaindDailyDosageInDay(chosen, name));
                DD = entry.Entity;
            }
            else { DD = new DailyDosage(chosen, name); }
            ThatDayDosage.DataContext = DD;
            Tackit.DataContext = DD;
        }
        
        private void UpdateDailyDosage_Click(object sender, RoutedEventArgs e)
        {
                double newDosage = 0;
                double.TryParse(ThatDayDosage.Text, out newDosage);
          
            use.UpdateOrAddDailyDosage(DD, newDosage, Tackit.IsChecked);
        
        }
 
    }
}
