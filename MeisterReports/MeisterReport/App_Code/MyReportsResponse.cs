using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for MyReportsResponse
    /// </summary>
   
    [JsonObject("myReports")]
    public class MyReportsResponse
{
        [JsonProperty("myReports")]
        public List<Myreport> myReports { get; set; }
        [JsonProperty("messages")]
        public List<Message> messages { get; set; }
    }

    [JsonObject("myReport")]
    public class Myreport
    {
        public string pky { get; set; }
        public string userName { get; set; }
        public string dateStamp { get; set; }
        public string timeStamp { get; set; }
        public Report report { get; set; }
    }

    [JsonObject("report")]
    public class Report
    {
        public string name { get; set; }
        public string description { get; set; }
        public string mode { get; set; }
        [JsonProperty("parameters")]
        public List<Parameter> parameters { get; set; }
        public string variant { get; set; }
        public bool withMetadata { get; set; }
        public bool columnsNamed { get; set; }
        public string status { get; set; }
    }
}