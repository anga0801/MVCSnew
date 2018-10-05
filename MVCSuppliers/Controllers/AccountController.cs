using CRUDSuppliers_DAL;
using CRUDSuppliers_DAL.Models;
using MVCSuppliers.Custom;
using MVCSuppliers.Logging;
using MVCSuppliers.Mapping;
using MVCSuppliers.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace MVCSuppliers.Controllers
{
    public class AccountController : Controller
    {

        private readonly string _ConnectionString;
        private readonly string _LogPath;
        private EmployeeDAO _EmployeeDAO;

        public AccountController()
        {
            string relativePath = ConfigurationManager.AppSettings["logPath"];
            _LogPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), relativePath);
            _ConnectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _EmployeeDAO = new EmployeeDAO(_ConnectionString, _LogPath);
        }
        
        // GET: Account
        [SecurityFilter(role: 1)]
        public ActionResult Index()
        {
            ActionResult response;
            try
            {
                List<EmployeeDO> allEmployees = _EmployeeDAO.ViewAllEmployees();
                List<EmployeePO> displayList = EmployeeMapper.DOToPO(allEmployees);
                response = View(displayList);
            }
            catch (SqlException ex)
            {
                TempData["ErrorMessage"] = "Sorry, we encountered an issue";
                response = View();

                Logger.ErrorLogPath = _LogPath;
                Logger.ExceptionLog(ex);
            }
            return response;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                EmployeeDO userData = _EmployeeDAO.ViewEmployeeByUsername(form.Username);

                if (!userData.EmployeeId.Equals(0))
                {
                    if (form.Password.Equals(userData.Password))
                    {
                        Session["EmployeeId"] = userData.EmployeeId;
                        Session["Username"] = userData.Username;
                        Session["Title"] = userData.Title;
                        Session["Role"] = userData.Role;

                        response = RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Username or password was incorrect");


                        response = View(form);
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Username or password was incorrect");


                    response = View(form);
                }
            }
            else
            {
                response = View(form);
            }
            return response;
        }


        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}