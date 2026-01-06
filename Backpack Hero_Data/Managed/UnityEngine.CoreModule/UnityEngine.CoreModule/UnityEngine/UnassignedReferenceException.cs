using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200021A RID: 538
	[Serializable]
	public class UnassignedReferenceException : SystemException
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x00025C41 File Offset: 0x00023E41
		public UnassignedReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00025C5C File Offset: 0x00023E5C
		public UnassignedReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00025C73 File Offset: 0x00023E73
		public UnassignedReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00025C8B File Offset: 0x00023E8B
		protected UnassignedReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000804 RID: 2052
		private const int Result = -2147467261;

		// Token: 0x04000805 RID: 2053
		private string unityStackTrace;
	}
}
