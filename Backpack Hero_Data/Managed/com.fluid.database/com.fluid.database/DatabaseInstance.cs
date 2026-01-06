using System;
using UnityEngine;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000003 RID: 3
	public class DatabaseInstance : IDatabaseInstance
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002050 File Offset: 0x00000250
		public IKeyValueData<bool> Bools { get; } = new KeyValueDataBool();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002058 File Offset: 0x00000258
		public IKeyValueData<string> Strings { get; } = new KeyValueDataString();

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002060 File Offset: 0x00000260
		public IKeyValueData<int> Ints { get; } = new KeyValueDataInt();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002068 File Offset: 0x00000268
		public IKeyValueData<float> Floats { get; } = new KeyValueDataFloat();

		// Token: 0x0600000C RID: 12 RVA: 0x00002070 File Offset: 0x00000270
		public void Clear()
		{
			this.Strings.Clear();
			this.Bools.Clear();
			this.Ints.Clear();
			this.Floats.Clear();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020A0 File Offset: 0x000002A0
		public string Save()
		{
			return JsonUtility.ToJson(new DatabaseInstance.SaveData
			{
				strings = this.Strings.Save(),
				bools = this.Bools.Save(),
				ints = this.Ints.Save(),
				floats = this.Floats.Save()
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020FC File Offset: 0x000002FC
		public void Load(string save)
		{
			DatabaseInstance.SaveData saveData = JsonUtility.FromJson<DatabaseInstance.SaveData>(save);
			this.Strings.Load(saveData.strings);
			this.Bools.Load(saveData.bools);
			this.Ints.Load(saveData.ints);
			this.Floats.Load(saveData.floats);
		}

		// Token: 0x02000013 RID: 19
		public class SaveData
		{
			// Token: 0x04000013 RID: 19
			public string strings;

			// Token: 0x04000014 RID: 20
			public string bools;

			// Token: 0x04000015 RID: 21
			public string ints;

			// Token: 0x04000016 RID: 22
			public string floats;
		}
	}
}
