using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000089 RID: 137
	[AttributeUsage(256)]
	[RequiredByNativeCode]
	public sealed class NativeFixedLengthAttribute : Attribute
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00004388 File Offset: 0x00002588
		public NativeFixedLengthAttribute(int fixedLength)
		{
			this.FixedLength = fixedLength;
		}

		// Token: 0x04000209 RID: 521
		public int FixedLength;
	}
}
