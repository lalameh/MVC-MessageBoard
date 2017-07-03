using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCPractise3.Models;
using MVCPractise3.DAC;
using System.Web.Security;

namespace MVCPractise3.Controllers
{
    public class MessageBoardController : Controller
    {
        //
        // GET: /MessageBoard/

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    //var systemuser = db.SystemUsers
        //    //    .Include(x => x.SystemRoles)
        //    //    .FirstOrDefault(x => x.Account == logonModel.Account);

        //    //if (systemuser == null)
        //    //{
        //    //    ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
        //    //    return View();
        //    //}

        //    //var password = CryptographyPassword(logonModel.Password, BaseController.PasswordSalt);

        //    //if (systemuser.Password.Equals(password))
        //    //{
        //    //    this.LoginProcess(systemuser, logonModel.Remember);
        //    //    return RedirectToAction("Index", "Home");
        //    //}
        //    //else
        //    //{
        //    //    ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
        //    //    return View();
        //    //}
        //    return View();
        //}
        public ActionResult Index()
        {
            using (MessageBoardDAC dac = new MessageBoardDAC())
            {
                return View("Index");
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "MessageBoard");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "MessageBoard");
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.User user)
        {
            using (MessageBoardDAC dac = new MessageBoardDAC())
            {
                dac.Register(user.UserName, user.Password);
            }
            return View("Login", user);
        }
        public  Item messageBoardList()
        {
            using (MessageBoardDAC dac = new MessageBoardDAC())
            {
                Item MessageBoardList = new Item();
                MessageBoardList.AllMessage = new List<MessageBoard>();
                for (int i = 1; i <= dac.GetCount(); i++)
                {
                    MessageBoardList.AllMessage.Add(dac.GetFunMVC(i));
                }
                //MessageBoard p1 = new MessageBoard { GUID = "P001", Name = "Mobile", Content = "aaa", Time = DateTime.Now.ToString() };
                //MessageBoard p2 = new MessageBoard { GUID = "P002", Name = "Laptop", Content = "bbb", Time = DateTime.Now.ToString() };
                //MessageBoard p3 = new MessageBoard { GUID = "P003", Name = "Netbook", Content = "ccc", Time = DateTime.Now.ToString() };
                //MessageBoardList.AllMessage.Add(p1);
                //MessageBoardList.AllMessage.Add(p2);
                //MessageBoardList.AllMessage.Add(p3);
                
                return MessageBoardList;
            }
        }
        public PartialViewResult ShowDetails()
        {
           System.Threading.Thread.Sleep(3000);
           using (MessageBoardDAC dac = new MessageBoardDAC())
            {
                string name = Request.Form["Name"];
                string content = Request.Form["Content"];
                dac.Set(name, content);
            }

           var mbList = messageBoardList();
           return PartialView("_ShowDetails", mbList);
        }
    }
}
