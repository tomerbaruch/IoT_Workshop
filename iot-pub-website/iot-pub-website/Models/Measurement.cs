using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iot_pub_website.Models
{
    public class Measurement
    {
        public int Id { get; set; }

        public int Device_id { get; set; }

        public Sensor_type type { get; set; }

        public int value { get; set; }

        public DateTime time { get; set; }

        public Measurement(int device_id, int id)
        {
            this.Device_id = device_id;
        }

        public Measurement()
        {

        }
    }


}