using MVCPractise3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Globalization;
using System.Text.RegularExpressions;



namespace MVCPractise3.DAC
{
    /// <summary>
    /// Data Access & Control
    /// </summary>
    public class _DAC : IDisposable
    {
        protected DataContext dcDB;
      
        /// <summary>
        /// 建立資料庫連結
        /// </summary>
        public _DAC()
        {
            dcDB = new DataContext(ConfigurationManager.ConnectionStrings["MessageBoard"].ConnectionString);
        }

        /// <summary>
        /// 關閉連結與釋放資源
        /// </summary>
        public void Dispose()
        {
            dcDB.Connection.Close();
            dcDB.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 轉換為民國年
        /// </summary>
        ///<param name="format">標準格式化語法</param>
        /// <returns></returns>
        public string ChangeTaiwanCalendar(DateTime x, string format)
        {
            DateTime now = x;
            TaiwanCalendar tc = new TaiwanCalendar();
            Regex regex = new System.Text.RegularExpressions.Regex(@"[yY]+");
            format = regex.Replace(format, tc.GetYear(x).ToString("000"));
            return x.ToString(format);
        }

    }
    /// <summary>
    /// DAC執行回傳
    /// </summary>
    public class dacResult
    {
        /// <summary>
        /// 是否執行成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 回傳物件
        /// </summary>
        public dynamic result { get; set; }
        /// <summary>
        /// 啟始物件
        /// </summary>
        /// <param name="success">是否執行成功</param>
        /// <param name="message">回傳訊息</param>
        public dacResult(bool success, string message, dynamic result)
        {
            this.success = success;
            this.message = message;
            this.result = result;
        }
    }
}


