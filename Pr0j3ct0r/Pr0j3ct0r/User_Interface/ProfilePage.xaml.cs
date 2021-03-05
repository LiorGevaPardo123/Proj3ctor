using Pr0j3ct0r.Memory;
using Pr0j3ct0r.View_Model;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Window
    {
        public ProfilePageVM vm;
        public IContactsBL contactBL;
        public ProfilePage()
        {
            InitializeComponent();
            contactBL = new ContactsBL();
            vm = new ProfilePageVM();
            this.DataContext = vm;
        }

        private void Edit1Btn_Click(object sender, RoutedEventArgs e)
        {
            save1Btn.Visibility = Visibility.Visible;
            txt1.IsEnabled = true;
            edit1Btn.Visibility = Visibility.Collapsed;
        }

        private void Save1Btn_Click(object sender, RoutedEventArgs e)
        {
            save1Btn.Visibility = Visibility.Collapsed;
            txt1.IsEnabled = false;
            edit1Btn.Visibility = Visibility.Visible;
            //contactBL.UpdatePlayerInfo(Cache.Instance.username, "Email", txt1.Text);
        }

        private void Edit2Btn_Click(object sender, RoutedEventArgs e)
        {
            save2Btn.Visibility = Visibility.Visible;
            txt2.IsEnabled = true;
            edit2Btn.Visibility = Visibility.Collapsed;
        }

        private void Save2Btn_Click(object sender, RoutedEventArgs e)
        {
            save2Btn.Visibility = Visibility.Collapsed;
            txt2.IsEnabled = false;
            edit2Btn.Visibility = Visibility.Visible;           
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txt1.IsEnabled = false;
            txt2.IsEnabled = false;            
        }       
    }
}
