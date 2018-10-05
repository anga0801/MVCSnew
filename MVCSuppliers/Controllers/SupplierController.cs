using CRUDSuppliers_DAL;
using CRUDSuppliers_DAL.Models;
using MVCSuppliers.Custom;
using MVCSuppliers.Logging;
using MVCSuppliers.Mapping;
using MVCSuppliers.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace MVCSuppliers.Controllers
{
    public class SupplierController : Controller
    {
        private readonly string _ConnectionString;
        private readonly string _LogPath;
        private SupplierDAO _SupplierDAO;
        private ProductDAO _ProductDAO;

        //CONSTRUCTOR
        public SupplierController()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            string relativePath = ConfigurationManager.AppSettings["logPath"];
            _LogPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), relativePath);
            _SupplierDAO = new SupplierDAO(_ConnectionString, _LogPath);
            _ProductDAO = new ProductDAO(_ConnectionString, _LogPath);
        }


        //VIEW ALL SUPPLIERS. ALLOWED WHEN SIGNED IN.
        [SecurityFilter(role: 3)]
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult response;

            try
            {
                //Obtain and store list of all suppliers from SQL.
                List<SupplierDO> allSuppliers = _SupplierDAO.ObtainAllSuppliers();

                //Map the DO info and store it in a PO list.
                List<SupplierPO> supplierInfo = SupplierMapper.FromDoToPo(allSuppliers);

                //Go to index view page giving info of the PO list.
                response = View(supplierInfo);
            }
            catch (SqlException sqlex)
            {
                response = RedirectToAction("Error", "Shared");
            }
            catch (Exception ex)
            {
                response = RedirectToAction("Error", "Shared");

                Logger.ErrorLogPath = _LogPath;
                Logger.ExceptionLog(ex);
            }

            return response;
        }


        //VIEW CREATE SUPPLIER PAGE(FORM). ALLOWED FOR 2 AND DOWN.
        [SecurityFilter(role: 2)]
        [HttpGet]
        public ActionResult CreateSupplier()
        {
            return View();
        }


        //CREATE SUPPLIER IN SQL, TAKING IN THE INFO FILLED OUT IN CREATE SUPPLIER VIEW PAGE. ALLOWED FOR 2 AND DOWN.
        [SecurityFilter(role: 2)]
        [HttpPost]
        public ActionResult CreateSupplier(SupplierPO form)
        {
            ActionResult response;

            //Check if valid info was given, Back to create supplier view page if not.
            if (ModelState.IsValid)
            {
                try
                {
                    //Map given info to DO.
                    SupplierDO newSupplier = SupplierMapper.PoToDo(form);

                    //Create supplier in SQL, giving DO info.
                    _SupplierDAO.CreateSupplier(newSupplier);

                    //Return to view all supplier page.
                    response = RedirectToAction("Index", "Supplier");
                }
                catch (SqlException sqlex)
                {
                    response = RedirectToAction("Error", "Shared");
                }
                catch (Exception ex)
                {
                    response = RedirectToAction("Error", "Shared");

                    Logger.ErrorLogPath = _LogPath;
                    Logger.ExceptionLog(ex);
                }
            }
            else
            {
                response = View();
            }
            return response;
        }


        //VIEW UPDATE PAGE WITH ORIGINAL INFO OF CHOSEN SUPPLIERID. ALLOWED FOR 2 AND DOWN.
        [SecurityFilter(role: 2)]
        [HttpGet]
        public ActionResult UpdateSupplier(int supplierId)
        {
            ActionResult response;
            
            if (ModelState.IsValid)
            {
                try
                {
                    //Obtain supplier info from SQL using given ID.
                    SupplierDO supplierDO = _SupplierDAO.ObtainSupplierById(supplierId);

                    //Map DO to PO.
                    SupplierPO supplier = SupplierMapper.DoToPo(supplierDO);

                    //Give view page with obtained info.
                    response = View(supplier);
                }
                catch (SqlException sqlex)
                {
                    response = RedirectToAction("Error", "Shared");
                }
                catch (Exception ex)
                {
                    response = RedirectToAction("Error", "Shared");

                    Logger.ErrorLogPath = _LogPath;
                    Logger.ExceptionLog(ex);
                }

            }
            else
            {
                response = View();
            }
            return response;
        }


        //UPDATE SQL WITH GIVEN INFO IN THE FORM. ALLOWED FOR 2 AND DOWN.
        [SecurityFilter(role: 2)]
        [HttpPost]
        public ActionResult UpdateSupplier(SupplierPO form)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    //Map given info to DO.
                    SupplierDO supplierDO = SupplierMapper.PoToDo(form);

                    //Update supplier in SQL giving DO info.
                    _SupplierDAO.UpdateSupplier(supplierDO);

                    //Return to view all suppliers page.
                    response = RedirectToAction("Index", "Supplier");
                }
                catch (SqlException sqlex)
                {
                    response = RedirectToAction("Error", "Shared");
                }
                catch (Exception ex)
                {
                    response = RedirectToAction("Error", "Shared");

                    Logger.ErrorLogPath = _LogPath;
                    Logger.ExceptionLog(ex);
                }
            }
            else
            {
                response = View();
            }
            return response;
        }


        //DELETE SUPPLIER BY GIVEN ID. ALLOWED FOR 1.
        [SecurityFilter(role: 1)]
        [HttpGet]
        public ActionResult DeleteSupplier(int supplierId)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    //Pass in ID and delete from SQL.
                    _SupplierDAO.DeleteSupplier(supplierId);

                    //Return to view all supplier page.
                    response = RedirectToAction("Index", "Supplier");
                }
                catch (SqlException sqlex)
                {
                    response = RedirectToAction("Error", "Shared");
                }
                catch (Exception ex)
                {
                    response = RedirectToAction("Error", "Shared");

                    Logger.ErrorLogPath = _LogPath;
                    Logger.ExceptionLog(ex);
                }
            }
            else
            {
                response = View();
            }
            return response;
        }


        //VIEW SPECIFIC SUPPLIER DETAILS(SUPPLIER INFO, PRODUCTS SUPPLIED). ALLOWED WHEN SIGNED IN.
        [SecurityFilter(role: 3)]
        [HttpGet]
        public ActionResult ViewSupplierDetails(int supplierId)
        {
            ActionResult response;

            if (ModelState.IsValid)
            {
                try
                {
                    SupplierVM supplierDetails = new SupplierVM();

                    //Getting specific supplier info from SQL with ID and storing it into SupplierViewModel. 
                    supplierDetails.SupplierData = SupplierMapper.DoToPo(_SupplierDAO.ObtainSupplierById(supplierId));

                    //Getting all products supplied by specific supplier ID and storing it into SupplierViewModel.
                    supplierDetails.ProductsBySupplier = ProductMapper.DOToPO(_ProductDAO.ObtainProductsBySupplierID(supplierId));

                    //View supplier details.
                    response = View(supplierDetails);
                }
                catch (SqlException sqlex)
                {
                    response = View("Error");
                }
                catch (Exception ex)
                {
                    response = View("Error");

                    Logger.ErrorLogPath = _LogPath;
                    Logger.ExceptionLog(ex);
                }
            }
            else
            {
                response = View();
            }

            return response;
        }
    }
}