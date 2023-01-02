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
            mockMediator.Setup(m => m.Send(It.IsAny<GetTransferListQuery>(),It.IsAny<CancellationToken>())).ReturnsAsync(new Response<TransferListReponse>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetEmployeeDetailsByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<EmployeeDetailsForTransferList>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetSelectedTransferListByCoQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<TransferListVM>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetMatchingRequestTransferListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<List<MatchingRequestTransferVacancy>>());
            mockMediator.Setup(m => m.Send(It.IsAny<TransferListForZOCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<ZOTransferListReponse>());


            return mockMediator;
        }
    }
}
