using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000002 RID: 2
	public abstract class Cloner<T> : ICloner
	{
		// Token: 0x06000002 RID: 2
		public abstract bool Handles(Type type);

		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		void ICloner.BeforeClone(Type type, object original)
		{
			this.BeforeClone(type, (T)((object)original));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
		public virtual void BeforeClone(Type type, T original)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002069 File Offset: 0x00000269
		object ICloner.ConstructClone(Type type, object original)
		{
			return this.ConstructClone(type, (T)((object)original));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000207D File Offset: 0x0000027D
		public virtual T ConstructClone(Type type, T original)
		{
			return (T)((object)Activator.CreateInstance(type, true));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000208C File Offset: 0x0000028C
		void ICloner.FillClone(Type type, ref object clone, object original, CloningContext context)
		{
			T t = (T)((object)clone);
			this.FillClone(type, ref t, (T)((object)original), context);
			clone = t;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020BA File Offset: 0x000002BA
		public virtual void FillClone(Type type, ref T clone, T original, CloningContext context)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		void ICloner.AfterClone(Type type, object clone)
		{
			this.AfterClone(type, (T)((object)clone));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020CB File Offset: 0x000002CB
		public virtual void AfterClone(Type type, T clone)
		{
		}
	}
}
