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
    public class FormQuestionService: IBasicOperationService<FormQuestion>, IFormQuestionService<FormQuestion>
    {
        private IBasicOperationsDAO<FormQuestion> _basicOperationDAO;
        private IFormQuestionDAO<FormQuestion> _formQuestionDAO;

        public FormQuestionService(IBasicOperationsDAO<FormQuestion> basicOperationDAO, IFormQuestionDAO<FormQuestion> formQuestionDAO)
        {
            _basicOperationDAO = basicOperationDAO;
            _formQuestionDAO = formQuestionDAO;
        }

        public async Task Add(FormQuestion entity)
        {
            await _basicOperationDAO.Add(entity);
        }
        
        public async Task<IEnumerable<FormQuestion>?> GetQuestionsOfForm(int formId)
        {
            return await _formQuestionDAO.GetQuestionsOfForm(formId);
        }

        public async Task<IEnumerable<FormQuestion>?> GetAll()
        {
            return await _basicOperationDAO.GetAll();
        }

        public async Task Delete(FormQuestion entity)
        {
            await _basicOperationDAO.Delete(entity);
        }

        public async Task<IEnumerable<FormQuestion>?> GetFormQuestionsAsync(int id)
        {
            return await _formQuestionDAO.GetFormQuestionsAsync(id);
        }

        public async Task<IEnumerable<int>?> GetFormQuestionIDsAsync(int id)
        {
            return await _formQuestionDAO.GetFormQuestionIDsAsync(id);
        }

        public async Task Update(FormQuestion entity)
        {
            await _basicOperationDAO.Update(entity);
        }

        public async Task<FormQuestion?> GetFormQuestionAsync(int qid, int fid)
        {
            return await _formQuestionDAO.GetFormQuestionAsync(qid, fid);
        }

        public async Task<FormQuestion?> GetFormQuestionByFormIdAndOrder(int order, int fid)
        {
            return await _formQuestionDAO.GetFormQuestionByFormIdAndOrder(order, fid);
        }

        public Task<FormQuestion> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FormQuestion>?> GetFormQuestionsOfGroup(int groupId)
        {
            return await _formQuestionDAO.GetFormQuestionsOfGroup(groupId);
        }
    }
}