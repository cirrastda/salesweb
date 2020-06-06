using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Sales Records";
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? start, DateTime? end)
        {
            ViewData["Title"] = "Sales Records - Simple Search";
            if (!start.HasValue) { start = new DateTime(DateTime.Now.Year, 1, 1); }
            if (!end.HasValue) { end = DateTime.Now; }
            ViewData["start"] = start.Value.ToString("dd/MM/yyyy");
            ViewData["end"] = end.Value.ToString("dd/MM/yyyy");
            
            var salesRecords = await _salesRecordService.FindByDateAsync(start, end);
            return View(salesRecords);
        }

        public async Task<IActionResult> GroupSearch(DateTime? start, DateTime? end)
        {
            ViewData["Title"] = "Sales Records - Group Search";
            if (!start.HasValue) { start = new DateTime(DateTime.Now.Year, 1, 1); }
            if (!end.HasValue) { end = DateTime.Now; }
            ViewData["start"] = start.Value.ToString("dd/MM/yyyy");
            ViewData["end"] = end.Value.ToString("dd/MM/yyyy");

            var salesRecords = await _salesRecordService.FindByDateGroupAsync(start, end);
            return View(salesRecords);
        }
    }
}
