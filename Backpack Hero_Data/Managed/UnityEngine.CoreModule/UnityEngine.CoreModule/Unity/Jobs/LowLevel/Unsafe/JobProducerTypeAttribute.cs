using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000067 RID: 103
	[AttributeUsage(1024)]
	public sealed class JobProducerTypeAttribute : Attribute
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00003751 File Offset: 0x00001951
		public Type ProducerType { get; }

		// Token: 0x0600018D RID: 397 RVA: 0x00003759 File Offset: 0x00001959
		public JobProducerTypeAttribute(Type producerType)
		{
			this.ProducerType = producerType;
		}
	}
}
