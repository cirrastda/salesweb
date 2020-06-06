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

        public async Task<List<Seller>> FindAllAsync()
        {

            return await _context.Seller
                .Include(item => item.Department)
                .OrderBy(item => item.Name)
                .ToListAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {

            return await _context.Seller
                .Include(item => item.Department)
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await this.FindByIdAsync(id);
            if (seller != null)
            {
                try
                {
                    _context.Remove(seller);
                    await _context.SaveChangesAsync();
                    return;
                } catch(DbUpdateException e)
                {
                    throw new IntegrityException(e.Message);
                }
            }
            else
            {
                throw new NotFoundException("Seller not found");
            }

        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(item => item.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Seller not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
        /*
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
        */
    }
}
