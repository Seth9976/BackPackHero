using System;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000002 RID: 2
	public interface IDatabaseInstance
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		IKeyValueData<bool> Bools { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		IKeyValueData<string> Strings { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		IKeyValueData<int> Ints { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		IKeyValueData<float> Floats { get; }

		// Token: 0x06000005 RID: 5
		void Clear();

		// Token: 0x06000006 RID: 6
		string Save();

		// Token: 0x06000007 RID: 7
		void Load(string save);
	}
}
