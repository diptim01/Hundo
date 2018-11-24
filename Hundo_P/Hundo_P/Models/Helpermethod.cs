using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hundo_P.Models
{
    public class newHelpermethod
    {
        public class RegisterDetails
        {
            public string question { get; set; }
            public string value { get; set; }

        }

        public class UserInfo
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

        }
    }
}