using System;
using System.Collections.Generic;

namespace Pathfinding.Serialization
{
	// Token: 0x020000B2 RID: 178
	public class GraphMeta
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x00035624 File Offset: 0x00033824
		public Type GetGraphType(int index, Type[] availableGraphTypes)
		{
			if (string.IsNullOrEmpty(this.typeNames[index]))
			{
				return null;
			}
			for (int i = 0; i < availableGraphTypes.Length; i++)
			{
				if (availableGraphTypes[i].FullName == this.typeNames[index])
				{
					return availableGraphTypes[i];
				}
			}
			throw new Exception("No graph of type '" + this.typeNames[index] + "' could be created, type does not exist");
		}

		// Token: 0x040004BA RID: 1210
		public Version version;

		// Token: 0x040004BB RID: 1211
		public int graphs;

		// Token: 0x040004BC RID: 1212
		public List<string> guids;

		// Token: 0x040004BD RID: 1213
		public List<string> typeNames;
	}
}
