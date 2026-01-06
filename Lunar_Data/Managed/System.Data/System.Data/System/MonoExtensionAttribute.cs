using System;

namespace System
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : MonoTODOAttribute
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00005749 File Offset: 0x00003949
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
