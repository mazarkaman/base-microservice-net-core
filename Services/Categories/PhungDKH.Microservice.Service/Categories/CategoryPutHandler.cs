namespace PhungDKH.Microservice.Service.Categories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using PhungDKH.Microservice.Domain.Entities.Contexts;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryPutHandler : IRequestHandler<CategoryPutRequest, ResponseModel>
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CategoryPutHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public CategoryPutHandler(
            AppDbContext db,
            IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(CategoryPutRequest request, CancellationToken cancellationToken)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "The category is not found"
                };
            }

            category.Name = request.Name;
            await this.db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "The category was updated successfully"
            };
        }
    }
}
