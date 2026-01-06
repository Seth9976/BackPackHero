using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000003 RID: 3
	internal class ActivationPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002167 File Offset: 0x00000367
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.None;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216A File Offset: 0x0000036A
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return Playable.Create(graph, 0);
		}
	}
}
