using System;
using System.Collections.Generic;

namespace UnityEngine.Animations
{
	// Token: 0x02000064 RID: 100
	public interface IConstraint
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000579 RID: 1401
		// (set) Token: 0x0600057A RID: 1402
		float weight { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600057B RID: 1403
		// (set) Token: 0x0600057C RID: 1404
		bool constraintActive { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600057D RID: 1405
		// (set) Token: 0x0600057E RID: 1406
		bool locked { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600057F RID: 1407
		int sourceCount { get; }

		// Token: 0x06000580 RID: 1408
		int AddSource(ConstraintSource source);

		// Token: 0x06000581 RID: 1409
		void RemoveSource(int index);

		// Token: 0x06000582 RID: 1410
		ConstraintSource GetSource(int index);

		// Token: 0x06000583 RID: 1411
		void SetSource(int index, ConstraintSource source);

		// Token: 0x06000584 RID: 1412
		void GetSources(List<ConstraintSource> sources);

		// Token: 0x06000585 RID: 1413
		void SetSources(List<ConstraintSource> sources);
	}
}
