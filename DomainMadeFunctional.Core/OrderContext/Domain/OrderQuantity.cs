using System.Globalization;
using DomainMadeFunctional.Errors;
using Huy.Framework.Types;

namespace DomainMadeFunctional.OrderContext.Domain
{
	public abstract class OrderQuantity
	{
		public int EnumValue { get; }
		public string DisplayName { get; }
		public abstract decimal Value { get; }

		protected OrderQuantity(
			int enumValue,
			string displayName)
		{
			EnumValue = enumValue;
			DisplayName = displayName;
		}
	}
	
	public class UnitQuantity : OrderQuantity
	{
		public override decimal Value { get; }

		private UnitQuantity(decimal amount) : base(0, "Unit")
		{
			
			Value = amount;
		}
		
		public static Result<UnitQuantity> Of(decimal amount)
		{
			if (amount < 1m)
			{
				return Result<UnitQuantity>.Fail(new ValidationError("Unit Quantity can not be negative"));
			}

			if (amount > 1000m)
			{
				return Result<UnitQuantity>.Fail(new ValidationError("Unit Quantity can not be more than 1000"));
			}

			if (int.TryParse(amount.ToString(CultureInfo.CurrentCulture), out var _) == false)
			{
				return Result<UnitQuantity>.Fail(new ValidationError("Unit Quantity has to be a whole number"));
			}
			
			return Result<UnitQuantity>.Ok(new UnitQuantity(amount));
		}
	}

	public class KilogramQuantity : OrderQuantity
	{
		public override decimal Value { get; }

		private KilogramQuantity(decimal amount) : base(1, "Kilogram")
		{
			Value = amount;
		}

		public static Result<KilogramQuantity> Of(decimal amount)
		{
			if (amount <= 0m)
			{
				return Result<KilogramQuantity>.Fail(new ValidationError("Kilogram Quantity can not be negative"));
			}

			if (amount >= 1000m)
			{
				return Result<KilogramQuantity>.Fail(new ValidationError("Kilogram Quantity can not be more than 1000"));
			}
			
			return Result<KilogramQuantity>.Ok(new KilogramQuantity(amount));
		}

	}
}