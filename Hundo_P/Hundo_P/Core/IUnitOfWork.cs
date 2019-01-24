﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hundo_P.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDailyExecution DailyExecution { get;  }
        int Complete();
    }
}