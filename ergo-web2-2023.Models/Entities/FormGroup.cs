using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class FormGroup
    {
        public FormGroup()
        {
            FormQuestions = new HashSet<FormQuestion>();
        }

        public int GroupId { get; set; }
        public string Title { get; set; } = null!;
        public int? GroupOrder { get; set; }

        public virtual ICollection<FormQuestion> FormQuestions { get; set; }
    }
}
