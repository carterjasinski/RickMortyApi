using Newtonsoft.Json;
using System.Collections.Generic;

namespace Api.Models
{
  public class CharactersResponse
  {
    [JsonProperty(PropertyName = "characters")]
    public List<Character> Characters { get; set; }

    [JsonProperty(PropertyName = "currentPage")]
    public int CurrentPage { get; set; }

    [JsonProperty(PropertyName = "nextPage")]
    public int? NextPage { get; set; }
  }
}