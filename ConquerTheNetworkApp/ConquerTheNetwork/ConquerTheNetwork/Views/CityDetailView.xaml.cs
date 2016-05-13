using ConquerTheNetwork.Data;
using ConquerTheNetwork.ViewModels;
using Xamarin.Forms;

namespace ConquerTheNetwork.Views
{
    public partial class CityDetailView : ContentPage
    {
        private CityDetailViewModel ViewModel
        {
            get
            {
                return BindingContext as CityDetailViewModel;
            }
            set { BindingContext = value; }
        }

        public CityDetailView()
        {
            InitializeComponent();
        }

        public CityDetailView(City city) : this()
        {
            ViewModel = new CityDetailViewModel(this, city);
        }
    }
}
