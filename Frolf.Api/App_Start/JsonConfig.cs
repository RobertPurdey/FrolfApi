using System.Web.Http;

namespace Frolf.Api.App_Start
{
    public static class JsonConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var jsonConfig = config.Formatters.JsonFormatter.SerializerSettings;

            jsonConfig.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            jsonConfig.NullValueHandling    = Newtonsoft.Json.NullValueHandling.Ignore;
        }
    }
}