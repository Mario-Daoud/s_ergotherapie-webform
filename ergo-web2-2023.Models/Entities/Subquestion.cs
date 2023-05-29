using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class Subquestion
    {
        public int Id { get; set; }
        public int SubQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual Question SubQuestion { get; set; } = null!;
    }
}
