using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Data
{
	/// <summary>This exception is thrown when an ongoing operation is aborted by the user. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000101 RID: 257
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x000496F8 File Offset: 0x000478F8
		private OperationAbortedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232010;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		private OperationAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00049710 File Offset: 0x00047910
		internal static OperationAbortedException Aborted(Exception inner)
		{
			OperationAbortedException ex;
			if (inner == null)
			{
				ex = new OperationAbortedException(SR.GetString("Operation aborted."), null);
			}
			else
			{
				ex = new OperationAbortedException(SR.GetString("Operation aborted due to an exception (see InnerException for details)."), inner);
			}
			return ex;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal OperationAbortedException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
