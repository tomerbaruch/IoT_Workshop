using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iot_pub_website.Models;

namespace iot_pub_website.Controllers
{
    public class MeasurementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Measurements
        public ActionResult Index()
        {
            return View(db.Measurements.ToList());
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
        [Route("Add/{type}")]
        [HttpGet]
        public HttpStatusCode Add(int type)
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

        [Route("OverTheLimitByDate")]
        [HttpGet]
        public ActionResult OverTheLimitByDate(int id)
        {
            DateTime from = DateTime.Now.AddDays(-5);
            DateTime to = DateTime.Now;
            //int id = 1;

            List<Measurement> a = db.Measurements.SqlQuery("Select * from Measurements Where device_id={0} and time > {1} and time < {2} and type={3} and value>{4}", id, from, to, Sensor_type.CO2, Constants.CO2_LIMIT).ToList();
            return new JsonResult() { Data = a, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        
    }
}
