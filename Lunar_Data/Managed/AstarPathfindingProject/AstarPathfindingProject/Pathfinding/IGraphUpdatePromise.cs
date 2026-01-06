using System;
using System.Collections.Generic;
using Unity.Jobs;

namespace Pathfinding
{
	// Token: 0x0200009A RID: 154
	public interface IGraphUpdatePromise
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000057A6 File Offset: 0x000039A6
		float Progress
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000146B9 File Offset: 0x000128B9
		IEnumerator<JobHandle> Prepare()
		{
			return null;
		}

		// Token: 0x060004E7 RID: 1255
		void Apply(IGraphUpdateContext context);
	}
}
