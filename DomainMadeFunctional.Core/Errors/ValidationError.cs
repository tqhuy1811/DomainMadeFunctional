using Huy.Framework.Types;

namespace DomainMadeFunctional.Errors
{
	public class ValidationError: Error
	{
		public ValidationError(string message) : base(message)
		{
		}
	}
}