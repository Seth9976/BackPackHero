using System;
using UnityEngine;

namespace CleverCrow.Fluid.SimpleSpellcheck
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.Field)]
	public class TextAreaSpellCheckAttribute : PropertyAttribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public int Lines { get; }

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public TextAreaSpellCheckAttribute()
		{
			this.Lines = 3;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public TextAreaSpellCheckAttribute(int lines)
		{
			this.Lines = lines;
		}
	}
}
