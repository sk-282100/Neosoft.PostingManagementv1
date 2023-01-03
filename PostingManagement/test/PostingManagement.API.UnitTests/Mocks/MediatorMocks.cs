using PostingManagement.Application.Features.Categories.Commands.CreateCategory;
using PostingManagement.Application.Features.Categories.Commands.StoredProcedure;
using PostingManagement.Application.Features.Categories.Queries.GetCategoriesList;
using PostingManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using PostingManagement.Application.Features.Events.Commands.CreateEvent;
using PostingManagement.Application.Features.Events.Commands.DeleteEvent;
using PostingManagement.Application.Features.Events.Commands.Transaction;
using PostingManagement.Application.Features.Events.Commands.UpdateEvent;
using PostingManagement.Application.Features.Events.Queries.GetEventDetail;
using PostingManagement.Application.Features.Events.Queries.GetEventsExport;
using PostingManagement.Application.Features.Events.Queries.GetEventsList;
using PostingManagement.Application.Features.Orders.GetOrdersForMonth;
using PostingManagement.Application.Responses;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using PostingManagement.Application.Features.Account.Queries.GetAllUser;
using PostingManagement.Application.Features.Account.Queries.GetUserById;
using PostingManagement.Application.Features.Account.Queries.IsUserNamePresent;
using PostingManagement.Application.Features.Account.Command.AddUser;
using PostingManagement.Application.Features.Account.Command.EditUser;
using PostingManagement.Application.Features.Account.Command.DeleteUser;
using PostingManagement.Application.Features.Account.Command.ResetPassword;
using PostingManagement.Application.Features.JobFamily.Queries.GetAllJobFamilyquery;
using PostingManagement.Application.Features.JobFamily.Commands.AddJobFamily;
using PostingManagement.Application.Features.JobFamily.Commands.DeleteJobFamily;
using PostingManagement.Application.Features.JobFamily.Commands.EditJobFamily;
using PostingManagement.Application.Features.JobFamily.Queries.GetJobFamilyById;
using PostingManagement.Application.Features.JobFamily.Queries.IsJobFamilyAlreadyExist;
using PostingManagement.Application.Features.Roles.Commands.AddRole;
using PostingManagement.Application.Features.Roles.Commands.DeleteRole;
using PostingManagement.Application.Features.Roles.Commands.EditRole;
using PostingManagement.Application.Features.Roles.Queries.GetRoleById;
using PostingManagement.Application.Features.Roles.Queries.GetAllRoles;
using PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist;
using PostingManagement.Application.Features.Scales.Queries.GetAllScales;
using PostingManagement.Application.Features.Triggers.Commands.AddTrigger;
using PostingManagement.Application.Features.Triggers.Commands.UpdateTrigger;
using PostingManagement.Application.Features.Triggers.Commands.DeleteTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetAllTrigger;
using PostingManagement.Application.Features.Triggers.Queries.GetTriggerById;
using PostingManagement.Application.Features.TransferList.Queries.GetTransferList;
using PostingManagement.Domain.Entities;
using PostingManagement.Application.Features.TransferList.Queries.GetEmployeeDetailsById;
using PostingManagement.Application.Features.TransferList.Queries.GetSelectedTransferListByCo;
using PostingManagement.Application.Features.TransferList.Queries.GetMatchRequestTransferList;
using PostingManagement.Application.Features.TransferList.Commands.InsertIntoTransferListForZO;

namespace PostingManagement.API.UnitTests.Mocks
{
    public class MediatorMocks
    {
        public static Mock<IMediator> GetMediator()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetCategoriesListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<CategoryListVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetCategoriesListWithEventsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<CategoryEventListVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<CreateCategoryDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<StoredProcedureCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<StoredProcedureDto>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetEventsListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<EventListVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetEventDetailQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<EventDetailVm>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetEventsExportQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new EventExportFileVm() { Data = Encoding.UTF8.GetBytes(new string(' ', 100)), ContentType = "text/csv", EventExportFileName = "Filename"  });
            mockMediator.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<Guid>());
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<Guid>());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Unit());
            mockMediator.Setup(m => m.Send(It.IsAny<TransactionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<Guid>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetOrdersForMonthQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PagedResponse<IEnumerable<OrdersForMonthDto>>(null, 10, 1, 2));

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllUserQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<GetAllUserDetailsDto>>() );
            mockMediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<UserDetailsDto>() );
            mockMediator.Setup(m => m.Send(It.IsAny<IsUserNamePresentQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<EditUserCommad>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<ResetPasswordCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllJobFamilyQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<GetAllJobFamilyDto>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetJobFamilyByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<GetjobFamilyByIdDto>() );
            mockMediator.Setup(m => m.Send(It.IsAny<IsJobFamilyAlreadyExistQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<AddjobFamilyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteJobFamilyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<EditJobFamilyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetRoleByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<GetRoleByIdDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllRolesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<GetAllRolesDto>>());
            mockMediator.Setup(m => m.Send(It.IsAny<IsRoleAlreadyExistQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<AddRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );
            mockMediator.Setup(m => m.Send(It.IsAny<EditRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>() );

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllScalesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<ScaleDto>>() );

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllTriggerQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<GetAllTriggerDto>>() );
            mockMediator.Setup(m => m.Send(It.IsAny<GetTriggerByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<GetTriggerByIdDto>() );
            mockMediator.Setup(m => m.Send(It.IsAny<AddTriggerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateTriggerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteTriggerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetTransferListQuery>(),It.IsAny<CancellationToken>())).ReturnsAsync(new Response<TransferListReponse>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetEmployeeDetailsByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<EmployeeDetailsForTransferList>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetSelectedTransferListByCoQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<TransferListVM>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetMatchingRequestTransferListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<MatchingRequestTransferVacancy>>());
            mockMediator.Setup(m => m.Send(It.IsAny<TransferListForZOCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<ZOTransferListReponse>());


            return mockMediator;
        }
    }
}
