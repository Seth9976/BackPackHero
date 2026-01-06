using System;
using System.Collections.Generic;

namespace UnityEngine.Playables
{
	// Token: 0x02000439 RID: 1081
	public interface IPlayableAsset
	{
		// Token: 0x0600257F RID: 9599
		Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002580 RID: 9600
		double duration { get; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002581 RID: 9601
		IEnumerable<PlayableBinding> outputs { get; }
	}
}
