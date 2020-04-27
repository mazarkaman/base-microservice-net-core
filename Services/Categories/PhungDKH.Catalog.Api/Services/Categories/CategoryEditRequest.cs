namespace PhungDKH.Catalog.Service.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using MediatR;
    using PhungDKH.Core.Models.Common;

    public class CategoryEditRequest : IRequest<ResponseModel>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
