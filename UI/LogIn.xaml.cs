using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;

using DL.Model;
using BE;
using System.Data.Entity;

namespace UI
{
   
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
	{
        
        UserCollection usc = new UserCollection();
		public LogIn()
		{
			InitializeComponent();
            
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
 
            SpesificUser.ThisEntry = usc.SearchAndGetEntity(UserName.Text.ToString(), pwd.Password);
            if (SpesificUser.ThisEntry != null)
            {
                MessageBox.Show("you connected! ");
                
            }


        }
           
        
	}
}
