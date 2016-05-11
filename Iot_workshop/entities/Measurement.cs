using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot_workshop.entities
{
    public class Measurement
    {
        public int Id { get; set; }

        public int Device_id { get; set; }

        public Sensor_type type { get; set; }

        public int value { get; set; }

        public DateTime time { get; set; }
    }
}
