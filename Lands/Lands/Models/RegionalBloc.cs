using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Lands.Models
{
    public class RegionalBloc
    {
        [JsonProperty(PropertyName = "acronym")]
        public string Acronym { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "otherAcronyms")]
        public List<object> OtherAcronyms { get; set; }

        [JsonProperty(PropertyName = "otherNames")]
        public List<object> OtherNames { get; set; }
    }
}
