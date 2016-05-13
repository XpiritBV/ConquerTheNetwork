using ConquerTheNetwork.Data;
using ConquerTheNetwork.ViewModels;
using Xamarin.Forms;

namespace ConquerTheNetwork.Views
{
    public partial class CitiesView : ContentPage
    {
        private CitiesViewModel ViewModel
        {
            get
            {
                return BindingContext as CitiesViewModel;
            }
            set { BindingContext = value; }
        }

        public CitiesView()
        {
            InitializeComponent();

            ViewModel = new CitiesViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.GetCities();
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var city = e.SelectedItem as City;

            await Navigation.PushAsync(new CityDetailView(city));
        }
    }
}
