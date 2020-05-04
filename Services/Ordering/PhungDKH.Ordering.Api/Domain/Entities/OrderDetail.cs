namespace PhungDKH.Ordering.Domain.Entities
{
    using PhungDKH.Core.Models.Entity;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetail")]
    public class OrderDetail : BaseEntity
    {
        public OrderDetail() : base()
        {

        }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public Guid ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal ProductQuantity { get; set; }
    }
}
