using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003BB RID: 955
	internal abstract class Serializer
	{
		// Token: 0x06002ECE RID: 11982
		public abstract object Deserialize(Stream s);

		// Token: 0x06002ECF RID: 11983
		public abstract void Serialize(Stream s, object o);

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000CAD17 File Offset: 0x000C8F17
		protected Serializer(Type t)
		{
			this._type = t;
		}

		// Token: 0x04001BAD RID: 7085
		protected Type _type;
	}
}
