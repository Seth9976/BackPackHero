using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Graphs
{
	// Token: 0x0200002A RID: 42
	public class GraphRuntime : IGraph
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003EE0 File Offset: 0x000020E0
		public INode Root { get; }

		// Token: 0x060000CE RID: 206 RVA: 0x00003EE8 File Offset: 0x000020E8
		public GraphRuntime(IDialogueController dialogue, IGraphData data)
		{
			GraphRuntime <>4__this = this;
			this._dataToRuntime = data.Nodes.ToDictionary((INodeData k) => k, (INodeData v) => v.GetRuntime(<>4__this, dialogue));
			this.Root = this.GetCopy(data.Root);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003F5D File Offset: 0x0000215D
		public INode GetCopy(INodeData original)
		{
			return this._dataToRuntime[original];
		}

		// Token: 0x0400004F RID: 79
		private readonly Dictionary<INodeData, INode> _dataToRuntime;
	}
}
