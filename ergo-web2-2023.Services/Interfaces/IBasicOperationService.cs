using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Services.Interfaces
{
    public interface IBasicOperationService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<T> FindById(int Id);
    }
}
