namespace PhungDKH.Microservice.Service.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using MediatR;
    using PhungDKH.Microservice.Service.Common;

    public class CategoryEditRequest : IRequest<ResponseModel>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
