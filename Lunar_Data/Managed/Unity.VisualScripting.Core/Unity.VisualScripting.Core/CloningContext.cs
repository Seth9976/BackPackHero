using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200000C RID: 12
	public sealed class CloningContext : IPoolable, IDisposable
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A2D File Offset: 0x00000C2D
		public Dictionary<object, object> clonings { get; } = new Dictionary<object, object>(ReferenceEqualityComparer.Instance);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A35 File Offset: 0x00000C35
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A3D File Offset: 0x00000C3D
		public ICloner fallbackCloner { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A46 File Offset: 0x00000C46
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002A4E File Offset: 0x00000C4E
		public bool tryPreserveInstances { get; private set; }

		// Token: 0x06000045 RID: 69 RVA: 0x00002A57 File Offset: 0x00000C57
		void IPoolable.New()
		{
			this.disposed = false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A60 File Offset: 0x00000C60
		void IPoolable.Free()
		{
			this.disposed = true;
			this.clonings.Clear();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A74 File Offset: 0x00000C74
		public void Dispose()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			GenericPool<CloningContext>.Free(this);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A90 File Offset: 0x00000C90
		public static CloningContext New(ICloner fallbackCloner, bool tryPreserveInstances)
		{
			CloningContext cloningContext = GenericPool<CloningContext>.New(() => new CloningContext());
			cloningContext.fallbackCloner = fallbackCloner;
			cloningContext.tryPreserveInstances = tryPreserveInstances;
			return cloningContext;
		}

		// Token: 0x04000011 RID: 17
		private bool disposed;
	}
}
