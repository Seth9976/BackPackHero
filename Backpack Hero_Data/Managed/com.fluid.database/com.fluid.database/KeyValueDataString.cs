using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000012 RID: 18
	public class KeyValueDataString : KeyValueDataBase<string>
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000277C File Offset: 0x0000097C
		public override string Save()
		{
			KeyValueDataString.SaveData saveData = new KeyValueDataString.SaveData();
			saveData.keyValuePairs = (from kvp in this._data.ToArray<KeyValuePair<string, string>>()
				select new KeyValueDataString.SaveKeyValue
				{
					key = kvp.Key,
					value = kvp.Value
				}).ToList<KeyValueDataString.SaveKeyValue>();
			return JsonUtility.ToJson(saveData);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027D0 File Offset: 0x000009D0
		public override void Load(string save)
		{
			this._data = JsonUtility.FromJson<KeyValueDataString.SaveData>(save).keyValuePairs.ToDictionary((KeyValueDataString.SaveKeyValue k) => k.key, (KeyValueDataString.SaveKeyValue v) => v.value);
		}

		// Token: 0x0200001D RID: 29
		[Serializable]
		private class SaveData
		{
			// Token: 0x0400002C RID: 44
			public List<KeyValueDataString.SaveKeyValue> keyValuePairs;
		}

		// Token: 0x0200001E RID: 30
		[Serializable]
		private class SaveKeyValue
		{
			// Token: 0x0400002D RID: 45
			public string key;

			// Token: 0x0400002E RID: 46
			public string value;
		}
	}
}
