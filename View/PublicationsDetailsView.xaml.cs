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
    /// Interaction logic for PublicationsDetailsView.xaml
    /// </summary>
    public partial class PublicationsDetailsView : UserControl
    {
        public PublicationsDetailsView()
        {
            InitializeComponent();
        }

        private void dgDataPublications_Loaded(object sender, RoutedEventArgs e)
        {
            var list = this.DataContext as List<Publication>;
            dgDataPublications.Columns.Clear();
            dgDataPublications.ItemsSource = list;
        }
    }
}
