using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace MeisterReporting
{
    public partial class ReportParametersRequest
    {
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        [JsonProperty("variantName")]
        public string VariantName { get; set; }
    }
}