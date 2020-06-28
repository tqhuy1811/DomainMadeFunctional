using System.Threading.Tasks;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<ValidatedOrder>> CheckIfOrderValid(
		CheckAddressExists checkAddressExists,
		CheckProductCodeExists checkProductCodeExists,
		UnvalidatedOrder unvalidatedOrder);
}