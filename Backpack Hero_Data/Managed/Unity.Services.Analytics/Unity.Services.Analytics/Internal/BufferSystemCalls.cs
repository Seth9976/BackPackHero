using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000028 RID: 40
	internal class BufferSystemCalls : IBufferSystemCalls
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000391C File Offset: 0x00001B1C
		public string GenerateGuid()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000393C File Offset: 0x00001B3C
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}
