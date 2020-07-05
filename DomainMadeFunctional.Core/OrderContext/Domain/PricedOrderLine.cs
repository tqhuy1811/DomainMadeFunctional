namespace DomainMadeFunctional.OrderContext.Domain
{
	public class PricedOrderLine
	{
		public PricedOrderLine(
			ProductCode productCode,
			OrderQuantity orderQuantity,
			decimal cost)
		{
			ProductCode = productCode;
			OrderQuantity = orderQuantity;
			Cost = cost;
		}
		
		public ProductCode ProductCode { get; }
		public OrderQuantity OrderQuantity { get; }
		public decimal Cost { get; }
	}
}