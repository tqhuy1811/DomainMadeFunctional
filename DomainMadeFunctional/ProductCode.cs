namespace DomainMadeFunctional
{
	public abstract class ProductCode
	{
		public int Value { get; }
		public string DisplayName { get; }

		public static readonly ProductCode Widget 
			= new WidgetCode();
		public static readonly ProductCode Gizmo 
			= new GizmoCode();
		

		protected ProductCode(
			int value,
			string displayName)
		{
			Value = value;
			DisplayName = displayName;
		}
		
		private class WidgetCode : ProductCode
		{
			public WidgetCode(): base(0, "Widget")
			{
			}
		}

		private class GizmoCode : ProductCode
		{
			public GizmoCode(): base(0, "Widget")
			{
			}
		}
	}


}