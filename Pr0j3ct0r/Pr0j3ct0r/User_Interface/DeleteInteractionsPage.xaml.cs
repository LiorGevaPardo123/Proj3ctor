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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for DeleteInteractionsPage.xaml
    /// </summary>
    public partial class DeleteInteractionsPage : Window
    {
        IMissionsInteractionsBL missionsInteractionsBL;
        MissionsInteractionsVM vm;
        public DeleteInteractionsPage()
        {
            InitializeComponent();
            Cur.IsEnabled = true;
            Next.IsEnabled = false;
            missionsInteractionsBL = new MissionsInteractionsBL();
            vm = new MissionsInteractionsVM(Cache.Instance.currentProject);
            this.DataContext = vm;
        }

        private void DeleteInteractionBtnClick(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the interaction?", "caption", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                missionsInteractionsBL.DeleteInteraction(vm.SelectedMission.Code, vm.SelectedNextMission.Code);
                this.Close();
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cur.IsEnabled = false;
            Next.IsEnabled = true;
        }
    }
}
