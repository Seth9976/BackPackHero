using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000040 RID: 64
	internal class ImmediateModeException : Exception
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000081A0 File Offset: 0x000063A0
		public ImmediateModeException(Exception inner)
			: base("", inner)
		{
		}
	}
}
