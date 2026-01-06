using System;

namespace System
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : MonoTODOAttribute
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005749 File Offset: 0x00003949
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
