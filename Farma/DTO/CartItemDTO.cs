namespace Farma.DTO
{
    public class CartItemDTO
    {
        public Guid IDCartItem { get; set; }
        public Guid IDUser { get; set; }
        public Guid IDProduct { get; set; }
        public decimal CartAmount { get; set; } = 0!;
        public decimal CartPrice { get; set; } = 0!;
    }
}
