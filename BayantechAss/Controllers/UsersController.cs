using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BayantechAss.Models;

namespace BayantechAss.Controllers
{
    //[Authorize]

    public class UsersController : Controller
    {
        private DBContextClass db = new DBContextClass();

         public ActionResult Index()
        {
            User user = null;
            if (Session["UserSessionEmail"] != null)
            {
                string user_email = Session["UserSessionEmail"].ToString();
                user = db.Users.Single(u => u.Email == user_email);
                return View(user);
            }
            else {
                return RedirectToAction("Login", "Users");
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,Name,Email,Password,Gender,DateOfBirth,Address")] User user)
        {
            ViewBag.ConfirmPassword = Request.Form.Get("ConfirmPassword");

            if (user.Password != ViewBag.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password doesn't match");
                Response.Write("Error:<br/><font color='red'>Password doesn't match</font>");

                Response.Write("<br/>Password :<font color='red'>" + user.Password + "</font>");
                Response.Write("<br/>ConfirmPassword:<font color='red'>" + ViewBag.ConfirmPassword + "</font>");

            }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            User user = null;
            if (Session["UserSessionEmail"] != null)
            {
                string user_email = Session["UserSessionEmail"].ToString();
                user = db.Users.Single(u => u.Email == user_email);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

           
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            Session["UserSessionEmail"] = null;
            db.SaveChanges();
            return RedirectToAction("Register","Users");
        }

        public ActionResult Login()
        {
            if (Session["UserSessionEmail"] != null)
            {
               
                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] User user)
        {
            User UserLogin = null;
            try
            {
                UserLogin = db.Users.Single(u => u.Email == user.Email && u.Password == user.Password);
                Session["UserSessionEmail"] = user.Email;
                return RedirectToAction("Index","Users");
            }
            catch (Exception ex)
            {
                Response.Write("<br/><font color='red'> Some of your Information are incorrect,try again  Please ^_^</font>");
               // Response.Write("<br/>Error:<font color='red'>"+ex.Message+"</font>");
                return View();//return to the same page Create
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Session["UserSessionEmail"] != null)
            {

                return RedirectToAction("Index");
            }
            else { 
                 return View();
                  }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,Name,Email,Gender,DateOfBirth,Address,Password")] User user)
        {

            ViewBag.ConfirmPassword = Request.Form.Get("ConfirmPassword");
            //Response.Write("confirmPass is:" + ViewBag.ConfirmPassword);
            if (db.Users.Any(x => x.Email == user.Email))
            {

                ModelState.AddModelError("Email", "User Email is already exist");
               // Response.Write("Error:<br/><font color='red'>User Email is already exist</font>");


            }
            if (user.Password != ViewBag.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password doesn't match");
                Response.Write("Error:<br/><font color='red'>Password doesn't match</font>");

                Response.Write("<br/>Password :<font color='red'>" + user.Password + "</font>");
                Response.Write("<br/>ConfirmPassword:<font color='red'>" + ViewBag.ConfirmPassword + "</font>");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    Session["UserSessionEmail"] = user.Email;

                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index","Users");

                }
                catch (Exception ex)
                {

                    Response.Write("Error:<br/><font color='red'>" + ex.Message + "</font>");
                    return View();//return to the same page Create

                }
            }
            else
            {

                return View();//return to the same page Create
            }

        }

        public ActionResult Logout()
        {
            try
            {
                Session.Abandon();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Login","Users");

        }

        public ActionResult ContactUs() { return View(); }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
