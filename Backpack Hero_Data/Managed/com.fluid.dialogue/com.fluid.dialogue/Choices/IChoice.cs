using System;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Choices
{
	// Token: 0x0200002F RID: 47
	public interface IChoice : IUniqueId
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E7 RID: 231
		string Text { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E8 RID: 232
		string key { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E9 RID: 233
		bool keyOverride { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000EA RID: 234
		bool externalKey { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EB RID: 235
		string prefix { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EC RID: 236
		bool IsValid { get; }

		// Token: 0x060000ED RID: 237
		INode GetValidChildNode();
	}
}
