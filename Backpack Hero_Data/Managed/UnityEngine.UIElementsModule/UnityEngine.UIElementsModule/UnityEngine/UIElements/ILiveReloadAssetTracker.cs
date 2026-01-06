using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000039 RID: 57
	internal interface ILiveReloadAssetTracker<T> where T : ScriptableObject
	{
		// Token: 0x0600015A RID: 346
		int StartTrackingAsset(T asset);

		// Token: 0x0600015B RID: 347
		void StopTrackingAsset(T asset);

		// Token: 0x0600015C RID: 348
		bool IsTrackingAsset(T asset);

		// Token: 0x0600015D RID: 349
		bool IsTrackingAssets();

		// Token: 0x0600015E RID: 350
		bool CheckTrackedAssetsDirty();

		// Token: 0x0600015F RID: 351
		void UpdateAssetTrackerCounts(T asset, int newDirtyCount, int newElementCount, int newInlinePropertiesCount, int newAttributePropertiesDirtyCount);

		// Token: 0x06000160 RID: 352
		void OnAssetsImported(HashSet<T> changedAssets, HashSet<string> deletedAssets);

		// Token: 0x06000161 RID: 353
		void OnTrackedAssetChanged();
	}
}
