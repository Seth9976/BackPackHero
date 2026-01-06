using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200008A RID: 138
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(6)]
	public sealed class OnDisable : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00009690 File Offset: 0x00007890
		protected override string hookName
		{
			get
			{
				return "OnDisable";
			}
		}
	}
}
