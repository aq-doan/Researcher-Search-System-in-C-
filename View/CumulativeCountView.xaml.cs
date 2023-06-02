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
    /// Interaction logic for CumulativeCountView.xaml
    /// </summary>
    public partial class CumulativeCountView : UserControl
    {
        public CumulativeCountView()
        {
            InitializeComponent();
        }

        public class Data
        {
            public string Count { get; set; }
            public string Year { get; set; }
        }

        private void dgDataPublicationsCount_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext as List<(int, int)>;
            List<Data> data = new List<Data>();
            foreach (var item in a)
            {
                Data d = new Data()
                {
                    Count = item.Item2.ToString(),
                    Year = item.Item1.ToString(),
                };
                data.Add(d);    
            }
            dgDataPublicationsCount.Columns.Clear();

            dgDataPublicationsCount.ItemsSource = data;

        }
    }
}
