using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(128,MinimumLength = 3,ErrorMessage ="{0} length must be between {2} and {1}")]
        public String Name { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        [StringLength(128, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}")]
        [EmailAddress(ErrorMessage = "{0} must be a valid -email")]
        public String Email { get; set; }
        
        [Display (Name="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} required")]
        [Range(0, 1000000, ErrorMessage = "{0} must be between {1} and {2}")]
        public double Salary { get; set; }

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double salary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime startDate, DateTime endDate)
        {
            return Sales.Where(item => item.Date >= startDate && item.Date <= endDate)
                        .Select(item => item.Amount)
                        .DefaultIfEmpty(0.0)
                        .Sum();
        }
    }
}
