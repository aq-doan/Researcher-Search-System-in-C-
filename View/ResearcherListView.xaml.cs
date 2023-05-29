using AssigmentSpecification.Control;
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
    /// Interaction logic for ResearcherListView.xaml
    /// </summary>
    public partial class ResearcherListView : UserControl
    {

        public ResearcherListView()
        {
            InitializeComponent();
            dgDataNameLevel.ItemsSource = ResearcherController.LoadResearcher();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && cboLevel.Text.ToString() == "All")
            {
                var lstResearchName = ResearcherController.FilterByName(txtName.Text);
                if (lstResearchName.Count() == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cần tìm !", "Thong báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    dgDataNameLevel.ItemsSource = lstResearchName;
                    cboLevel.Text = "All";
                    txtName.Text = "";
                }
            }
            else if (cboLevel.Text.ToString() != "All" && txtName.Text == "")
            {
                EmploymentLevel levelTemp = new EmploymentLevel();
                if (cboLevel.Text == EmploymentLevel.Student.ToString()) { levelTemp = EmploymentLevel.Student; }
                if (cboLevel.Text == EmploymentLevel.A.ToString()) { levelTemp = EmploymentLevel.A; }
                if (cboLevel.Text == EmploymentLevel.B.ToString()) { levelTemp = EmploymentLevel.B; }
                if (cboLevel.Text == EmploymentLevel.C.ToString()) { levelTemp = EmploymentLevel.C; }
                if (cboLevel.Text == EmploymentLevel.D.ToString()) { levelTemp = EmploymentLevel.D; }
                if (cboLevel.Text == EmploymentLevel.E.ToString()) { levelTemp = EmploymentLevel.E; }
                if (cboLevel.Text == EmploymentLevel.Other.ToString()) { levelTemp = EmploymentLevel.Other; }
                var lstResearchName = ResearcherController.FilterBy(levelTemp);
                if (lstResearchName.Count() == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cần tìm !", "Thong báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    dgDataNameLevel.ItemsSource = lstResearchName;
                    cboLevel.Text = "All";
                    txtName.Text = "";
                }
            }
            else
            {
                EmploymentLevel levelTemp = new EmploymentLevel();
                if (cboLevel.Text == EmploymentLevel.Student.ToString()) { levelTemp = EmploymentLevel.Student; }
                if (cboLevel.Text == EmploymentLevel.A.ToString()) { levelTemp = EmploymentLevel.A; }
                if (cboLevel.Text == EmploymentLevel.B.ToString()) { levelTemp = EmploymentLevel.B; }
                if (cboLevel.Text == EmploymentLevel.C.ToString()) { levelTemp = EmploymentLevel.C; }
                if (cboLevel.Text == EmploymentLevel.D.ToString()) { levelTemp = EmploymentLevel.D; }
                if (cboLevel.Text == EmploymentLevel.E.ToString()) { levelTemp = EmploymentLevel.E; }
                if (cboLevel.Text == EmploymentLevel.Other.ToString()) { levelTemp = EmploymentLevel.Other; }
                dgDataNameLevel.ItemsSource = ResearcherController.FilterResearcherByNameAndLevel(levelTemp, txtName.Text);
                cboLevel.Text = "All";
                txtName.Text = "";
            }
        }

        private void dgDataNameLevel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        private void dgDataNameLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as DataGrid).SelectedItem as Researcher;
            ResearcherController.LoadDetailR(item.Id);
            var dataDetails = ResearcherController.CurrentResearcher;

            MainWindow w = this.DataContext as MainWindow;
           w.researcherDetailsView.Visibility = Visibility.Visible;
           w.performanceDetaiView.Visibility = Visibility.Visible;
            var view = w.researcherDetailsView;
            view.loadDetails(dataDetails);
        }
    }
}
