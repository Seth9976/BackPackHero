using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F7 RID: 247
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00032559 File Offset: 0x00030759
		[NullableContext(1)]
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00032569 File Offset: 0x00030769
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00032576 File Offset: 0x00030776
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00032583 File Offset: 0x00030783
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00032590 File Offset: 0x00030790
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003259D File Offset: 0x0003079D
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000406 RID: 1030
		[Nullable(1)]
		private readonly XDocumentType _documentType;
	}
}
