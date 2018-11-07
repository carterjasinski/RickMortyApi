using Newtonsoft.Json;

namespace Api.Models
{
  public class Character
  {
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "gender")]
    public string Gender { get; set; }

    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "species")]
    public string Species { get; set; }

    [JsonProperty(PropertyName = "origin")]
    public string Origin { get; set; }

    [JsonProperty(PropertyName = "location")]
    public string Location { get; set; }
  }
}
