using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Class)]
	[Obsolete("TrackMediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
	public class TrackMediaType : Attribute
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000997C File Offset: 0x00007B7C
		public TrackMediaType(TimelineAsset.MediaType mt)
		{
			this.m_MediaType = mt;
		}

		// Token: 0x040000E7 RID: 231
		public readonly TimelineAsset.MediaType m_MediaType;
	}
}
