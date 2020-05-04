namespace PhungDKH.Ordering.Api
{
    using AutoMapper;
    using PhungDKH.Ordering.Domain.Entities;
    using PhungDKH.Ordering.Service.Categories;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<OrderCreateRequest, Order>();
        }
    }
}
