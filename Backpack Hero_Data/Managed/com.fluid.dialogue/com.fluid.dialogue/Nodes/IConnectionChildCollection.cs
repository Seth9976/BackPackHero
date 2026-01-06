using System;
using System.Collections.Generic;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000020 RID: 32
	public interface IConnectionChildCollection
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009A RID: 154
		IReadOnlyList<NodeDataBase> Children { get; }

		// Token: 0x0600009B RID: 155
		void AddConnectionChild(NodeDataBase child);

		// Token: 0x0600009C RID: 156
		void RemoveConnectionChild(NodeDataBase child);

		// Token: 0x0600009D RID: 157
		void SortConnectionsByPosition();

		// Token: 0x0600009E RID: 158
		void ClearConnectionChildren();
	}
}
