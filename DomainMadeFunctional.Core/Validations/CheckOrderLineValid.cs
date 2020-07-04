using System;
using System.Threading.Tasks;
using DomainMadeFunctional.Errors;
using Huy.Framework.Functions;
using Huy.Framework.Types;
using static DomainMadeFunctional.Helpers.ProductCodeHelper;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<ValidatedOrderLine>> 
		CheckOrderLineValid(
			CheckProductCodeExists productCodeExists, 
			CheckQuantityValid checkQuantityValid,
			UnvalidatedOrderLine orderLine);
	
	public static partial class Validate
	{
		public static CheckOrderLineValid CheckOrderLineValid = async (
			CheckProductCodeExists checkProductCodeExists,
			CheckQuantityValid checkQuantityValid,
			UnvalidatedOrderLine unvalidatedOrderLine) =>
		{
			var productCodeResult = ToProductCode(unvalidatedOrderLine.ProductCode);

			var orderQuantityResult = await productCodeResult
				.Tap(code => checkProductCodeExists(code))
				.Bind(code => checkQuantityValid(code, unvalidatedOrderLine.Amount));

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