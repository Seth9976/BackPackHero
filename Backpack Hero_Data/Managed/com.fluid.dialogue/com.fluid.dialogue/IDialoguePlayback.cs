using System;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x0200000C RID: 12
	public interface IDialoguePlayback
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52
		IDialogueEvents Events { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53
		IDialogueController ParentCtrl { get; }

		// Token: 0x06000036 RID: 54
		void Next();

		// Token: 0x06000037 RID: 55
		void Play();

		// Token: 0x06000038 RID: 56
		void Tick();

		// Token: 0x06000039 RID: 57
		void SelectChoice(int index);

		// Token: 0x0600003A RID: 58
		void Stop();
	}
}
