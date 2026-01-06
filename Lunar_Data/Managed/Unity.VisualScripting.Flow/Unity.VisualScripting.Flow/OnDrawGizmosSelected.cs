using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200005A RID: 90
	[UnitCategory("Events/Editor")]
	public sealed class OnDrawGizmosSelected : ManualEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000089D6 File Offset: 0x00006BD6
		protected override string hookName
		{
			get
			{
				return "OnDrawGizmosSelected";
			}
		}
	}
}
