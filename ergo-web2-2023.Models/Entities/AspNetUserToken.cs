using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class AspNetUserToken
    {
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
    }
}
