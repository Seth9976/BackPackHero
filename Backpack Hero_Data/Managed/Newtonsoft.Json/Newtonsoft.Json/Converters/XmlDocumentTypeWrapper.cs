using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EF RID: 239
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x000321ED File Offset: 0x000303ED
		[NullableContext(1)]
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x000321FD File Offset: 0x000303FD
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0003220A File Offset: 0x0003040A
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00032217 File Offset: 0x00030417
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00032224 File Offset: 0x00030424
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00032231 File Offset: 0x00030431
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000401 RID: 1025
		[Nullable(1)]
		private readonly XmlDocumentType _documentType;
	}
}
