using System;

namespace Pathfinding
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public abstract class MonoModifier : VersionedMonoBehaviour, IPathModifier
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x000247FC File Offset: 0x000229FC
		protected virtual void OnEnable()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00024824 File Offset: 0x00022A24
		protected virtual void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600063D RID: 1597
		public abstract int Order { get; }

		// Token: 0x0600063E RID: 1598 RVA: 0x00024840 File Offset: 0x00022A40
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x0600063F RID: 1599
		public abstract void Apply(Path path);

		// Token: 0x04000383 RID: 899
		[NonSerialized]
		public Seeker seeker;
	}
}
