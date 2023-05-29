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
    public class FormService: IBasicOperationService<Form>
    {
        private IBasicOperationsDAO<Form> _formDAO;
        public FormService(IBasicOperationsDAO<Form> formDAO)
        {
            _formDAO = formDAO;
        }
        
        public async Task<IEnumerable<Form>?> GetAll()
        {
            return await _formDAO.GetAll();
        }
        public async Task<Form?> FindById(int id)
        {
            return await _formDAO.FindById(id);
        }

        public async Task Add(Form entity)
        {
            await _formDAO.Add(entity);
        }

        public async Task Delete(Form entity)
        {
            await _formDAO.Delete(entity);
        }

        public async Task Update(Form entity)
        {
            await _formDAO.Update(entity);
        }
    }
}
