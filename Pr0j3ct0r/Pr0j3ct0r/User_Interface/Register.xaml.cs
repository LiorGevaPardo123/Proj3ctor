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
using System.Windows.Shapes;

namespace Pr0j3ct0r
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public RegisterVM vm { get; set; }
        public Register()
        {
            InitializeComponent();
            vm = new RegisterVM();
            this.DataContext = vm;
            error.Visibility = Visibility.Collapsed;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            SignIn page = new SignIn();
            var myWindow = Window.GetWindow(this);

            myWindow.Close();
            page.Show();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!vm.ConfirmMatchPassword())
            {
                error.Visibility = Visibility.Visible;
                this.vm.IsAlertOnConPassword = true;
                this.vm.UserAlertOnConfirmPass = "The password and the confirm password aren't equal";
                return;
            }
            else
            {
                error.Visibility = Visibility.Collapsed;
                this.vm.IsAlertOnConPassword = false;
            }
            vm.sendDataForNewOPlayer();
            MessageBox.Show("Your account has been created","New User",MessageBoxButton.OK, MessageBoxImage.Information);
            SignIn page = new SignIn();
            var myWindow = Window.GetWindow(this);            
            myWindow.Close();
            page.Show();
        }
       
        private void FNTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.FNTxt.Text.All(IsCharLetterOrWhiteSpace))
            {
                char[] temp = this.FNTxt.Text.Where(c => IsCharLetterOrWhiteSpace(c)).ToArray();
                this.FNTxt.Text = new string(temp);
                MessageBox.Show("Name must contains only letters");
            }            
        }
        private bool IsCharLetterOrWhiteSpace(char c)
        {
            return char.IsLetter(c) || char.IsWhiteSpace(c);            
        }
    }
}
