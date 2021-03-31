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
    /// Interaction logic for MissionProfilePage.xaml
    /// </summary>
    public partial class MissionProfilePage : Window
    {
        MissionVM viewModel;
        IMissionsBL missionsBL;
        public MissionProfilePage(MissionVM vm, bool editable)
        {
            InitializeComponent();
            if (editable)
            {
                this.desTxt.IsEnabled = true;
                deleteMissionBtn.Visibility = Visibility.Visible;
            }
            else
            {
                this.desTxt.IsEnabled = false;
                deleteMissionBtn.Visibility = Visibility.Collapsed;
            }
            missionsBL = new MissionsBL();            
            this.DataContext = vm;           
            viewModel = vm;
        }

        private void plusBtnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.Progress < 100)
            {
                viewModel.Progress += 10;
            }            
        }

        private void minusBtnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.Progress > 0)
            {
                viewModel.Progress -= 10;
            }           
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Mission temp = new Mission(viewModel.Name, viewModel.Duration, viewModel.Description, viewModel.ProjectId, viewModel.Status, viewModel.Code ,viewModel.Progress);            
            missionsBL.UpdateMission(temp);
        }       

        private void deleteMissionBtnClick(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the mission?", "caption", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Mission m = new Mission();
                m.Id = viewModel.Code;
                missionsBL.DeleteMission(m);
                this.Close();
            }           
        }
    }
}
