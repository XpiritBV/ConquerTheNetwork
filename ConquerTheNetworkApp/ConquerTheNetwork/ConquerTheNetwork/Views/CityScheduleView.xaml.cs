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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.GetSchedule();
        }

        public void Schedule_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
