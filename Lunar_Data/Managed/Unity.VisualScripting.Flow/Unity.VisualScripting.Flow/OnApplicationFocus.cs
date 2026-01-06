using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000051 RID: 81
	[UnitCategory("Events/Application")]
	public sealed class OnApplicationFocus : GlobalEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00008805 File Offset: 0x00006A05
		protected override string hookName
		{
			get
			{
				return "OnApplicationFocus";
			}
		}
	}
}
