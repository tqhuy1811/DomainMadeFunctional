using System.Threading.Tasks;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<ValidatedOrderLine>> 
		CheckOrderLineValid(
			CheckProductCodeExists productCodeExists, 
			UnvalidatedOrderLine orderLine);
}