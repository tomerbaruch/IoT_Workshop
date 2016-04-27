using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Iot_workshop.entities
{
    class tempfile
    {
        public static async void b()
        {
            tomer();
            var x = 2;
        }
        public static async void tomer()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetStringAsync("http://iot-pub.azurewebsites.net/api/values/tomer?ZUMO-API-VERSION=2.0.0");
                Device d;
                try { 
                    d = JsonConvert.DeserializeObject<Device>(res);
                }
                catch (Exception ex)
                {
                    var x = 1;
                }
                var create2 = true;
                return;
            }
        }
    }
}
