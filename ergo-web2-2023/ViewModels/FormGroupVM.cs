using ergo_web2_2023.ViewModels;

namespace ergo_web2_2023.ViewModels
{
    public class FormGroupVM
    {
        public int GroupId { get; set; }

        public string Title { get; set; } = null!;
        public int? GroupOrder { get; set; }

        public IEnumerable<QuestionsVM>? Questions { get; set; }
        public int? OptionId { get; set; }
        public int? SuperQuestionId { get; set; }

        public char subquestionGroupChar { get; set; }
    }
}
