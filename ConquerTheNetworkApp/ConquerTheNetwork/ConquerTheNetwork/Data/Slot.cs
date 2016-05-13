using System;
using Newtonsoft.Json;

namespace ConquerTheNetwork.Data
{
    public class Slot
    {
        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public string Title { get; set; }

        [JsonIgnore]
        public string DayFormatted
        {
            get { return StartTime.Date.ToString("D"); }
        }

        [JsonIgnore]
        public string StartTimeFormatted
        {
            get { return StartTime.ToString("HH:mm"); }
        }

        [JsonIgnore]
        public string EndTimeFormatted
        {
            get { return StartTime.Add(Duration).ToString("HH:mm"); }
        }
    }
}
