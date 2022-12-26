using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using PostingManagement.Application.Features.Scales.Queries.GetAllScales;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Profiles
{
    public class GetScaleCustomMapper : ITypeConverter<Scale,ScaleDto>
    {
        private readonly IDataProtector _dataProtector;
        public GetScaleCustomMapper(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("");
        }

        public ScaleDto Convert(Scale source, ScaleDto destination, ResolutionContext context)
        {
            ScaleDto dto = new ScaleDto()
            {
                ScaleId = _dataProtector.Protect(source.ScaleId.ToString()),
                ScaleName = source.Name
            };
            return dto;
        }
    }
}
