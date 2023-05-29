using ergo_web2_2023.Models.Entities;

namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class OptionsVM
    {
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public IEnumerable<QuestionOption> Options { get; set; }
        public string NewOptionText { get; set; }
    }
}
