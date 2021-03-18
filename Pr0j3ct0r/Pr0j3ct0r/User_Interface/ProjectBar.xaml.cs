using Pr0j3ct0r.Memory;
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
    /// Interaction logic for ProjectBar.xaml
    /// </summary>
    public partial class ProjectBar : UserControl
    {
        public ProjectBar()
        {
            InitializeComponent();
        }

        private void backBtnClick(object sender, RoutedEventArgs e)
        {
            BackToProjects();
        }

        public void BackToProjects()
        {
            ProjectsPage page = new ProjectsPage();
            var myWindow = Window.GetWindow(this);
            myWindow.Visibility = Visibility.Collapsed;
            page.Show();
            myWindow.Close();
        }

        private void participantsBtnClick(object sender, RoutedEventArgs e)
        {
            ProjectParticipantsPage page = new ProjectParticipantsPage(Cache.Instance.currentProject);
            var myWindow = Window.GetWindow(this);
            myWindow.Visibility = Visibility.Collapsed;
            page.Show();
            myWindow.Close();
        }

        private void MissionsBtnClick(object sender, RoutedEventArgs e)
        {
            MissionsPage page = new MissionsPage(Cache.Instance.currentProject);
            var myWindow = Window.GetWindow(this);
            myWindow.Visibility = Visibility.Collapsed;
            page.Show();
            myWindow.Close();
        }

        private void ProfileBtnClick(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Visibility = Visibility.Collapsed;
            if (Cache.Instance.isManagerOfCurrentProject)
            {
                ManagedProjectPage page = new ManagedProjectPage(Cache.Instance.currentProject);                
                page.Show();               
            }
            else
            {
               SharedProjectPage page = new SharedProjectPage(Cache.Instance.currentProject);           
               page.Show();  
            }            
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
