using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000145 RID: 325
	public abstract class LudiqScriptableObject : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060008BC RID: 2236 RVA: 0x00026530 File Offset: 0x00024730
		// (remove) Token: 0x060008BD RID: 2237 RVA: 0x00026568 File Offset: 0x00024768
		internal event Action OnDestroyActions;

		// Token: 0x060008BE RID: 2238 RVA: 0x000265A0 File Offset: 0x000247A0
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (Serialization.isCustomSerializing)
			{
				return;
			}
			Serialization.isUnitySerializing = true;
			try
			{
				this.OnBeforeSerialize();
				this._data = this.Serialize(true);
				this.OnAfterSerialize();
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("Failed to serialize scriptable object.\n{0}", ex), this);
			}
			Serialization.isUnitySerializing = false;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00026600 File Offset: 0x00024800
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (Serialization.isCustomSerializing)
			{
				return;
			}
			Serialization.isUnitySerializing = true;
			try
			{
				object obj = this;
				this.OnBeforeDeserialize();
				this._data.DeserializeInto(ref obj, true);
				this.OnAfterDeserialize();
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("Failed to deserialize scriptable object.\n{0}", ex), this);
			}
			Serialization.isUnitySerializing = false;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00026664 File Offset: 0x00024864
		protected virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00026666 File Offset: 0x00024866
		protected virtual void OnAfterSerialize()
		{
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00026668 File Offset: 0x00024868
		protected virtual void OnBeforeDeserialize()
		{
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002666A File Offset: 0x0002486A
		protected virtual void OnAfterDeserialize()
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002666C File Offset: 0x0002486C
		protected virtual void OnPostDeserializeInEditor()
		{
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002666E File Offset: 0x0002486E
		private void OnDestroy()
		{
			Action onDestroyActions = this.OnDestroyActions;
			if (onDestroyActions == null)
			{
				return;
			}
			onDestroyActions();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00026680 File Offset: 0x00024880
		protected virtual void ShowData()
		{
			this._data.ShowString(this.ToString());
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00026693 File Offset: 0x00024893
		public override string ToString()
		{
			return this.ToSafeString();
		}

		// Token: 0x04000217 RID: 535
		[SerializeField]
		[DoNotSerialize]
		protected SerializationData _data;
	}
}
