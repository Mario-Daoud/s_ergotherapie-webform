using System.ComponentModel;

namespace ergo_web2_2023.Areas.Administrator.ViewModels
{
    public class FormGroupVM
    {
        [DisplayName("Group")]
        public int GroupId { get; set; }
        public string Title { get; set; }
        [DisplayName("Group Order")]
        public int? GroupOrder { get; set; }
    }
}
