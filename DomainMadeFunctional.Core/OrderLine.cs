using System;

namespace DomainMadeFunctional
{
	public class OrderLine
	{
		public Guid Id { get; }
		public Guid OrderId { get; }
		public ProductCode ProductCode { get; }
		public OrderQuantity OrderQuantity { get; }
		public object Price { get; }

		public OrderLine()
		{
			var value = OrderQuantity switch
			{
				UnitQuantity uq => uq.Amount,
				KilogramQuantity kq => kq.Amount,
				_ => default
			};
		}
	}
}