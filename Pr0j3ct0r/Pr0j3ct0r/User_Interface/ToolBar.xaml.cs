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

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }             

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            SignIn page = new SignIn();           
            var myWindow = Window.GetWindow(this);
                        
            myWindow.Close();
            page.Show();
           
        }                   

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage page = new ProfilePage();
            var myWindow = Window.GetWindow(this);

            page.Show();
            myWindow.Close();
        }

        private void ContactsBtn_Click(object sender, RoutedEventArgs e)
        {
            ContactsPage page = new ContactsPage();
            var myWindow = Window.GetWindow(this);

            page.Show();
            myWindow.Close();
        }        

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            HomePage page = new HomePage();
            var myWindow = Window.GetWindow(this);

            page.Show();
            myWindow.Close();
        }

        private void projectBtnClick(object sender, RoutedEventArgs e)
        {
            ProjectsPage page = new ProjectsPage();
            var myWindow = Window.GetWindow(this);

            page.Show();
            myWindow.Close();
        }

        private void AnalysisBtnClick(object sender, RoutedEventArgs e)
        {
            CriticalPathForAProjectPage page = new CriticalPathForAProjectPage();
            var myWindow = Window.GetWindow(this);
            myWindow.Visibility = Visibility.Collapsed;
            page.Show();
            myWindow.Close();
        }
    }
}
