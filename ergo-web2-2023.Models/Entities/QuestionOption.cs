using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class QuestionOption
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string Optiontext { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Question Question { get; set; } = null!;
    }
}
