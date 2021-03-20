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
    /// Interaction logic for ManagedProjectPage.xaml
    /// </summary>
    public partial class ManagedProjectPage : Window
    {
        public IProjectsBL projectBL;
        Project p = new Project();
        public ProjectVM vm { get; private set; }
        public ManagedProjectPage(ProjectVM pvm)
        {            
            InitializeComponent();           
            projectBL = new ProjectsBL();
            Cache.Instance.currentProject = pvm;
            Cache.Instance.isManagerOfCurrentProject = true;
            this.vm = pvm;
            this.DataContext = vm;
            if (vm.EndDate == "01/01/0001")//default null date
            {
                this.msgEndDateLbl.Visibility = Visibility.Visible;
                this.msgEndDateLbl.Content = "The project is still running";
                this.endDateLbl.Visibility = Visibility.Collapsed;
                this.endDateVal.Visibility = Visibility.Collapsed;
            }
            //שצריך לסדר שבהתחברות המשתמש יהיה במדוייק כמו המשתמש שיצרו
            if (pvm.Manager.UserName == Cache.Instance.username)
            {
                editProjectBtn.Visibility = Visibility.Visible;
            }
            else
            {
                editProjectBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void editProjectBtnClick(object sender, RoutedEventArgs e)
        {
            startDateVal.IsEnabled = true;
            desTxt.IsEnabled = true;
            editProjectBtn.Visibility = Visibility.Collapsed;
            saveProjectBtn.Visibility = Visibility.Visible;
        }

        private void saveProjectBtnClick(object sender, RoutedEventArgs e)
        {
            startDateVal.IsEnabled = false;
            desTxt.IsEnabled = false;
            editProjectBtn.Visibility = Visibility.Visible;
            saveProjectBtn.Visibility = Visibility.Collapsed;
            Project resullt = new Project(vm.Code, DateTime.Parse(startDateVal.Text.ToString()), desTxt.Text);
            projectBL.UpdateProject(resullt);
        }

        private void deleteProjectBtnClick(object sender, RoutedEventArgs e)
        {
            Project p = new Project();            
            p.Code = Cache.Instance.currentProject.Code;           
            projectBL.DeleteProject(p);
            toolbar.BackToProjects();        
        }

        private void ProjectDoneBtnClick(object sender, RoutedEventArgs e)
        {
            endDateLbl.Visibility = Visibility.Visible;
            endDateVal.Visibility = Visibility.Visible;
            this.msgEndDateLbl.Visibility = Visibility.Collapsed;
            endDateVal.Text = DateTime.Today.ToString();
            Project resullt = new Project(vm.Code, DateTime.Parse(endDateVal.Text.ToString()));
            projectBL.SetEndDate(resullt);
        }
    }
}
