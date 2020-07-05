using DomainMadeFunctional.Errors;
using DomainMadeFunctional.OrderContext.Domain;
using Huy.Framework.Types;

namespace DomainMadeFunctional.OrderContext.Helpers
{
	public static class ProductCodeHelper
	{
		public static Result<ProductCode> ToProductCode(string code)
		{
			var gizmoCodeResult = GizmoCode.Of(code);
			var widgetCodeResult = WidgetCode.Of(code);

			return (gizmoCodeResult.Success, widgetCodeResult.Success) switch
			{
				(true, false) => Result<ProductCode>.Ok(gizmoCodeResult.Data),
				(false, true) => Result<ProductCode>.Ok(widgetCodeResult.Data),
				_ => Result<ProductCode>.Fail(new ValidationError("Invalid Product Code"))
			};
		}
	}
}