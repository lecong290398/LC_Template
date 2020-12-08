using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCommand
{
    public interface IDapperWrapper
    {
        List<T> StoredProcWithParams<T>(string procname, dynamic parms, string cnnString = "");

        List<T> StoredProcNoneParams<T>(string procname, string cnnString = "");

        List<T> SqlWithParams<T>(string sql, dynamic parms, string cnnString = "");

        T StoreQuerySingle<T>(string procname, dynamic parms = null, string cnnString = "");

        int InsertUpdateOrDeleteSql(string sql, dynamic parms, string cnnString = "");

        int InsertUpdateOrDeleteStoredProc(string procName, dynamic parms, string cnnString = "");
        string InsertUpdateOrDeleteStoredProcString(string procName, dynamic parms, string cnnString = "");

        IEnumerable<object>[] StoreProcQueryMultiple(string procname, dynamic parms = null, string cnnString = "",
            params Type[] types);

        DateTime GetDateTimeFromServer(string cnnString = "");
    }
}
