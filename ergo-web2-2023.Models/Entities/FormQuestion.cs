using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class FormQuestion
    {
        public int FormId { get; set; }
        public int QuestionId { get; set; }
        public int? QuestionOrder { get; set; }
        public int? GroupId { get; set; }

        public virtual Form Form { get; set; } = null!;
        public virtual FormGroup? Group { get; set; }
        public virtual Question Question { get; set; } = null!;
    }
}
