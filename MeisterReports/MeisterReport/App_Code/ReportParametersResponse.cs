using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeisterReporting
{

    /// <summary>
    /// Summary description for ReportParametersResponse
    /// </summary>
    public partial class ReportParametersResponse
    {
        [JsonProperty("reportMetadata")]
        public ReportMetadata ReportMetadata { get; set; }
    }
    public partial class ReportMetadata
    {
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        [JsonProperty("reportParameters")]
        public List<ParameterOut> ReportParameters { get; set; }

        [JsonProperty("variants")]
        public List<VariantOut> Variants { get; set; }
    }

    public partial class ParameterOut
    {
        [JsonProperty("selName")]
        public string SelName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("option")]
        public string Option { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dtyp")]
        public string Dtyp { get; set; }

        [JsonProperty("dbField")]
        public string DbField { get; set; }

        [JsonProperty("noSelset")]
        public bool NoSelset { get; set; }

        [JsonProperty("obligatory")]
        public bool Obligatory { get; set; }
    }

    public partial class VariantOut
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isProtected")]
        public bool IsProtected { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

}