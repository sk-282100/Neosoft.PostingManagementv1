using System.ComponentModel.DataAnnotations;

namespace PostingManagement.UI.Models
{
    public class JobFamilyModel
    {
        public string JobFamilyId { get; set; }
        [MaxLength(15)]
        public string JobFamilyName { get; set; }   

    }
}
