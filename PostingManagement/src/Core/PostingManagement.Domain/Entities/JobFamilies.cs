using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class JobFamilies
    {
        [Key]
        public int JobFamilyId { get; set; }
        public string JobFamilyName { get; set; }   
    }
}
