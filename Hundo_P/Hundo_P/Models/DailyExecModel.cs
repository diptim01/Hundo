using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Hundo_P.Models
{
    public class DailyExecModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DailyExecModelId { get; set; }

        public string DailySummary { get; set; }

        public String ProductivityInPercent { get; set; }

        public string TimeSpent { get; set; }

        public string DayOfTheWeek { get; set; }

        public DateTime DateCreated { get; set; }

        public int PointStoredDaily { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUser_Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


        // methods
        public int GetStoredDailyPoints(DateTime dateTime)
        {
            int returnedpointAdded = 0;

            DayOfWeek weekday = dateTime.DayOfWeek;

            if (dateTime.Day > 0)
            {
                switch (weekday)
                {
                    case DayOfWeek.Sunday:
                        returnedpointAdded = 20;
                        break;
                    case DayOfWeek.Monday:
                        returnedpointAdded = AddDailyPoint(dateTime, 1);
                        break;
                    case DayOfWeek.Tuesday:
                        returnedpointAdded = AddDailyPoint(dateTime, 2);
                        break;
                    case DayOfWeek.Wednesday:
                        returnedpointAdded = AddDailyPoint(dateTime, 3);
                        break;
                    case DayOfWeek.Thursday:
                        returnedpointAdded = AddDailyPoint(dateTime, 4);
                        break;
                    case DayOfWeek.Friday:
                        returnedpointAdded = AddDailyPoint(dateTime, 5);
                        break;
                    case DayOfWeek.Saturday:
                        returnedpointAdded = AddDailyPoint(dateTime, 6);
                        break;
                }
            }
            return returnedpointAdded;
        }

        private static int AddDailyPoint(DateTime dateTime, int interval)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int pointAdded = 0;

            // check for previous day's record....
            DateTime check = dateTime.AddDays(-1);
            //


            for (int i = interval; i > 0; i--)
            {
                DateTime dayCount = dateTime.AddDays(-i);
                DailyExecModel days = db.DailyExecModels.Where(x => x.DateCreated == dayCount).FirstOrDefault();

                if (days != null && days.PointStoredDaily > 0)
                {
                    if (dayCount == check && days.PointStoredDaily == 0) { pointAdded += 10; }
                    else
                    {
                        pointAdded += 20;
                    }

                }
            }

            return pointAdded;
        }




        //Methods
        public string ConfirmProductivity(string nosOfMinute)
        {
            string confirmProductivity = "Invalid Computation";
            switch (nosOfMinute)
            {
                case "less than 30 mins":
                    confirmProductivity = "100%";
                    break;

                case "30mins - 1hr":
                    confirmProductivity = "90%";
                    break;

                case "1hr - 1hr, 30mins":
                    confirmProductivity = "80%";
                    break;
                case "2hrs - 2hrs, 30mins":
                    confirmProductivity = "70%";
                    break;
                case "2hr, 30mins - 3hrs":
                    confirmProductivity = "60%";
                    break;
                case "3hrs - 3hrs, 30mins":
                    confirmProductivity = "50%";
                    break;
                case "3hrs, 30mins - 4hrs":
                    confirmProductivity = "40%";
                    break;
                case "4hrs - 4hrs, 30mins":
                    confirmProductivity = "30%";
                    break;
                case "4hrs, 30mins - 5hrs":
                    confirmProductivity = "20%";
                    break;
                case "5hrs - 5hrs, 30mins":
                    confirmProductivity = "10%";
                    break;
                case "Greater than 5hrs, 20mins":
                    confirmProductivity = "5%";
                    break;
            }

            return confirmProductivity;


        }
    }
}