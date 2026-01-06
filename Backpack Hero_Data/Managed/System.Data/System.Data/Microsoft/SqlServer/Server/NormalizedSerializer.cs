using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003BC RID: 956
	internal sealed class NormalizedSerializer : Serializer
	{
		// Token: 0x06002ED1 RID: 11985 RVA: 0x000CAD28 File Offset: 0x000C8F28
		internal NormalizedSerializer(Type t)
			: base(t)
		{
			SqlUserDefinedTypeAttribute udtAttribute = SerializationHelperSql9.GetUdtAttribute(t);
			this._normalizer = new BinaryOrderedUdtNormalizer(t, true);
			this._isFixedSize = udtAttribute.IsFixedLength;
			this._maxSize = this._normalizer.Size;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000CAD6D File Offset: 0x000C8F6D
		public override void Serialize(Stream s, object o)
		{
			this._normalizer.NormalizeTopObject(o, s);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000CAD7C File Offset: 0x000C8F7C
		public override object Deserialize(Stream s)
		{
			return this._normalizer.DeNormalizeTopObject(this._type, s);
		}

		// Token: 0x04001BAE RID: 7086
		private BinaryOrderedUdtNormalizer _normalizer;

		// Token: 0x04001BAF RID: 7087
		private bool _isFixedSize;

		// Token: 0x04001BB0 RID: 7088
		private int _maxSize;
	}
}
