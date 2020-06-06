using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Sellers";
            List<Seller> sellers = _sellerService.FindAll();
            return View(sellers);
        }

        private IActionResult CreateViewModel(String title, Seller seller = null)
        {
            List<Department> departments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            if (seller != null)
            {
                ViewModel.Seller = seller;
            }
            
            ViewData["Title"] = title;
            return View(ViewModel);

        }
        public IActionResult Create()
        {
            return this.CreateViewModel("Add Seller");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return this.CreateViewModel("Add Seller", seller);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToError("Empty Id");
            }
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) { return RedirectToError("Id not found"); }
            ViewData["Title"] = "Delete Seller";
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) { return RedirectToError("Empty Id"); }
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) { return RedirectToError("Id not found"); }
            ViewData["Title"] = "Seller Details";
            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) { return RedirectToError("Empty Id"); }
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null) { RedirectToError("Id not found"); }

            return this.CreateViewModel("Edit Seller", seller);
        }

        private IActionResult RedirectToError(string message)
        {
            return RedirectToAction(nameof(Error), new { message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id == null) { return RedirectToError("Empty Id"); }
            if (id != seller.Id) { return RedirectToError("The provided Id is different of current seller id"); }
            try { 
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } catch(NotFoundException e) {
                return RedirectToError(e.Message);
            } catch(DbConcurrencyException e)
            {
                return RedirectToError("Another resource is using this registry. Cannot perform the action: "+e.Message);
                //return BadRequest();
            }
        }


        public IActionResult Error(string message)
        {
            var ViewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(ViewModel);
        }

    }
}
 