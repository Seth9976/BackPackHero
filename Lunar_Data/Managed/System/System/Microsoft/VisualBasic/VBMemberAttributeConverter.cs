using System;
using System.CodeDom;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000137 RID: 311
	internal sealed class VBMemberAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00018304 File Offset: 0x00016504
		private VBMemberAttributeConverter()
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00018398 File Offset: 0x00016598
		public static VBMemberAttributeConverter Default { get; } = new VBMemberAttributeConverter();

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001839F File Offset: 0x0001659F
		protected override string[] Names { get; } = new string[] { "Public", "Protected", "Protected Friend", "Friend", "Private" };

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000183A7 File Offset: 0x000165A7
		protected override object[] Values { get; } = new object[]
		{
			MemberAttributes.Public,
			MemberAttributes.Family,
			MemberAttributes.FamilyOrAssembly,
			MemberAttributes.Assembly,
			MemberAttributes.Private
		};

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x000183AF File Offset: 0x000165AF
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}
	}
}
