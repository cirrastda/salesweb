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

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Sellers";
            var sellers = await _sellerService.FindAllAsync();
            return View(sellers);
        }

        private async Task<IActionResult> CreateViewModel(String title, Seller seller = null)
        {
            var departments = await _departmentService.FindAllAsync();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            if (seller != null)
            {
                ViewModel.Seller = seller;
            }
            
            ViewData["Title"] = title;
            return View(ViewModel);

        }
        public async Task<IActionResult> Create()
        {
            return await this.CreateViewModel("Add Seller");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return await this.CreateViewModel("Add Seller", seller);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToError("Empty Id");
            }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null) { return RedirectToError("Id not found"); }
            ViewData["Title"] = "Delete Seller";
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            } catch(IntegrityException e)
            {
                return RedirectToError(e.Message);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return RedirectToError("Empty Id"); }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null) { return RedirectToError("Id not found"); }
            ViewData["Title"] = "Seller Details";
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return RedirectToError("Empty Id"); }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null) { RedirectToError("Id not found"); }

            return await this.CreateViewModel("Edit Seller", seller);
        }

        private IActionResult RedirectToError(string message)
        {
            return RedirectToAction(nameof(Error), new { message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id == null) { return RedirectToError("Empty Id"); }
            if (id != seller.Id) { return RedirectToError("The provided Id is different of current seller id"); }
            try { 
                await _sellerService.UpdateAsync(seller);
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
 