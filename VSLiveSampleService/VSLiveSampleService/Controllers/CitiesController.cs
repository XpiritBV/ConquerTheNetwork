using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VSLiveSampleService.Data;

namespace VSLiveSampleService.Controllers
{
    public class CitiesController : ApiController
    {
        // GET: api/cities
        public IEnumerable<City> Get()
        {
            return MockDatabase.Cities;
        }

        // GET api/cities/5
        public City Get(string id)
        {
            if (MockDatabase.Cities.Any(c => c.Id == id))
            {
                return MockDatabase.Cities.FirstOrDefault(c => c.Id == id);
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
