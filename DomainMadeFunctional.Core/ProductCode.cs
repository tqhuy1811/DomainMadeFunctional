using System.Text.RegularExpressions;
using DomainMadeFunctional.Errors;
using Huy.Framework.Types;

namespace DomainMadeFunctional
{
	public abstract class ProductCode
	{
		public abstract string Code { get; }
		public int EnumValue { get; }
		public string DisplayName { get; }
		
		protected ProductCode(
			int enumValue,
			string displayName)
		{
			EnumValue = enumValue;
			DisplayName = displayName;
		}
	}
	
	public class WidgetCode: ProductCode
	{
		private WidgetCode(
			string code,
			int enumValue,
			string displayName) : base(enumValue, displayName)
		{
			Code = code;
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

		public override string Code { get; }
	}

	public class GizmoCode : ProductCode
	{
		private GizmoCode(
			string code,
			int enumValue,
			string displayName) : base(enumValue, displayName)
		{
			Code = code;
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

		public override string Code { get; }
	}
}