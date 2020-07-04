using System;
using System.Threading.Tasks;
using DomainMadeFunctional.Helpers;
using DomainMadeFunctional.Validations;
using Huy.Framework.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainMadeFunctional.Core.Test
{
	[TestClass]
	public class ValidationTest
	{
		[TestMethod]
		public async Task CheckAddressExists_InvalidAddress_False()
		{
			var validateResult = await Validate.CheckAddressExists(new UnvalidatedAddress
			{
				Address1 = string.Empty,
				Address2 = string.Empty,
				Address3 = string.Empty
			});

			Assert.AreEqual(true, validateResult.Failure);
			Assert.AreEqual("Invalid address", validateResult.Error.Message);
		}

		[TestMethod]
		public async Task CheckAddressExists_ValidAddress_True()
		{
			var validateResult = await Validate.CheckAddressExists(new UnvalidatedAddress
			{
				Address1 = "address 1",
				Address2 = "address 2",
				Address3 = "address 3"
			});

			Assert.AreEqual(true, validateResult.Success);
			Assert.AreEqual(true, validateResult.Data);
		}

		[TestMethod]
		public async Task CheckProductCodeExists_ValidProductCode_True()
		{
			var validateResult =
				await Validate.CheckProductCodeExists(MockProductCode)(Helpers.ProductCodeHelper.ToProductCode("G124KC").Data);

			Assert.AreEqual(true, validateResult.Success);
			Assert.AreEqual(true, validateResult.Data);
		}

		[TestMethod]
		public async Task CheckProductCodeExists_InvalidValidProductCode_False()
		{
			var validateResult =
				await Validate.CheckProductCodeExists(MockProductCode)(Helpers.ProductCodeHelper.ToProductCode("G123KC").Data);

			Assert.AreEqual(true, validateResult.Failure);
		}

		[TestMethod]
		public async Task CheckProductCodeExists_InvalidValidProductCode_Exception()
		{
			await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Validate.CheckProductCodeExists(MockProductCode)(Helpers
				.ProductCodeHelper.ToProductCode("122C").Data));
		}

		[TestMethod]
		public async Task CheckOrderLineValid_CodeIsNotGizmoOrWidget_False()
		{
			var validationResult = await Validate.CheckOrderLineValid(
				Validate.CheckProductCodeExists(MockProductCode),
				Validate.CheckQuantityValid,
				new UnvalidatedOrderLine
				{
					Amount = 100,
					ProductCode = "ssss"
				});
			Assert.AreEqual(true, validationResult.Failure);
			Assert.AreEqual("Invalid Product Code", validationResult.Error.Message);
		}

		[TestMethod]
		public async Task CheckOrderLineValid_CodeNotExistInDb_False()
		{
			var validationResult = await Validate.CheckOrderLineValid(
				Validate.CheckProductCodeExists(MockProductCode),
				Validate.CheckQuantityValid,
				new UnvalidatedOrderLine
				{
					Amount = 100,
					ProductCode = "G123KC"
				});
			Assert.AreEqual(true, validationResult.Failure);
			Assert.AreEqual("Code not found", validationResult.Error.Message);
		}

		[TestMethod]
		public async Task CheckOrderLineValid_InvalidAmountForWidget_False()
		{
			var validationResult = await Validate.CheckOrderLineValid(
				Validate.CheckProductCodeExists(MockProductCode),
				Validate.CheckQuantityValid,
				new UnvalidatedOrderLine
				{
					Amount = 100.12m,
					ProductCode = "W1234DKC"
				});
			Assert.AreEqual(true, validationResult.Failure);
			Assert.AreEqual("Unit Quantity has to be a whole number", validationResult.Error.Message);
		}

		[TestMethod]
		public async Task CheckOrderLineValid_ValidWidgetOrderLine_True()
		{
			var validationResult = await Validate.CheckOrderLineValid(
				Validate.CheckProductCodeExists(MockProductCode),
				Validate.CheckQuantityValid,
				new UnvalidatedOrderLine
				{
					Amount = 100m,
					ProductCode = "W1234DKC"
				});
			Assert.AreEqual(true, validationResult.Success);
			Assert.AreEqual(ProductCodeHelper.ToProductCode("W1234DKC").Data, validationResult.Data.ProductCode);
			Assert.AreEqual(100m, validationResult.Data.OrderQuantity.Value);
			Assert.AreEqual(0, validationResult.Data.OrderQuantity.EnumValue);
			Assert.AreEqual("Unit", validationResult.Data.OrderQuantity.DisplayName);
		}

		[TestMethod]
		public async Task CheckOrderLineValid_ValidGizmoOrderLine_True()
		{
			var validationResult = await Validate.CheckOrderLineValid(
				Validate.CheckProductCodeExists(MockProductCode),
				Validate.CheckQuantityValid,
				new UnvalidatedOrderLine
				{
					Amount = 100.22m,
					ProductCode = "G124KC"
				});
			Assert.AreEqual(true, validationResult.Success);
			Assert.AreEqual(ProductCodeHelper.ToProductCode("G124KC").Data, validationResult.Data.ProductCode);
			Assert.AreEqual(100.22m, validationResult.Data.OrderQuantity.Value);
			Assert.AreEqual(1, validationResult.Data.OrderQuantity.EnumValue);
			Assert.AreEqual("Kilogram", validationResult.Data.OrderQuantity.DisplayName);
		}

		[TestMethod]
		public void CheckQuantityValid_InvalidQuantityForWidgetCode_False()
		{
			var validationResult = Validate.CheckQuantityValid(
				ProductCodeHelper.ToProductCode("W1234DKC").Data, 
				100.21m);
			
			Assert.AreEqual(true, validationResult.Failure);
		}
		
		[TestMethod]
		public void CheckQuantityValid_InvalidQuantityNumberGreaterThan1000ForWidgetCode_False()
		{
			var validationResult = Validate.CheckQuantityValid(
				ProductCodeHelper.ToProductCode("W1234DKC").Data, 
				10000m);
			
			Assert.AreEqual(true, validationResult.Failure);
		}
		
		[TestMethod]
		public void CheckQuantityValid_InvalidQuantityNumberNegativeForWidgetCode_False()
		{
			var validationResult = Validate.CheckQuantityValid(
				ProductCodeHelper.ToProductCode("W1234DKC").Data, 
				-10m);
			
			Assert.AreEqual(true, validationResult.Failure);
		}
		
		[TestMethod]
		public void CheckQuantityValid_InvalidQuantityNumberGreaterThan1000ForGizmoCode_False()
		{
			var validationResult = Validate.CheckQuantityValid(
				ProductCodeHelper.ToProductCode("G124KC").Data, 
				10000m);
			
			Assert.AreEqual(true, validationResult.Failure);
		}
		
		[TestMethod]
		public void CheckQuantityValid_InvalidQuantityNumberNegativeForGizmoCode_False()
		{
			var validationResult = Validate.CheckQuantityValid(
				ProductCodeHelper.ToProductCode("G124KC").Data, 
				-10m);
			
			Assert.AreEqual(true, validationResult.Failure);
		}

		private static async Task<Result<ProductCode[]>> MockProductCode()
		{
			await Task.Yield();
			return Result<ProductCode[]>.Ok(new ProductCode[]
			{
				GizmoCode.Of("G124KC").Data,
				WidgetCode.Of("W1234DKC").Data,
				WidgetCode.Of("W1234DKZ").Data,
				WidgetCode.Of("W1244DKZ").Data,
				GizmoCode.Of("G125KC").Data,
			});
		}
	}
}