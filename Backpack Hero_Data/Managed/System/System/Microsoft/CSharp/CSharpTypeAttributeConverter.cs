using System;
using System.Reflection;

namespace Microsoft.CSharp
{
	// Token: 0x0200013F RID: 319
	internal sealed class CSharpTypeAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		private CSharpTypeAttributeConverter()
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001E731 File Offset: 0x0001C931
		public static CSharpTypeAttributeConverter Default { get; } = new CSharpTypeAttributeConverter();

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001E738 File Offset: 0x0001C938
		protected override string[] Names { get; } = new string[] { "Public", "Internal" };

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001E740 File Offset: 0x0001C940
		protected override object[] Values { get; } = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001E748 File Offset: 0x0001C948
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.NotPublic;
			}
		}
	}
}
