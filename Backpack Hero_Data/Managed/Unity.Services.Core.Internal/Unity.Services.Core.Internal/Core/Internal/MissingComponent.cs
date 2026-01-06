using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000041 RID: 65
	internal class MissingComponent : IServiceComponent
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000035E1 File Offset: 0x000017E1
		public Type IntendedType { get; }

		// Token: 0x06000118 RID: 280 RVA: 0x000035E9 File Offset: 0x000017E9
		internal MissingComponent(Type intendedType)
		{
			this.IntendedType = intendedType;
		}
	}
}
