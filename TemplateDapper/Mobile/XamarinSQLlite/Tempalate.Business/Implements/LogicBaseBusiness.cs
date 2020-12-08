using DbCommand;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempalate.Business.Interface;

namespace Tempalate.Business.Implements
{
    public class LogicBaseBusiness : ILogicBaseBusiness
    {
        protected IDapperWrapper Dapper = DbCommand.NinjectFactory.Get<IDapperWrapper>();
        protected static readonly ILog log = LogManager.GetLogger(typeof(AccountBusiness));

        /// <summary>
        /// Lấy giờ hiện tại trên server
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDateTimeFromServer()
        {
            try
            {
                var result = Dapper.GetDateTimeFromServer("");
                return result;
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Error(ex.Message);
                return DateTime.Now;
            }
        }
    }
}
