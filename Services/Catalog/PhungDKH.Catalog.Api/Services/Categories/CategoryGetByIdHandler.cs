namespace PhungDKH.Catalog.Service.Categories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using PhungDKH.Catalog.Domain.Entities.Contexts;

    public class CategoryGetByIdHandler : IRequestHandler<CategoryGetByIdRequest, CategoryViewModel>
    {
        private readonly AppCatalogDbContext db;
        private readonly IMapper mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CategoryGetByIdHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public CategoryGetByIdHandler(
            AppCatalogDbContext db,
            IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CategoryViewModel> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x=>x.Id == request.Id);
            return new CategoryViewModel(category);
        }
    }
}
