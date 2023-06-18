using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _salesWebMVCContext;

        public SeedingService(SalesWebMVCContext salesWebMVCContext)
        {
            _salesWebMVCContext = salesWebMVCContext;
        }

        public void Seed()
        {
            // Testa se existe algum registros
            if (_salesWebMVCContext.Seller.Any() ||
                _salesWebMVCContext.Department.Any() ||
                _salesWebMVCContext.SalesRecords.Any() )
            {
                return; //DB has been seeded
            }

            Department d1 = new Department("Computers");
            Department d2 = new Department("Electronics");
            Department d3 = new Department("Fashion");
            Department d4 = new Department("Books");

            Seller s1 = new Seller("Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller("Maria Green", "mariagreen@gmail.com", new DateTime(1979, 3, 21), 1000.0, d1);

            SalesRecord sr1 = new SalesRecord(new DateTime(2023, 06, 15), 11000.0, SalesStatus.Billed, s1);


            //AddRange permite que add varios objetos
            _salesWebMVCContext.Department.AddRange(d1, d2, d3, d4);

            _salesWebMVCContext.Seller.AddRange(s1,s2);

            _salesWebMVCContext.SalesRecords.Add(sr1);

            _salesWebMVCContext.SaveChanges();

        }
    }
}
