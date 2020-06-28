using System.Threading.Tasks;
using DomainMadeFunctional.Errors;
using DomainMadeFunctional.Validations;
using Huy.Framework.Functions;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Helpers
{
	public static class AddressHelper
	{
		public static Task<Result<ValidatedAddress>> ToAddress(
			CheckAddressExists exists,
			UnvalidatedAddress address)
		{
			return exists(address)
				.Bind((isAddressExistResult) => ToAddressLocalFunc(isAddressExistResult, address));
			
			static Result<ValidatedAddress> ToAddressLocalFunc(
				bool isAddressExistResult,
				UnvalidatedAddress address)
			{
				return isAddressExistResult
					? Result<ValidatedAddress>.Ok(
						new ValidatedAddress(
							address1: address.Address1,
							address2: address.Address2,
							address3: address.Address3))
					: Result<ValidatedAddress>.Fail(new ValidationError("Invalid Address"));
			}
		}
	}
}