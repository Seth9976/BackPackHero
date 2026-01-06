using System;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x0200000D RID: 13
	public interface IKeyValueData<V>
	{
		// Token: 0x06000027 RID: 39
		V Get(string key, V defaultValue = default(V));

		// Token: 0x06000028 RID: 40
		void Set(string key, V value);

		// Token: 0x06000029 RID: 41
		bool Has(string key);

		// Token: 0x0600002A RID: 42
		void AddKeyListener(string key, Action<V> callback);

		// Token: 0x0600002B RID: 43
		void RemoveKeyListener(string test, Action<V> callback);

		// Token: 0x0600002C RID: 44
		void Clear();

		// Token: 0x0600002D RID: 45
		string Save();

		// Token: 0x0600002E RID: 46
		void Load(string save);
	}
}
