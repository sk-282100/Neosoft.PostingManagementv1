using AutoMapper;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Application.Features.ExcelUpload.BaseExcelUploadCommandHandler;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.ExcelUpload.Command
{
    public class InterRegionalPromotionExcelCommandHandler : BaseExcelUploadCommandHandler<InterRegionalPromotion>
    {
        public InterRegionalPromotionExcelCommandHandler(IExcelUploadRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
