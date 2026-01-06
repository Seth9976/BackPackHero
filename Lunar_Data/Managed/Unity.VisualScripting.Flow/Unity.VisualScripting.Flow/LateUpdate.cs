using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000088 RID: 136
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(5)]
	[UnitTitle("On Late Update")]
	public sealed class LateUpdate : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00009672 File Offset: 0x00007872
		protected override string hookName
		{
			get
			{
				return "LateUpdate";
			}
		}
	}
}
