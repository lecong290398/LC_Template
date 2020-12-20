using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinSQLlite.Model;

namespace XamarinSQLlite.Core.Logic
{
    public abstract class Lc_BaseLogic
    {
        public static SQLiteConnection GetConnection()
        {
            return (App.Current as App)._connection;
        }
    }
}