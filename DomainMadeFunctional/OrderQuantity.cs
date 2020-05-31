using DomainMadeFunctional.Errors;
using Huy.Framework.Types;

namespace DomainMadeFunctional
{
	public abstract class OrderQuantity
	{
		public int Value { get; }
		public string DisplayName { get; }

		protected OrderQuantity(
			int value,
			string displayName)
		{
			Value = value;
			DisplayName = displayName;
		}
	}
	
	public class UnitQuantity : OrderQuantity
	{
		public int Amount { get; }
		
		private UnitQuantity(int amount) : base(0, "Unit")
		{
			Amount = amount;
		}
		
		public static Result<UnitQuantity> Of(int amount)
		{
			if (amount < 1)
			{
				return Result<UnitQuantity>.Fail(new ValidationError("Unit Quantity can not be negative"));
			}

			if (amount > 1000)
			{
				return Result<UnitQuantity>.Fail(new ValidationError("Unit Quantity can not be more than 1000"));
			}
			
			return Result<UnitQuantity>.Ok(new UnitQuantity(amount));
		}
	}

	public class KilogramQuantity : OrderQuantity
	{
		public double Amount { get; }

		private KilogramQuantity(double amount) : base(1, "Kilogram")
		{
			Amount = amount;
		}

		public static Result<KilogramQuantity> Of(double amount)
		{
			return null;
		}

	}
}