using ergo_web2_2023.Models.Entities;
using ergo_web2_2023.Models.Enums;

namespace ergo_web2_2023.ViewModels
{
    public class QuestionsVM
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public QuestionType Type { get; set; }
        public string? Hint { get; set; }
        public int? OptionId { get; set; }  
        public int? SuperQuestionId { get; set; }
        public char? subQuestionCharacter { get; set; }
        public int? GroupId { get; set; }
        public Boolean IsSubquestion { get; set; }
        public IEnumerable<QuestionOptionsVM> QuestionOptions { get; set; }
    }
}
