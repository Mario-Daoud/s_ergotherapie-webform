using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories;
using ergo_web2_2023.Repositories.Interfaces;
using ergo_web2_2023.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Services
{
    public class SubquestionService: IBasicOperationService<Subquestion>, ISubquestionService<Subquestion>
    {
        private IBasicOperationsDAO<Subquestion> _basicDAO;
        private ISubquestionDAO<Subquestion> _subQuestionDAO;

        public SubquestionService(IBasicOperationsDAO<Subquestion> basicDAO, ISubquestionDAO<Subquestion> subQuestionDAO)
        {
            _basicDAO = basicDAO;
            _subQuestionDAO = subQuestionDAO;
        }

        public async Task<IEnumerable<Subquestion>?> GetAll()
        {
            return await _basicDAO.GetAll();
        }

        public async Task<Subquestion?> FindById(int id)
        {
            return await _basicDAO.FindById(id);
        }

        public async Task Add(Subquestion entity)
        {
            await _basicDAO.Add(entity);
        }
        public async Task Update(Subquestion entity)
        {
            await _basicDAO.Update(entity);
        }

        public async Task<Subquestion?> GetSubquestionOfOption(int optionId)
        {
            return await _subQuestionDAO.GetSubquestionOfOption(optionId);
        }

        public async Task<ICollection<Subquestion>?> GetSubquestionByQuestion(int questionId)
        {
            return await _subQuestionDAO.GetSubquestionByQuestion(questionId);
        }
        
        public async Task Delete(Subquestion entity)
        {
            await _basicDAO.Delete(entity);
        }
    }
}
