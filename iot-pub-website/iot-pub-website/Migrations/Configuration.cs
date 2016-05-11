namespace iot_pub_website.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using iot_pub_website.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<iot_pub_website.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(iot_pub_website.Models.ApplicationDbContext context)
        {
            //context.Measurements.AddOrUpdate(
            //   new Measurement
            //   {
            //       Device_id = 1,
            //       type = Sensor_type.CO2,
            //       value = 30,                  
            //   },
            //    new Measurement
            //    {
            //        Device_id = 1,
            //        type = Sensor_type.CO2,
            //        value = 70,
            //    },
            //    new Measurement
            //    {
            //        Device_id = 2,
            //        type = Sensor_type.CO2,
            //        value = 10,
            //    },
            //    new Measurement
            //    {
            //        Device_id = 2,
            //        type = Sensor_type.Sound,
            //        value = 30,
            //    },
            //    new Measurement
            //    {
            //        Device_id = 3,
            //        type = Sensor_type.Alcohol,
            //        value = 80,
            //    }
            //    );
        }
    }
}
