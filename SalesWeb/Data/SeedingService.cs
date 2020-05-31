using SalesWeb.Models;
using SalesWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Data
{
    public class SeedingService
    {
        private SalesWebContext _context;

        public SeedingService(SalesWebContext context)
        {
            _context = context;
        }



        public void Seed()
        {

            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                //Já foi populado
                return;
            }
            List<Department> departments = new List<Department>();
            departments.Add(new Department(1,"Computers"));
            departments.Add(new Department(2,"Electronics"));
            departments.Add(new Department(3,"Books"));
            departments.Add(new Department(4,"Tools"));

            List<Seller> sellers = new List<Seller>();
            sellers.Add(new Seller(1, "Alex Hunter", "alex@bayern.co.de",DateTime.Parse("21/09/1995"),2300,departments[1] ));
            sellers.Add(new Seller(2, "James Suneoka", "james.suneoka@bustamove.co.jp", DateTime.Parse("12/04/1974"), 1900, departments[2]));
            sellers.Add(new Seller(3, "Cloud Strife", "cloud@shinra.com", DateTime.Parse("01/07/1982"), 3700, departments[3]));
            sellers.Add(new Seller(4, "Zack Fair", "zack@shinra.com", DateTime.Parse("01/08/1978"), 6400, departments[3]));
            sellers.Add(new Seller(5, "Genesis Rhapsodos", "genesis@shinra.com", DateTime.Parse("01/06/1972"), 12500, departments[3]));
            sellers.Add(new Seller(6, "Bill Gates", "bill@microsoft.com", DateTime.Parse("21/09/1960"), 400000, departments[0]));

            List<SalesRecord> records = new List<SalesRecord>();
            records.Add(new SalesRecord(1, DateTime.Parse("25/09/2018"), 11000.0, SaleStatus.Billed, sellers[0]));
            records.Add(new SalesRecord(2, DateTime.Parse("04/09/2018"), 7000, SaleStatus.Canceled, sellers[1]));
            records.Add(new SalesRecord(3, DateTime.Parse("13/09/2018"), 4000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(4, DateTime.Parse("01/09/2018"), 8000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(5, DateTime.Parse("21/09/2018"), 3000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(6, DateTime.Parse("15/09/2018"), 2000.0, SaleStatus.Billed, sellers[4]));
            records.Add(new SalesRecord(7, DateTime.Parse("28/09/2018"), 13000.0, SaleStatus.Billed, sellers[4]));
            records.Add(new SalesRecord(8, DateTime.Parse("11/09/2018"), 4000.0, SaleStatus.Billed, sellers[3]));
            records.Add(new SalesRecord(9, DateTime.Parse("14/09/2018"), 11000.0, SaleStatus.Billed, sellers[0]));
            records.Add(new SalesRecord(10, DateTime.Parse("01/09/2018"), 8500, SaleStatus.Pending, sellers[0]));
            records.Add(new SalesRecord(11, DateTime.Parse("05/09/2018"), 7000.0, SaleStatus.Billed, sellers[1]));
            records.Add(new SalesRecord(12, DateTime.Parse("07/09/2018"), 4000.0, SaleStatus.Billed, sellers[5]));
            records.Add(new SalesRecord(13, DateTime.Parse("08/09/2018"), 21000.0, SaleStatus.Billed, sellers[1]));
            records.Add(new SalesRecord(14, DateTime.Parse("09/09/2018"), 10000.0, SaleStatus.Billed, sellers[3]));
            records.Add(new SalesRecord(15, DateTime.Parse("11/09/2018"), 8000.0, SaleStatus.Pending, sellers[3]));
            records.Add(new SalesRecord(16, DateTime.Parse("14/09/2018"), 3000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(17, DateTime.Parse("16/09/2018"), 2000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(19, DateTime.Parse("18/09/2018"), 1000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(20, DateTime.Parse("22/09/2018"), 1000.0, SaleStatus.Pending, sellers[1]));
            records.Add(new SalesRecord(21, DateTime.Parse("25/09/2018"), 1000.0, SaleStatus.Billed, sellers[1]));
            records.Add(new SalesRecord(22, DateTime.Parse("29/09/2018"), 4000.0, SaleStatus.Billed, sellers[1]));
            records.Add(new SalesRecord(23, DateTime.Parse("01/10/2018"), 4500.0, SaleStatus.Billed, sellers[0]));
            records.Add(new SalesRecord(24, DateTime.Parse("04/10/2018"), 11000.0, SaleStatus.Canceled, sellers[0]));
            records.Add(new SalesRecord(25, DateTime.Parse("08/10/2018"), 10000.0, SaleStatus.Billed, sellers[0]));
            records.Add(new SalesRecord(26, DateTime.Parse("03/10/2018"), 8000.0, SaleStatus.Billed, sellers[5]));
            records.Add(new SalesRecord(27, DateTime.Parse("15/10/2018"), 5000.0, SaleStatus.Billed, sellers[5]));
            records.Add(new SalesRecord(28, DateTime.Parse("18/10/2018"), 2000.0, SaleStatus.Canceled, sellers[5]));
            records.Add(new SalesRecord(29, DateTime.Parse("21/10/2018"), 21000.0, SaleStatus.Billed, sellers[2]));
            records.Add(new SalesRecord(30, DateTime.Parse("25/10/2018"), 2000.0, SaleStatus.Billed, sellers[2]));

            foreach (Department d in departments) { _context.Department.Add(d); }
            foreach (Seller s in sellers) { _context.Seller.Add(s); }
            foreach (SalesRecord sr in records) { _context.SalesRecord.Add(sr); }

            _context.SaveChanges();
        }
    }
}
