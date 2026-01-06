using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002E RID: 46
	public static class TrackAssetExtensions
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000897E File Offset: 0x00006B7E
		public static GroupTrack GetGroup(this TrackAsset asset)
		{
			if (asset == null)
			{
				return null;
			}
			return asset.parent as GroupTrack;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008998 File Offset: 0x00006B98
		public static void SetGroup(this TrackAsset asset, GroupTrack group)
		{
			if (asset == null || asset == group || asset.parent == group)
			{
				return;
			}
			if (group != null && asset.timelineAsset != group.timelineAsset)
			{
				throw new InvalidOperationException("Cannot assign to a group in a different timeline");
			}
			TimelineAsset timelineAsset = asset.timelineAsset;
			TrackAsset trackAsset = asset.parent as TrackAsset;
			TimelineAsset timelineAsset2 = asset.parent as TimelineAsset;
			if (trackAsset != null || timelineAsset2 != null)
			{
				if (timelineAsset2 != null)
				{
					timelineAsset2.RemoveTrack(asset);
				}
				else
				{
					trackAsset.RemoveSubTrack(asset);
				}
			}
			if (group == null)
			{
				asset.parent = asset.timelineAsset;
				timelineAsset.AddTrackInternal(asset);
				return;
			}
			group.AddChild(asset);
		}
	}
}
