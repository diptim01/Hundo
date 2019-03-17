using Hundo_P.Models;
using Hundo_P.Vmodels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hundo_P.Controllers
{

    [Authorize]
    public class DailyExecController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult DailyExecutionModel() { return View(); }

        public ActionResult Pageone() { return View(); }

        public ActionResult PageTwo()
        {
            //check the date         

            //Temporal means of getting User id instead of Session in the Login page
            string userId = User.Identity.GetUserId();
            IEnumerable<DailyExecModel> userDailyProfile = db.DailyExecModels.Where(app => app.ApplicationUser_Id == userId).AsEnumerable();

            List<DailyExecVM> dailyExecVMs = ConvertToViewModels(userDailyProfile);

            IEnumerable<DailyExecVM> results = GetDateResults(dailyExecVMs);

            return View(results);
        }

        private List<DailyExecVM> ConvertToViewModels(IEnumerable<DailyExecModel> userDailyProfile)
        {
            if (userDailyProfile == null)
                return new List<DailyExecVM>();

            List<DailyExecVM> execDTO = new List<DailyExecVM>();
            foreach (DailyExecModel item in userDailyProfile)
            {
                execDTO.Add(new DailyExecVM
                {
                    DailySummary = item.DailySummary,
                    DailyExecModelId = item.DailyExecModelId,
                    DailyTheme = item.DailyTheme,
                    ProductivityInPercent = item.ProductivityInPercent,
                    TimeSpent = item.TimeSpent,
                    DayOfTheWeek = item.DayOfTheWeek,
                    DateCreated = item.DateCreated,
                    PointStoredDaily = item.PointStoredDaily,
                    FinalComment = item.FinalComment,
                    TimeOfCompletion = item.TimeOfCompletion
                });
            }
            return execDTO;
        }

        private IEnumerable<DailyExecVM> GetDateResults(IEnumerable<DailyExecVM> userDailyProfile)
        {
            List<string> results = new List<String>();

            try
            {
                foreach (DailyExecVM item in userDailyProfile)
                {
                    //compare the prev and current date
                    DateTime finishingDate = AddRemainingDateEnding(item.DateCreated, item.TimeSpent);

                    item.TimeOfCompletion = finishingDate;

                    int result = DateTime.Compare(DateTime.Now, finishingDate);

                    if (result < 0)
                        item.DateComment = "You're early!";
                    else if (result == 0)
                        item.DateComment = "Right on time";
                    else
                        item.DateComment = "Oops, you're late!";
                }
                return userDailyProfile;
            }
            catch (Exception ex)
            {
                return new List<DailyExecVM>();
            }

        }

        private DateTime AddRemainingDateEnding(DateTime dateCreated, string timeSpent)
        {
            if (string.IsNullOrEmpty(timeSpent))
                return dateCreated;
            int hoursAdded = Convert.ToInt32(timeSpent.Substring(0, 1));
            return dateCreated.AddHours(hoursAdded);
        }

        public ActionResult FinalPage_3()
        {
            return View();
        }


        public async Task<JsonResult> quotesApi()
        {
            List<QuotesLogic.Quotes> tenQuotesApi = await QuotesLogic.GetQuotesAsync();
            List<QuotesDuplicate> quotesDuplicate = new List<QuotesDuplicate>();
            int number = 1;
            foreach (QuotesLogic.Quotes item in tenQuotesApi)
            {
                quotesDuplicate.Add(new QuotesDuplicate()
                {
                    Id = number.ToString(),
                    author = item.author,
                    category = item.category,
                    quote = item.quote,
                }
                    );
                number++;
            }

            return Json(quotesDuplicate, JsonRequestBehavior.AllowGet);
        }



        // POST: DailyExec
        [HttpPost]
        public ActionResult Pageone(DailyExecModel dailyExecModel)
        {
            //dailyExecModel.ProductivityInPercent = dailyExecModel.ConfirmProductivity(dailyExecModel.TimeSpent);
            if (string.IsNullOrEmpty(dailyExecModel.DailyTheme))
            {
                return View();
            }
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

        [HttpPost]
        public ActionResult PageTwo(int? id, string finalComment)
        {
            if (!id.HasValue)
                return View("An error occured with the Id Passed to the server....");

            DailyExecModel taskModel = db.DailyExecModels.Find(id);

            taskModel.FinalComment = finalComment;
            taskModel.TimeOfCompletion = DateTime.Now;

            db.DailyExecModels.Add(taskModel);

            db.SaveChanges();

            return RedirectToAction("DailyExecutionModel");
        }

        public ActionResult getDailyInput()
        {

            return View();
        }


        public ActionResult NewChart()
        {
            return View();
        }


        [HttpPost]
        public JsonResult NewChart2(string dateSelected)
        {
            List<object> iData = new List<object>();

            //Creating sample data  - refactor later
            ApplicationDbContext db = new ApplicationDbContext();

            DataTable dt = new DataTable();
            dt.Columns.Add("Days of the Week");
            dt.Columns.Add("Performance Scale");

            DataRow dr0 = dt.NewRow();
            dr0["Days of the Week"] = 0;
            dr0["Performance Scale"] = 0;
            dt.Rows.Add(dr0);

            // divide  weekly dates in a month into range i.e 1-7 would be 1 week and 8-15 will be week 2. so whatever date picked, check the range and spool records for that particluar records

            DateTime enteredDate = DateTime.Parse(dateSelected);

            DateTime dateTime = new DateTime(enteredDate.Year, enteredDate.Month, enteredDate.Day);

            int counter = 0;
            if (dateTime.Day >= 0 && dateTime.Day <= 7)
            {
                //week one
                //counter starts at 0;
                counter = 0;
            }
            if (dateTime.Day >= 8 && dateTime.Day <= 15)
            {
                //week two
                //counter starts at 8;
                counter = 8;
            }
            if (dateTime.Day >= 16 && dateTime.Day <= 23)
            {
                //week two
                //counter starts at 16;
                counter = 16;
            }

            if (dateTime.Day >= 24 && dateTime.Day <= 33)
            {
                //week one
                //counter starts at 24;
                counter = 24;
            }

            int limiter = counter + 7;

            for (int i = counter; i < limiter; i++)
                {

                    DateTime time = dateTime.AddDays(i);
                    DailyExecModel model = db.DailyExecModels.Where(x => DbFunctions.TruncateTime(x.DateCreated) == time).FirstOrDefault();
                    if (model != null)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Days of the Week"] = model.DayOfTheWeek;
                        dr["Performance Scale"] = model.PointStoredDaily;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Days of the Week"] = time.DayOfWeek;
                        dr["Performance Scale"] = 0;
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


        public ActionResult TestChart()
        {
            return View();
        }

        public class QuotesDuplicate
        {
            public string quote { get; set; }
            public string author { get; set; }
            public string category { get; set; }
            public string Id { get; set; }
        }
    }
}
