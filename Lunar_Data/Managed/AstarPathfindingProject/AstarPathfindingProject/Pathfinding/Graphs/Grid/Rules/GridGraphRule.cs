using System;
using Pathfinding.Serialization;

namespace Pathfinding.Graphs.Grid.Rules
{
	// Token: 0x02000209 RID: 521
	[JsonDynamicType]
	[JsonDynamicTypeAlias("Pathfinding.RuleTexture", typeof(RuleTexture))]
	[JsonDynamicTypeAlias("Pathfinding.RuleAnglePenalty", typeof(RuleAnglePenalty))]
	[JsonDynamicTypeAlias("Pathfinding.RuleElevationPenalty", typeof(RuleElevationPenalty))]
	[JsonDynamicTypeAlias("Pathfinding.RulePerLayerModifications", typeof(RulePerLayerModifications))]
	public abstract class GridGraphRule
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0004F9E6 File Offset: 0x0004DBE6
		public virtual int Hash
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0004F9EE File Offset: 0x0004DBEE
		public virtual void SetDirty()
		{
			this.dirty++;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void DisposeUnmanagedData()
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void Register(GridGraphRules rules)
		{
		}

		// Token: 0x04000981 RID: 2433
		[JsonMember]
		public bool enabled = true;

		// Token: 0x04000982 RID: 2434
		private int dirty = 1;

		// Token: 0x0200020A RID: 522
		public enum Pass
		{
			// Token: 0x04000984 RID: 2436
			BeforeCollision,
			// Token: 0x04000985 RID: 2437
			BeforeConnections,
			// Token: 0x04000986 RID: 2438
			AfterConnections,
			// Token: 0x04000987 RID: 2439
			AfterErosion,
			// Token: 0x04000988 RID: 2440
			PostProcess,
			// Token: 0x04000989 RID: 2441
			AfterApplied
		}
	}
}
