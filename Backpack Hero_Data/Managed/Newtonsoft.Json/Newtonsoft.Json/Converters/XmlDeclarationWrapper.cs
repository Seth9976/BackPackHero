using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EE RID: 238
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x0003219A File Offset: 0x0003039A
		[NullableContext(1)]
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x000321AA File Offset: 0x000303AA
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x000321B7 File Offset: 0x000303B7
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x000321C4 File Offset: 0x000303C4
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x000321D2 File Offset: 0x000303D2
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x000321DF File Offset: 0x000303DF
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x04000400 RID: 1024
		[Nullable(1)]
		private readonly XmlDeclaration _declaration;
	}
}
