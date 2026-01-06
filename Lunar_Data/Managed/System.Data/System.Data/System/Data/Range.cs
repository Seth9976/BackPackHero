using System;

namespace System.Data
{
	// Token: 0x020000C5 RID: 197
	internal struct Range
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x000341F8 File Offset: 0x000323F8
		public Range(int min, int max)
		{
			if (min > max)
			{
				throw ExceptionBuilder.RangeArgument(min, max);
			}
			this._min = min;
			this._max = max;
			this._isNotNull = true;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003421B File Offset: 0x0003241B
		public int Count
		{
			get
			{
				if (!this.IsNull)
				{
					return this._max - this._min + 1;
				}
				return 0;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00034236 File Offset: 0x00032436
		public bool IsNull
		{
			get
			{
				return !this._isNotNull;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00034241 File Offset: 0x00032441
		public int Max
		{
			get
			{
				this.CheckNull();
				return this._max;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0003424F File Offset: 0x0003244F
		public int Min
		{
			get
			{
				this.CheckNull();
				return this._min;
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003425D File Offset: 0x0003245D
		internal void CheckNull()
		{
			if (this.IsNull)
			{
				throw ExceptionBuilder.NullRange();
			}
		}

		// Token: 0x04000785 RID: 1925
		private int _min;

		// Token: 0x04000786 RID: 1926
		private int _max;

		// Token: 0x04000787 RID: 1927
		private bool _isNotNull;
	}
}
