using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSQLlite.Model;


namespace XamarinSQLlite.Core.Logic
{
    public class Lc_LoginLogic : Lc_BaseLogic
    {


        public async Task<List<LC_User>> LoginAsyns(string username, string password)
        {
            using (var conn = GetConnection())
            {
                try
                {
                   // var results = conn.Table<LC_User>().ToList();
                    var results = conn.Query<LC_User>("SELECT * FROM LC_User");

                    conn.Close();
                    return results;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
                
            }
        }
    }
}