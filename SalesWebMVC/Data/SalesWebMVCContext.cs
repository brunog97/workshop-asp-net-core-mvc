﻿using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Models
{
    public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext (DbContextOptions<SalesWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
        public DbSet<Seller> Seller { get; set; }
    }
}
