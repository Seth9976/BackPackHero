using System;
using System.Collections.Generic;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000C9 RID: 201
	public class ES3Prefab : MonoBehaviour
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0001EF80 File Offset: 0x0001D180
		public void Awake()
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase == null)
			{
				return;
			}
			foreach (KeyValuePair<Object, long> keyValuePair in this.localRefs)
			{
				if (keyValuePair.Key != null)
				{
					es3ReferenceMgrBase.Add(keyValuePair.Key);
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		public long Get(Object obj)
		{
			long num;
			if (this.localRefs.TryGetValue(obj, out num))
			{
				return num;
			}
			return -1L;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001F020 File Offset: 0x0001D220
		public long Add(Object obj)
		{
			long newRefID;
			if (this.localRefs.TryGetValue(obj, out newRefID))
			{
				return newRefID;
			}
			if (!ES3ReferenceMgrBase.CanBeSaved(obj))
			{
				return -1L;
			}
			newRefID = ES3Prefab.GetNewRefID();
			this.localRefs.Add(obj, newRefID);
			return newRefID;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001F060 File Offset: 0x0001D260
		public Dictionary<string, string> GetReferences()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase == null)
			{
				return dictionary;
			}
			foreach (KeyValuePair<Object, long> keyValuePair in this.localRefs)
			{
				long num = es3ReferenceMgrBase.Add(keyValuePair.Key);
				dictionary.Add(keyValuePair.Value.ToString(), num.ToString());
			}
			return dictionary;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public void ApplyReferences(Dictionary<long, long> localToGlobal)
		{
			if (ES3ReferenceMgrBase.Current == null)
			{
				return;
			}
			foreach (KeyValuePair<Object, long> keyValuePair in this.localRefs)
			{
				long num;
				if (localToGlobal.TryGetValue(keyValuePair.Value, out num))
				{
					ES3ReferenceMgrBase.Current.Add(keyValuePair.Key, num);
				}
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001F170 File Offset: 0x0001D370
		public static long GetNewRefID()
		{
			return ES3ReferenceMgrBase.GetNewRefID();
		}

		// Token: 0x0400010A RID: 266
		public long prefabId = ES3Prefab.GetNewRefID();

		// Token: 0x0400010B RID: 267
		public ES3RefIdDictionary localRefs = new ES3RefIdDictionary();
	}
}
