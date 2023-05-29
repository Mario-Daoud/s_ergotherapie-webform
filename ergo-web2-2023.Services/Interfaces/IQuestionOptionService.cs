using ergo_web2_2023.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories.Interfaces
{
    public interface IQuestionOptionService<T> where T : class
    {
        Task<IEnumerable<T>?> GetOptionsOfQuestion(int questionId);

        Task CreateOption(T entity);

        Task Delete(T entity);

        Task<T?> FindById(int id);

        Task<IEnumerable<T>> GetAll();
    }
}
