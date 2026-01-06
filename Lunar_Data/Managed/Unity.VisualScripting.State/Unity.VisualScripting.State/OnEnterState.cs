using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000E RID: 14
	[UnitCategory("Events/State")]
	public class OnEnterState : ManualEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002723 File Offset: 0x00000923
		protected override string hookName
		{
			get
			{
				return "OnEnterState";
			}
		}
	}
}
