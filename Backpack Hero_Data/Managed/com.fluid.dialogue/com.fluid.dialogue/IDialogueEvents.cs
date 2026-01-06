using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Nodes;
using CleverCrow.Fluid.Utilities.UnityEvents;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x0200000F RID: 15
	public interface IDialogueEvents
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000050 RID: 80
		IUnityEvent Begin { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000051 RID: 81
		IUnityEvent End { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000052 RID: 82
		IUnityEvent<IActor, string> Speak { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000053 RID: 83
		IUnityEvent<IActor, string, AudioClip> SpeakWithAudio { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000054 RID: 84
		IUnityEvent<IActor, string, List<IChoice>> Choice { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000055 RID: 85
		IUnityEvent<INode> NodeEnter { get; }
	}
}
