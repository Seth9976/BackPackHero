using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000058 RID: 88
	public class InvalidConversionException : InvalidCastException
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00006575 File Offset: 0x00004775
		public InvalidConversionException()
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000657D File Offset: 0x0000477D
		public InvalidConversionException(string message)
			: base(message)
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006586 File Offset: 0x00004786
		public InvalidConversionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
