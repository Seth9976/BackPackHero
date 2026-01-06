using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	public class MissingComponentException : SystemException
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x00025C41 File Offset: 0x00023E41
		public MissingComponentException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00025C5C File Offset: 0x00023E5C
		public MissingComponentException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00025C73 File Offset: 0x00023E73
		public MissingComponentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00025C8B File Offset: 0x00023E8B
		protected MissingComponentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000802 RID: 2050
		private const int Result = -2147467261;

		// Token: 0x04000803 RID: 2051
		private string unityStackTrace;
	}
}
