using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
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
            var second = DateTime.UtcNow.Second;

            // Fail half the time in case of city 3
            if (second % 2 == 0 && id == "3")
            {
                var fakeError = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Fake error message"),
                    ReasonPhrase = "Something went wrong here..."
                };
                throw new HttpResponseException(fakeError);

            }

            if (MockDatabase.Cities.Any(c => c.Id == id))
            {
                Thread.Sleep(2000); // fake a slow method
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
