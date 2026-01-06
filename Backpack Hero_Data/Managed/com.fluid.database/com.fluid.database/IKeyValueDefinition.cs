using System;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x02000004 RID: 4
	public interface IKeyValueDefinition<V>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16
		string Key { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17
		V DefaultValue { get; }
	}
}
