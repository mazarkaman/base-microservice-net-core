namespace PhungDKH.Ordering.Service.Categories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using PhungDKH.EvenBus.Abstractions;
    using PhungDKH.Ordering.Domain.Entities;
    using PhungDKH.Ordering.Domain.Entities.Contexts;
    using PhungDKH.Ordering.Service.Categories.Events;
    using PhungDKH.Core.Models.Common;

    public class OrderCreateHandler : IRequestHandler<OrderCreateRequest, ResponseModel>
    {
        private readonly AppOrderingDbContext db;
        private readonly IMapper mapper;
        private readonly IEventBus eventBus;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OrderCreateHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public OrderCreateHandler(
            AppOrderingDbContext db,
            IMapper mapper,
            IEventBus eventBus)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<ResponseModel> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var category = this.mapper.Map<Order>(request);

            this.db.Orders.Add(category);

            await this.db.SaveChangesAsync(cancellationToken);

            var eventPost = new OrderCreatedEvent(request.Name);
            this.eventBus.Publish(eventPost);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "The ordering was created successfully"
            };
        }
    }
}
