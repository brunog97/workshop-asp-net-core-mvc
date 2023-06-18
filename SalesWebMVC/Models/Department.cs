using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Descrição")]
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        // constructor common
        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Department(string name)
        {
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
