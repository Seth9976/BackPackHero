using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000087 RID: 135
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(4)]
	[UnitTitle("On Fixed Update")]
	public sealed class FixedUpdate : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00009663 File Offset: 0x00007863
		protected override string hookName
		{
			get
			{
				return "FixedUpdate";
			}
		}
	}
}
