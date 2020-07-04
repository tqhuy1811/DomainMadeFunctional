using System.Linq;
using System.Threading.Tasks;
using DomainMadeFunctional.Errors;
using DomainMadeFunctional.Repository;
using Huy.Framework.Functions;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<bool>> CheckProductCodeExists(
		ProductCode productCode);

	public static partial class Validate
	{
		public static CheckProductCodeExists CheckProductCodeExists(
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
					return codes.Any(c => c.Value == code.Value)
						? Result<bool>.Ok(true)
						: Result<bool>.Fail(new ValidationError("Code not found"));
				}
			};
		}
	}
}