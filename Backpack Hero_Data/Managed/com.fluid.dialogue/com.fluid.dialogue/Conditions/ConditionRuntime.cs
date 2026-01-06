using System;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues.Conditions
{
	// Token: 0x02000032 RID: 50
	public class ConditionRuntime : ICondition, IUniqueId
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000041EA File Offset: 0x000023EA
		public string UniqueId { get; }

		// Token: 0x060000F5 RID: 245 RVA: 0x000041F2 File Offset: 0x000023F2
		public ConditionRuntime(IDialogueController dialogueController, string uniqueId, IConditionData data)
		{
			this._data = data;
			this._dialogueController = dialogueController;
			this.UniqueId = uniqueId;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000420F File Offset: 0x0000240F
		public bool GetIsValid(INode parent)
		{
			if (!this._initTriggered)
			{
				this._data.OnInit(this._dialogueController);
				this._initTriggered = true;
			}
			return this._data.OnGetIsValid(parent);
		}

		// Token: 0x04000063 RID: 99
		private readonly IDialogueController _dialogueController;

		// Token: 0x04000064 RID: 100
		private readonly IConditionData _data;

		// Token: 0x04000065 RID: 101
		private bool _initTriggered;
	}
}
