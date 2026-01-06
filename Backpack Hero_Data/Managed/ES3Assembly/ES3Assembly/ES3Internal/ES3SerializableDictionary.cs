using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public abstract class ES3SerializableDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISerializationCallbackReceiver
	{
		// Token: 0x0600044B RID: 1099
		protected abstract bool KeysAreEqual(TKey a, TKey b);

		// Token: 0x0600044C RID: 1100
		protected abstract bool ValuesAreEqual(TVal a, TVal b);

		// Token: 0x0600044D RID: 1101 RVA: 0x00020DE8 File Offset: 0x0001EFE8
		public void OnBeforeSerialize()
		{
			this._Keys = new List<TKey>();
			this._Values = new List<TVal>();
			foreach (KeyValuePair<TKey, TVal> keyValuePair in this)
			{
				try
				{
					this._Keys.Add(keyValuePair.Key);
					this._Values.Add(keyValuePair.Value);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00020E7C File Offset: 0x0001F07C
		public void OnAfterDeserialize()
		{
			if (this._Keys == null || this._Values == null)
			{
				return;
			}
			if (this._Keys.Count != this._Values.Count)
			{
				throw new Exception(string.Format("Key count is different to value count after deserialising dictionary.", Array.Empty<object>()));
			}
			base.Clear();
			for (int i = 0; i < this._Keys.Count; i++)
			{
				if (this._Keys[i] != null)
				{
					try
					{
						base.Add(this._Keys[i], this._Values[i]);
					}
					catch
					{
					}
				}
			}
			this._Keys = null;
			this._Values = null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00020F38 File Offset: 0x0001F138
		public int RemoveNullValues()
		{
			List<TKey> list = (from pair in this
				where pair.Value == null
				select pair.Key).ToList<TKey>();
			foreach (TKey tkey in list)
			{
				base.Remove(tkey);
			}
			return list.Count;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00020FDC File Offset: 0x0001F1DC
		public bool ChangeKey(TKey oldKey, TKey newKey)
		{
			if (this.KeysAreEqual(oldKey, newKey))
			{
				return false;
			}
			TVal tval = base[oldKey];
			base.Remove(oldKey);
			base[newKey] = tval;
			return true;
		}

		// Token: 0x04000121 RID: 289
		[SerializeField]
		private List<TKey> _Keys;

		// Token: 0x04000122 RID: 290
		[SerializeField]
		private List<TVal> _Values;
	}
}
