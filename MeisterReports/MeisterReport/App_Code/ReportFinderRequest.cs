using Newtonsoft.Json;
namespace MeisterReporting
{
    /// <summary>
    /// Summary description for ReportFinderRequest
    /// </summary>
    public partial class ReportFinderRequest
    {
        [JsonProperty("criteria")]
        public string Criteria { get; set; }
    }
}