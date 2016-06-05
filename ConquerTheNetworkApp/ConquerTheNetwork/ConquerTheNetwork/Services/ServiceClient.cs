using System.Collections.Generic;
using System.Threading.Tasks;
using ConquerTheNetwork.Data;
using Refit;
using System.Net;
using ModernHttpClient;
using System.Net.Http;
using System;
using Polly;
using System.Diagnostics;

namespace ConquerTheNetwork.Services
{
    public class ServiceClient
    {
        public static string ApiBaseAddress = "http://vslivesampleservice.azurewebsites.net";

        private IConferenceApi _client;

        public ServiceClient()
        {
            var client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };
            _client = RestService.For<IConferenceApi>(client);

        }

        public async Task<List<City>> GetCities()
        {
            return await Policy
                .Handle<ApiException>(ex => ex.StatusCode != HttpStatusCode.NotFound)
                .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromMinutes(1))
                .ExecuteAsync(async () =>
                {
                    Debug.WriteLine("Trying cities service call...");
                    return await _client.GetCities();
                });
        }

        public async Task<Schedule> GetScheduleForCity(string id)
        {
            try
            {
                return await Policy
                        .Handle<ApiException>(ex => ex.StatusCode != HttpStatusCode.NotFound)
                        .WaitAndRetryAsync
                        (
                            retryCount: 3,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => {
                            Debug.WriteLine("Trying schedule service call...");
                            return await _client.GetScheduleForCity(id);
                        });
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
