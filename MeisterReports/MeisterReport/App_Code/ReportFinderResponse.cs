using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeisterReporting
{
    public partial class ReportFinderResponse
    {
        [JsonProperty("finder")]
        public List<Finder> Finder { get; set; }
    }

    public partial class Finder
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reportType")]
        public string ReportType { get; set; }
    }
}