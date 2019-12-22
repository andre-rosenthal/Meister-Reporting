using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeisterReporting
{
    public partial class ReportRunRequest
    {
        public ReportRunRequest()
        {
            Report = new Report();
        }
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("report")]
        public Report Report { get; set; }
    }

}