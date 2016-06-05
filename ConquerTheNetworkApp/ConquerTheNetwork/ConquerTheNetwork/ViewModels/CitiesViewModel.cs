using System.Collections.Generic;
using System.Threading.Tasks;
using ConquerTheNetwork.Services;
using Xamarin.Forms;
using ConquerTheNetwork.Data;
using Akavache;
using System;

namespace ConquerTheNetwork.ViewModels
{
    class CitiesViewModel: ViewModelBase
    {
        private Command refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return refreshCommand ??
                    (refreshCommand = new Command(() => ExecuteRefreshCommand(), () => !IsLoading));
            }
        }

        private void ExecuteRefreshCommand()
        {
            IsLoading = true;
            GetCities(force: true);
            IsLoading = false;
        }

        public void GetCities(bool force = false)
        {
            var cache = BlobCache.LocalMachine;
            var cachedCities = cache.GetAndFetchLatest("cities", GetRemoteCitiesAsync,
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return force || elapsed > new TimeSpan(hours: 0, minutes: 30, seconds: 0);
                })
                .Subscribe((cities) =>
                {
                    Cities = cities;
                });
        }

        private async Task<List<City>> GetRemoteCitiesAsync()
        {
            var client = new ServiceClient();
            var cities = await client.GetCities();
            return cities;
        }

        private IEnumerable<City> _cities;
        public IEnumerable<City> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }
    }
}
