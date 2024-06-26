﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CosmosExampleAPI.Models
{
    public class Elements
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "category")]
        public string? Category { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }
        [JsonProperty(PropertyName = "isComplete")]
        public bool IsComplete { get; set; }
    }
}
