using System;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Conditions
{
	// Token: 0x02000033 RID: 51
	public interface ICondition : IUniqueId
	{
		// Token: 0x060000F7 RID: 247
		bool GetIsValid(INode parent);
	}
}
