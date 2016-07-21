﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TipTeam.Models;

namespace TipTeam.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "TipTeam";
            return View();
        }
        public ActionResult Products_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (var northwind = new memodbEntities())
            {
                IQueryable<Tip> products = northwind.Tips;
                DataSourceResult result = products.ToDataSourceResult(request);
                return Json(result);
            }
        }

        public ActionResult Products_Create([DataSourceRequest]DataSourceRequest request,
          TipViewModel tip)
        {
            if (ModelState.IsValid)
            {
                using (var memodb = new memodbEntities())
                {
                    // Create a new Product entity and set its properties from the posted ProductViewModel.
                    var entity = new Tip
                    {
                        data = tip.Data,
                        un = tip.Un
                    };
                    // Add the entity.
                    memodb.Tips.Add(entity);
                    // Insert the entity in the database.
                    memodb.SaveChanges();
                    // Get the ProductID generated by the database.
                    tip.Id = entity.id;
                }
            }
            // Return the inserted product. The grid needs the generated ProductID. Also return any validation errors.
            return Json(new[] { tip }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Products_Update([DataSourceRequest]DataSourceRequest request,
            TipViewModel tip)
        {
            if (ModelState.IsValid)
            {
                using (var memodb = new memodbEntities())
                {
                    // Create a new Product entity and set its properties from the posted ProductViewModel.
                    var entity = new Tip
                    {
                        id = tip.Id,
                        data = tip.Data,
                        un = tip.Un
                    };
                    // Attach the entity.
                    memodb.Tips.Attach(entity);
                    // Change its state to Modified so Entity Framework can update the existing product instead of creating a new one.
                    memodb.Entry(entity).State = EntityState.Modified;
                    // Or use ObjectStateManager if using a previous version of Entity Framework.
                    // northwind.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    // Update the entity in the database.
                    memodb.SaveChanges();
                }
            }
            // Return the updated product. Also return any validation errors.
            return Json(new[] { tip }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Products_Destroy([DataSourceRequest]DataSourceRequest request, 
            TipViewModel tip)
    {
        if (ModelState.IsValid)
        {
            using (var memodb = new memodbEntities())
            {
                // Create a new Product entity and set its properties from the posted ProductViewModel.
                var entity = new Tip
                {
                          id = tip.Id,
                        data = tip.Data,
                        un = tip.Un
                };
                // Attach the entity.
                memodb.Tips.Attach(entity);
                // Delete the entity.
                memodb.Tips.Remove(entity);
                // Or use DeleteObject if using a previous versoin of Entity Framework.
                // northwind.Products.DeleteObject(entity);
                // Delete the entity in the database.
                memodb.SaveChanges();
            }
        }
        // Return the removed product. Also return any validation errors.
        return Json(new[] { tip }.ToDataSourceResult(request, ModelState));
    }
    }


}