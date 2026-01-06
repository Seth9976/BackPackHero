using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000107 RID: 263
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x00035CFD File Offset: 0x00033EFD
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00035D12 File Offset: 0x00033F12
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00035D15 File Offset: 0x00033F15
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00035D27 File Offset: 0x00033F27
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000435 RID: 1077
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
