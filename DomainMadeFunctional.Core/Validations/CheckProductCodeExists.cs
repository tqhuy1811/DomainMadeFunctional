using System.Linq;
using System.Threading.Tasks;
using DomainMadeFunctional.Repository;
using Huy.Framework.Functions;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<bool>> CheckProductCodeExists(
		GetProductCode getProductCode,
		string productCode);
}