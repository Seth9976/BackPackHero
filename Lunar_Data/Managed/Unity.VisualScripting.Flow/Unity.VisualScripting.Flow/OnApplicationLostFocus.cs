using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000052 RID: 82
	[UnitCategory("Events/Application")]
	public sealed class OnApplicationLostFocus : GlobalEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008814 File Offset: 0x00006A14
		protected override string hookName
		{
			get
			{
				return "OnApplicationLostFocus";
			}
		}
	}
}
