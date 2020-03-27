namespace PhungDKH.Microservice.Service.Categories
{
    using MediatR;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryPageListRequest : BaseRequestModel, IRequest<PagedList<CategoryViewModel>>
    {
    }
}
