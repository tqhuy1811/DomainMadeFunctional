using Huy.Framework.Types;

namespace DomainMadeFunctional.Validations
{
	public delegate Result<ValidatedAddress> AddressValidationService(UnvalidatedAddress address);
}