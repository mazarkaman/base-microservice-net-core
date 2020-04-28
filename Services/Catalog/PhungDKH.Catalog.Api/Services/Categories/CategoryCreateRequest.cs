namespace PhungDKH.Catalog.Service.Categories
{
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using PhungDKH.Core.Models.Common;

    public class CategoryCreateRequest : IRequest<ResponseModel>
    {
        [Required]
        public string Name { get; set; }
    }
}
