using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000059 RID: 89
	[UnitCategory("Events/Editor")]
	public sealed class OnDrawGizmos : ManualEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000089C7 File Offset: 0x00006BC7
		protected override string hookName
		{
			get
			{
				return "OnDrawGizmos";
			}
		}
	}
}
