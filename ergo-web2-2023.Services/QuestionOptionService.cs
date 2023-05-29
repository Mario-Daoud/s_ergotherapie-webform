using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Repositories;
using ergo_web2_2023.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Services
{
    public class QuestionOptionService: IQuestionOptionService<QuestionOption>
    {
        private IQuestionOptionDAO<QuestionOption> _questionOptionDAO;
        public QuestionOptionService(IQuestionOptionDAO<QuestionOption> questionOptionDAO)
        {
            _questionOptionDAO = questionOptionDAO;
        }

        public async Task<IEnumerable<QuestionOption>?> GetOptionsOfQuestion(int questionId)
        {
            return await _questionOptionDAO.GetOptionsOfQuestion(questionId);
        }

        public async Task CreateOption(QuestionOption entity)
        {
            await _questionOptionDAO.CreateOption(entity);
        }

        public async Task Delete(QuestionOption entity)
        {
            await _questionOptionDAO.Delete(entity);
        }

        public async Task<QuestionOption?> FindById(int id)
        {
            return await _questionOptionDAO.FindById(id);
        }

        public async Task<IEnumerable<QuestionOption>> GetAll()
        {
            return await _questionOptionDAO.GetAll();
        }
    }
}
