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
    public class QuestionOptionDAO: IQuestionOptionDAO<QuestionOption>
    {
        private readonly ErgoDbContext _dbContext;

        public QuestionOptionDAO()
        {
            _dbContext = new ErgoDbContext();
        }

        public async Task<IEnumerable<QuestionOption>?> GetOptionsOfQuestion(int questionId)
        {
            try
            {
                return await _dbContext.QuestionOptions.Where(o => o.QuestionId == questionId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<ICollection<QuestionOption>?> GetAll()
        {
            try
            {
                return await _dbContext.QuestionOptions.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task CreateOption(QuestionOption entity)
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

        public async Task Delete(QuestionOption entity)
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

        public async Task<QuestionOption?> FindById(int id)
        {
            try
            {
                return await _dbContext.QuestionOptions.Where(o => o.OptionId == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
