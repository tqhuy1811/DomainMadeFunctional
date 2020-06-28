using System.Threading.Tasks;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<bool>> CheckProductCodeExists(
		ProductCode productCode);
}