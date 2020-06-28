using System;
using System.Threading.Tasks;
using DomainMadeFunctional.Validations;
using Huy.Framework.Types;

namespace DomainMadeFunctional.Application
{

	public static partial class Validate
	{
		public static readonly CheckAddressExists CheckAddressExists = async (UnvalidatedAddress address) =>
		{
			await Task.Delay(2_000);
			return Result<bool>.Ok(true);
		};
	}
}