using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
    public static partial class Serialize
    {
        public static string ToJson<T>(this T self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Converter.Settings);
        }
        public static T FromJson<T>(dynamic d)
        {
            return JsonConvert.DeserializeObject<T>(d, Converter.Settings);
        }
    }
    public partial class Variant
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
    public partial class Parameter
    {
        public enum KindTypes
        {
            Selection = 'S',
            Parameter = 'P',
            Global = 'G'
        }
        [JsonProperty("selName")]
        public string SelName { get; set; }

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
    }
    public partial class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("message")]
        public string MessageMessage { get; set; }

        [JsonProperty("logNo")]
        public string LogNo { get; set; }

        [JsonProperty("logMsgNo")]
        public long LogMsgNo { get; set; }

        [JsonProperty("messageV1")]
        public string MessageV1 { get; set; }

        [JsonProperty("messageV2")]
        public string MessageV2 { get; set; }

        [JsonProperty("messageV3")]
        public string MessageV3 { get; set; }

        [JsonProperty("messageV4")]
        public string MessageV4 { get; set; }

        [JsonProperty("parameter")]
        public string Parameter { get; set; }

        [JsonProperty("row")]
        public long Row { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }
    }
}