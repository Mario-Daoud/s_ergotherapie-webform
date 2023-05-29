using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories;
using ergo_web2_2023.Repositories.Interfaces;
using ergo_web2_2023.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Services
{
    public class QuestionService: IBasicOperationService<Question>, IQuestionService<Question>
    {
        private IBasicOperationsDAO<Question> _questionDAO;
        private IQuestionDAO<Question> _qDAO;
        public QuestionService(IBasicOperationsDAO<Question> questionDAO, IQuestionDAO<Question> qDAO)
        {
            _questionDAO = questionDAO;
            _qDAO = qDAO;
        }

        public async Task<IEnumerable<Question>?> GetAll()
        {
            return await _questionDAO.GetAll();
        }

        public async Task Add(Question entity)
        {
            await _questionDAO.Add(entity);
        }

        public async Task<Question?> FindById(int id)
        {
            return await _questionDAO.FindById(id);
        }

        public async Task Update(Question entity)
        {
            await _questionDAO.Update(entity);
        }

        public async Task Delete(Question entity)
        {
            await _questionDAO.Delete(entity);
        }

        public async Task<IEnumerable<Question?>> GetQuestionsByFormIdAsync(int v, int id)
        {
            return await _qDAO.GetQuestionsByFormIdAsync(v, id);
        }

    }
}
