using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hundo_P.Core;
using Hundo_P.Models;

namespace Hundo_P.Persisitence
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            DailyExecution = new DailyExecutionRepository(_context);
        }

        public IDailyExecution DailyExecution { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}