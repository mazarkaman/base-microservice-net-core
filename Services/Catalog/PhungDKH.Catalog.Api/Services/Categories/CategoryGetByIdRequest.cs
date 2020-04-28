namespace PhungDKH.Catalog.Service.Categories
{
    using System;
    using MediatR;

    public class CategoryGetByIdRequest : IRequest<CategoryViewModel>
    {
        public Guid Id { get; set; }
    }
}
