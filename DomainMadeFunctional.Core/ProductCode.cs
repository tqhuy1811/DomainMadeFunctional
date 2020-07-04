using System;
using System.Text.RegularExpressions;
using DomainMadeFunctional.Errors;
using Huy.Framework.Types;

namespace DomainMadeFunctional
{
	public abstract class ProductCode: IEquatable<ProductCode>
	{
		public abstract string Value { get; }
		public int EnumValue { get; }
		public string DisplayName { get; }
		
		protected ProductCode(
			int enumValue,
			string displayName)
		{
			EnumValue = enumValue;
			DisplayName = displayName;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ProductCode) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(EnumValue, DisplayName, Value);
		}

		public bool Equals(ProductCode other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return EnumValue == other.EnumValue && DisplayName == other.DisplayName && Value == other.Value;
		}
	}
	
	public class WidgetCode: ProductCode
	{
		private WidgetCode(
			string code,
			int enumValue,
			string displayName) : base(enumValue, displayName)
		{
			Value = code;
		}

		public static Result<WidgetCode> Of(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return Result<WidgetCode>.Fail(new ValidationError("Code is empty or null"));
			}

			if (Regex.Match(code, @"^W\d\d\d\d").Success == false)
			{
				return Result<WidgetCode>.Fail(new ValidationError("Code does not start with W and then 4 digits"));
			}
			return Result<WidgetCode>.Ok(new WidgetCode(code: code, enumValue: 1, displayName: "WidgetCode"));
			
		}

		public override string Value { get; }
	}

	public class GizmoCode : ProductCode
	{
		private GizmoCode(
			string code,
			int enumValue,
			string displayName) : base(enumValue, displayName)
		{
			Value = code;
		}

		public static Result<GizmoCode> Of(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return Result<GizmoCode>.Fail(new ValidationError("Code is empty or null"));
			}

			if (Regex.Match(code, @"^G\d\d\d").Success == false)
			{
				return Result<GizmoCode>.Fail(new ValidationError("Code does not start with G and then 3 digits"));
			}
			
			return Result<GizmoCode>.Ok(new GizmoCode(code: code, enumValue: 1, displayName: "WidgetCode"));
		}

		public override string Value { get; }
	}
}