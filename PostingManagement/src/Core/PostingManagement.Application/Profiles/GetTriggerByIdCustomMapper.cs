using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Triggers.Queries.GetTriggerById;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Profiles
{
    public class GetTriggerByIdCustomMapper : ITypeConverter<Trigger, GetTriggerByIdDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetTriggerByIdCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }
        public GetTriggerByIdDto Convert(Trigger source, GetTriggerByIdDto destination, ResolutionContext context)
        {
            GetTriggerByIdDto dto = new GetTriggerByIdDto()
            {
                TriggerId = _dataProtector.Protect(source.TriggerId.ToString()),
                ScaleId = _dataProtector.Protect(source.ScaleId.ToString()),
                Tenure = source.Tenure,
                Mandatory = source.Mandatory
            };
            return dto;
        }
    }
}
