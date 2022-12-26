using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Domain.Entities
{
    public class TransferListReponse
    {
        public List<TransferListVM> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
