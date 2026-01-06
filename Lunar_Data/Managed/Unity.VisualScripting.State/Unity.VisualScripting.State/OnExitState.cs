using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000F RID: 15
	[UnitCategory("Events/State")]
	public class OnExitState : ManualEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002732 File Offset: 0x00000932
		protected override string hookName
		{
			get
			{
				return "OnExitState";
			}
		}
	}
}
