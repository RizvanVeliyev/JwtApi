﻿namespace Application.Features.Products.Queries.GetAll
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
