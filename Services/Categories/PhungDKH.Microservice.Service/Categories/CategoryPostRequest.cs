namespace PhungDKH.Microservice.Service.Categories
{
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryPostRequest : IRequest<ResponseModel>
    {
        [Required]
        public string Name { get; set; }
    }
}
