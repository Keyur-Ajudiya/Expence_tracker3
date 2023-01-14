using Expence_tracker3.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Validation;
using System.Web.Mvc;

namespace Expence_tracker3.Controllers
{
    public class ExpenceController : Controller
    {
        // GET: Expence
        Expence_trackerEntities dbObj= new Expence_trackerEntities();
        public ActionResult Expence(Category obj)
        {
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            Category obj = new Category();
            if (ModelState.IsValid) 
            {

                //obj.CategoryId = model.CategoryId;
                obj.CategoryName = model.CategoryName;
                obj.CategoryLimit = model.CategoryLimit;

                if (model.CategoryId == 0)
                {
                    dbObj.Categories.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified; 
                    dbObj.SaveChanges(); 
                }

                
            }
            
            ModelState.Clear();

            return View("Expence");
        }

       public ActionResult CategoryList() 
        {
            var res= dbObj.Categories.ToList();
            return View(res); 
        }
        public ActionResult Delete (int CategoryId)
        {
            var res = dbObj.Categories.Where(x => x.CategoryId== CategoryId).First();
            dbObj.Categories.Remove(res);
            dbObj.SaveChanges();

            var List = dbObj.Categories.ToList();

            return View("CategoryList", List);
        }
        public ActionResult CreateExp()
        {
            var list = dbObj.Categories.ToList();
            ViewBag.CategoryId = new SelectList(list, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult AddExpence(ExpenceData ex) 
        {
            ExpenceData obj = new ExpenceData();
            if (ModelState.IsValid)
            {
                
                obj.Title = ex.Title;
                obj.Description = ex.Description;
                obj.Amount= ex.Amount;
                obj.CategoryId = ex.CategoryId;
                obj.date_and_time = ex.date_and_time;

               
                
                dbObj.ExpenceDatas.Add(obj);
                dbObj.SaveChanges();
                
                //else
                //{
                //    dbObj.Entry(obj).State= EntityState.Modified;
                //    dbObj.SaveChanges();
                //}
                return RedirectToAction("ExpenceList");

                
            }
            ModelState.Clear();

            return View("CreateExp"); 
        }

        [HttpGet]
         public ActionResult EditExpence(int id)
        {
            var list = dbObj.Categories.ToList();
            ViewBag.CategoryId = new SelectList(list, "CategoryId", "CategoryName");

            var expdta = dbObj.ExpenceDatas.Where(x => x.ExpenceId == id).FirstOrDefault();
            return View(expdta);
        }

        [HttpPost]
        public ActionResult EditExpence(ExpenceData Ed)
        {
            var list = dbObj.Categories.ToList();
            ViewBag.CategoryId = new SelectList(list, "CategoryId", "CategoryName");

            ExpenceData Eobj = new ExpenceData();
            if(ModelState.IsValid)
            {
                Eobj.ExpenceId= Ed.ExpenceId;
                Eobj.Title = Ed.Title;
                Eobj.Description = Ed.Description;
                Eobj.Amount = Ed.Amount;
                Eobj.CategoryId = Ed.CategoryId;
                Eobj.date_and_time = Convert.ToString(DateTime.Now);

                dbObj.Entry(Eobj).State= EntityState.Modified;
               
               
                dbObj.SaveChanges();
                TempData["Msg"] = "Record Update Successful";

                return RedirectToAction("ExpenceList");
            }
            ModelState.Clear();
            return View();
        }
        public ActionResult ExpenceList()
        {
            var mes = dbObj.ExpenceDatas.ToList();

            return View(mes); 
        }
        public ActionResult DeleteEx (int ExpenceId)
        {
            var mes = dbObj.ExpenceDatas.Where(x => x.ExpenceId == ExpenceId).First();
            dbObj.ExpenceDatas.Remove(mes); 
            dbObj.SaveChanges();

            var List = dbObj.ExpenceDatas.ToList();

            return View("ExpenceList", List); 
        }

        public ActionResult Total_limit()
        { 
            return View();
        }

        public ActionResult Deshboard()
        {
            return View();
        }

       
        
        // GET: Expense

        [HttpGet]
        public ActionResult Totallimit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTotallimit(TotalLimit model)
        {
            TotalLimit obj = new TotalLimit();
            obj.Total_limit = model.Total_limit;

            dbObj.TotalLimits.Add(obj);
            dbObj.SaveChanges();
            return View();
        }



    }
}