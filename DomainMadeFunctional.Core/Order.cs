using System;

namespace DomainMadeFunctional
{
	public class Order
	{
		public Guid Id { get; }
		public Guid CustomerId { get; }
		public object BillingAddress { get; }
		public object ShippingAddress { get; }
		public object OrderLines { get; }
		public object AmountToBill { get; }
	}
}