using System;

namespace Pathfinding
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public abstract class PathModifier : IPathModifier
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000635 RID: 1589
		public abstract int Order { get; }

		// Token: 0x06000636 RID: 1590 RVA: 0x000247C7 File Offset: 0x000229C7
		public void Awake(Seeker seeker)
		{
			this.seeker = seeker;
			if (seeker != null)
			{
				seeker.RegisterModifier(this);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000247E0 File Offset: 0x000229E0
		public void OnDestroy(Seeker seeker)
		{
			if (seeker != null)
			{
				seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000247F2 File Offset: 0x000229F2
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x06000639 RID: 1593
		public abstract void Apply(Path path);

		// Token: 0x04000382 RID: 898
		[NonSerialized]
		public Seeker seeker;
	}
}
