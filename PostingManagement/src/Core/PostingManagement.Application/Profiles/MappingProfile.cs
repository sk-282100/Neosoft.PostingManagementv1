using AutoMapper;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Application.Features.Categories.Commands.CreateCategory;
using PostingManagement.Application.Features.Categories.Commands.StoredProcedure;
using PostingManagement.Application.Features.Categories.Queries.GetCategoriesList;
using PostingManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using PostingManagement.Application.Features.Events.Commands.CreateEvent;
using PostingManagement.Application.Features.Events.Commands.Transaction;
using PostingManagement.Application.Features.Events.Commands.UpdateEvent;
using PostingManagement.Application.Features.Events.Queries.GetEventDetail;
using PostingManagement.Application.Features.Events.Queries.GetEventsExport;
using PostingManagement.Application.Features.Events.Queries.GetEventsList;
using PostingManagement.Application.Features.ExcelUpload.Command;
using PostingManagement.Application.Features.ExcelUpload.Queries.GetUploadHistoryList;
using PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery;
using PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById;
using PostingManagement.Application.Features.Orders.GetOrdersForMonth;
using PostingManagement.Application.Features.Roles.Commands.EditRole;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Features.Scales.Queries.GetAllScales;
using PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetTriggerById;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {          
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, TransactionCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, StoredProcedureCommand>();
            CreateMap<Category, StoredProcedureDto>();

            CreateMap<Order, OrdersForMonthDto>();

            CreateMap<ExcelUploadResult, ExcelUploadDto>().ReverseMap();

            CreateMap<UploadHistoryDetails, GetUploadHistoryDto>().ReverseMap();


            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();

            CreateMap<UserDetails, UserDetailsDto>().ConvertUsing<UserDetailsCustomMapper>();
            CreateMap<UserDetailsVm, GetAllUserDetailsDto>().ConvertUsing<GetAllUserDetailsCustomMapper>();

            CreateMap<Trigger, GetTriggerByIdDto>().ConvertUsing<GetTriggerByIdCustomMapper>();
            CreateMap<TriggerVm, GetAllTriggerDto>().ConvertUsing<GetAllTriggerCustomMapper>();

            CreateMap<Scale, ScaleDto>().ConvertUsing<GetScaleCustomMapper>();

            CreateMap<Role, EditRoleCommand>().ReverseMap();
            CreateMap<Role, GetAllRolesDto>().ConvertUsing<GetAllRolesDtoCustomMapper>();
            CreateMap<Role, GetRoleByIdDto>().ConvertUsing<GetRolesByIdCustomMapper>();
            CreateMap<JobFamilies, GetAllJobFamilyDto>().ConvertUsing<GetAllJobFamiliesDtoCustomMapper>();
            CreateMap<JobFamilies, GetjobFamilyByIdDto>().ConvertUsing<GetJobFamilyByIdCustomMapper>();
        }
    }
}
