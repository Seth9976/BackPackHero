using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000055 RID: 85
	[UnitCategory("Events/Application")]
	public sealed class OnApplicationResume : GlobalEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00008841 File Offset: 0x00006A41
		protected override string hookName
		{
			get
			{
				return "OnApplicationResume";
			}
		}
	}
}
