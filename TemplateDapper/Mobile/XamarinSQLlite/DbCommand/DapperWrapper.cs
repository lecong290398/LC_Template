using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DbCommand
{
    public class DapperWrapper : IDapperWrapper
    {
        private readonly string _connectionString = "Server=101.99.32.48,9899;Database=DemoTemplateWebsite;User Id=admin;Password=123123Cong.";

        private SqlConnection GetOpenConnection(string cnnString = "")
        {
            if (string.IsNullOrWhiteSpace(cnnString)) cnnString = _connectionString;

            var connection = new SqlConnection(cnnString);
            connection.Open();
            return connection;
        }

        public List<T> StoredProcWithParams<T>(string procname, dynamic parms, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return connection.Query<T>(procname, (object)parms, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<T> StoredProcNoneParams<T>(string procname, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return connection.Query<T>(procname, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<T> SqlWithParams<T>(string sql, dynamic parms, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        public T StoreQuerySingle<T>(string procname, dynamic parms = null, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return connection.QuerySingleOrDefault<T>(procname, (object)parms,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int InsertUpdateOrDeleteSql(string sql, dynamic parms, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return connection.Query<int>(sql, (object)parms).Single();
            }
        }

        public int InsertUpdateOrDeleteStoredProc(string procName, dynamic parms, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return
                    connection.Query<int>(procName, (object)parms, commandType: CommandType.StoredProcedure)
                        .SingleOrDefault();
            }
        }

        public string InsertUpdateOrDeleteStoredProcString(string procName, dynamic parms, string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                return
                    connection.Query<string>(procName, (object)parms, commandType: CommandType.StoredProcedure)
                        .SingleOrDefault();
            }
        }

        public IEnumerable<object>[] StoreProcQueryMultiple(string procname, dynamic parms = null, string cnnString = "",
            params Type[] types)
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                var multi = connection.QueryMultiple(procname, (object)parms, commandType: CommandType.StoredProcedure);
                var values = new IEnumerable<object>[types.Length];
                for (var i = 0; i < types.Length; i++)
                {
                    values[i] = multi.Read(types[i]);
                }
                return values;
            }
        }

        /// <summary>
        /// Lấy giờ hiện tại trên server
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDateTimeFromServer(string cnnString = "")
        {
            using (var connection = GetOpenConnection(cnnString))
            {
                var resultParameter = new SqlParameter("@result", SqlDbType.DateTime);
                resultParameter.Size = 50;
                resultParameter.Direction = ParameterDirection.Output;
                var result = connection.Query<DateTime>("set @result =  GETDATE();", resultParameter).FirstOrDefault();
                return result;
            }
        }
    }
}
