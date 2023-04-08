namespace Farma.DTO
{
    public class ProductDTO
    {
        public Guid IDProduct { get; set; }
        public Guid IDProductType { get; set; }
        public Guid IDClass { get; set; }
        public Guid IDOrigin { get; set; }
        public string ProductName { get; set; } = string.Empty!;
        public decimal SupplyKG { get; set; } = 0!;
        public decimal PriceKG { get; set; } = 0!;
        public string ProductPictureURL { get; set; } = string.Empty!;
        public string ProductDescription { get; set; } = string.Empty!;
        public decimal DiscountPercentage { get; set; } = 0!;
    }
}
