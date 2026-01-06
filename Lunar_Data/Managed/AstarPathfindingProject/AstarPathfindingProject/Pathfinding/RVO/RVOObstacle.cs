using System;

namespace Pathfinding.RVO
{
	// Token: 0x020002AA RID: 682
	public abstract class RVOObstacle : VersionedMonoBehaviour
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001042 RID: 4162
		protected abstract bool ExecuteInEditor { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001043 RID: 4163
		protected abstract bool LocalCoordinates { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001044 RID: 4164
		protected abstract bool StaticObstacle { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001045 RID: 4165
		protected abstract float Height { get; }

		// Token: 0x04000C67 RID: 3175
		public RVOObstacle.ObstacleVertexWinding obstacleMode;

		// Token: 0x04000C68 RID: 3176
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x020002AB RID: 683
		public enum ObstacleVertexWinding
		{
			// Token: 0x04000C6A RID: 3178
			KeepOut,
			// Token: 0x04000C6B RID: 3179
			KeepIn
		}
	}
}
