namespace PhungDKH.Microservice.Service.Categories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using PhungDKH.Microservice.Domain.Entities;
    using PhungDKH.Microservice.Domain.Entities.Contexts;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryPostHandler : IRequestHandler<CategoryPostRequest, ResponseModel>
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CategoryPostHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public CategoryPostHandler(
            AppDbContext db,
            IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(CategoryPostRequest request, CancellationToken cancellationToken)
        {
            var category = this.mapper.Map<Category>(request);

            this.db.Categories.Add(category);

            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "The category was created successfully"
            };
        }
    }
}
