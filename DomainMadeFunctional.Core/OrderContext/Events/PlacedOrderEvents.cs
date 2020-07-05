namespace DomainMadeFunctional.OrderContext.Events
{
	public class PlacedOrderEvents
	{
		public object AcknowledgmentSent { get; }
		public object OrderPlaced { get; }
		public object BillableOrderPlaced { get; }
	}
}