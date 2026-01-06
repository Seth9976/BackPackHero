using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000089 RID: 137
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(7)]
	public sealed class OnDestroy : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00009681 File Offset: 0x00007881
		protected override string hookName
		{
			get
			{
				return "OnDestroy";
			}
		}
	}
}
