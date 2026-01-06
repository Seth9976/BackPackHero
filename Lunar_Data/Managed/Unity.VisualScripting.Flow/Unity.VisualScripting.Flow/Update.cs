using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200008D RID: 141
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(3)]
	[UnitTitle("On Update")]
	public sealed class Update : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000096BD File Offset: 0x000078BD
		protected override string hookName
		{
			get
			{
				return "Update";
			}
		}
	}
}
