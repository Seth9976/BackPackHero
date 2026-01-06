using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000144 RID: 324
	public abstract class LudiqBehaviour : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x00026440 File Offset: 0x00024640
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
				Debug.LogError(string.Format("Failed to serialize behaviour.\n{0}", ex), this);
			}
			Serialization.isUnitySerializing = false;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000264A0 File Offset: 0x000246A0
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
				Debug.LogError(string.Format("Failed to deserialize behaviour.\n{0}", ex), this);
			}
			Serialization.isUnitySerializing = false;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00026504 File Offset: 0x00024704
		protected virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00026506 File Offset: 0x00024706
		protected virtual void OnAfterSerialize()
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00026508 File Offset: 0x00024708
		protected virtual void OnBeforeDeserialize()
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002650A File Offset: 0x0002470A
		protected virtual void OnAfterDeserialize()
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002650C File Offset: 0x0002470C
		protected virtual void ShowData()
		{
			this._data.ShowString(this.ToString());
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002651F File Offset: 0x0002471F
		public override string ToString()
		{
			return this.ToSafeString();
		}

		// Token: 0x04000216 RID: 534
		[SerializeField]
		[DoNotSerialize]
		protected SerializationData _data;
	}
}
