using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PushUI.Models.Application.UI
{
    public class JsonDotNetResult : JsonResult
    {
        private readonly object obj;
        private readonly Formatting formatting;
        private readonly JsonSerializerSettings settings;
        public JsonDotNetResult(object obj)
        {
            this.obj = obj;
            formatting = Formatting.None;
            settings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() };
        }
        public JsonDotNetResult(object obj, Formatting formatting, JsonSerializerSettings settings) : this(obj)
        {
            this.formatting = formatting;
            this.settings = settings;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.AddHeader("content-type", "application/json");
            context.HttpContext.Response.Write(JsonConvert.SerializeObject(obj, formatting, settings));
        }
    }

    public class JsonDotNetCamelCaseResult : JsonDotNetResult
    {
        public JsonDotNetCamelCaseResult(object obj)
            : base(obj, Formatting.None, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
        {
            
        }
    }
}