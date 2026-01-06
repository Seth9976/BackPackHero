using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000086 RID: 134
	public abstract class VersionedMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver, IVersionedMonoBehaviourInternal
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x00027B3B File Offset: 0x00025D3B
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00027B56 File Offset: 0x00025D56
		protected virtual void Reset()
		{
			this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00027B6A File Offset: 0x00025D6A
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00027B6C File Offset: 0x00025D6C
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			int num = this.OnUpgradeSerializedData(this.version, false);
			if (num >= 0)
			{
				this.version = num;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00027B92 File Offset: 0x00025D92
		protected virtual int OnUpgradeSerializedData(int version, bool unityThread)
		{
			return 1;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00027B98 File Offset: 0x00025D98
		void IVersionedMonoBehaviourInternal.UpgradeFromUnityThread()
		{
			int num = this.OnUpgradeSerializedData(this.version, true);
			if (num < 0)
			{
				throw new Exception();
			}
			this.version = num;
		}

		// Token: 0x040003DA RID: 986
		[SerializeField]
		[HideInInspector]
		private int version;
	}
}
