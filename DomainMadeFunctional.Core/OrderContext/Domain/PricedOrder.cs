using System;
using System.Linq;

namespace DomainMadeFunctional.OrderContext.Domain
{
	public class PricedOrder
	{
		public PricedOrder(Guid id,
			string customerName,
			ValidatedAddress billingAddress,
			ValidatedAddress shippingAddress,
			PricedOrderLine[] orderLines)
		{
			Id = id;
			CustomerName = customerName;
			BillingAddress = billingAddress;
			ShippingAddress = shippingAddress;
			OrderLines = orderLines;
		}

		public Guid Id { get; }
		public string CustomerName { get; }
		public ValidatedAddress BillingAddress { get; }
		public ValidatedAddress ShippingAddress { get; }
		public PricedOrderLine[] OrderLines { get; }
		public decimal TotalCost => OrderLines.Sum(_ => _.Cost * _.OrderQuantity.Value);
	}
}