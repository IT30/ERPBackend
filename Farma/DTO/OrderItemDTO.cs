namespace Farma.DTO
{
    public class OrderItemDTO
    {
        public Guid IDOrderItem { get; set; }
        public Guid IDOrder { get; set; }
        public Guid IDProduct { get; set; }
        public decimal OrderAmount { get; set; } = 0!;
        public decimal OrderPrice { get; set; } = 0!;
    }
}
