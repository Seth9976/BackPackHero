using System;

namespace System.Data.Common
{
	// Token: 0x0200031D RID: 797
	[Serializable]
	internal sealed class NameValuePair
	{
		// Token: 0x06002519 RID: 9497 RVA: 0x000A823C File Offset: 0x000A643C
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000A8259 File Offset: 0x000A6459
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000A8261 File Offset: 0x000A6461
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x000A8269 File Offset: 0x000A6469
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000A8271 File Offset: 0x000A6471
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x000A8279 File Offset: 0x000A6479
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NameValuePairNext);
				}
				this._next = value;
			}
		}

		// Token: 0x04001840 RID: 6208
		private readonly string _name;

		// Token: 0x04001841 RID: 6209
		private readonly string _value;

		// Token: 0x04001842 RID: 6210
		private readonly int _length;

		// Token: 0x04001843 RID: 6211
		private NameValuePair _next;
	}
}
