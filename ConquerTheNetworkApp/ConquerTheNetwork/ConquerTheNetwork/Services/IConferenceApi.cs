using ConquerTheNetwork.Data;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConquerTheNetwork.Services
{
    [Headers("Accent: application/json")]
    public interface IConferenceApi
    {
        [Get("/api/cities")]
        Task<List<City>> GetCities();

        [Get("/api/schedule/{id}")]
        Task<Schedule> GetScheduleForCity(string id);
    }
}
