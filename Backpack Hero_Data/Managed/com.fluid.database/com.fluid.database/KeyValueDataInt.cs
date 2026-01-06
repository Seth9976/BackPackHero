using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000011 RID: 17
	public class KeyValueDataInt : KeyValueDataBase<int>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000026BC File Offset: 0x000008BC
		public override string Save()
		{
			KeyValueDataInt.SaveData saveData = new KeyValueDataInt.SaveData();
			saveData.keyValuePairs = (from kvp in this._data.ToArray<KeyValuePair<string, int>>()
				select new KeyValueDataInt.SaveKeyValue
				{
					key = kvp.Key,
					value = kvp.Value
				}).ToList<KeyValueDataInt.SaveKeyValue>();
			return JsonUtility.ToJson(saveData);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002710 File Offset: 0x00000910
		public override void Load(string save)
		{
			this._data = JsonUtility.FromJson<KeyValueDataInt.SaveData>(save).keyValuePairs.ToDictionary((KeyValueDataInt.SaveKeyValue k) => k.key, (KeyValueDataInt.SaveKeyValue v) => v.value);
		}

		// Token: 0x0200001A RID: 26
		[Serializable]
		private class SaveData
		{
			// Token: 0x04000025 RID: 37
			public List<KeyValueDataInt.SaveKeyValue> keyValuePairs;
		}

		// Token: 0x0200001B RID: 27
		[Serializable]
		private class SaveKeyValue
		{
			// Token: 0x04000026 RID: 38
			public string key;

			// Token: 0x04000027 RID: 39
			public int value;
		}
	}
}
