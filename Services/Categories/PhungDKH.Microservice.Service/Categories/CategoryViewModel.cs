using System;
using System.Collections.Generic;
using System.Text;
using PhungDKH.Microservice.Domain.Entities;

namespace PhungDKH.Microservice.Service.Categories
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category) : this()
        {
            if (category != null)
            {
                Name = category.Name;
            }
        }

        public string Name { get; set; } = string.Empty;
    }
}
