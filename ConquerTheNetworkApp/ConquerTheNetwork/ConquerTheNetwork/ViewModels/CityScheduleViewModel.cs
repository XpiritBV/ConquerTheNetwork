using System.Linq;
using System.Threading.Tasks;
using ConquerTheNetwork.Data;
using ConquerTheNetwork.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Plugin.Connectivity;

namespace ConquerTheNetwork.ViewModels
{
    public class CityScheduleViewModel: ViewModelBase
    {
        private string _cityId;

		public CityScheduleViewModel(string cityId, string cityName)
        {
            _cityId = cityId;
            _cityName = cityName;
        }
			
		private ObservableCollection<Grouping<string, Slot>> _groupedSlots;
		public ObservableCollection<Grouping<string, Slot>> GroupedSlots
		{
			get {
				return _groupedSlots;
			}
			set {
				_groupedSlots = value;
				OnPropertyChanged ();
			}
		}

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
        }

        private Command refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return refreshCommand ??
                    (refreshCommand = new Command(async () => await ExecuteRefreshCommand(), () => !IsLoading));
            }
        }
			
        private async Task ExecuteRefreshCommand()
        {
            IsLoading = true;
            await GetSchedule();
            IsLoading = false;
        }

        public async Task GetSchedule()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                // Optionally, you can show a warning saying 
                // the client is not connected
                return;
            }

            var client = new ServiceClient();
			var schedule = await client.GetScheduleForCity (_cityId);

			if (schedule != null)
			{
			    await Task.Run(() => from slot in schedule.Slots
			        orderby slot.StartTime
			        group slot by slot.DayFormatted
			        into slotGroup
			        select new Grouping<string, Slot>(slotGroup.Key, slotGroup)).ContinueWith(r =>
			    {
			        GroupedSlots = new ObservableCollection<Grouping<string, Slot>>(r.Result);
			    });
			}
        }
    }
}
