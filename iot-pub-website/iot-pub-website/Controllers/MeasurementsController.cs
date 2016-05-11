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
            //List<Measurement> l = new List<Measurement>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Measurement m = new Measurement();
            //    m.Device_id = 1;
            //    Random random = new Random();
            //    m.type = (Sensor_type) random.Next(1, 4);
            //    m.value = random.Next(0, 99);
            //    m.time = DateTime.Now;
            //    l.Add(m);
            //}
            //db.Measurements.AddRange(l);
            //db.SaveChanges();
            //ArrayList tomer = db.Measurements.ToList().Select(x => x.time.ToString()).ToArray();
            Mpage tomer = new Mpage();
            tomer.measurements = db.Measurements.ToList();
            tomer.labels = db.Measurements.ToList().Select(x => x.time.ToString()).ToArray();
            return View(db.Measurements.ToList());
        }

        [HttpPost]
        public ActionResult labels(int dataType, string idTime)
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
            return Json(db.Measurements.ToList().Where(z => z.time > from && z.time < to).Where(y => (int)y.type == dataType).Select(x => x.time.Date.ToString("d")).ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult data(int dataType, String idTime)
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
            return Json(db.Measurements.ToList().Where(z => z.time > from && z.time < to).Where(y => (int)y.type == dataType).Select(x => x.value).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FullDetails(int deviceId)
        {
            return View(db.Measurements.ToList().Where(z => z.Device_id == deviceId));
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

        // POST: Measurements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Device_id,type,value,time")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Measurements.Add(measurement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(measurement);
        }

        // GET: Measurements/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Measurements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Device_id,type,value,time")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(measurement);
        }

        // GET: Measurements/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Measurement measurement = db.Measurements.Find(id);
            db.Measurements.Remove(measurement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //my api
        [Route("Addddd")]
        [HttpGet]
        public HttpStatusCode Addddd(int type)
        {
            Measurement m = new Measurement();
            m.Device_id = 1;
            m.type = (Sensor_type)type;
            m.value = 75;
            m.time = DateTime.Now;
            db.Measurements.Add(m);
            db.SaveChangesAsync();
            return HttpStatusCode.OK;
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
            return View("Index", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult OneWeek(int id)
        {
            DateTime from = DateTime.Now.AddDays(-7);
            DateTime to = DateTime.Now;
            return View("Index", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult OneMonth(int id)
        {
            DateTime from = DateTime.Now.AddMonths(-1);
            DateTime to = DateTime.Now;
            return View("Index", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult ThreeMonths(int id)
        {
            DateTime from = DateTime.Now.AddMonths(-3);
            DateTime to = DateTime.Now;
            return View("Index", GetDeviceMeasurementsByDate(id, from, to));
        }

        public ActionResult OneYear(int id)
        {
            DateTime from = DateTime.Now.AddYears(-1);
            DateTime to = DateTime.Now;
            return View("Index", GetDeviceMeasurementsByDate(id, from, to));
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
