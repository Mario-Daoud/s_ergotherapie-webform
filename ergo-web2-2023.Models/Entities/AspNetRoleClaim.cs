using System;
using System.Collections.Generic;

namespace ergo_web2_2023.Models.Entities
{
    public partial class AspNetRoleClaim
    {
        public int Id { get; set; }
        public string RoleId { get; set; } = null!;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AspNetRole Role { get; set; } = null!;
    }
}
