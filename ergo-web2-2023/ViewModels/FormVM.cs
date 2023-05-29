using ergo_web2_2023.ViewModels;

namespace ergo_web2_2023.ViewModels
{
    public class FormVM
    {
        public int FormId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public IEnumerable<FormQuestionVM> FormQuestionsVM { get; set; }
    }
}
