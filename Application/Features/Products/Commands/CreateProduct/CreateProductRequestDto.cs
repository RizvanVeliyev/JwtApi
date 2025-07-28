namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
