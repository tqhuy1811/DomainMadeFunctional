using System;
using DomainMadeFunctional.Errors;
using Huy.Framework.Functions;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Result<OrderQuantity> CheckQuantityValid(ProductCode code,
		decimal amount);

	public static partial class Validate
	{
		public static CheckQuantityValid CheckQuantityValid = (ProductCode code,
			decimal amount) =>
		{
			return code switch
			{
				null => Result<OrderQuantity>.Fail(new ValidationError("Code can\'t be null")),
				WidgetCode _ => UnitQuantity
					.Of(amount)
					.Bind(Result<OrderQuantity>.Ok),

				GizmoCode _ => KilogramQuantity
					.Of(amount)
					.Bind(Result<OrderQuantity>.Ok),
				_ => throw new ArgumentOutOfRangeException(nameof(code))
			};
		};
	}
}