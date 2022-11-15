using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface IExcelUploadRepository
    {
        public Task<ExcelUploadResult> AddAsync<T>(string uploadedBy, List<T> excelData,string fileName);
        public Task<List<UploadHistoryDetails>> GetUploadHistoryList(int FileTypeCode);
    }
}
