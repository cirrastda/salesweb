using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            
            return _context.Seller
                .Include(item => item.Department)
                .OrderBy(item => item.Name)
                .ToList();
        }

        public Seller FindById(int id)
        {
            
            return _context.Seller
                .Include(item => item.Department)
                .FirstOrDefault(item => item.Id == id);
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            Seller seller = this.FindById(id);
            if (seller != null)
            {
                _context.Remove(seller);
                _context.SaveChanges();
                return;
            } else
            {
                throw new NotFoundException("Seller not found");
            }
            
        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(item => item.Id == seller.Id))
            {
                throw new NotFoundException("Seller not found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            } catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
