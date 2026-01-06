using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000EC RID: 236
	public abstract class OperatorException : InvalidCastException
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0001C175 File Offset: 0x0001A375
		protected OperatorException()
		{
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001C17D File Offset: 0x0001A37D
		protected OperatorException(string message)
			: base(message)
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001C186 File Offset: 0x0001A386
		protected OperatorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
