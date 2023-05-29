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
    public class FormGroupService: IBasicOperationService<FormGroup>
    {
        private IBasicOperationsDAO<FormGroup> _formGroupDAO;
        public FormGroupService(IBasicOperationsDAO<FormGroup> formGroupDAO)
        {
            _formGroupDAO = formGroupDAO;
        }


        public async Task Add(FormGroup entity)
        {
            await _formGroupDAO.Add(entity);
        }

        public async Task Delete(FormGroup entity)
        {
            await _formGroupDAO.Delete(entity);
        }

        public async Task<FormGroup?> FindById(int id)
        {
            return await _formGroupDAO.FindById(id);
        }

        public async Task<IEnumerable<FormGroup>?> GetAll()
        {
            return await _formGroupDAO.GetAll();
        }
        public async Task Update(FormGroup entity)
        {
            await _formGroupDAO.Update(entity);
        }

    }
}
