using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
  
    [JsonObject("parameter")]
    public class Parameter
    {
        public string selName { get; set; }
        public string kind { get; set; }
        public string sign { get; set; }
        public string option { get; set; }
        public string low { get; set; }
        public string high { get; set; }
    }
    [JsonObject("message")]
    public class Message
    {
        public string type { get; set; }
        public string id { get; set; }
        public int number { get; set; }
        public string message { get; set; }
        public string logNo { get; set; }
        public int logMsgNo { get; set; }
        public string messageV1 { get; set; }
        public string messageV2 { get; set; }
        public string messageV3 { get; set; }
        public string messageV4 { get; set; }
        public string parameter { get; set; }
        public int row { get; set; }
        public string field { get; set; }
        public string system { get; set; }
    }
}