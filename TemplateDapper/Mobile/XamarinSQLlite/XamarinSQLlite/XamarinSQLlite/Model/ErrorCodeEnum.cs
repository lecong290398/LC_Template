using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSQLlite.Model
{
    /// <summary>
    /// Mã lỗi API
    /// </summary>
    public enum ErrorCodeEnum
    {
        /// <summary>
        /// Không có lỗi
        /// </summary>
        None = 0,

        /// <summary>
        /// Lỗi dữ liệu đầu vào
        /// </summary>
        ValidationFail = 101,

        /// <summary>
        /// Lỗi nghiệp vụ máy chủ
        /// </summary>
        UnknowLogic = 102,

        /// <summary>
        /// Lỗi xử lý máy chủ
        /// </summary>
        UnknowApi = 103,

        /// <summary>
        /// Lỗi đăng nhập thất bại
        /// </summary>
        LoginFail = 104,
    }
}
