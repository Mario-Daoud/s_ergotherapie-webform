using ergo_web2_2023.Models.Data;
using ergo_web2_2023.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ergo_web2_2023.Repositories.Interfaces;

namespace ergo_web2_2023.Repositories
{
    public class FormDAO : IBasicOperationsDAO<Form>
    {
        private readonly ErgoDbContext _dbContext;

        public FormDAO()
        {
            _dbContext = new ErgoDbContext();
        }

        public async Task<IEnumerable<Form>?> GetAll()
        {
            try
            {
                return await _dbContext.Forms.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong... {ex}");
                throw;
            }
        }
        
        public async Task Add(Form entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Form?> FindById(int id)
        {
            try
            {

                return await _dbContext.Forms.Where(a => a.FormId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error DAO"); }
        }

        public async Task Delete(Form entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Update(Form entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
