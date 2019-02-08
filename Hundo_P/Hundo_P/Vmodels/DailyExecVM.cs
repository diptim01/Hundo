using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hundo_P.Vmodels
{
    public class DailyExecVM
    {
        public int DailyExecModelId { get; set; }

        public string DailySummary { get; set; }

        public String ProductivityInPercent { get; set; }

        public string TimeSpent { get; set; }

        public string DayOfTheWeek { get; set; }

        public DateTime DateCreated { get; set; }

        public string DailyTheme { get; set; }

        public int PointStoredDaily { get; set; }

        public string FinalComment { get; set; }

        public DateTime? TimeOfCompletion { get; set; }

        public string DateComment { get; set; }
    }
}