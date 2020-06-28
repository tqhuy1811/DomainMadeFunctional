using System.Threading.Tasks;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Repository
{
	public delegate Task<Result<ProductCode[]>> GetProductCode();
}