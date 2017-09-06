using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HumanArc.Compliance.Web.Helpers
{
    public class JsonNetResult : ActionResult
    {
        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings();
            SerializerSettings.Converters.Add(new StringEnumConverter());
            SerializerSettings.Converters.Add(new DecimalConverter());
            SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        }

        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            // Don't let IIS override any error responses when using this class:
            response.TrySkipIisCustomErrors = true;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
                ? ContentType
                : "application/json";

            if (StatusCode.HasValue)
            {
                response.StatusCode = (int)StatusCode.Value;
            }

            if (!string.IsNullOrEmpty(StatusDescription))
            {
                response.StatusDescription = StatusDescription;
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }
    }
}
