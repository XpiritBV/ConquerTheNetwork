using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VSLiveSampleService.Data;

namespace VSLiveSampleService.Controllers
{
    public class ScheduleController : ApiController
    {
        // GET: api/schedule
        public IEnumerable<Schedule> Get()
        {
            return MockDatabase.Schedules;
        }

        // GET: api/schedule/{id}
        public Schedule Get(string id)
        {
            if (MockDatabase.Cities.Any(c => c.Id == id))
            {
                var items = MockDatabase.Schedules.Where(s => s.CityId == id);
                return items.FirstOrDefault();
            }

            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent($"No city with ID = {id}"),
                ReasonPhrase = "City ID Not Found"
            };
            throw new HttpResponseException(resp);
        }
    }
}
