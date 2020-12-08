using Dapper;
using DbCommand;
using log4net;
using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempalate.Business.Interface;
using static Xamarin.DAL.DataModel.DmAccount;

namespace Tempalate.Business.Implements
{
    public class LoginBusiness : ILoginBusiness
    {
        protected IDapperWrapper Dapper = DbCommand.NinjectFactory.Get<IDapperWrapper>();
        protected static readonly ILog log = LogManager.GetLogger(typeof(AccountBusiness));


        public async Task<GetAccount> Login(string Username , string Password)
        {
            try
            {
                var hashedPassword = EncryptProvider.Sha1(Password);
                GetAccount account = new GetAccount();
                string Store = "sp_LC_Login";
                var Para = new DynamicParameters();
                Para.Add("@UserName", Username, DbType.String, ParameterDirection.Input);
                Para.Add("@Password", Password, DbType.String, ParameterDirection.Input);
                var result =  Dapper.StoredProcWithParams<GetAccount>(Store, Para, "").FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return null;
            }
        }

    }
}
