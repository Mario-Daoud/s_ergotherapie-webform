namespace ergo_web2_2023.Areas.Superuser.ViewModels
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    
        public class AdministratorVM
        {
            public string? Id { get; set; }

            //[RegularExpression("[A-Z][a-z]{2,50}")]
            [Required]
            public string? UserName { get; set; }
            [Required]
            [EmailAddress(ErrorMessage = "Incorrect pattern")]
            public string? Email { get; set; }
 
            public string? Phone { get; set; }

            //[DataType(DataType.Password)]
            //[StringLength(50, MinimumLength = 6)]
            public string? Password { get; set; }

            public bool IsSuperUser { get; set; }
        }
    }

