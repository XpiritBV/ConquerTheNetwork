using System.Collections.Generic;
using System.Threading.Tasks;
using ConquerTheNetwork.Data;
using Refit;
using System.Net;

namespace ConquerTheNetwork.Services
{
    public class ServiceClient
    {
        public static string ApiBaseAddress = "http://vslivesampleservice.azurewebsites.net";

        private IConferenceApi _client;

        public ServiceClient()
        {
            _client = RestService.For<IConferenceApi>(ApiBaseAddress);
        }

        public async Task<List<City>> GetCities()
        {
            return await _client.GetCities();
        }

        public async Task<Schedule> GetScheduleForCity(string id)
        {
            try
            {
                return await _client.GetScheduleForCity(id);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }
    }
}
