﻿namespace PhungDKH.Catalog.Domain.Entities
{
    using PhungDKH.Core.Models.Entity;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductInCategory")]
    public class ProductInCategory : BaseEntity
    {
        public ProductInCategory() : base()
        {

        }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
