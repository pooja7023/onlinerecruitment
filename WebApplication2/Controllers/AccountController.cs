using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
   
    public class AccountController : Controller
    {

        //public changesEntities usr = new changesEntities();
        changesEntities2 usr = new changesEntities2();
        GraduateDetail tb1 = new GraduateDetail();
        AdminTable adm = new AdminTable();
        //AdminTable1 ad = new AdminTable1();
        // GET: Account
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(GraduateDetail user)
        {
            var userexists = usr.GraduateDetails.FirstOrDefault(t => t.email == user.email && t.password1 == user.password1 );
            if (userexists != null )
            {
                Session["uname"] = user.email;
                return RedirectToAction("LoginSuccessfull");
            }
            
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminTable user1)

        {
            //var userexists = usr.GraduateDetails.FirstOrDefault(t => t.email == user1.email && t.password1==user1.password1);
            //var userexists = usr.AdminTables.FirstOrDefault(t => t.email == user1.email && t.password1 == user1.password1);
            var userexists = usr.AdminTables.FirstOrDefault(t => t.email == user1.email && t.password1 == user1.password1);
            if (userexists != null )
            {
                Session["uname"] = user1.email;
                return RedirectToAction("AdminLoginSuccessfull");
            }
            

            else
            {
                return View();
            }


        }
        public ActionResult LoginSuccessfull()
        {
            GraduateDetail grad = new GraduateDetail();
            //orsEntities2 usr2 = new orsEntities2();
            //Session["uid"] = grad.gradid;
            if (Session["uname"] != null)
            {
                string uid = Session["uname"].ToString();
                var graduate = (from e in usr.GraduateDetails where e.email == uid select e).FirstOrDefault();
                var id = usr.GraduateDetails.Where(r => r.email == uid).FirstOrDefault();
                Session["gid"] = id.gradid;
                usr.SaveChanges();

                return View(graduate);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult LogOut()
        {
            #region cmmts
            //string uid = Session["uname"].ToString();
            //Session.Abandon();
            //return RedirectToAction("Index","Home");
            #endregion
            Session["uname"] = null;
            return View();

        }
        public ActionResult AdminLoginSuccessfull(GraduateDetail user1)
        {

            //
            var userexists = usr.GraduateDetails.FirstOrDefault(t => t.email == user1.email);
            //orsEntities2 usr1 = new orsEntities2();

            usr.SaveChanges();

            return View(usr.GraduateDetails.ToList());


        }
        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(GraduateDetail newuser)
        {
            //tb1.loginid = 2;
            tb1.email = newuser.email;
            tb1.password1 = newuser.password1;
            tb1.mobilephone = newuser.mobilephone;

            usr.GraduateDetails.Add(tb1);
            usr.SaveChanges();

            return RedirectToAction("Login");
        }
        //[HttpGet]
        //public ActionResult EditGraduate(string email1)
        //{
        //    //string uid = Session["uname"].ToString();
        //    var editgraduate = (from e in usr.GraduateDetails where e.email == email1 select e).FirstOrDefault();
        //    return View(editgraduate);

        //}
        [HttpGet]
        public ActionResult EditGraduate(GraduateDetail user)
        {

            //string uid = Session["uname"].ToString();
            string uid = user.email;
            var editgraduate = usr.GraduateDetails.Where(e => e.email == uid).FirstOrDefault();
            editgraduate.email = user.email;
            editgraduate.mobilephone = user.mobilephone;
            editgraduate.password1 = user.password1;
            usr.SaveChanges();
            return RedirectToAction("AdminLoginSuccessfull", "Account");


        }
        public ActionResult DeleteGraduate(int gid)
        {
            var deletegraduate = (from e in usr.GraduateDetails
                                  where e.gradid == gid
                                  select e).FirstOrDefault();
            usr.GraduateDetails.Remove(deletegraduate);
            usr.SaveChanges();
            return RedirectToAction("AdminLoginSuccessfull");
        }
        [HttpGet]
        public ActionResult EditGraduateProfile()
        {
            string uid = Session["uname"].ToString();
            var editgraduate = (from e in usr.GraduateDetails where e.email == uid select e).FirstOrDefault();
            return View(editgraduate);
        }
        [HttpPost]
        public ActionResult EditGraduateProfile(GraduateDetail grd)
        {
            string uid = Session["uname"].ToString();
            var editgraduate = (from e in usr.GraduateDetails where e.email == uid select e).FirstOrDefault();

            editgraduate.firstname = grd.firstname;
            editgraduate.lastname = grd.lastname;
            editgraduate.gender = grd.gender;
            editgraduate.dateofbirth = grd.dateofbirth;
            editgraduate.schoolname = grd.schoolname;
            editgraduate.aggregate1 = grd.aggregate1;
            editgraduate.intercollege = grd.intercollege;
            editgraduate.aggregate2 = grd.aggregate2;
            editgraduate.graduatecollege = grd.graduatecollege;
            editgraduate.branch = grd.branch;
            editgraduate.aggregate3 = grd.aggregate3;
            usr.SaveChanges();
           
            return RedirectToAction("LoginSuccessfull");

        }
        public ActionResult Employees()
        {
            //orsEntities2 ors3 = new orsEntities2();
            var employees = usr.Employees;
            return View(employees);
        }

        public ActionResult ViewEmployees(int sno)
        {
            var viewemployee = (from e in usr.Employees
                                where e.sno == sno
                                select e).FirstOrDefault();
            return View(viewemployee);
        }
        public ActionResult ViewGraduates()
        {
            var gid = Convert.ToInt32(Session["gid"].ToString());
            //var u=usr.GraduateDetails.Where( e => e.gradid==)
            var viewgraduate = (from e in usr.GraduateDetails
                                where e.gradid == gid
                                select e).FirstOrDefault();
            return View(viewgraduate);
        }
        public ActionResult ApplicationStatus()
        {
            return View();
        }
        public ActionResult GraduateStatus()
        {
            return View();
        }



        public ActionResult SearchEmployees()
        {
            var employees = usr.Employees;
            #region cmnts
            //if (searchemp.location == "hyderabad" && searchemp.experience == 3 && searchemp.technology == "python")
            //{
            //    var viewemployee = (from e in usr.Employees

            //                        select e).FirstOrDefault();
            //    return View(viewemployee);
            //}
            //else if (searchemp.location == "hyderabad" && searchemp.experience == 2 && searchemp.technology == "bigdata")
            //{
            //    var viewemployee = (from e in usr.Employees

            //                        select e).FirstOrDefault();
            //    return View(viewemployee);
            //}
            //else if (searchemp.location == "hyderabad" && searchemp.experience == 1 && searchemp.technology == "hadoop")
            //{
            //    var viewemployee = (from e in usr.Employees

            //                        select e).FirstOrDefault();
            //    return View(viewemployee);
            //}

            //else if (searchemp.location == "hyderabad" && searchemp.experience == 0 && searchemp.technology == "asp.net")
            //{
            //    var viewemployee = (from e in usr.Employees

            //                        select e).FirstOrDefault();
            //    return View(viewemployee);
            //}
            #endregion
            return View(employees);
        }


    }


}