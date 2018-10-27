using Hundo_P.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Hundo_P.Controllers
{

    [Authorize]
    public class DailyExecController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult DailyExecutionModel() { return View(); }

        // POST: DailyExec
        [HttpPost]
        public ActionResult Index(DailyExecModel dailyExecModel)
        {
            //dailyExecModel.ProductivityInPercent = dailyExecModel.ConfirmProductivity(dailyExecModel.TimeSpent);
            if (!string.IsNullOrEmpty(dailyExecModel.DailySummary))
            {

                dailyExecModel.DateCreated = DateTime.Now;

                dailyExecModel.PointStoredDaily = dailyExecModel.GetStoredDailyPoints(dailyExecModel.DateCreated);

                dailyExecModel.DayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

                dailyExecModel.ApplicationUser_Id = User.Identity.GetUserId();

                db.DailyExecModels.Add(dailyExecModel);

                db.SaveChanges();
            }


            return View();
        }

        public ActionResult getDailyInput()
        {

            return View();
        }

        // GET: DailyExec/GenerateReport
        [HttpPost]
        public ActionResult GenerateReport(int id)
        {
            return View();
        }


        public ActionResult NewChart()
        {
            return View();
        }


        [HttpPost]
        public JsonResult NewChart2()
        {
            List<object> iData = new List<object>();
            //Creating sample data  
            ApplicationDbContext db = new ApplicationDbContext();

            DataTable dt = new DataTable();
            dt.Columns.Add("Days of the Week");
            dt.Columns.Add("Performance Scale");

            DataRow dr0 = dt.NewRow();
            dr0["Days of the Week"] = 0;
            dr0["Performance Scale"] = 0;
            dt.Rows.Add(dr0);

            DateTime dateTime = new DateTime(2018, 09, 16);
            for (int i = 0; i < 8; i++)
            {

                DateTime time = dateTime.AddDays(i);
                var model = db.DailyExecModels.Where(x => DbFunctions.TruncateTime(x.DateCreated) == time).FirstOrDefault();
                if (model != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["Days of the Week"] = model.DayOfTheWeek;
                    dr["Performance Scale"] = model.PointStoredDaily;
                    dt.Rows.Add(dr);
                }

            }  

           
         
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult NewChart2()
        //{
        //    List<object> iData = new List<object>();
        //    //Creating sample data  
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Employee");
        //    dt.Columns.Add("Credit");

        //    DataRow dr = dt.NewRow();
        //    dr["Employee"] = "Sam";
        //    dr["Credit"] = 123;
        //    dt.Rows.Add(dr);

        //    dr = dt.NewRow();
        //    dr["Employee"] = "Alex";
        //    dr["Credit"] = 456;
        //    dt.Rows.Add(dr);

        //    dr = dt.NewRow();
        //    dr["Employee"] = "Michael";
        //    dr["Credit"] = 587;
        //    dt.Rows.Add(dr);
        //    //Looping and extracting each DataColumn to List<Object>  
        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        List<object> x = new List<object>();
        //        x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
        //        iData.Add(x);
        //    }
        //    //Source data returned as JSON  
        //    return Json(iData, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult TestChart()
        {
            return View();
        }
    }
}
