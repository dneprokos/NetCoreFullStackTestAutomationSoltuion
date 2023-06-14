using Newtonsoft.Json;
using System;

namespace JsonPlaceholder.Api.Client.ApiModels
{
    public class PostApiModelV2 : PostApiModelV1
    {
        [JsonProperty("dateCreated")]
        public DateTime? DateCreated { get; set; }
    }
}
