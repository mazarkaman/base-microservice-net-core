namespace PhungDKH.Microservice.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public class Category : BaseEntity
    {
        public Category() : base()
        {

        }

        public string Name { get; set; }
    }
}
