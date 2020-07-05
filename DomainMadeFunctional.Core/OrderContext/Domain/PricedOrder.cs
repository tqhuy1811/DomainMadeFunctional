using System;
using System.Linq;

namespace DomainMadeFunctional.OrderContext.Domain
{
	public class PricedOrder
	{
		public PricedOrder(Guid id,
			Guid customerId,
			ValidatedAddress billingAddress,
			ValidatedAddress shippingAddress,
			PricedOrderLine[] orderLines)
		{
			Id = id;
			CustomerId = customerId;
			BillingAddress = billingAddress;
			ShippingAddress = shippingAddress;
			OrderLines = orderLines;
		}

		public Guid Id { get; }
		public Guid CustomerId { get; }
		public ValidatedAddress BillingAddress { get; }
		public ValidatedAddress ShippingAddress { get; }
		public PricedOrderLine[] OrderLines { get; }
		public decimal TotalCost => OrderLines.Sum(_ => _.Cost * _.OrderQuantity.Value);
	}
}