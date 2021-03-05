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
    /// Interaction logic for ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : Window
    {
        public ProjectsVM vm;
        public ProjectsPage()
        {
            InitializeComponent();
            vm = new ProjectsVM();
            this.DataContext = vm;
        }
        private void UpdateUserMsg()
        {
            if (this.vm.PersonalManagerProjectsList.Count == 0)
            {
                this.myMsg.Visibility = Visibility.Visible;
                this.mySV.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.myMsg.Visibility = Visibility.Collapsed;
                this.mySV.Visibility = Visibility.Visible;
            }

            if (this.vm.PersonalParticipantProjectsList.Count == 0)
            {
                this.shareMsg.Visibility = Visibility.Visible;
                this.sharedSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.shareMsg.Visibility = Visibility.Collapsed;
                this.sharedSV.Visibility = Visibility.Visible;
            }            
        }
        private void Grid1_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.managerProjectsList.ItemsSource);
            UpdateUserMsg();
        }

        private void Grid2_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.participantsProjectsList.ItemsSource);
            UpdateUserMsg();
        }

        private void OpenMyProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b == null)
            {
                return;
            }
            StackPanel s = b.Content as StackPanel;
            if (s == null)
            {
                return;
            }
            ProjectVM pvm = s.DataContext as ProjectVM;
            if (pvm == null)
            {
                return;
            }
            ManagedProjectPage mpp = new ManagedProjectPage(pvm);            
            mpp.Show();
            this.Close();           
        }

        private void OpenSharedProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b == null)
            {
                return;
            }
            StackPanel s = b.Content as StackPanel;
            if (s == null)
            {
                return;
            }
            ProjectVM pvm = s.DataContext as ProjectVM;
            if (pvm == null)
            {
                return;
            }
            SharedProjectPage mpp = new SharedProjectPage(pvm);            
            mpp.Show();
            this.Close();            
        }

        private void AddProjectBtnClick(object sender, RoutedEventArgs e)
        {
            AddProjectPage mpp = new AddProjectPage();
            this.Visibility = Visibility.Collapsed;            
            mpp.ShowDialog();
            vm.Init();
            UpdateUserMsg();
            this.Visibility = Visibility.Visible;
        }
    }
}
