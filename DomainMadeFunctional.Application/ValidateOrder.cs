using System.Linq;
using DomainMadeFunctional.Errors;
using DomainMadeFunctional.Validations;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Application
{
	public static partial class Validate
	{
		public static CheckIfOrderValid ValidatedOrder = async (
			CheckAddressExists checkAddressExists,
			CheckProductCodeExists checkProductCodeExists,
			UnvalidatedOrder order) =>
		{
			if (string.IsNullOrEmpty(order.CustomerName))
			{
				return Result<ValidatedOrder>.Fail(new ValidationError("Customer Name must not be empty"));
			}

			await checkAddressExists(order.BillingAddress);
			await checkAddressExists(order.ShippingAddress);
			return null;
			//order.UnvalidatedOrderLines.Select(v => v.)
		};
	}
}