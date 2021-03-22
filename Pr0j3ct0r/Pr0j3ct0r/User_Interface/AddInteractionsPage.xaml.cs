using Pr0j3ct0r.Memory;
using Pr0j3ct0r.View_Model;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
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
        AddInteractionVM vm;        
        IMissionsInteractionsBL missionsInteractionsBL;

        public AddInteractionsPage()
        {
            InitializeComponent();
            Next.IsEnabled = false;
            AddInteractionBtn.IsEnabled = false;
            missionsInteractionsBL = new MissionsInteractionsBL();            
            vm = new AddInteractionVM();
            this.DataContext = vm;
        }

        private void AddInteractionBtnClick(object sender, RoutedEventArgs e)
        {            
            missionsInteractionsBL.CreateInteraction(vm.CurruntMission.Code,vm.NextMission.Code);
            this.Close();
        }

        private void Currunt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Currunt.IsEnabled = false;
            Next.IsEnabled = true;
        }

        private void Next_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddInteractionBtn.IsEnabled = true;
        }
    }
}
