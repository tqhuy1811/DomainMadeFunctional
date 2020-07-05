using System.Threading.Tasks;
using DomainMadeFunctional.OrderContext.Domain;
using Huy.Framework.Functions;
using Huy.Framework.Types;
using static DomainMadeFunctional.OrderContext.Helpers.ProductCodeHelper;

namespace DomainMadeFunctional.OrderContext.Validations
{
	public delegate Task<Result<ValidatedOrderLine>>
		CheckOrderLineValid(UnvalidatedOrderLine orderLine);

	public static partial class Validate
	{
		public static CheckOrderLineValid CheckOrderLineValid(
			CheckProductCodeExists checkProductCodeExists,
			CheckQuantityValid checkQuantityValid)

		{
			return async (UnvalidatedOrderLine orderline) =>
			{
				var productCodeResult = ToProductCode(orderline.ProductCode);

				var orderQuantityResult = await productCodeResult
					.Tap(code => checkProductCodeExists(code))
					.Bind(code => checkQuantityValid(code, orderline.Amount));

				return (productCodeResult.Success, orderQuantityResult.Success) switch
				{
					(true, true) => Result<ValidatedOrderLine>.Ok(
						new ValidatedOrderLine(
							productCode: productCodeResult.Data,
							orderQuantity: orderQuantityResult.Data)),
					(false, _) => productCodeResult.Error,
					(_, false) => orderQuantityResult.Error
				};
			};
		}
	}
}