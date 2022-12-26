using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Profiles
{
    public class GetAllTriggerCustomMapper : ITypeConverter<TriggerVm, GetAllTriggerDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetAllTriggerCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }
        public GetAllTriggerDto Convert(TriggerVm source, GetAllTriggerDto destination, ResolutionContext context)
        {
            GetAllTriggerDto dto = new GetAllTriggerDto()
            {
                TriggerId = _dataProtector.Protect(source.TriggerId.ToString()),
                ScaleName = source.ScaleName,
                Tenure = source.Tenure,
                Mandatory = source.Mandatory
            };
            return dto;
        }
    }
}
