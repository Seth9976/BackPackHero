using System;

namespace Pathfinding
{
	// Token: 0x02000084 RID: 132
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UniqueComponentAttribute : Attribute
	{
		// Token: 0x040003D9 RID: 985
		public string tag;
	}
}
