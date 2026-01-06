using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000017 RID: 23
	[ExecuteAlways]
	[AddComponentMenu("")]
	internal class ProbeVolumePerSceneData : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00006314 File Offset: 0x00004514
		public void OnAfterDeserialize()
		{
			if (this.serializedAssets == null)
			{
				return;
			}
			this.assets = new Dictionary<ProbeVolumeState, ProbeVolumeAsset>();
			foreach (ProbeVolumePerSceneData.SerializableAssetItem serializableAssetItem in this.serializedAssets)
			{
				this.assets.Add(serializableAssetItem.state, serializableAssetItem.asset);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000638C File Offset: 0x0000458C
		public void OnBeforeSerialize()
		{
			if (this.assets == null || this.serializedAssets == null)
			{
				return;
			}
			this.serializedAssets.Clear();
			foreach (ProbeVolumeState probeVolumeState in this.assets.Keys)
			{
				ProbeVolumePerSceneData.SerializableAssetItem serializableAssetItem;
				serializableAssetItem.state = probeVolumeState;
				serializableAssetItem.asset = this.assets[probeVolumeState];
				this.serializedAssets.Add(serializableAssetItem);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006420 File Offset: 0x00004620
		internal void StoreAssetForState(ProbeVolumeState state, ProbeVolumeAsset asset)
		{
			this.assets[state] = asset;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006430 File Offset: 0x00004630
		internal void InvalidateAllAssets()
		{
			foreach (ProbeVolumeAsset probeVolumeAsset in this.assets.Values)
			{
				if (probeVolumeAsset != null)
				{
					ProbeReferenceVolume.instance.AddPendingAssetRemoval(probeVolumeAsset);
				}
			}
			this.assets.Clear();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000064A0 File Offset: 0x000046A0
		internal ProbeVolumeAsset GetCurrentStateAsset()
		{
			if (this.assets.ContainsKey(this.m_CurrentState))
			{
				return this.assets[this.m_CurrentState];
			}
			return null;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000064C8 File Offset: 0x000046C8
		internal void QueueAssetLoading()
		{
			ProbeReferenceVolume instance = ProbeReferenceVolume.instance;
			if (this.assets.ContainsKey(this.m_CurrentState) && this.assets[this.m_CurrentState] != null)
			{
				instance.AddPendingAssetLoading(this.assets[this.m_CurrentState]);
				this.m_PreviousState = this.m_CurrentState;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000652C File Offset: 0x0000472C
		internal void QueueAssetRemoval()
		{
			if (this.assets.ContainsKey(this.m_CurrentState) && this.assets[this.m_CurrentState] != null)
			{
				ProbeReferenceVolume.instance.AddPendingAssetRemoval(this.assets[this.m_CurrentState]);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006580 File Offset: 0x00004780
		private void OnEnable()
		{
			this.QueueAssetLoading();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006588 File Offset: 0x00004788
		private void OnDisable()
		{
			this.QueueAssetRemoval();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006590 File Offset: 0x00004790
		private void OnDestroy()
		{
			this.QueueAssetRemoval();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006598 File Offset: 0x00004798
		private void Update()
		{
			this.m_CurrentState = ProbeVolumeState.Default;
			if (this.m_PreviousState != this.m_CurrentState)
			{
				if (this.assets.ContainsKey(this.m_PreviousState) && this.assets[this.m_PreviousState] != null)
				{
					ProbeReferenceVolume.instance.AddPendingAssetRemoval(this.assets[this.m_PreviousState]);
				}
				this.QueueAssetLoading();
			}
		}

		// Token: 0x0400009E RID: 158
		internal Dictionary<ProbeVolumeState, ProbeVolumeAsset> assets = new Dictionary<ProbeVolumeState, ProbeVolumeAsset>();

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private List<ProbeVolumePerSceneData.SerializableAssetItem> serializedAssets;

		// Token: 0x040000A0 RID: 160
		private ProbeVolumeState m_CurrentState;

		// Token: 0x040000A1 RID: 161
		private ProbeVolumeState m_PreviousState = ProbeVolumeState.Invalid;

		// Token: 0x02000122 RID: 290
		[Serializable]
		private struct SerializableAssetItem
		{
			// Token: 0x040004B4 RID: 1204
			[SerializeField]
			public ProbeVolumeState state;

			// Token: 0x040004B5 RID: 1205
			[SerializeField]
			public ProbeVolumeAsset asset;
		}
	}
}
