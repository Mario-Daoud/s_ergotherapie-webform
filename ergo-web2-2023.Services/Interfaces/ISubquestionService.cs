using ergo_web2_2023.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories.Interfaces
{
    public interface ISubquestionService<T> where T : class
    {
        Task<Subquestion?> GetSubquestionOfOption(int optionId);

        Task<ICollection<Subquestion>?> GetSubquestionByQuestion(int questionId);
    }
}
