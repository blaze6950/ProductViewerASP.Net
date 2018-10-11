using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ProductViewer.WebUI.Models;
using ProductViewer.WebUI.Repository;
using ProductViewer.WebUI.ViewModels;

namespace ProductViewer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ServiceRepository _service;

        public HomeController()
        {
            _service = new ServiceRepository();
        }

        public ActionResult Index()
        {
            if (Request != null && Request.HttpMethod.ToUpper().Equals("GET"))
            {
                return View();
            }
            return HttpNotFound("Http method is POST, but required GET");
        }

        public ActionResult RemoveItem(int id)
        {
            if (Request != null && Request.HttpMethod.ToUpper().Equals("POST"))
            {
                HttpResponseMessage response = _service.DeleteResponse("api/Products/Delete?id=" + id.ToString());
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Home");
            }
            return HttpNotFound("Http method is GET, but required POST");
        }

        public object AddOrEditProduct(ProductViewModel product = null, bool isEditing = false, int id = -1)
        {
            if (Request != null && Request.HttpMethod.ToUpper().Equals("GET"))
            {
                if (isEditing)
                {
                    ViewBag.Title = "Editing an existing product";
                    HttpResponseMessage response = _service.GetResponse("api/Products/GetProduct?id=" + id.ToString());
                    response.EnsureSuccessStatusCode();
                    ProductViewModel product1 = response.Content.ReadAsAsync<ProductViewModel>().Result;
                    return PartialView("Partials/AddOrEditProduct", product1);
                }
                else
                {
                    ViewBag.Title = "Adding new product";
                    return PartialView("Partials/AddOrEditProduct");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (product.ProductEntityId != 0)
                        {
                            HttpResponseMessage response = _service.PutResponse("api/Products/Update", product);
                            response.EnsureSuccessStatusCode();
                            return true;
                        }
                        else
                        {
                            HttpResponseMessage response = _service.PostResponse("api/Products/Create", product);
                            response.EnsureSuccessStatusCode();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        TempData["error"] = string.Format("{0} has not been saved! Error message: {1}", product.ProductEntityName, e.Message);
                        return PartialView("Partials/AddOrEditProduct", product);
                    }
                }
                else
                {
                    // there is something wrong with the data values
                    return PartialView("Partials/AddOrEditProduct", product);
                }
            }
        }


        public ViewResult ProductDetails(int id)
        {
            HttpResponseMessage response = _service.GetResponse("api/Products/GetProduct?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            ProductViewModel product = response.Content.ReadAsAsync<ProductViewModel>().Result;
            return View(product);
        }
    }
}