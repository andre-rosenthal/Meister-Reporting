using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    public partial class SchedulerResponse
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        [JsonProperty("schedules")]
        public List<Schedule> Schedules { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}