using ergo_web2_2023.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;

namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class SubquestionEditVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Subquestion")]
        public int SubQuestionId { get; set; }


        public IEnumerable<SelectListItem>? Subvragen { get; set; }

        [DisplayName("Question")]
        public int QuestionId { get; set; }

        public IEnumerable<SelectListItem>? Vragen { get; set; }

        [DisplayName("Option ID")]
        public int OptionId { get; set; }

        public IEnumerable<SelectListItem>? Options { get; set; }
    }
}
