using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      config.EnableCors();

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApiGet",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { action = "Get", id = RouteParameter.Optional },
          constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
      );
    }
  }
}
