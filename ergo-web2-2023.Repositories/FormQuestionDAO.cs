using ergo_web2_2023.Models.Data;
using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories
{
    public class FormQuestionDAO: IBasicOperationsDAO<FormQuestion>, IFormQuestionDAO<FormQuestion>
    {
        private readonly ErgoDbContext _dbContext;

        public FormQuestionDAO()
        {
            _dbContext = new ErgoDbContext();
        }
        
        public async Task Add(FormQuestion entity)
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
        
        public async Task<IEnumerable<FormQuestion>?> GetQuestionsOfForm(int formId)
        {
            try
            {
                return await _dbContext.FormQuestions.Where(f => f.FormId == formId).Include(a => a.Group).Include(a => a.Question).ThenInclude(a => a.QuestionOptions).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<FormQuestion>?> GetAll()
        {
            try
            {
                return await _dbContext.FormQuestions
                    .Include(e => e.Question)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong... {ex}");
                throw;
            }
        }

        public async Task Delete(FormQuestion entity)
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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

        public async Task<IEnumerable<FormQuestion>?> GetFormQuestionsAsync(int id)
        {
            try
            {
                return await _dbContext.FormQuestions.Where(a => a.FormId == id)
                    .Include(e => e.Question)
                    .ToListAsync();
            }
            catch (Exception ex)
            { throw new Exception("error in DAO"); }
        }

        public async Task<IEnumerable<int>?> GetFormQuestionIDsAsync(int id)
        {
            try
            {
                return await _dbContext.FormQuestions
                    .Where(a => a.FormId == id)
                    .Select(a => a.QuestionId)
                    .ToListAsync();
            }
            catch (Exception ex)
            { throw new Exception("error in DAO"); }
        }


        public async Task Update(FormQuestion entity)
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

        public async Task<FormQuestion?> GetFormQuestionAsync(int qid, int fid)
        {
            try
            {
                return await _dbContext.FormQuestions.Where(a => a.QuestionId == qid).Where(a => a.FormId == fid).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error in DAO"); }
        }

        public async Task<FormQuestion?> GetFormQuestionByFormIdAndOrder(int order, int fid)
        {
            try
            {
                return await _dbContext.FormQuestions
                    .Where(a => a.FormId == fid)
                    .Where(a => a.QuestionOrder == order)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception("error in DAO"); }
        }


        public Task<FormQuestion> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FormQuestion>?> GetFormQuestionsOfGroup(int groupId)
        {
            try
            {
                return await _dbContext.FormQuestions
                    .Where(a => a.GroupId == groupId)
                    .Include(e => e.Question)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("error in DAO");
            }
        }
    }
}
