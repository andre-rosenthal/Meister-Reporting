using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for ReadReportRequest
    /// </summary>
    public class ReadReportRequest
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }
        [JsonProperty("keepReport")]
        public bool KeepReport { get; set; }
    }
}
