namespace Farma.DTO
{
    public class OrdersDTO
    {
        public Guid IDOrder { get; set; }
        public Guid IDUser { get; set; }
        public decimal TotalOrderPrice { get; set; } = 0!;
        public DateTime? TransactionDate { get; set; }
    }
}
