using System.Collections.Generic;

namespace ConquerTheNetwork.Data
{
    public class Schedule
    {
        public string CityId { get; set; }

        public IEnumerable<Slot> Slots { get; set; } 
    }
}
