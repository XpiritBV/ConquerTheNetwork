using ConquerTheNetwork.Data;
using ConquerTheNetwork.Views;

namespace ConquerTheNetwork.ViewModels
{
    class CityDetailViewModel: ViewModelBase
    {
		private CityDetailView _page;
		
        private City _city;

        private CityScheduleViewModel _schedule;

        public CityDetailViewModel(CityDetailView page, City city)
        {
			_page = page;
            _city = city;

            _schedule = new CityScheduleViewModel(city.Id, city.Name);
        }

        public string Id
        {
            get { return _city.Id; }
        }

        public string Name
        {
            get { return _city.Name; }
        }

        public string ImageUrl
        {
            get { return _city.ImageUrl; }
        }

		private Xamarin.Forms.Command _viewScheduleCommand;
		public Xamarin.Forms.Command ViewScheduleCommand {
			get {
				return _viewScheduleCommand ??
					(_viewScheduleCommand = new Xamarin.Forms.Command (async (o) => {
						if (_page != null)
						{
							await _page.Navigation.PushAsync (new CityScheduleView (_schedule));
						}
					}, (o) => true));
			}
		}
    }
}
