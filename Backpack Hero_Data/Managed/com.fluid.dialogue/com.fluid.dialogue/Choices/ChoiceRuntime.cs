using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Choices
{
	// Token: 0x0200002E RID: 46
	public class ChoiceRuntime : IChoice, IUniqueId
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000409B File Offset: 0x0000229B
		public string UniqueId { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000040A3 File Offset: 0x000022A3
		public string Text { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000040AB File Offset: 0x000022AB
		public string key { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000040B3 File Offset: 0x000022B3
		public bool keyOverride { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000040BB File Offset: 0x000022BB
		public bool externalKey { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000040C3 File Offset: 0x000022C3
		public string prefix { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000040CB File Offset: 0x000022CB
		public bool IsValid
		{
			get
			{
				if (this.Children.Count != 0)
				{
					return this.Children.Find((INode c) => c.IsValid) != null;
				}
				return true;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000410C File Offset: 0x0000230C
		private List<INode> Children
		{
			get
			{
				List<INode> list;
				if ((list = this._childrenRuntimeCache) == null)
				{
					list = (this._childrenRuntimeCache = this._children.Select(new Func<INodeData, INode>(this._runtime.GetCopy)).ToList<INode>());
				}
				return list;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004150 File Offset: 0x00002350
		public ChoiceRuntime(IGraph runtime, string text, string key, bool keyOverride, bool externalKey, string prefix, string uniqueId, List<INodeData> children)
		{
			this._runtime = runtime;
			this.Text = text;
			this.key = key;
			this.keyOverride = keyOverride;
			this.externalKey = externalKey;
			this.prefix = prefix;
			this.UniqueId = uniqueId;
			this._children = children;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000041A0 File Offset: 0x000023A0
		public INode GetValidChildNode()
		{
			return this.Children.Find((INode c) => c.IsValid);
		}

		// Token: 0x0400005A RID: 90
		private readonly IGraph _runtime;

		// Token: 0x0400005B RID: 91
		private readonly List<INodeData> _children;

		// Token: 0x0400005C RID: 92
		private List<INode> _childrenRuntimeCache;
	}
}
