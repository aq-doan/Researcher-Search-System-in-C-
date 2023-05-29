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
using System.Windows.Shapes;

namespace AssigmentSpecification.View
{
    /// <summary>
    /// Interaction logic for MainPublicationsAndCount.xaml
    /// </summary>
    public partial class MainPublicationsAndCount : Window
    {
        public MainPublicationsAndCount()
        {
            InitializeComponent();
        }

        public MainPublicationsAndCount(List<Publication> list)
        {
            InitializeComponent();
            var a = ResearcherController.CumulativeCount();
            publicationsDetailsView.DataContext = list;

            cumulativeCountView.DataContext = a;

        }
    }
}
