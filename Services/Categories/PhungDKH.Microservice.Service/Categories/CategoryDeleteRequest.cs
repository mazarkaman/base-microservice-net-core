namespace PhungDKH.Microservice.Service.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
