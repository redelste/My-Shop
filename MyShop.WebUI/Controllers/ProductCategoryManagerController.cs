using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        //Creates an instance of the product repository
        InMemoryRepository<ProductCategory> context;
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            //Returns a list of products and sent  back in through the view. 
            List<ProductCategory> productCateogries = context.Collection().ToList();
            return View(productCateogries);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            //Thsi will try to load the product from the database so we use the .find method
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                //if it's not found, we return an http error 
                return HttpNotFound();
            }
            else
            {
                //else We return the product with the view we have found
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            // Find the product in the DB and load it. 
            ProductCategory producCategorytToEdit = context.Find(Id);

            // If the product is null, return the http error.
            if (producCategorytToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //if the model is in the wrong state, return the original product.
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }

                //here we set the product == to the product we're going to edit
                producCategorytToEdit.Category = productCategory.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}