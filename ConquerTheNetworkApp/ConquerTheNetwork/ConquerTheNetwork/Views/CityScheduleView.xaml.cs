using ConquerTheNetwork.ViewModels;
using Xamarin.Forms;

namespace ConquerTheNetwork.Views
{
    public partial class CityScheduleView : ContentPage
    {
        private CityScheduleViewModel ViewModel
        {
            get
            {
                return BindingContext as CityScheduleViewModel;
            }
            set { BindingContext = value; }
        }

        public CityScheduleView()
        {
            InitializeComponent();
        }

        public CityScheduleView(CityScheduleViewModel viewModel) : this()
        {
            ViewModel = viewModel;
        }

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ServiceError")
            {
                if (ViewModel.ServiceError)
                {
                    await this.DisplayAlert("Error", "Something went wrong :-(", "OK");
                    ViewModel.ServiceError = false;
                }
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            await ViewModel.GetSchedule();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        public void Schedule_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
