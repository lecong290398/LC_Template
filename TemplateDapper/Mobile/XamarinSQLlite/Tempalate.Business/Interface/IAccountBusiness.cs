using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.DAL.DataModel.DmAccount;

namespace Tempalate.Business.Interface
{
    public interface IAccountBusiness
    {
        List<ListAccount> GetListAccount();
        GetAccount GetAccount(long userID);
        bool UpdateAccount(CreateOrUpdateAccount input);
        int CreateAccount(CreateOrUpdateAccount input);
        bool DeleteAccount(long UserID);
    }
}
