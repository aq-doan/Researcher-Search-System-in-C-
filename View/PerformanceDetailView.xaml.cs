using AssigmentSpecification.Entity;
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

namespace AssigmentSpecification.View
{
    /// <summary>
    /// Interaction logic for PerformanceDetailView.xaml
    /// </summary>
    public partial class PerformanceDetailView : UserControl
    {
        public PerformanceDetailView()
        {
            InitializeComponent();
        }

        public void loadData(List<PerformanceReport> listPre)
        {
            //dgDataBelowExpectations.Columns.Clear();
            dgDataBelowExpectations.ItemsSource = "";
            dgDataBelowExpectations.ItemsSource = listPre;
        }

        private void dgDataBelowExpectations_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           // dgDataBelowExpectations.Columns.Clear();
        }
    }
}
