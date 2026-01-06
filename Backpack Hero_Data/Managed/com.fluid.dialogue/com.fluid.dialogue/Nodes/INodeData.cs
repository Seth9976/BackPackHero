using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Choices;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x0200001F RID: 31
	public interface INodeData : IGetRuntime<INode>, ISetup, IUniqueId, IConnectionChildCollection
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000098 RID: 152
		string Text { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000099 RID: 153
		List<ChoiceData> Choices { get; }
	}
}
