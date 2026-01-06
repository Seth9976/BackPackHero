using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000010 RID: 16
	public class KeyValueDataFloat : KeyValueDataBase<float>
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000025FC File Offset: 0x000007FC
		public override string Save()
		{
			KeyValueDataFloat.SaveData saveData = new KeyValueDataFloat.SaveData();
			saveData.keyValuePairs = (from kvp in this._data.ToArray<KeyValuePair<string, float>>()
				select new KeyValueDataFloat.SaveKeyValue
				{
					key = kvp.Key,
					value = kvp.Value
				}).ToList<KeyValueDataFloat.SaveKeyValue>();
			return JsonUtility.ToJson(saveData);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002650 File Offset: 0x00000850
		public override void Load(string save)
		{
			this._data = JsonUtility.FromJson<KeyValueDataFloat.SaveData>(save).keyValuePairs.ToDictionary((KeyValueDataFloat.SaveKeyValue k) => k.key, (KeyValueDataFloat.SaveKeyValue v) => v.value);
		}

		// Token: 0x02000017 RID: 23
		[Serializable]
		private class SaveData
		{
			// Token: 0x0400001E RID: 30
			public List<KeyValueDataFloat.SaveKeyValue> keyValuePairs;
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		private class SaveKeyValue
		{
			// Token: 0x0400001F RID: 31
			public string key;

			// Token: 0x04000020 RID: 32
			public float value;
		}
	}
}
