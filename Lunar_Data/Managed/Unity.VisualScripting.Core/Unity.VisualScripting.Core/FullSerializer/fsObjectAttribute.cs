using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A2 RID: 418
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class fsObjectAttribute : Attribute
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x0002E817 File Offset: 0x0002CA17
		public fsObjectAttribute()
		{
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002E826 File Offset: 0x0002CA26
		public fsObjectAttribute(string versionString, params Type[] previousModels)
		{
			this.VersionString = versionString;
			this.PreviousModels = previousModels;
		}

		// Token: 0x0400028D RID: 653
		public Type[] PreviousModels;

		// Token: 0x0400028E RID: 654
		public string VersionString;

		// Token: 0x0400028F RID: 655
		public fsMemberSerialization MemberSerialization = fsMemberSerialization.Default;

		// Token: 0x04000290 RID: 656
		public Type Converter;

		// Token: 0x04000291 RID: 657
		public Type Processor;
	}
}
