using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000088 RID: 136
	[AttributeUsage(AttributeTargets.Field)]
	public class HLSLArray : Attribute
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x00014877 File Offset: 0x00012A77
		public HLSLArray(int arraySize, Type elementType)
		{
			this.arraySize = arraySize;
			this.elementType = elementType;
		}

		// Token: 0x040002CA RID: 714
		public int arraySize;

		// Token: 0x040002CB RID: 715
		public Type elementType;
	}
}
