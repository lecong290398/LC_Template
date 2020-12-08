using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DbCommand;
using log4net;
using System.Data;
using System.Configuration;
using Tempalate.Business.Interface;
using System;
using static Xamarin.DAL.DataModel.DmAccount;
using System.Collections.Generic;

namespace Tempalate.Business.Implements
{
    public class AccountBusiness : IAccountBusiness
    {
        protected IDapperWrapper Dapper = DbCommand.NinjectFactory.Get<IDapperWrapper>();
        protected static readonly ILog log = LogManager.GetLogger(typeof(AccountBusiness));

        public int CreateAccount(CreateOrUpdateAccount input)
        {
            try
            {
                string Store = "sp_Account_Insert";
                var Para = new DynamicParameters();
                int result = 0;

                //Para.Add("@GiaoDichID", dVC_HOSO.@GiaoDichID, DbType.Int64, ParameterDirection.Input);
                //Para.Add("@KhachHang", dVC_HOSO.@KhachHang, DbType.String, ParameterDirection.Input);
                //Para.Add("@DiaChi", dVC_HOSO.@DiaChi, DbType.String, ParameterDirection.Input);
              
                result = Dapper.InsertUpdateOrDeleteStoredProc(Store, Para, "");
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return -1;
            }
        }

        public bool DeleteAccount(long UserID)
        {
            try
            {
                bool result = false;
                string Store = "sp_AccountDeleteById";
                var Para = new DynamicParameters();
                Para.Add("@TiepDinhKemID", UserID, DbType.String, ParameterDirection.Input);
                int kq = Dapper.InsertUpdateOrDeleteStoredProc(Store, Para, "");
                if (kq >= 0)
                {
                    result = true;

                }
                return result;
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return false;
            }
        }

        public GetAccount GetAccount(long userID)
        {
            try
            {
                GetAccount account = new GetAccount();
                string Store = "sp_GetAccountObject";
                var Para = new DynamicParameters();
                Para.Add("@userID", userID, DbType.String, ParameterDirection.Input);
                var result = Dapper.StoredProcWithParams<GetAccount>(Store, Para, "").FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return null;
            }
        }

        public List<ListAccount> GetListAccount()
        {
            try
            {
                string Store = "sp_GetAccount_Lst";
                var Para = new DynamicParameters();
                //Para.Add("@Text", Text, DbType.String, ParameterDirection.Input);
                //Para.Add("@isAdmin", isAdmin, DbType.String, ParameterDirection.Input);
                //Para.Add("@Role", Role, DbType.String, ParameterDirection.Input);
                var result = Dapper.StoredProcWithParams<ListAccount>(Store, Para, "");
                return result;
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return null;
            }
        }

        public bool UpdateAccount(CreateOrUpdateAccount input)
        {
            try
            {
                bool result = false;
                string Store = "sp_Account_UpdAccountObject";
                var Para = new DynamicParameters();
                //Para.Add("@pKhachHangID", UserID, DbType.String, ParameterDirection.Input);
                var kq = Dapper.InsertUpdateOrDeleteStoredProc(Store, Para, "");
                if (kq >= 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return false;
            }
        }
    }
}
