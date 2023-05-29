using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class Form
    {
        public Form()
        {
            FormQuestions = new HashSet<FormQuestion>();
        }

        public int FormId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<FormQuestion> FormQuestions { get; set; }
    }
}
