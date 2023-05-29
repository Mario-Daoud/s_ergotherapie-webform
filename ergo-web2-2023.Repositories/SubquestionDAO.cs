using ergo_web2_2023.Models.Data;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories
{
    public class SubquestionDAO : IBasicOperationsDAO<Subquestion>, ISubquestionDAO<Subquestion>
    {
        private readonly ErgoDbContext _dbContext;

        public SubquestionDAO()
        {
            _dbContext = new ErgoDbContext();
        }

        public async Task<IEnumerable<Subquestion>?> GetAll()
        {
            try
            {
                return await _dbContext.Subquestions.Include(b => b.SubQuestion).Include(b => b.Question).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw;
            }
        }


        public async Task<Subquestion?> FindById(int id)
        {
            try
            {
                var subquestion = await _dbContext.Subquestions.Where(a => a.Id == id).Include(b => b.SubQuestion).Include(b => b.Question).FirstOrDefaultAsync();
                if (subquestion == null)
                {
                    throw new Exception($"Subquestion with ID {id} not found");
                }
                return subquestion;
            }
            catch (Exception ex)
            {
                throw new Exception("error in DAO", ex);
            }
        }

        public async Task Add(Subquestion entity)
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

        public async Task Update(Subquestion entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<Subquestion?> GetSubquestionOfOption(int optionId)
        {
            try
            {
                return await _dbContext.Subquestions.Where(s => s.OptionId == optionId).Include(s => s.Question).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in DAO");
            }
        }

        public async Task<ICollection<Subquestion>?> GetSubquestionByQuestion(int questionId)
        {
            try
            {
                return await _dbContext.Subquestions.Where(s => s.QuestionId == questionId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in DAO");
            }
        }

        public async Task Delete(Subquestion entity)
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
