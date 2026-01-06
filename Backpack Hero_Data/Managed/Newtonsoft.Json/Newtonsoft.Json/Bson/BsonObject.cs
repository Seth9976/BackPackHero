using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000106 RID: 262
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000D7E RID: 3454 RVA: 0x00035CA0 File Offset: 0x00033EA0
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00035CCD File Offset: 0x00033ECD
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00035CD0 File Offset: 0x00033ED0
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00035CE2 File Offset: 0x00033EE2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000434 RID: 1076
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
