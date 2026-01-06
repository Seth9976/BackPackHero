using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomStyleAttribute : Attribute
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00009A2C File Offset: 0x00007C2C
		public CustomStyleAttribute(string ussStyle)
		{
			this.ussStyle = ussStyle;
		}

		// Token: 0x040000F3 RID: 243
		public readonly string ussStyle;
	}
}
