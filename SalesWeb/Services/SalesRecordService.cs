using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? start, DateTime? end)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (start.HasValue) {
                result = result.Where(item => item.Date >= start.Value);
            }
            if (end.HasValue)
            {
                result = result.Where(item => item.Date >= start.Value);
            }
            result = result.Include(item => item.Seller)
                            .Include(item => item.Seller.Department)
                            .OrderByDescending(item => item.Date);

            return await result.ToListAsync();
            /*
            return await _context.SalesRecord
                .Where(item => item.Date >= start.GetValueOrDefault() && item.Date <= end)
                .Include(item => item.Seller)
                .Include(item => item.Seller.Department)
                .OrderBy(item => item.Date)
                .ToListAsync();
            */
        }
    }
}
