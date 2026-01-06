using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class PlayableTrack : TrackAsset
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000994E File Offset: 0x00007B4E
		protected override void OnCreateClip(TimelineClip clip)
		{
			if (clip.asset != null)
			{
				clip.displayName = clip.asset.GetType().Name;
			}
		}
	}
}
