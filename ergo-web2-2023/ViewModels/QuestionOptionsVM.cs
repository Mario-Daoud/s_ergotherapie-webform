using ergo_web2_2023.ViewModels;

namespace ergo_web2_2023.ViewModels
{
    public class QuestionOptionsVM
    {
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public string? Description { get; set; }

    }
}
