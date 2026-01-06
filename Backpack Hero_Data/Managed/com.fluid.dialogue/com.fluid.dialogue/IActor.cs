using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000003 RID: 3
	public interface IActor
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7
		string DisplayName { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8
		Sprite Portrait { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000009 RID: 9
		float TalkPitch { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000A RID: 10
		IActor.ActorType actorType { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000B RID: 11
		List<string> Tasks { get; }

		// Token: 0x02000058 RID: 88
		public enum ActorType
		{
			// Token: 0x0400009D RID: 157
			NPC,
			// Token: 0x0400009E RID: 158
			Player
		}
	}
}
