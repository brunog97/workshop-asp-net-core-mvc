using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;
using System.Threading.Tasks;
using System;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _salesWebMVCContext;

        public SellerService(SalesWebMVCContext salesWebMVCContext)
        {
            _salesWebMVCContext = salesWebMVCContext;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            // Acessa tudo e converte a lista
           // Operação sincrona... App fica bloqueada 
           // Mais pra frente ficara assincrona
            return await  _salesWebMVCContext.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _salesWebMVCContext.Add(seller);
            await _salesWebMVCContext.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _salesWebMVCContext.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _salesWebMVCContext.Seller.FindAsync(id);
            try
            {
                _salesWebMVCContext.Seller.Remove(obj);
                await _salesWebMVCContext.SaveChangesAsync();

            }catch(DbUpdateException e)
            {
                string mensagem = "";
                switch (e.Message)
                {
                    case "An error occurred while updating the entries. See the inner exception for details.":
                        mensagem = "Você não pode remover este vendedor porque ele possui vendas." + "\n" + e.InnerException ;
                        break;
                    default:
                        mensagem = e.Message;
                        break;
                }
                       
                throw new IntegrityException(mensagem);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _salesWebMVCContext.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _salesWebMVCContext.Update<Seller>(obj);
                await _salesWebMVCContext.SaveChangesAsync();

            }catch(DbUpdateConcurrencyException e)
            {
                // Lançando minha excessão 
                // "Segregando camadas"
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
