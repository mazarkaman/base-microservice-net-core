namespace PhungDKH.Microservice.Domain.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public class Product : BaseEntity
    {
        public Product() : base()
        {

        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Thumbnail { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
