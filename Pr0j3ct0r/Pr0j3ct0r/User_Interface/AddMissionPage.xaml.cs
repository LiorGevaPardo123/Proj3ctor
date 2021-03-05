using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddMissionPage.xaml
    /// </summary>
    public partial class AddMissionPage : Window
    {
        IMissionsBL missionsBL;
        public AddMissionPage()
        {
            InitializeComponent();
            missionsBL = new MissionsBL();
        }

        private void AddMissionBtnClick(object sender, RoutedEventArgs e)
        {
            Mission m = new Mission();
            m.Name = nameTxt.Text;
            m.ProjectId = Cache.Instance.currentProject.Code;
            m.StatusId = 2;
            m.Description = StringFromRichTextBox(desTxt);
            m.Duration = int.Parse(numberTextBox.Text);            
            missionsBL.CreateMission(m);
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

        private void NumberTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {            
            if (string.IsNullOrEmpty(numberTextBox.Text))
            {
                numberTextBox.Text = "1";
            }
            else if (numberTextBox.Text[0]=='0')
            {
                numberTextBox.Text = "1";
            }
            else
            {
                string input = numberTextBox.Text;
                string temp = "";
                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsDigit(input[i]))
                    {
                        temp += input[i];
                    }
                }
                numberTextBox.Text = temp;
            }
        }

        private void plusBtnClick(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(numberTextBox.Text);
            count++;
            numberTextBox.Text = count.ToString();
        }

        private void minusBtnClick(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(numberTextBox.Text);
            count--;
            numberTextBox.Text = count.ToString();
        }
    }
}
