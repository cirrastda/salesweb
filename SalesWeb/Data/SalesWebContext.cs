using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Models;

namespace SalesWeb.Data
{
    public class SalesWebContext : DbContext
    {
        public SalesWebContext (DbContextOptions<SalesWebContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }

        public ICollection<object> GetAllTables()
        {
            // Find method to get entities:
            var model = this.GetType().GetProperty("Model");
            var searchMethod = model.PropertyType.GetMethod("GetEntityTypes");

            // Get registered entities:
            var entities = searchMethod.Invoke(model.GetValue(this.GetType(), null), null) as List<object>;

            return entities;
        }
    }
}
