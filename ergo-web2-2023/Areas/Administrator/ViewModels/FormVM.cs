using Microsoft.Build.Framework;
using System.ComponentModel;

namespace ergo_web2_2023.Areas.Administrator.ViewModels;

public class FormVM
{
    [DisplayName("Form")]

    public int FormId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set;  }
}