using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000065 RID: 101
	public struct BatchQueryJobStruct<T> where T : struct
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000036F4 File Offset: 0x000018F4
		public static IntPtr Initialize()
		{
			bool flag = BatchQueryJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				BatchQueryJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), null, null, null);
			}
			return BatchQueryJobStruct<T>.jobReflectionData;
		}

		// Token: 0x04000181 RID: 385
		internal static IntPtr jobReflectionData;
	}
}
