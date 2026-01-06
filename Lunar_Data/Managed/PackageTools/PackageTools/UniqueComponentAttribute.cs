using System;

namespace Pathfinding
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UniqueComponentAttribute : Attribute
	{
		// Token: 0x04000001 RID: 1
		public string tag;
	}
}
