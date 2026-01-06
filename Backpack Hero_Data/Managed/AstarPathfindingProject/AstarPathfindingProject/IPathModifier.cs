using System;

namespace Pathfinding
{
	// Token: 0x02000076 RID: 118
	public interface IPathModifier
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000632 RID: 1586
		int Order { get; }

		// Token: 0x06000633 RID: 1587
		void Apply(Path path);

		// Token: 0x06000634 RID: 1588
		void PreProcess(Path path);
	}
}
