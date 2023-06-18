using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _salesWebMVCContext;

        public SalesRecordService(SalesWebMVCContext salesWebMVCContext)
        {
            _salesWebMVCContext = salesWebMVCContext;
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //esta consulta não é executada ao ser definida
            var result = from obj in _salesWebMVCContext.SalesRecords select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                   .Include(x => x.Seller)
                   .Include(x => x.Seller.Department)
                   .OrderByDescending(x => x.Date)
                   .GroupBy(x => x.Seller.Department)
                   .ToListAsync();

        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //esta consulta não é executada ao ser definida
            var result = from obj in _salesWebMVCContext.SalesRecords select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                   .Include(x => x.Seller)
                   .Include(x => x.Seller.Department)
                   .OrderByDescending(x => x.Date)
                   .ToListAsync();
            
        }
    }
}
