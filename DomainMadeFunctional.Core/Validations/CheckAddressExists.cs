using System.Threading.Tasks;
using DomainMadeFunctional.Errors;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Task<Result<bool>> CheckAddressExists(UnvalidatedAddress address);
	
	public static partial class Validate
	{
		public static readonly CheckAddressExists CheckAddressExists = async (UnvalidatedAddress address) =>
		{
			await Task.Delay(2_000);
			// TODO: Fake validation
			if (string.IsNullOrEmpty(address.Address1) || string.IsNullOrEmpty(address.Address2) || string.IsNullOrEmpty(address.Address3))
			{
				return Result<bool>.Fail(new ValidationError("Invalid address")); 
			}
			return Result<bool>.Ok(true);
		};
	}
}