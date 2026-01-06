using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004D RID: 77
	public interface IPropertyPreview
	{
		// Token: 0x060002E2 RID: 738
		void GatherProperties(PlayableDirector director, IPropertyCollector driver);
	}
}
