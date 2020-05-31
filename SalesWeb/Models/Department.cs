using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWeb.Models
{
    public class Department
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() {}

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            this.Sellers.Add(seller);
        }
        public void RemoveSeller(Seller seller)
        {
            this.Sellers.Remove(seller);
        }

        public double TotalSales(DateTime startDate, DateTime endDate)
        {
            return Sellers.Sum(seller => seller.TotalSales(startDate, endDate));
        }
    }

}
