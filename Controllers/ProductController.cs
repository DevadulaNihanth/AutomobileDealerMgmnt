using HyundaiAutomobileDealer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyundaiAutomobileDealer.Controllers
{
    public class ProductController : Controller
    {
        HyundaiEntities db = new HyundaiEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HyundaiList()
        {
            if (Session["UserRoleID"] != null)
            {
                List<usp_view_tbl_hyundai_product_1334712_Result> prodList = new List<usp_view_tbl_hyundai_product_1334712_Result>();
                prodList = db.usp_view_tbl_hyundai_product_1334712().ToList();

                return View(prodList);
            }
            return View();
        }
        public ActionResult AddProduct()
        {
            ViewBag.DropList = new SelectList(db.tbl_hyundai_prod_category, "ProductCategoryID", "ProductCategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(tbl_hyundai_product_1334712 objProd)
        {
            if ((int)Session["UserRoleID"] == 1)
            {
                if (ModelState.IsValid)
                {
                    ObjectParameter objParam = new ObjectParameter("ID_out", typeof(int));
                    int save = db.usp_save_tbl_hyundai_product_1334712(objProd.ProductID, objProd.ProductName, objProd.ProductCategoryID, objProd.UnitPrice, objProd.TotalNumberAvailable, objProd.Description, objParam);
                    this.db.SaveChanges();
                    int saveVal = Convert.ToInt32(objParam.Value);
                }
            }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            return RedirectToAction("HyundaiList");
        }
        public ActionResult EditProduct(int? ProductID)
        {
            usp_viewbyid_tbl_hyundai_product_1334712_Result objProd = new usp_viewbyid_tbl_hyundai_product_1334712_Result();
            if ((int)Session["UserRoleID"] == 1)
            {
                if (ProductID != null)
                {
                    objProd = db.usp_viewbyid_tbl_hyundai_product_1334712(ProductID).FirstOrDefault();
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View(objProd);
        }
        [HttpPost]
        public ActionResult EditProduct(usp_viewbyid_tbl_hyundai_product_1334712_Result objProd)
        {
            ObjectParameter objParam = new ObjectParameter("ID_out", typeof(int));
            int save = db.usp_save_tbl_hyundai_product_1334712(objProd.ProductID, objProd.ProductName, objProd.ProductCategoryID, objProd.UnitPrice, objProd.TotalNumberAvailable, objProd.Description, objParam);
            this.db.SaveChanges();
            int saveVal = Convert.ToInt32(objParam.Value);
            return RedirectToAction("HyundaiList");
        }

        public ActionResult DeleteProduct(int? ID)
        {
            usp_viewbyid_tbl_hyundai_product_1334712_Result objProd = new usp_viewbyid_tbl_hyundai_product_1334712_Result();
            if ((int)Session["UserRoleID"] == 1)
            {
                if (ID != null)
                {
                    objProd = db.usp_viewbyid_tbl_hyundai_product_1334712(ID).FirstOrDefault();
                }
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
            return View(objProd);
        }
        [HttpPost]
        public ActionResult DeleteProduct(usp_viewbyid_tbl_hyundai_product_1334712_Result objProd)
        {
            int result = db.usp_delete_tbl_hyundai_product_1334712(objProd.ProductID);
            this.db.SaveChanges();
            return RedirectToAction("HyundaiList");
        }
    }
}