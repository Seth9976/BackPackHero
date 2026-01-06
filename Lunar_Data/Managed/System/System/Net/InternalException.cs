using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x020003ED RID: 1005
	internal class InternalException : SystemException
	{
		// Token: 0x060020AC RID: 8364 RVA: 0x000778C3 File Offset: 0x00075AC3
		internal InternalException()
		{
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0005569B File Offset: 0x0005389B
		internal InternalException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
