using System;

namespace Pathfinding.Serialization
{
	// Token: 0x02000236 RID: 566
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class JsonDynamicTypeAliasAttribute : Attribute
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x00053BF8 File Offset: 0x00051DF8
		public JsonDynamicTypeAliasAttribute(string alias, Type type)
		{
			this.alias = alias;
			this.type = type;
		}

		// Token: 0x04000A65 RID: 2661
		public string alias;

		// Token: 0x04000A66 RID: 2662
		public Type type;
	}
}
