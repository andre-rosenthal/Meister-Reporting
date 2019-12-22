using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    public partial class UpsertVariantRequest
    {
        public UpsertVariantRequest()
        {
            Parameters = new List<Parameter>();
        }
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        [JsonProperty("variantName")]
        public string VariantName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }
}
