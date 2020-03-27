namespace PhungDKH.Microservice.Api
{
    using AutoMapper;
    using PhungDKH.Microservice.Domain.Entities;
    using PhungDKH.Microservice.Service.Categories;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CategoryPostRequest, Category>();
            CreateMap<CategoryPutRequest, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
