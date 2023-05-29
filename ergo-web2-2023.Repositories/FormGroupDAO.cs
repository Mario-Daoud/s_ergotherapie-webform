using ergo_web2_2023.Models.Data;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories
{
    public class FormGroupDAO: IBasicOperationsDAO<FormGroup>
    {

        private readonly ErgoDbContext _dbContext;

        public FormGroupDAO()
        {
            _dbContext = new ErgoDbContext();
        }

        public async Task Add(FormGroup entity)
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

        public async Task Delete(FormGroup entity)
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

        public async Task<FormGroup?> FindById(int id)
        {
            try
            {
                return await _dbContext.FormGroups.Where(a => a.GroupId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<FormGroup>?> GetAll()
        {
            try
            {
                return await _dbContext.FormGroups.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }

        public async Task Update(FormGroup entity)
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
