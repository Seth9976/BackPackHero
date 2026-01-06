using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Field)]
	public class DisplayInfoAttribute : Attribute
	{
		// Token: 0x040001AF RID: 431
		public string name;

		// Token: 0x040001B0 RID: 432
		public int order;
	}
}
