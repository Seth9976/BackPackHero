using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000053 RID: 83
	[UnitCategory("Events/Application")]
	public sealed class OnApplicationPause : GlobalEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008823 File Offset: 0x00006A23
		protected override string hookName
		{
			get
			{
				return "OnApplicationPause";
			}
		}
	}
}
