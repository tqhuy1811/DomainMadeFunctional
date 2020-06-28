namespace DomainMadeFunctional
{
	public class UnvalidatedOrder
	{
		public string CustomerName { get; set; }
		public UnvalidatedAddress BillingAddress { get; set; }
		public UnvalidatedAddress ShippingAddress { get; set; }
		public UnvalidatedOrderLine[] UnvalidatedOrderLines { get; set; }
	}
}