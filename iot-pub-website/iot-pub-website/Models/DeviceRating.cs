using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iot_pub_website.Models
{
    public class DeviceRating
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int Rating_Co { get; set; }

        public int Rating_Sound { get; set; }

        public int Rating_Alcohol { get; set; }


        public DeviceRating(int Id,String Location, String Name, int Rating_Co, int Rating_Sound, int Rating_Alcohol)
        {
            this.Id = Id;
            this.Location = Location;
            this.Name = Name;
            this.Rating_Co = Rating_Co;
            this.Rating_Sound = Rating_Sound;
            this.Rating_Alcohol = Rating_Alcohol;
        }
    }
}