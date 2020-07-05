namespace DomainMadeFunctional.OrderContext.Domain
{
	public class ValidatedOrder
	{
		public ValidatedOrder(
			string customerName,
			ValidatedAddress billingAddress,
			ValidatedAddress shippingAddress,
			ValidatedOrderLine[] validatedOrderLines)
		{
			CustomerName = customerName;
			BillingAddress = billingAddress;
			ShippingAddress = shippingAddress;
			ValidatedOrderLines = validatedOrderLines;
		}

		public string CustomerName { get; }
		public ValidatedAddress BillingAddress { get; }
		public ValidatedAddress ShippingAddress { get; }
		public ValidatedOrderLine[] ValidatedOrderLines { get; }
	}
}