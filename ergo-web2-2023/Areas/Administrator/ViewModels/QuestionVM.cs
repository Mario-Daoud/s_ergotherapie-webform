using Microsoft.Build.Framework;
using System.ComponentModel;

namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class QuestionVM
    {
        [DisplayName("Question")]
        public int QuestionId { get; set; }
        
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Type { get; set; }

        public string? Hint { get; set; }
    }
}
