using HyundaiAutomobileDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyundaiAutomobileDealer.Controllers
{
    public class LoginController : Controller
    {
        HyundaiEntities db = new HyundaiEntities();

        // GET: Login
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]

        public ActionResult Login(tbl_hyundai_login_master objLogin)
        {
            if (ModelState.IsValid)
            {
                var obj = db.tbl_hyundai_login_master.Where(a => a.UserName.Equals(objLogin.UserName) && a.Password.Equals(objLogin.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserName"] = Convert.ToString(objLogin.UserName);
                    Session["LoginID"] = Convert.ToInt32(objLogin.LoginID);
                    Session["SessionID"] = Convert.ToString(DateTime.Now);
                    if(Session["Username"].Equals("admin"))
                    {
                        Session["UserRoleID"] = 1;
                    }
                    else
                    {
                        Session["UserRoleID"] = 2;
                    }
                    return RedirectToAction("WelcomePage");
                }
                else
                {
                    TempData["notice"] = "Invalid credentials";
                    return RedirectToAction("Login");
                    //Response.Write("<script>alert('Error');</script>");
        
                }
                
            }
            return View(objLogin);
        }

        public ActionResult WelcomePage()
        {

            if (Session["UserRoleID"] !=null)
            {
                string SessionName = Session["UserName"].ToString();
                ViewBag.Message = SessionName;
                //Session.RemoveAll();
                
            }
            else
            {


                return RedirectToAction("Login");
            }

            return View();
        }

    }
}