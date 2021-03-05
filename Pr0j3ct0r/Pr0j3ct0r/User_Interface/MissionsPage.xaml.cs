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
    /// Interaction logic for MissionsPage.xaml
    /// </summary>
    public partial class MissionsPage : Window
    {
        bool flag = false;
        public MissionsByStatusVM missionsByStatusVM;
        public MissionsInteractionsVM missionsInteractionsVM;
        public MissionsPage(ProjectVM curProject)
        {
            InitializeComponent();

            this.ProjectNameLbl.Text = curProject.Name;

            this.MissionsByStatusGrid.Visibility = Visibility.Visible;
            this.MissionsInteractionsGrid.Visibility = Visibility.Collapsed;

            missionsByStatusVM = new MissionsByStatusVM(curProject);
            missionsInteractionsVM = new MissionsInteractionsVM(curProject);
            this.MissionsByStatusGrid.DataContext = missionsByStatusVM;
            this.MissionsInteractionsGrid.DataContext = missionsInteractionsVM;
        }

        private void UpdateUserMsg()
        {
            if (this.missionsByStatusVM.InProcessMissionsList.Count == 0)
            {
                this.InProcessMsg.Visibility = Visibility.Visible;
                this.InProcessSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.InProcessMsg.Visibility = Visibility.Collapsed;
                this.InProcessSV.Visibility = Visibility.Visible;
            }

            if (this.missionsByStatusVM.PendingMissionsList.Count == 0)
            {
                this.PendingMsg.Visibility = Visibility.Visible;
                this.PendingSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.PendingMsg.Visibility = Visibility.Collapsed;
                this.PendingSV.Visibility = Visibility.Visible;
            }

            if (this.missionsByStatusVM.DoneMissionsList.Count == 0)
            {
                this.DoneMsg.Visibility = Visibility.Visible;
                this.DoneSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.DoneMsg.Visibility = Visibility.Collapsed;
                this.DoneSV.Visibility = Visibility.Visible;
            }
        }

        private void InProcessMissionsGridLoaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.InProcessMissionsList.ItemsSource);
            UpdateUserMsg();
        }

        private void DoneMissionsGridLoaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.DoneMissionsList.ItemsSource);
            UpdateUserMsg();
        }

        private void PendingMissionsGridLoaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.PendingMissionsList.ItemsSource);
            UpdateUserMsg();
        }

        private void AddMissionBtnClick(object sender, RoutedEventArgs e)
        {
            AddMissionPage mpp = new AddMissionPage();
            this.Visibility = Visibility.Collapsed;
            mpp.ShowDialog();
            missionsByStatusVM.Init();
            UpdateUserMsg();
            this.Visibility = Visibility.Visible;
        }          

        private void OpenMissionBtnClick(object sender, RoutedEventArgs e)
        {
            MissionVM selected = (MissionVM)(((Button)sender).DataContext);
            MissionProfilePage mpp = new MissionProfilePage(selected, Cache.Instance.currentProject.Manager.UserName == Cache.Instance.username);
            this.Visibility = Visibility.Collapsed;
            mpp.ShowDialog();            
            missionsByStatusVM.Init();
            UpdateUserMsg();
            this.Visibility = Visibility.Visible;
        }

        private void PageNextBtnClick(object sender, RoutedEventArgs e)
        {
            if (!flag)
            {
                this.MissionsByStatusGrid.Visibility = Visibility.Collapsed;
                this.MissionsInteractionsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                this.MissionsByStatusGrid.Visibility = Visibility.Visible;
                this.MissionsInteractionsGrid.Visibility = Visibility.Collapsed;
            }     
            flag = !flag;
        }

        private void AddInteractionBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
