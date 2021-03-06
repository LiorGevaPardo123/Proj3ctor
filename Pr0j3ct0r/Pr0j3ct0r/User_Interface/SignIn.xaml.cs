using Pr0j3ct0r.Memory;
using Pr0j3ct0r.User_Interface;
using Pr0j3ct0r.View_Model;
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

namespace Pr0j3ct0r
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignIn : Window
    {        
        private LogInVM loginVM;
        public SignIn()
        {
            loginVM = new LogInVM();
            InitializeComponent();            
            this.DataContext = loginVM;
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            loginVM.Password = this.passBox.Password;
            if (loginVM.AccessPermission())
            {
                Cache.Instance.username = loginVM.Username;
                HomePage page = new HomePage();
                var myWindow = Window.GetWindow(this);

                myWindow.Close();
                page.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid details", "LogIn Form", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            Register page = new Register();            
            var myWindow = Window.GetWindow(this);
                        
            myWindow.Close();
            page.Show();
        }

        private void ShowPassClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(passBox.Password);
        }
    }
}
