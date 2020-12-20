using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace XamarinSQLlite.Model
{
    internal class LogicException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        /// <summary>
        /// Lỗi logic
        /// </summary>
        /// <param name="errorCode"></param>
        public LogicException(ErrorCodeEnum errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Lỗi logic
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        public LogicException(ErrorCodeEnum errorCode, Exception innerException) : base(errorCode.ToString(), innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Lỗi logic
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LogicException(ErrorCodeEnum errorCode, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCodeEnum ErrorCode { get; private set; }
    }
}
