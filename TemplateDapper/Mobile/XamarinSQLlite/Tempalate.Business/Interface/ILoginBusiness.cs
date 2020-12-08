using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.DAL.DataModel.DmAccount;

namespace Tempalate.Business.Interface
{
    public interface ILoginBusiness
    {
        Task<GetAccount> Login(string Username, string Password);
    }
}
