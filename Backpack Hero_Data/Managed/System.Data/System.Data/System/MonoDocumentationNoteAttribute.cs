using System;

namespace System
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : MonoTODOAttribute
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00005749 File Offset: 0x00003949
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
