using System;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions
{
	// Token: 0x02000038 RID: 56
	public abstract class ActionDataBase : NodeNestedDataBase<IAction>, IActionData
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000042A1 File Offset: 0x000024A1
		public virtual void OnInit(IDialogueController dialogue)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000042A3 File Offset: 0x000024A3
		public virtual void OnStart()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000042A5 File Offset: 0x000024A5
		public virtual ActionStatus OnUpdate()
		{
			return ActionStatus.Success;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000042A8 File Offset: 0x000024A8
		public virtual void OnExit()
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000042AA File Offset: 0x000024AA
		public virtual void OnReset()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000042AC File Offset: 0x000024AC
		public override IAction GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new ActionRuntime(dialogue, this._uniqueId, Object.Instantiate<ActionDataBase>(this));
		}
	}
}
