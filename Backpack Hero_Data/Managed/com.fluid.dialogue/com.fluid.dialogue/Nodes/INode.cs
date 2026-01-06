using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x0200001B RID: 27
	public interface INode : IUniqueId
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000081 RID: 129
		List<IAction> EnterActions { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000082 RID: 130
		List<IAction> ExitActions { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000083 RID: 131
		bool IsValid { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000084 RID: 132
		List<IChoice> HubChoices { get; }

		// Token: 0x06000085 RID: 133
		INode Next();

		// Token: 0x06000086 RID: 134
		void Play(IDialoguePlayback playback);

		// Token: 0x06000087 RID: 135
		IChoice GetChoice(int index);
	}
}
