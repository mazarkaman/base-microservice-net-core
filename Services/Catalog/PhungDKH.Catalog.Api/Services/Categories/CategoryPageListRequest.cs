namespace PhungDKH.Catalog.Service.Categories
{
    using MediatR;
    using PhungDKH.Core.Models.Common;

    public class CategoryPageListRequest : BaseRequestModel, IRequest<PagedList<CategoryViewModel>>
    {
    }
}
