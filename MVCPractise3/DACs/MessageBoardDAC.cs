using MVCPractise3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Mvc;

namespace MVCPractise3.DAC
{
    public class MessageBoardDAC :_DAC
    {
        public MessageBoard GetFunMVC(int num)
        {
             StringBuilder sbSql = new StringBuilder();
             sbSql.AppendLine(@"select NAME, CONTENT, TIME from (select *, ROW_NUMBER() OVER (ORDER BY id DESC) AS ROWNUMBER  from [MVCFuns]) z where ROWNUMBER = {0}");
            return dcDB.ExecuteQuery<MessageBoard>(sbSql.ToString(), num.ToString()
                           ).FirstOrDefault();
        }
        public int GetCount()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(@"select count(*) from [MVCFuns]");
            return dcDB.ExecuteQuery<int>(sbSql.ToString()).FirstOrDefault();
        }
        public void Set(string name, string content)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(@"Insert Into [MVCFuns] (Name, Content, Time) Values( {0}, {1}, {2} )");
            dcDB.ExecuteCommand(sbSql.ToString(), name, content, DateTime.Now.ToString());
        }
        public void Register(string userName, string password)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(@"Insert Into[System_Users] (UserName, Password, Email, RegDate, Region) Values({0}, {1}, {2}, {3}, {4})");
            dcDB.ExecuteCommand(sbSql.ToString(), userName, password, "", DateTime.Now.ToString(), "");
        }
        //public void Modify(string value)
        //{
        //    StringBuilder sbSql = new StringBuilder();
        //    sbSql.AppendLine(@"select COUNT(*) from [MVCFuns] with (NOLOCK)");
        //    IList<int> iList = dcDB.ExecuteQuery<int>(sbSql.ToString()).ToList();
        //    ///有一筆資料的話就update
        //    if (iList[0] >= 1)
        //    {
        //        sbSql.AppendLine(@"update [MVCFuns] set Title = {0}");
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            try
        //            {
        //                dcDB.ExecuteCommand(sbSql.ToString(), value);
        //                scope.Complete();
        //            }
        //            catch (Exception e)
        //            {

        //                Console.Write(e);
        //                throw;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        sbSql.AppendLine(@"insert into [MVCFuns](id , Title) values ({0}, {1})");
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            try
        //            {
        //                dcDB.ExecuteCommand(sbSql.ToString(), 0, value);
        //                scope.Complete();
        //            }
        //            catch (Exception e)
        //            {

        //                Console.Write(e);
        //                throw;
        //            }
        //        }
        //    }
        
        //}
    }
   
}