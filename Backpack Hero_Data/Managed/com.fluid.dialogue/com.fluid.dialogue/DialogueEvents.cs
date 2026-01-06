using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Nodes;
using CleverCrow.Fluid.Utilities.UnityEvents;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x0200000E RID: 14
	public class DialogueEvents : IDialogueEvents
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000278A File Offset: 0x0000098A
		public IUnityEvent Begin { get; } = new UnityEventPlus();

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002792 File Offset: 0x00000992
		public IUnityEvent End { get; } = new UnityEventPlus();

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000279A File Offset: 0x0000099A
		public IUnityEvent<IActor, string> Speak { get; } = new UnityEventPlus<IActor, string>();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000027A2 File Offset: 0x000009A2
		public IUnityEvent<IActor, string, AudioClip> SpeakWithAudio { get; } = new UnityEventPlus<IActor, string, AudioClip>();

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000027AA File Offset: 0x000009AA
		public IUnityEvent<IActor, string, List<IChoice>> Choice { get; } = new UnityEventPlus<IActor, string, List<IChoice>>();

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000027B2 File Offset: 0x000009B2
		public IUnityEvent<INode> NodeEnter { get; } = new UnityEventPlus<INode>();
	}
}
