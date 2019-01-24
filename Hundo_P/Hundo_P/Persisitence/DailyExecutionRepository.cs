using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hundo_P.Core;
using Hundo_P.Models;

namespace Hundo_P.Persisitence
{
    public class DailyExecutionRepository : Repository<DailyExecModel>, IDailyExecution
    {
        public DailyExecutionRepository(ApplicationDbContext _context): base(_context)
        {
                
        }


        public IEnumerable<DailyExecModel> GetListOfDailyTask()
        {
            return ApplicationDbContext.DailyExecModels.ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}