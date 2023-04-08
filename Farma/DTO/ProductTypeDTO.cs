namespace Farma.DTO
{
    public class ProductTypeDTO
    {
        public Guid IDProductType { get; set; }
        public string Category { get; set; } = string.Empty!;
        public string CategoryDescription { get; set; } = string.Empty!;
    }
}
