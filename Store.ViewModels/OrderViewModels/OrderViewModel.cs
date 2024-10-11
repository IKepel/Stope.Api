namespace Store.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double PriceUah { get; set; }
        public double PriceUsd { get; set; }
        public double PriceEuro { get; set; }
        public int Status { get; set; }
    }
}
