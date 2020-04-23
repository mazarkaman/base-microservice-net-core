namespace PhungDKH.Microservice.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductInCategory")]
    public class ProductInCategory : BaseEntity
    {
        public ProductInCategory() : base()
        {

        }

        public Guid ProductId { get; set; }

        public Product User { get; set; }

        public Guid CategoryId { get; set; }

        public Category Role { get; set; }
    }
}
