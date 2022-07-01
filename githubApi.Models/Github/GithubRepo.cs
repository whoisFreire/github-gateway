using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace githubApi.Models.Github
{
    public class GithubRepo
    {
        public string Name { get; set; }

        [JsonProperty("html_url")]
        public string Url { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
