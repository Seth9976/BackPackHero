using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200008B RID: 139
	[UnitCategory("Events/Lifecycle")]
	[UnitOrder(1)]
	public sealed class OnEnable : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000969F File Offset: 0x0000789F
		protected override string hookName
		{
			get
			{
				return "OnEnable";
			}
		}
	}
}
