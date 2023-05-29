using ergo_web2_2023.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergo_web2_2023.Repositories.Interfaces
{
    public interface IFormQuestionService<T> where T : class
    {
        Task<IEnumerable<FormQuestion>?> GetQuestionsOfForm(int formId);

        Task<IEnumerable<FormQuestion>?> GetFormQuestionsAsync(int id);

        Task<IEnumerable<int>?> GetFormQuestionIDsAsync(int id);

        Task<FormQuestion?> GetFormQuestionAsync(int qid, int fid);

        Task<FormQuestion?> GetFormQuestionByFormIdAndOrder(int order, int fid);

        Task<IEnumerable<FormQuestion>?> GetFormQuestionsOfGroup(int groupId);
    }
}
