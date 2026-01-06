using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CD RID: 205
	public sealed class GenericClosingException : Exception
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x0000C50F File Offset: 0x0000A70F
		public GenericClosingException(string message)
			: base(message)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000C518 File Offset: 0x0000A718
		public GenericClosingException(Type open, Type closed)
			: base(string.Format("Open-constructed type '{0}' is not assignable from closed-constructed type '{1}'.", open, closed))
		{
		}
	}
}
