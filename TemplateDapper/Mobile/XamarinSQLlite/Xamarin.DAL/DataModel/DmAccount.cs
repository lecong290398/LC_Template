using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.DAL.DataModel
{
    public class DmAccount
    {
        public class ListAccount
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class CreateOrUpdateAccount
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class GetAccount
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
        }
    }
}
