using System;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x020000C7 RID: 199
	public class ProfiledSegmentCollection : KeyedCollection<string, ProfiledSegment>
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000ACDD File Offset: 0x00008EDD
		protected override string GetKeyForItem(ProfiledSegment item)
		{
			return item.name;
		}
	}
}
