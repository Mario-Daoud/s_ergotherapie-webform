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
    public class QuestionDAO : IBasicOperationsDAO<Question>, IQuestionDAO<Question>
    {
        private readonly ErgoDbContext _dbContext;

        public QuestionDAO()
        {
            _dbContext = new ErgoDbContext();
        }

        public async Task<IEnumerable<Question>?> GetAll()
        {
            try
            {
                return await _dbContext.Questions.Include(q => q.QuestionOptions).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }

        public async Task Add(Question entity)
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

        public async Task<Question?> FindById(int id)
        {
            try
            {
                return await _dbContext.Questions.Where(a => a.QuestionId == id)
                    .Include(q => q.QuestionOptions)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error in DAO"); }
        }


        public async Task Update(Question entity)
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

        public Task<IEnumerable<Question?>> GetQuestionsByFormIdAsync(int v, int id)
        {
            throw new NotImplementedException();
        }


        public async Task Delete(Question entity)
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
    }
}
