namespace PhungDKH.Catalog.Service.Categories
{
    using System;
    using MediatR;
    using PhungDKH.Core.Models.Common;

    public class CategoryDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
