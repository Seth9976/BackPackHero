using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000009 RID: 9
	public sealed class UnitRelation : IUnitRelation, IConnection<IUnitPort, IUnitPort>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002610 File Offset: 0x00000810
		public UnitRelation(IUnitPort source, IUnitPort destination)
		{
			Ensure.That("source").IsNotNull<IUnitPort>(source);
			Ensure.That("destination").IsNotNull<IUnitPort>(destination);
			if (source.unit != destination.unit)
			{
				throw new NotSupportedException("Cannot create relations across nodes.");
			}
			this.source = source;
			this.destination = destination;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000266A File Offset: 0x0000086A
		public IUnitPort source { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002672 File Offset: 0x00000872
		public IUnitPort destination { get; }
	}
}
