using System;

namespace Pathfinding
{
	// Token: 0x0200010F RID: 271
	public interface IPathModifier
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000891 RID: 2193
		int Order { get; }

		// Token: 0x06000892 RID: 2194
		void Apply(Path path);

		// Token: 0x06000893 RID: 2195
		void PreProcess(Path path);
	}
}
