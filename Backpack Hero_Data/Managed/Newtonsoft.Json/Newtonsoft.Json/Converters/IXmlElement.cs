using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000CBE RID: 3262
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000CBF RID: 3263
		[return: Nullable(2)]
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CC0 RID: 3264
		bool IsEmpty { get; }
	}
}
