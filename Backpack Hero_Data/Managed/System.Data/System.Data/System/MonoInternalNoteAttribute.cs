using System;

namespace System
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005749 File Offset: 0x00003949
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
