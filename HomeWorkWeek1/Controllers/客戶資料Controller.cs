using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using HomeWorkWeek1.Models;

namespace HomeWorkWeek1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private Entities db = new Entities();
        客戶資料Repository repo =  RepositoryHelper.Get客戶資料Repository();
        // GET: 客戶資料

        public ActionResult Index()
        {
            var list = repo.All();

            DDL();

            return View(list.ToList().Take(10));
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,category,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,category,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 =repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string keyword)
        {
            DDL();

            var 客戶資料 = repo.Searchname(keyword);

            return View("Index", 客戶資料);
        }
        public ActionResult Category(string Cate)
        {
            DDL();

            var 客戶資料 = repo.Category(Cate);
            return View("Index", 客戶資料);
        }


        public ActionResult Sortby(string col,string sort)
        {


            DDL();

            var 客戶資料 = repo.Sortby(col,sort);
            return View("Index", 客戶資料);
        }
        public ActionResult DDL()
        {

            var dropdown = repo.All().Distinct();
            SelectList selectlist = new SelectList(dropdown, "Category", "Category");
            ViewBag.selectlist = selectlist;

            List<SelectListItem> colname = new List<SelectListItem>();

            colname.AddRange(new[]{
        new SelectListItem() {Text = "客戶名稱", Value = "客戶名稱", Selected = false},
        new SelectListItem() {Text = "統一編號", Value = "統一編號", Selected = false},
         new SelectListItem() {Text = "電話", Value = "電話", Selected = false},
          new SelectListItem() {Text = "傳真", Value = "傳真", Selected = false},
           new SelectListItem() {Text = "地址", Value = "地址", Selected = false},
           new SelectListItem() {Text = "Email", Value = "Email", Selected = false},
           new SelectListItem() {Text = "category", Value = "category", Selected = false},
    });

            SelectList col = new SelectList(colname, "Value", "Text");

            ViewBag.col = col;

            List<SelectListItem> mysort = new List<SelectListItem>();

            mysort.AddRange(new[]{
        new SelectListItem() {Text = "ASC", Value = "ASC", Selected = true},
        new SelectListItem() {Text = "DESC", Value = "DESC", Selected = false},

    });

            SelectList sort = new SelectList(mysort, "Value", "Text");

            ViewBag.sort = sort;
            return View();
        }

        [HttpPost]
        public FileResult Export()
        {
             
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("客戶名稱"),
                                            new DataColumn("類別"),
                                            new DataColumn("統一編號"),
                                            new DataColumn("電話"),
                new DataColumn("傳真"),
                new DataColumn("地址"),
            new DataColumn("Email")});

            var customers = repo.All();

            foreach (var customer in customers)
            {
                dt.Rows.Add(customer.客戶名稱, customer.category, customer.統一編號, customer.電話,customer.傳真,customer.地址, customer.Email);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        public JsonResult IsEmailExists(string Email)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!db.客戶資料.Any(x => x.Email == Email), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
