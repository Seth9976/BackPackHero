using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x06001769 RID: 5993 RVA: 0x00025C41 File Offset: 0x00023E41
		public MissingReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00025C5C File Offset: 0x00023E5C
		public MissingReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00025C73 File Offset: 0x00023E73
		public MissingReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00025C8B File Offset: 0x00023E8B
		protected MissingReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000806 RID: 2054
		private const int Result = -2147467261;

		// Token: 0x04000807 RID: 2055
		private string unityStackTrace;
	}
}
