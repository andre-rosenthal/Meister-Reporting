using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for ReadReportResponse
    /// </summary>
    public partial class ReadReportResponse
    {
        public ReadReportResponse()
        {
            Report = new ThisReport();
        }
        [JsonProperty("report")]
        public ThisReport Report { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }

    public partial class ThisReport
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("keepReport")]
        public bool KeepReport { get; set; }

        [JsonProperty("fromHub")]
        public bool FromHub { get; set; }

        [JsonProperty("notFound")]
        public bool NotFound { get; set; }

        [JsonProperty("edm")]
        public string Edm { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public partial class ReadReportResponse
    {
        public static List<ReadReportResponse> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<ReadReportResponse>>(json, Converter.Settings);
        }
    }

    public static partial class Serialize
    {
        public static string ToJson(this List<ReadReportResponse> self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }


}
