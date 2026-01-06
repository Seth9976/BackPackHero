using System;
using System.Reflection;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000139 RID: 313
	internal sealed class VBTypeAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x000184B4 File Offset: 0x000166B4
		private VBTypeAttributeConverter()
		{
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00018501 File Offset: 0x00016701
		public static VBTypeAttributeConverter Default { get; } = new VBTypeAttributeConverter();

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00018508 File Offset: 0x00016708
		protected override string[] Names { get; } = new string[] { "Public", "Friend" };

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00018510 File Offset: 0x00016710
		protected override object[] Values { get; } = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00018518 File Offset: 0x00016718
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.Public;
			}
		}
	}
}
