using System;

namespace Pathfinding
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public abstract class PathModifier : IPathModifier
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000894 RID: 2196
		public abstract int Order { get; }

		// Token: 0x06000895 RID: 2197 RVA: 0x0002D743 File Offset: 0x0002B943
		public void Awake(Seeker seeker)
		{
			this.seeker = seeker;
			if (seeker != null)
			{
				seeker.RegisterModifier(this);
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002D75C File Offset: 0x0002B95C
		public void OnDestroy(Seeker seeker)
		{
			if (seeker != null)
			{
				seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x06000898 RID: 2200
		public abstract void Apply(Path path);

		// Token: 0x040005AB RID: 1451
		[NonSerialized]
		public Seeker seeker;
	}
}
