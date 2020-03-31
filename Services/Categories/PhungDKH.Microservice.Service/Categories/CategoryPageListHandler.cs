namespace PhungDKH.Microservice.Service.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using PhungDKH.Core;
    using PhungDKH.Microservice.Domain.Entities.Contexts;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryPageListHandler : IRequestHandler<CategoryPageListRequest, PagedList<CategoryViewModel>>
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CategoryPageListHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public CategoryPageListHandler(
            AppDbContext db,
            IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedList<CategoryViewModel>> Handle(CategoryPageListRequest request, CancellationToken cancellationToken)
        {
            var list = await this.db.Categories.Where(
                x => (string.IsNullOrEmpty(request.Query))
                || (x.Name.Contains(request.Query)
                )).Select(x => new CategoryViewModel(x)).ToListAsync();

            var viewModelProperties = GetAllPropertyNameOfViewModel();
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            string matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(CategoryViewModel);
            var sortProperty = type.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            return new PagedList<CategoryViewModel>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
        }

        private List<string> GetAllPropertyNameOfViewModel()
        {
            var postViewModel = new CategoryViewModel();
            var type = postViewModel.GetType();

            return ReflectionUtilities.GetAllPropertyNamesOfType(type);
        }
    }
}
