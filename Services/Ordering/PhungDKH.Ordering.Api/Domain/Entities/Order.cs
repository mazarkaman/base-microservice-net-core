namespace PhungDKH.Ordering.Domain.Entities
{
    using PhungDKH.Core.Models.Entity;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Order")]
    public class Order : BaseEntity
    {
        public Order() : base()
        {

        }

        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalQuantity { get; set; }
    }
}
