using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hundo_P.Models;

namespace Hundo_P.Core
{
    public interface IDailyExecution: IRepository<DailyExecModel>
    {
        IEnumerable<DailyExecModel> GetListOfDailyTask();
    }
}