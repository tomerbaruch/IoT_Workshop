using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iot_pub_website.Models;
using System.Web.Helpers;
using System.Collections;

namespace iot_pub_website.Controllers
{
    public class DevicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Devices
        public ActionResult Index()
        {
            return View();
        }

        [Route("getMeasurementsByType")]
        [HttpGet]
        public ActionResult getMeasurementsByType(Sensor_type type)
        {
            List<Device> devices = db.Devices.ToList();
            DateTime startTime = DateTime.Now.AddYears(-1);
            DateTime endTime = DateTime.Now;
            Dictionary<int, int> map_co = new Dictionary<int, int>();
            Dictionary<int, int> map_sound = new Dictionary<int, int>();
            Dictionary<int, int> map_alcohol = new Dictionary<int, int>();
            List<Measurement> a;
            for (int i = 0; i < devices.Count; i++)
            {
                a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0} and time > {1} and time < {2} and type=1", devices[i].Id, startTime, endTime, type).ToList();
                double avg = a.Count > 0 ? a.Average(m => m.value) : 0;
                map_co.Add(devices[i].Id, Convert.ToInt32(avg));
                //map.Add(devices[i].Id, Constants.AVG_VALUE - Convert.ToInt32(avg));
            }

            for (int i = 0; i < devices.Count; i++)
            {
                a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0} and time > {1} and time < {2} and type=2", devices[i].Id, startTime, endTime, type).ToList();
                double avg = a.Count > 0 ? a.Average(m => m.value) : 0;
                map_sound.Add(devices[i].Id, Convert.ToInt32(avg));
                //map.Add(devices[i].Id, Constants.AVG_VALUE - Convert.ToInt32(avg));
            }

            for (int i = 0; i < devices.Count; i++)
            {
                a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0} and time > {1} and time < {2} and type=3", devices[i].Id, startTime, endTime, type).ToList();
                double avg = a.Count > 0 ? a.Average(m => m.value) : 0;
                map_alcohol.Add(devices[i].Id, Convert.ToInt32(avg));
                //map.Add(devices[i].Id, Constants.AVG_VALUE - Convert.ToInt32(avg));
            }

            List<DeviceRating> result = new List<DeviceRating>();
            for (int i = 0; i < devices.Count; i++)
            {
                DeviceRating dr = new DeviceRating(devices[i].Id, devices[i].Location, devices[i].Name, map_co[devices[i].Id], map_sound[devices[i].Id], map_alcohol[devices[i].Id]);
                result.Add(dr);
            }

            if (type == Sensor_type.Alcohol)
            {
                result.Sort((c, b) => c.Rating_Alcohol.CompareTo(b.Rating_Alcohol));
            }
            else if (type == Sensor_type.CO2)
            {
                result.Sort((c, b) => c.Rating_Co.CompareTo(b.Rating_Co));
            }
            else
            {
                result.Sort((c, b) => c.Rating_Sound.CompareTo(b.Rating_Sound));
            }


            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    
        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        public ActionResult device_details(int id)
        {
            Device device = db.Devices.Find(id);
            return View(device);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            return View();
        }

        //// GET: Devices/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Device device = db.Devices.Find(id);
        //    if (device == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(device);
        //}

        //// POST: Devices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Device device = db.Devices.Find(id);
        //    db.Devices.Remove(device);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("GetDevices")]
        [HttpGet]
        public ActionResult GetDevices()
        {
            List<Device> devices = db.Devices.ToList();
            return new JsonResult() { Data = devices, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
