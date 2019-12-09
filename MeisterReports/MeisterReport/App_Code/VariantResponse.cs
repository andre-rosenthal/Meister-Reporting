using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for VariantResponse
    /// </summary>
    public class VariantResponse
    {
        public string report { get; set; }
        public List<Variant> variants { get; set; }
        public List<Message> messages { get; set; }
    }

    [JsonObject("variant")]
    public class Variant
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool isProtected { get; set; }
        [JsonProperty("parameters")]
        public List<Parameter> parameters { get; set; }
    }
}