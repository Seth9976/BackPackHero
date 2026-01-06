using System;

namespace Pathfinding.Graphs.Grid.Rules
{
	// Token: 0x02000205 RID: 517
	public class CustomGridGraphRuleEditorAttribute : Attribute
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0004F51D File Offset: 0x0004D71D
		public CustomGridGraphRuleEditorAttribute(Type type, string name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x04000974 RID: 2420
		public Type type;

		// Token: 0x04000975 RID: 2421
		public string name;
	}
}
