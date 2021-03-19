using LiveCharts;
using LiveCharts.Wpf;
using Pr0j3ct0r.Memory;
using Pr0j3ct0r.View_Model;
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
    /// Interaction logic for CriticalPathForAProjectPage.xaml
    /// </summary>
    public partial class CriticalPathForAProjectPage : Window
    {
        IProjectsBL projectsBL;
        public CriticalPathForAProjectPage()
        {
            InitializeComponent();
            projectsBL = new ProjectsBL();
            CriticalPathVM vm;
            vm = new CriticalPathVM(new List<int>(projectsBL.GetPersonalManagerProjects(Cache.Instance.username).Select(p => p.Code)));


            SeriesCollection = new SeriesCollection();
            ChartValues<double> DesirableValues = new ChartValues<double>();
            ChartValues<double> CommonValues = new ChartValues<double>();

            foreach (var item in vm.Data)
            {
                CommonValues.Add(item.Common);
                DesirableValues.Add(item.Desirable);
            }
            SeriesCollection.Add(

                new RowSeries
                {
                    Title = "Desirable",
                    Values = DesirableValues
                }
            ) ;

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new RowSeries
            {
                Title = "Common",
                Values = CommonValues
            });          
           
            // Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Labels = vm.Data.Select(item => item.Name).ToArray();
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
