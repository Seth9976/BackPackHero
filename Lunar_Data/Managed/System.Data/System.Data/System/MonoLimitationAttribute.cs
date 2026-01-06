using System;

namespace System
{
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : MonoTODOAttribute
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00005749 File Offset: 0x00003949
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
