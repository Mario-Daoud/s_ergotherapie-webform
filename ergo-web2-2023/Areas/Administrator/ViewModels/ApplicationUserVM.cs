using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class ApplicationUserVM
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? UserName { get; set; }
    }
}

