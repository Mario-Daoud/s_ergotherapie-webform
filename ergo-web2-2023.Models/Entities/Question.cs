using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class Question
    {
        public Question()
        {
            FormQuestions = new HashSet<FormQuestion>();
            QuestionOptions = new HashSet<QuestionOption>();
            SubquestionQuestions = new HashSet<Subquestion>();
            SubquestionSubQuestions = new HashSet<Subquestion>();
        }

        public int QuestionId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Type { get; set; }
        public string? Hint { get; set; }

        public virtual ICollection<FormQuestion> FormQuestions { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<Subquestion> SubquestionQuestions { get; set; }
        public virtual ICollection<Subquestion> SubquestionSubQuestions { get; set; }
    }
}
