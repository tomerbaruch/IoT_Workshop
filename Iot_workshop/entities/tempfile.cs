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
            // Uri baseURI = new Uri("http://iot-pub.azurewebsites.net");

            //using (var client = new HttpClient())
            //{
            //    try
            //    {
            //        var res2 = await client.GetStringAsync("http://iot-pub.azurewebsites.net/Account/Login");

            //    }
            //    catch (Exception ex)
            //    {
            //        var x2 = 1;
            //    }
            //    var create22 = true;
            //    return;
            //}

            using (var client = new HttpClient())
            {
                List<Measurement> list = new List<Measurement>();
                Measurement m = new Measurement();
                m.Device_id = 1;
                m.time = DateTime.Now;
                m.type = Sensor_type.Sound;
                m.value = 99;

                Measurement m2 = new Measurement();
                m2.Device_id = 1;
                m2.time = DateTime.Now;
                m2.type = Sensor_type.Sound;
                m2.value = 98;

                list.Add(m);
                list.Add(m2);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("m", json)
                });

                try
                {
                    var res = await client.PostAsync("http://localhost:44967/Measurements/Add", content);
                }
                catch (Exception ex)
                {
                    var x = 1;
                }
                var create2 = true;
                return;




                //List<Measurement> d;
                //try { 
                //    d = JsonConvert.DeserializeObject<List<Measurement>>(res);
                //}
                //catch (Exception ex)
                //{
                //    var x = 1;
                //}
                //var create2 = true;
                //return;
            }
        }
    }
}
