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

namespace UI
{
    /// <summary>
    /// Interaction logic for Exposite.xaml
    /// </summary>
    public partial class Exposite : UserControl
    {
        public Exposite()
        {
            InitializeComponent();
            Alpha.Content = Alpha.Content + "\n press to continue";
        }
        private void Alpha_Click(object sender, RoutedEventArgs e)
        {
            //Alpha.Focusable = false;// if its hidden-dont needed
          //  Alpha.IsEnabled = false;//jast dont giv me push, but shoun
            Alpha.Visibility = System.Windows.Visibility.Collapsed; //does not render the control and does not reserve the whitespace
            menue.Visibility = System.Windows.Visibility.Visible;
           
        }
 
 
    }
}
