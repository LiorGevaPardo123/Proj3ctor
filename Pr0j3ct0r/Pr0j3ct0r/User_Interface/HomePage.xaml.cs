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
using System.Windows.Threading;

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePageVM vm;
        public HomePage()
        {            
            InitializeComponent();
            vm = new HomePageVM();
            this.DataContext = vm;           
        }

        private void VideoBtnClick(object sender, RoutedEventArgs e)
        {
            VideoPlayer page = new VideoPlayer();
            page.Show();
        }
    }
}
