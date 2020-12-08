using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.DAL.EntityModels
{
    public class ResultResponse<T>
    {
        public ResultResponse()
        {

        }
        public ResultResponse(T payload)
        {
            IsOk = true;
            PayLoad = payload;
        }

        public ResultResponse(ErrorCode error)
        {
            IsOk = false;
            Error = error;
        }


        public bool IsOk { get; set; }
        public ErrorCode Error { get; }
        public List<AtApiError> ListError { get; set; }
        public T PayLoad { get; set; }

        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
    public class AtApiError
    {
        public string Key { get; set; }
        public ErrorCode Error { get; set; }
    }
}
