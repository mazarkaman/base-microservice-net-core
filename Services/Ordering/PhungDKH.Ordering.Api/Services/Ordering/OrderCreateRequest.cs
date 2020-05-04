namespace PhungDKH.Ordering.Service.Categories
{
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using PhungDKH.Core.Models.Common;

    public class OrderCreateRequest : IRequest<ResponseModel>
    {
        [Required]
        public string Name { get; set; }
    }
}
