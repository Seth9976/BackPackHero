using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000054 RID: 84
	[UnitCategory("Events/Application")]
	public sealed class OnApplicationQuit : GlobalEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008832 File Offset: 0x00006A32
		protected override string hookName
		{
			get
			{
				return "OnApplicationQuit";
			}
		}
	}
}
