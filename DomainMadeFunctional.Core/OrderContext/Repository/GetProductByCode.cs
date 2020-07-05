using System.Threading.Tasks;
using DomainMadeFunctional.OrderContext.Domain;
using Huy.Framework.Types;

namespace DomainMadeFunctional.OrderContext.Repository
{
	public delegate Task<Result<ProductCode[]>> GetProductCode();
}