using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000369 RID: 873
	[DebuggerDisplay("id = {id}, keyword = {keyword}, number = {number}, boolean = {boolean}, color = {color}, object = {resource}")]
	[StructLayout(2)]
	internal struct StyleValue
	{
		// Token: 0x04000DDD RID: 3549
		[FieldOffset(0)]
		public StylePropertyId id;

		// Token: 0x04000DDE RID: 3550
		[FieldOffset(4)]
		public StyleKeyword keyword;

		// Token: 0x04000DDF RID: 3551
		[FieldOffset(8)]
		public float number;

		// Token: 0x04000DE0 RID: 3552
		[FieldOffset(8)]
		public Length length;

		// Token: 0x04000DE1 RID: 3553
		[FieldOffset(8)]
		public Color color;

		// Token: 0x04000DE2 RID: 3554
		[FieldOffset(8)]
		public GCHandle resource;
	}
}
