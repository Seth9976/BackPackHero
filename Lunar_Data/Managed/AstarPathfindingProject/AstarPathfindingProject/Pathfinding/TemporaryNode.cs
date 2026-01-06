using System;

namespace Pathfinding
{
	// Token: 0x020000AF RID: 175
	public struct TemporaryNode
	{
		// Token: 0x040003A8 RID: 936
		public uint associatedNode;

		// Token: 0x040003A9 RID: 937
		public Int3 position;

		// Token: 0x040003AA RID: 938
		public int targetIndex;

		// Token: 0x040003AB RID: 939
		public TemporaryNodeType type;
	}
}
