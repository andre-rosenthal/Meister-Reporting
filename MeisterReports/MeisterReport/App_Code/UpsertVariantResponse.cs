using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for UpsertVariantResponse
    /// </summary>
    public class UpsertVariantResponse
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}