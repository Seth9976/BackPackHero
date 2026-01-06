using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000031 RID: 49
	public class CircularDependencyException : ServicesInitializationException
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00002ABC File Offset: 0x00000CBC
		public CircularDependencyException()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public CircularDependencyException(string message)
			: base(message)
		{
		}
	}
}
