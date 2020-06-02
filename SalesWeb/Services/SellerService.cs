using Microsoft.AspNetCore.Mvc;
using SalesWeb.Data;
using SalesWeb.Models;
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
            return _context.Seller.ToList();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(item => item.Id == id);
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public bool Remove(int id)
        {
            Seller seller = this.FindById(id);
            if (seller != null)
            {
                _context.Remove(seller);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
