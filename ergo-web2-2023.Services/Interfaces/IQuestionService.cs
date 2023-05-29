using ergo_web2_2023.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories.Interfaces
{
    public interface IQuestionService<T> where T : class
    {
        Task<IEnumerable<Question?>> GetQuestionsByFormIdAsync(int v, int id);
    }
}
