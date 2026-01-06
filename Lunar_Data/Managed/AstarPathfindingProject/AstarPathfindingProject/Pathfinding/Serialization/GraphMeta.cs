using System;
using System.Collections.Generic;

namespace Pathfinding.Serialization
{
	// Token: 0x02000231 RID: 561
	public class GraphMeta
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x00053B6C File Offset: 0x00051D6C
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

		// Token: 0x04000A5E RID: 2654
		public Version version;

		// Token: 0x04000A5F RID: 2655
		public int graphs;

		// Token: 0x04000A60 RID: 2656
		public List<string> guids;

		// Token: 0x04000A61 RID: 2657
		public List<string> typeNames;
	}
}
