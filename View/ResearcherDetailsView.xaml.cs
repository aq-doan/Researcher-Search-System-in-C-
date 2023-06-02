using AssigmentSpecification.Entity;
using AssigmentSpecification.Control;
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
    /// Interaction logic for ResearcherDetailsView.xaml
    /// </summary>
    public partial class ResearcherDetailsView : UserControl
    {
        private List<Publication> list;
        private List<PerformanceReport> listPer = new List<PerformanceReport>();

        public ResearcherDetailsView()
        {
            InitializeComponent();
        }


        public void ChangeText()
        {
            txtName1.Text = "abc";
        }

        public void loadDetails(Researcher r)
        {
            txtName1.Text = r.GivenName + " " + r.FamilyName;
            txtTitle.Text = r.Title;
            txtCampus.Text = r.Campus;
            txtEmail.Text = r.Email;
            txtSchool.Text = r.School;
            if (r.Positions != null)
            {
                txtCurrentJob.Text = r.CurrentJobTitle;
                txtCommencedwithcurrentposition.Text = r.GetEarliestJob().start.ToString("yyyy-MM-dd");
            }
            else
            {
                txtCurrentJob.Text = "";
                txtCommencedwithcurrentposition.Text = "";
            }
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(r.Photo, UriKind.Absolute);
            bitmap.EndInit();
            imgAvatar.Source = bitmap;
            var AgePub = r.PublicationList.Sum(x => x.AgePub);
            double totalage = r.PublicationCount * 100;
            double percentage = Math.Round(AgePub / totalage, 2) * 100;
            txtCommencedwithinstititution.Text = r.start_job.ToString("yyyy-MM-dd");
            txtTenure.Text = r.Tenure().ToString();
            txtPublications.Text = r.PublicationCount.ToString();
            txtQ1Percentage.Text = percentage.ToString() + "%";
            //txtName2.Text = r.GivenName + " " + r.FamilyName;
            if (r is Staff)
            {
                var staff = (r as Staff);
                txtYearAverage.Text = staff.ThreeYearAverage().ToString() + " Publications in 3 Years";
                txtFundingReceived.Text = "$" + staff.FundingReceived.ToString();
                txtPublicationPerformance.Text = staff.PerformanceByPublication().ToString() + " Publications per Year";
                txtFundingPerformance.Text = "$" + staff.PerformanceByFunding().ToString() + " Fundings per Year";
                PerformanceReport pr = new PerformanceReport()
                {
                    Name = r.GivenName + " " + r.FamilyName,
                    Performance = staff.PerformanceByPublication().ToString() + "%",
                    Mail = r.Email
                };
                listPer.Add(pr);
            }
            else
            {
                var listRe = ResearcherController.GetSupervisionList(r);
                if (listRe.Count > 0)
                {
                    var text = "";
                    foreach (var item in listRe)
                    {
                        text += (item as Student).SupervisorName + "\n";
                    }
                    text = text.Remove(text.Length - 1);
                    txtSupevision.Text = text;
                }
                else
                {
                    txtSupevision.Text = "";
                }
            }


            MainWindow w = this.DataContext as MainWindow;
            var view = w.performanceDetaiView;
            view.loadData(listPer);
            // Test giao diện mới
            list = r.PublicationList;
        }

        private void BtnPublications_Click(object sender, RoutedEventArgs e)
        {
            MainPublicationsAndCount mainpubliandcount = new MainPublicationsAndCount(list);

            mainpubliandcount.Show();
        }

        private void BtnCumulativeCount_Click(object sender, RoutedEventArgs e)
        {
            CumulaCountView mainpubliandcount = new CumulaCountView(list);

            mainpubliandcount.Show();
        }


        public List<Researcher> GetSelectedResearcherSupervieeList()
        {
            return (ResearcherController.CurrentResearcher as Staff).Supervisions;
        }

        private void OnSuperviseeButtonPress(object sender, RoutedEventArgs e)
        {
            //create and display supervisees window
            SupervisionView supervisionWindow = new SupervisionView();
            supervisionWindow.Show();
        }
    }
}
