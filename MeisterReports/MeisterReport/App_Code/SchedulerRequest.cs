using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    public partial class SchedulerRequest
    {
        [JsonProperty("option")]
        public string Option { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("schedule")]
        public Schedule Schedule { get; set; }
    }

    public partial class Schedule
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("agendaType")]
        public string AgendaType { get; set; }

        [JsonProperty("dow")]
        public string Dow { get; set; }

        [JsonProperty("slot")]
        public string Slot { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        [JsonProperty("variant")]
        public string Variant { get; set; }

        [JsonProperty("withMetadata")]
        public bool WithMetadata { get; set; }

        [JsonProperty("columnsNamed")]
        public bool ColumnsNamed { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }
}