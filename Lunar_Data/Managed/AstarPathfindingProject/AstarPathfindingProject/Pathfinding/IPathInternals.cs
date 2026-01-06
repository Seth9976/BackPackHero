using System;

namespace Pathfinding
{
	// Token: 0x020000AC RID: 172
	internal interface IPathInternals
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000578 RID: 1400
		PathHandler PathHandler { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000579 RID: 1401
		// (set) Token: 0x0600057A RID: 1402
		bool Pooled { get; set; }

		// Token: 0x0600057B RID: 1403
		void AdvanceState(PathState s);

		// Token: 0x0600057C RID: 1404
		void OnEnterPool();

		// Token: 0x0600057D RID: 1405
		void Reset();

		// Token: 0x0600057E RID: 1406
		void ReturnPath();

		// Token: 0x0600057F RID: 1407
		void PrepareBase(PathHandler handler);

		// Token: 0x06000580 RID: 1408
		void Prepare();

		// Token: 0x06000581 RID: 1409
		void Cleanup();

		// Token: 0x06000582 RID: 1410
		void CalculateStep(long targetTick);

		// Token: 0x06000583 RID: 1411
		string DebugString(PathLog logMode);
	}
}
