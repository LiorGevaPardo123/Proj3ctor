using Pr0j3ct0r.Memory;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for AddInteractionsPage.xaml
    /// </summary>
    public partial class AddInteractionsPage : Window
    {
        ICriticalPath criticalPath;
        int num = 0;
        public AddInteractionsPage()
        {
            InitializeComponent();
            criticalPath = new CriticalPath();
            num = criticalPath.CalcLongestPath(Cache.Instance.currentProject.Code);
        }
    }
}
