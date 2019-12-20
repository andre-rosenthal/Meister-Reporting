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
    /// Summary description for MyReportsResponse
    /// </summary>

    public partial class MyReportsResponse
    {
        [JsonProperty("myReports")]
        public List<MyReport> MyReports { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }

    public partial class MyReport
    {
        [JsonProperty("pky")]
        public string Pky { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("dateStamp")]
        public string DateStamp { get; set; }

        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("report")]
        public Report Report { get; set; }
    }

    public partial class Report
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }

        [JsonProperty("variant")]
        public string Variant { get; set; }

        [JsonProperty("withMetadata")]
        public bool WithMetadata { get; set; }

        [JsonProperty("columnsNamed")]
        public bool ColumnsNamed { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class MyReportsResponse
    {
        public static List<MyReportsResponse> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<MyReportsResponse>>(json, Converter.Settings);
        }
    }

    public static partial class Serialize
    {
        public static string ToJson(this List<MyReportsResponse> self)
        {
            return JsonConvert.SerializeObject(self, MeisterReporting.Converter.Settings);
        }
    }
}