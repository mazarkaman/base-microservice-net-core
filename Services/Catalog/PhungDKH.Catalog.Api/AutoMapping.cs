namespace PhungDKH.Catalog.Api
{
    using AutoMapper;
    using PhungDKH.Catalog.Domain.Entities;
    using PhungDKH.Catalog.Service.Categories;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryEditRequest, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
