using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000073 RID: 115
	public interface IGraphNester : IGraphParent
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600039C RID: 924
		IGraphNest nest { get; }

		// Token: 0x0600039D RID: 925
		void InstantiateNest();

		// Token: 0x0600039E RID: 926
		void UninstantiateNest();
	}
}
