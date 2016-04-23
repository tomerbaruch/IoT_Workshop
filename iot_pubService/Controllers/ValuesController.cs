using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using iot_pubService.Models;
using iot_pubService.DataObjects;

namespace iot_pubService.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            ITraceWriter traceWriter = this.Configuration.Services.GetTraceWriter();

            string host = settings.HostName ?? "localhost";
            string greeting = "Hello from " + host;
            
            traceWriter.Info(greeting);
            return greeting;
        }

        // POST api/values
        public string Post()
        {
            return "Hello World!";
        }

        [Route("tomer/{name}")]
        [HttpGet]
        public Device tomer(string name)
        {
            iot_pubContext a = new iot_pubContext();
            //TodoItem b = new TodoItem();
            //b.Text = "xxxxx";
            //b.Complete = true;
            //b.Deleted = true;
            //b.Id = "a";
            //b.Version = new byte[4];
            //a.TodoItems.Add(b);

            Device d = new Device();
            d.Id = "1";
            d.Name = name;
            d.Location = "home";
            a.Devices.Add(d);
            a.SaveChangesAsync();
            return d;
        }
    }
}
