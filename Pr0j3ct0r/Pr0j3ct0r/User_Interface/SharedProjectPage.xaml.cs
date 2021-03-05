using Pr0j3ct0r.Memory;
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

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for SharedProjectPage.xaml
    /// </summary>
    public partial class SharedProjectPage : Window
    {
        public ProjectVM vm;
        public SharedProjectPage(ProjectVM pvm)
        {
            InitializeComponent();
            this.vm = pvm;
            Cache.Instance.currentProject = pvm;
            Cache.Instance.isManagerOfCurrentProject = false;
            this.DataContext = vm;
            if (vm.EndDate == "01/01/0001")//default null date
            {
                this.msgEndDateLbl.Visibility = Visibility.Visible;
                this.msgEndDateLbl.Content = "The project is still running";
                this.endDateLbl.Visibility = Visibility.Collapsed;
                this.endDateVal.Visibility = Visibility.Collapsed;
            }
        }
    }
}
