namespace DomainMadeFunctional
{
	public class ValidatedOrderLine
	{
		internal ValidatedOrderLine(
			ProductCode productCode,
			OrderQuantity orderQuantity)
		{
			ProductCode = productCode;
			OrderQuantity = orderQuantity;
		}

		public ProductCode ProductCode { get; }
		public OrderQuantity OrderQuantity { get; }
	}
}