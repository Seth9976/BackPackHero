using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000103 RID: 259
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00035019 File Offset: 0x00033219
		public byte[] Value { get; }

		// Token: 0x06000D55 RID: 3413 RVA: 0x00035021 File Offset: 0x00033221
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}
