using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iot_pub_website.Models;
using System.Collections;
using Newtonsoft.Json;

namespace iot_pub_website.Controllers
{
    [AllowAnonymous]
    public class MeasurementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Measurements
        public ActionResult Index()
        {
            return View(db.Measurements.ToList());
        }

        [HttpPost]
        public ActionResult labels(int dataType, string idTime, int deviceId)
        {
            DateTime from = DateTime.Now.AddYears(-5);
            DateTime to = DateTime.Now;
            switch (idTime)
            {
                case "oneDay":
                    from = DateTime.Now.AddDays(-1);
                    to = DateTime.Now;
                    break;
                case "oneWeek":
                    from = DateTime.Now.AddDays(-7);
                    to = DateTime.Now;
                    break;
                case "oneMonth":
                    from = DateTime.Now.AddMonths(-1);
                    to = DateTime.Now;
                    break;
                case "threeMonths":
                    from = DateTime.Now.AddMonths(-3);
                    to = DateTime.Now;
                    break;
                case "oneYear":
                    from = DateTime.Now.AddYears(-1);
                    to = DateTime.Now;
                    break;
                default:
                    break;
            }
            List<String> result = db.Measurements.ToList().Where(z => z.time > from && z.time < to).Where(y => (int)y.type == dataType).Where(r => r.Device_id == deviceId).Select(x => x.time.Date.ToString("d")).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult data(int dataType, String idTime, int deviceId)
        {
            DateTime from = DateTime.Now.AddYears(-5);
            DateTime to = DateTime.Now;
            switch (idTime){
                case "oneDay":
                    from = DateTime.Now.AddDays(-1);
                    to = DateTime.Now;
                    break;
                case "oneWeek":
                    from = DateTime.Now.AddDays(-7);
                    to = DateTime.Now;
                    break;
                case "oneMonth":
                    from = DateTime.Now.AddMonths(-1);
                    to = DateTime.Now;
                    break;
                case "threeMonths":
                    from = DateTime.Now.AddMonths(-3);
                    to = DateTime.Now;
                    break;
                case "oneYear":
                    from = DateTime.Now.AddYears(-1);
                    to = DateTime.Now;
                    break;
                default:
                    break;
            }
            return Json(db.Measurements.ToList().Where(z => z.time > from && z.time < to).Where(y => (int)y.type == dataType).Where(r => r.Device_id == deviceId).Select(x => x.value).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FullDetails(int deviceId)
        {
            List<Measurement> m = db.Measurements.ToList().Where(z => z.Device_id == deviceId).ToList();
            if (m.Count == 0)
            {
                m.Add(new Measurement(deviceId, -1));
            }
            return View(m);
        }

        // GET: Measurements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement measurement = db.Measurements.Find(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // GET: Measurements/Create
        public ActionResult Create()
        {
            return View();
        }

        [Route("deleteRecords")]
        [HttpGet]
        public ActionResult DeleteRecords(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Measurement> a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0}", id).ToList();
            db.Measurements.RemoveRange(a);
            db.SaveChanges();
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("Add")]
        [HttpPost]
        public HttpStatusCode Add(String m)
        {
            List<Measurement> toAdd = JsonConvert.DeserializeObject<List<Measurement>>(m);
            db.Measurements.AddRange(toAdd);
            db.SaveChanges();
            return HttpStatusCode.OK;
        }

        [Route("GetDeviceMeasurements/{id}")]
        [HttpGet]
        public List<Measurement> GetAllDevice(int id)
        {
            List<Measurement> a = db.Measurements.SqlQuery("Select * from Measurements Where id={0}", id).ToList();
            return a;
        }

        public ActionResult OneDay(int id)
        {
            DateTime from = DateTime.Now.AddDays(-5);
            DateTime to = DateTime.Now;
            return View("FullDetails", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult OneWeek(int id)
        {
            DateTime from = DateTime.Now.AddDays(-7);
            DateTime to = DateTime.Now;
            return View("FullDetails", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult OneMonth(int id)
        {
            DateTime from = DateTime.Now.AddMonths(-1);
            DateTime to = DateTime.Now;
            return View("FullDetails", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult ThreeMonths(int id)
        {
            DateTime from = DateTime.Now.AddMonths(-3);
            DateTime to = DateTime.Now;
            return View("FullDetails", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult OneYear(int id)
        {
            DateTime from = DateTime.Now.AddYears(-1);
            DateTime to = DateTime.Now;
            return View("FullDetails", GetDeviceMeasurementsByDate(id, from, to));
        }

        [Route("GetDeviceMeasurementsByDate")]
        [HttpGet]
        public List<Measurement> GetDeviceMeasurementsByDate(int id, DateTime start, DateTime end)
        {
            List<Measurement> a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0} and time > {1} and time < {2}", id, start, end).ToList();
            return a;
        }

        [AllowAnonymous]
        [Route("OverTheLimitByDate")]
        [HttpGet]
        public ActionResult OverTheLimitByDate(int id)
        {
            DateTime from = DateTime.Now.AddYears(-5);
            DateTime to = DateTime.Now;
            //int id = 1;

            List<Measurement> a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0} and time > {1} and time < {2} and type={3} and value>{4}", id, from, to, Sensor_type.CO2, Constants.CO2_LIMIT).ToList();
            return new JsonResult() { Data = a, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        
    }
}
