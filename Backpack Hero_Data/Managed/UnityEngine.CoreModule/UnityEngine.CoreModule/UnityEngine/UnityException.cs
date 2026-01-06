using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000218 RID: 536
	[RequiredByNativeCode]
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x00025C41 File Offset: 0x00023E41
		public UnityException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00025C5C File Offset: 0x00023E5C
		public UnityException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00025C73 File Offset: 0x00023E73
		public UnityException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00025C8B File Offset: 0x00023E8B
		protected UnityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000800 RID: 2048
		private const int Result = -2147467261;

		// Token: 0x04000801 RID: 2049
		private string unityStackTrace;
	}
}
