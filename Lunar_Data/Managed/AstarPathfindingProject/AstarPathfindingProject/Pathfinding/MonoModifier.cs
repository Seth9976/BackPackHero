using System;

namespace Pathfinding
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public abstract class MonoModifier : VersionedMonoBehaviour, IPathModifier
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0002D76E File Offset: 0x0002B96E
		protected virtual void OnEnable()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002D796 File Offset: 0x0002B996
		protected virtual void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600089C RID: 2204
		public abstract int Order { get; }

		// Token: 0x0600089D RID: 2205 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x0600089E RID: 2206
		public abstract void Apply(Path path);

		// Token: 0x040005AC RID: 1452
		[NonSerialized]
		public Seeker seeker;
	}
}
