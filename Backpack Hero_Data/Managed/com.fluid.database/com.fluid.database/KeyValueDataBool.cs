using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x0200000F RID: 15
	public class KeyValueDataBool : KeyValueDataBase<bool>
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000253C File Offset: 0x0000073C
		public override string Save()
		{
			KeyValueDataBool.SaveData saveData = new KeyValueDataBool.SaveData();
			saveData.keyValuePairs = (from kvp in this._data.ToArray<KeyValuePair<string, bool>>()
				select new KeyValueDataBool.SaveKeyValue
				{
					key = kvp.Key,
					value = kvp.Value
				}).ToList<KeyValueDataBool.SaveKeyValue>();
			return JsonUtility.ToJson(saveData);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002590 File Offset: 0x00000790
		public override void Load(string save)
		{
			this._data = JsonUtility.FromJson<KeyValueDataBool.SaveData>(save).keyValuePairs.ToDictionary((KeyValueDataBool.SaveKeyValue k) => k.key, (KeyValueDataBool.SaveKeyValue v) => v.value);
		}

		// Token: 0x02000014 RID: 20
		[Serializable]
		private class SaveData
		{
			// Token: 0x04000017 RID: 23
			public List<KeyValueDataBool.SaveKeyValue> keyValuePairs;
		}

		// Token: 0x02000015 RID: 21
		[Serializable]
		private class SaveKeyValue
		{
			// Token: 0x04000018 RID: 24
			public string key;

			// Token: 0x04000019 RID: 25
			public bool value;
		}
	}
}
