using System;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001AC RID: 428
	public struct fsOption<T>
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00030C34 File Offset: 0x0002EE34
		public bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00030C3C File Offset: 0x0002EE3C
		public bool IsEmpty
		{
			get
			{
				return !this._hasValue;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00030C47 File Offset: 0x0002EE47
		public T Value
		{
			get
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException("fsOption is empty");
				}
				return this._value;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00030C62 File Offset: 0x0002EE62
		public fsOption(T value)
		{
			this._hasValue = true;
			this._value = value;
		}

		// Token: 0x040002C5 RID: 709
		private bool _hasValue;

		// Token: 0x040002C6 RID: 710
		private T _value;

		// Token: 0x040002C7 RID: 711
		public static fsOption<T> Empty;
	}
}
