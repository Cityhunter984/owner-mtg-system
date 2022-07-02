using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MyBetProject.Models.Form;
using MyBetProject.Models.Management;
using MODEL;

namespace MyBetProject.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getLogin(UserForm frm)
        {
            List<UserForm> lstUserLogin = UserManagement.getLogin(frm);

            bool result = lstUserLogin.Count() == 0 ? false : true;

            if (result)
            {
                HttpCookie cookieLogin = new HttpCookie(CONSTANT.cookieLogin);
                cookieLogin.Value = "Login Successfull";
                Response.Cookies.Add(cookieLogin);

                HttpCookie cookieUserLoginPosition = new HttpCookie(CONSTANT.cookieUserLoginPosition);
                cookieUserLoginPosition.Value = lstUserLogin.Select(x => x.PositionID).FirstOrDefault().ToString();
                Response.Cookies.Add(cookieUserLoginPosition);

                HttpCookie cookieUserLoginUsername = new HttpCookie(CONSTANT.cookieUserLoginUsername);
                cookieUserLoginUsername.Value = lstUserLogin.Select(x => x.UserName).FirstOrDefault();
                Response.Cookies.Add(cookieUserLoginUsername);
            }

            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Response.Cookies[CONSTANT.cookieLogin].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[CONSTANT.cookieUserLoginPosition].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[CONSTANT.cookieUserLoginUsername].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Index", "Login");
        }
	}
}