using ergo_web2_2023.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class SubquestionVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Subquestion title")]
        public string SubQuestionTitle { get; set; }

        [DisplayName("Subquestion")]
        public int SubQuestionId { get; set; }

        [DisplayName("Question title")]
        public string QuestionTitle { get; set; }

        [DisplayName("Question")]
        public int QuestionId { get; set; }


        [DisplayName("Option ID")]
        public int OptionId { get; set; }

    }
}
