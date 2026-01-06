using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200008C RID: 140
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(2)]
	[UnitTitle("On Start")]
	public sealed class Start : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000096AE File Offset: 0x000078AE
		protected override string hookName
		{
			get
			{
				return "Start";
			}
		}
	}
}
