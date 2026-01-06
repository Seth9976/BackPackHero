using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000388 RID: 904
	internal sealed class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x0006C57C File Offset: 0x0006A77C
		internal TrackingStringDictionary()
			: this(false)
		{
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0006C585 File Offset: 0x0006A785
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this._isReadOnly = isReadOnly;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0006C594 File Offset: 0x0006A794
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0006C59C File Offset: 0x0006A79C
		internal bool IsChanged
		{
			get
			{
				return this._isChanged;
			}
			set
			{
				this._isChanged = value;
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0006C5A5 File Offset: 0x0006A7A5
		public override void Add(string key, string value)
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Add(key, value);
			this._isChanged = true;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0006C5C9 File Offset: 0x0006A7C9
		public override void Clear()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Clear();
			this._isChanged = true;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0006C5EB File Offset: 0x0006A7EB
		public override void Remove(string key)
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Remove(key);
			this._isChanged = true;
		}

		// Token: 0x170005E1 RID: 1505
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (this._isReadOnly)
				{
					throw new InvalidOperationException("The collection is read-only.");
				}
				base[key] = value;
				this._isChanged = true;
			}
		}

		// Token: 0x04000F67 RID: 3943
		private readonly bool _isReadOnly;

		// Token: 0x04000F68 RID: 3944
		private bool _isChanged;
	}
}
