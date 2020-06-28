using System.Linq;
using DomainMadeFunctional.Repository;
using DomainMadeFunctional.Validations;
using Huy.Framework.Functions;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Application
{
	public static partial class Validate
	{
		public static CheckProductCodeExists CheckProductCodeExists (
			GetProductCode getProductCode)
		{
			return (ProductCode productCode) =>
			{
				return getProductCode()
					.Bind(codes => CheckProductCodeExistsLocalFunc(codes, productCode));

				static Result<bool> CheckProductCodeExistsLocalFunc(
					ProductCode[] codes,
					ProductCode code)
				{
					return Result<bool>.Ok(codes.Any(c => c.Value == code.Value));
				}
			};

		}
	}
}