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
    /// Interaction logic for AddProjectPage.xaml
    /// </summary>
    public partial class AddProjectPage : Window
    {
        private IProjectsBL projectsBL;
        public AddProjectPage()
        {
            InitializeComponent();
            projectsBL = new ProjectsBL();
            startDateCalendar.SelectedDate = DateTime.Today;
        }

        private void AddProjectBtnClick(object sender, RoutedEventArgs e)
        {
            Project p = new Project();
            p.Description = StringFromRichTextBox(desTxt);
            p.Name = nameTxt.Text;
            p.Manager = Cache.Instance.username;
            p.StartDate = startDateCalendar.SelectedDate.Value;
            projectsBL.CreateNewProject(p);
            this.Close();
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
            // TextPointer to the start of content in the RichTextBox.
            rtb.Document.ContentStart,
            // TextPointer to the end of content in the RichTextBox.
            rtb.Document.ContentEnd);

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }

    }
}
