using DomainMadeFunctional.Errors;
using DomainMadeFunctional.Validations;
using Huy.Framework.Functions;
using Huy.Framework.Types;
using static DomainMadeFunctional.Helpers.ProductCodeHelper;

namespace DomainMadeFunctional.Application
{
	public static class Validation
	{
		public static CheckOrderLineValid CheckOrderLineValid = async (
			CheckProductCodeExists checkProductCodeExists,
			UnvalidatedOrderLine unvalidatedOrderLine) =>
		{
			var productCodeResult = ToProductCode(unvalidatedOrderLine.ProductCode);

			var orderQuantityResult = await productCodeResult
				.Tap(code => checkProductCodeExists(code))
				.Bind(code =>
				{
					return code switch
					{
						WidgetCode _ => UnitQuantity
							.Of(unvalidatedOrderLine.Amount)
							.Bind(Result<OrderQuantity>.Ok),

						GizmoCode _ => KilogramQuantity
							.Of(unvalidatedOrderLine.Amount)
							.Bind(Result<OrderQuantity>.Ok),

						_ => Result<OrderQuantity>.Fail(new ValidationError("Invalid ProductCode"))
					};
				});

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