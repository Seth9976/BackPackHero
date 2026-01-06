using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F6 RID: 246
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x000324FA File Offset: 0x000306FA
		[Nullable(1)]
		internal XDeclaration Declaration
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00032502 File Offset: 0x00030702
		[NullableContext(1)]
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00032512 File Offset: 0x00030712
		public override XmlNodeType NodeType
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00032516 File Offset: 0x00030716
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00032523 File Offset: 0x00030723
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00032530 File Offset: 0x00030730
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0003253E File Offset: 0x0003073E
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0003254B File Offset: 0x0003074B
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
