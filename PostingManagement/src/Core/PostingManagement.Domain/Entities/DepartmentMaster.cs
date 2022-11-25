﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class DepartmentMaster
    {        
        [Key]
        public int DepartmentCode { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
