using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000030 RID: 48
	public interface ILayerable
	{
		// Token: 0x06000263 RID: 611
		Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount);
	}
}
