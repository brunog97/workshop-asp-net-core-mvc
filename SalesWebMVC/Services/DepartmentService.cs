using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _salesWebMVCContext;

        public DepartmentService(SalesWebMVCContext salesWebMVCContext)
        {
            _salesWebMVCContext = salesWebMVCContext;
        }


        public async Task<List<Department>> FindAllAsync()
        {
            return await _salesWebMVCContext.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
