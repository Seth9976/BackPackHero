using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000109 RID: 265
	internal class BsonValue : BsonToken
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x00035D72 File Offset: 0x00033F72
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00035D88 File Offset: 0x00033F88
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00035D90 File Offset: 0x00033F90
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000439 RID: 1081
		private readonly object _value;

		// Token: 0x0400043A RID: 1082
		private readonly BsonType _type;
	}
}
