using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web.Http.Cors;

namespace Api
{
  [RoutePrefix("api/character")]
  public class CharacterController : ApiController
  {
    private string _address = "https://rickandmortyapi.com/api/character/";

    [Route("{name}/characters")]
    [HttpGet]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public async Task<IHttpActionResult> GetCharacters(string name)
    {
      try
      {
        var characters = await getCharacters(name, null);
        var response = Request.CreateResponse(HttpStatusCode.OK, characters);
        return ResponseMessage(response);
      }
      catch (Exception ex)
      {
        var response = Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        return ResponseMessage(response);
      }
    }

    [Route("{name}/characters/{page}")]
    [HttpGet]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public async Task<IHttpActionResult> GetCharactersByPage(string name, int page)
    {
      try
      {
        var characters = await getCharacters(name, page);
        var response = Request.CreateResponse(HttpStatusCode.OK, characters);
        return ResponseMessage(response);
      }
      catch (Exception ex)
      {
        var response = Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        return ResponseMessage(response);
      }
    }

    private async Task<CharactersResponse> getCharacters(string nameQuery, int? pageQuery)
    {
      var client = new HttpClient();
      var uri = new Uri(_address + "?name=" + nameQuery + (pageQuery.HasValue ? "&page=" + pageQuery.Value : string.Empty));
      HttpResponseMessage response = await client.GetAsync(uri);
      response.EnsureSuccessStatusCode();
      var result = await response.Content.ReadAsStringAsync();
      return createResponse(result, pageQuery ?? 1);
    }

    private CharactersResponse createResponse(string content, int page)
    {
      var deserializedResponse = JsonConvert.DeserializeObject<JObject>(content);
      var pageCount = (int)deserializedResponse.GetValue("info")["pages"];
      var characters = deserializedResponse.Last.Cast<JArray>().Children().Select(j => new Character()
      {
        Id = (int)j["id"],
        Name = (string)j["name"],
        Gender = (string)j["gender"],
        Status = (string)j["status"],
        Species = (string)j["species"],
        Origin = (string)((JObject)j["origin"]).GetValue("name"),
        Location = (string)((JObject)j["location"]).GetValue("name")
      }).ToList();

      return new CharactersResponse()
      {
        Characters = characters,
        CurrentPage = page,
        NextPage = (pageCount > page) ? (page + 1) : (int?)null
      };
    }
  }
}
