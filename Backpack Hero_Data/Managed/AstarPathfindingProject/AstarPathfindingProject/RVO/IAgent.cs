using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000D0 RID: 208
	public interface IAgent
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060008CD RID: 2253
		// (set) Token: 0x060008CE RID: 2254
		Vector2 Position { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060008CF RID: 2255
		// (set) Token: 0x060008D0 RID: 2256
		float ElevationCoordinate { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060008D1 RID: 2257
		Vector2 CalculatedTargetPoint { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060008D2 RID: 2258
		float CalculatedSpeed { get; }

		// Token: 0x060008D3 RID: 2259
		void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed);

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060008D4 RID: 2260
		// (set) Token: 0x060008D5 RID: 2261
		bool Locked { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060008D6 RID: 2262
		// (set) Token: 0x060008D7 RID: 2263
		float Radius { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060008D8 RID: 2264
		// (set) Token: 0x060008D9 RID: 2265
		float Height { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060008DA RID: 2266
		// (set) Token: 0x060008DB RID: 2267
		float AgentTimeHorizon { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060008DC RID: 2268
		// (set) Token: 0x060008DD RID: 2269
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060008DE RID: 2270
		// (set) Token: 0x060008DF RID: 2271
		int MaxNeighbours { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060008E0 RID: 2272
		int NeighbourCount { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060008E1 RID: 2273
		// (set) Token: 0x060008E2 RID: 2274
		RVOLayer Layer { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060008E3 RID: 2275
		// (set) Token: 0x060008E4 RID: 2276
		RVOLayer CollidesWith { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060008E5 RID: 2277
		// (set) Token: 0x060008E6 RID: 2278
		bool DebugDraw { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060008E7 RID: 2279
		[Obsolete]
		List<ObstacleVertex> NeighbourObstacles { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060008E8 RID: 2280
		// (set) Token: 0x060008E9 RID: 2281
		float Priority { get; set; }

		// Token: 0x17000136 RID: 310
		// (set) Token: 0x060008EA RID: 2282
		Action PreCalculationCallback { set; }

		// Token: 0x060008EB RID: 2283
		void SetCollisionNormal(Vector2 normal);

		// Token: 0x060008EC RID: 2284
		void ForceSetVelocity(Vector2 velocity);
	}
}
