using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace NetFrameworkWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //DIFFERENT FORMATTING

            //Indent JSON data
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //Camel case instead of Pascal case
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            ////Return ONLY json
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            ////Return ONLY xml
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //Returns json in all browsers
            //The problem with this approach is that Content - Type header of the response is set to text / html which is misleading.
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            ////Calling Custom Json Formatter : Problem in above approach is overcome
            //config.Formatters.Add(new CustomJsonFormatter());

            //CROSS DOMAIN/ORIGIN RESOURCE SHARING BY jsonp / cors 
            //Support for JSONp
            //var jsonpformatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpformatter);

            //Enable CORS
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");//set for the complete webapi
            //config.EnableCors(cors);
            //OR
            config.EnableCors();//This will only enable CORS, but defined at controller/method level

            //To REDIRECT HTTP requests to HTTPS
            //config.Filters.Add(new RequireHttpsAttribute());
            
            //To ADD Basic Authentication
            config.Filters.Add(new BasicAuthenticationAttribute());
        }

        public class CustomJsonFormatter : JsonMediaTypeFormatter
        {
            public CustomJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
    }
}
