using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Result<ValidatedOrder> ValidatedOrder(
		CheckAddressExists checkAddressExists,
		CheckProductCodeExists checkProductCodeExists,
		UnvalidatedOrder unvalidatedOrder);
}