using System;

namespace System
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	internal class ReflectionOnlyType : RuntimeType
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x0005A592 File Offset: 0x00058792
		private ReflectionOnlyType()
		{
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0005A59A File Offset: 0x0005879A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
		}
	}
}
