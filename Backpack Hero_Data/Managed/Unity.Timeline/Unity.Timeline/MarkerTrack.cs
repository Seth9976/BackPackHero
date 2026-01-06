using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000028 RID: 40
	[TrackBindingType(typeof(GameObject))]
	[HideInMenu]
	[ExcludeFromPreset]
	[Serializable]
	public class MarkerTrack : TrackAsset
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000856C File Offset: 0x0000676C
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				if (!(this == base.timelineAsset.markerTrack))
				{
					return base.outputs;
				}
				return new List<PlayableBinding> { ScriptPlayableBinding.Create(base.name, null, typeof(GameObject)) };
			}
		}
	}
}
