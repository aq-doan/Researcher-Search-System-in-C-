using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AssigmentSpecification.Control;
using AssigmentSpecification.Entity;

namespace AssigmentSpecification.View
{
    public partial class PublicationView : UserControl
    {
        //callback function when publictaion is selected
        public delegate void OnPublicationSelectDelegate(Publication selectedPublication);
        public event OnPublicationSelectDelegate OnPublicationSelectListeners = null;

        private int _startYear = 0;
        private int _endYear = DateTime.Now.Year;

        public PublicationView()
        {
            InitializeComponent();
        }

        public void UpdatePublicationList(Researcher researcher)
        {
            //assign publication list of selected researcher to item source to listbox for display
            publication_list.ItemsSource = PublicationController.LoadPublicationList(researcher);
        }

        private void OnYearFromFilterEnter(object sender, KeyEventArgs e)
        {
            //get the start year filter input
            if (!Int32.TryParse((sender as TextBox).Text, out _startYear))
            {
                _startYear = 0;
            }

            //assign filtered publication list to listbox as item source
            publication_list.ItemsSource = PublicationController.LoadByYear(_startYear, _endYear);
        }

        private void OnYearToFilterEnter(object sender, KeyEventArgs e)
        {
            //get the end year filter input
            if (!Int32.TryParse((sender as TextBox).Text, out _endYear))
            {
                _endYear = DateTime.Now.Year;
            }

            //assign filtered publication list to listbox as item source
            publication_list.ItemsSource = PublicationController.LoadByYear(_startYear, _endYear);
        }

        private void OnInvertButtonPress(object sender, RoutedEventArgs e)
        {
            //assign inverted publication list to listbox as item source
            publication_list.ItemsSource = PublicationController.InvertPublicationList();
        }

        private void OnPublicationSelect(object sender, SelectionChangedEventArgs e)
        {
            //selected publication with basic details
            Publication selectedPublication = (sender as ListBox).SelectedItem as Publication;

            if (selectedPublication != null)
            {
                //load full detail for the selected publication
                PublicationController.LoadPubDetails(selectedPublication.DOI);

                //send out selected pubilication to all listener functions
                OnPublicationSelectListeners(PublicationController.pub);
            }
        }
    }
}
