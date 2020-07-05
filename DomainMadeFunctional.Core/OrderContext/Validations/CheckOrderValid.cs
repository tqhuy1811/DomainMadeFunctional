using System.Linq;
using System.Threading.Tasks;
using DomainMadeFunctional.Errors;
using DomainMadeFunctional.OrderContext.Domain;
using Huy.Framework.Functions;
using Huy.Framework.Types;

namespace DomainMadeFunctional.OrderContext.Validations
{
	public delegate Task<Result<ValidatedOrder>> CheckOrderValid(
		CheckAddressExists checkAddressExists,
		CheckOrderLineValid checkOrderLineValid,
		UnvalidatedOrder unvalidatedOrder);

	public static partial class Validate
	{
		public static CheckOrderValid ValidatedOrder = async (
			CheckAddressExists checkAddressExists,
			CheckOrderLineValid checkOrderLineValid,
			UnvalidatedOrder order) =>
		{
			var billingAddressValidatedResult = await Helpers.AddressHelper.ToAddress(checkAddressExists, order.BillingAddress);
			var shipingAddressValidatedResult = await Helpers.AddressHelper.ToAddress(checkAddressExists, order.ShippingAddress);
			var validatedOrderLineResult =
				(await Task.WhenAll(order.UnvalidatedOrderLines.Select(_ => checkOrderLineValid(_))))
				.Combine(",",
					(error) => new ValidationError(error));


			return (billingAddressValidatedResult.Success,
					shipingAddressValidatedResult.Success,
					validatedOrderLineResult.Success) switch
				{
					(true, true, true) => Result<ValidatedOrder>.Ok(
						new ValidatedOrder(
							order.CustomerName,
							billingAddressValidatedResult.Data,
							shipingAddressValidatedResult.Data,
							validatedOrderLineResult.Data)),
					(false, _, _) => Result<ValidatedOrder>.Fail(billingAddressValidatedResult.Error),
					(_, false, _) => Result<ValidatedOrder>.Fail(shipingAddressValidatedResult.Error),
					(_, _, false) => Result<ValidatedOrder>.Fail(validatedOrderLineResult.Error),
				};
			
		};
	}
}