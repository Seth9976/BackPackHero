using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal class SupportsChildTracksAttribute : Attribute
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x000099E7 File Offset: 0x00007BE7
		public SupportsChildTracksAttribute(Type childType = null, int levels = 2147483647)
		{
			this.childType = childType;
			this.levels = levels;
		}

		// Token: 0x040000F0 RID: 240
		public readonly Type childType;

		// Token: 0x040000F1 RID: 241
		public readonly int levels;
	}
}
