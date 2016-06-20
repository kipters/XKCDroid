using System;
using Newtonsoft.Json;

namespace XKCDroid.Model
{
    public class XkcdJson
    {
        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("num")]
        public int Number { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("news")]
        public string News { get; set; }

        [JsonProperty("safe_title")]
        public string SafeTitle { get; set; }

        [JsonProperty("transcript")]
        public string Transcript { get; set; }

        [JsonProperty("alt")]
        public string AlternativeText { get; set; }

        [JsonProperty("img")]
        public string ImageUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }
    }
}