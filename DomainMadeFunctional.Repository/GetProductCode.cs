using System;
using System.Linq;
using System.Threading.Tasks;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Repository
{
	public static class Database
	{
		public static GetProductCode GetProductCode = async () =>
		{
			await Task.Delay(2_000);
			var productCode = new ProductCode[]
			{
				GizmoCode.Of("G123KC").Data,
				GizmoCode.Of("W123DKC").Data,
				GizmoCode.Of("W123DKZ").Data,
				GizmoCode.Of("W124DKZ").Data,
				GizmoCode.Of("G125KC").Data,
			};
			return await Task.FromResult(Result<ProductCode[]>.Ok(productCode));
		};
	}
}