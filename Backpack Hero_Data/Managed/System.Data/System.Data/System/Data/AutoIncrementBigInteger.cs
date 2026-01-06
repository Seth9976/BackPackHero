using System;
using System.Data.Common;
using System.Numerics;

namespace System.Data
{
	// Token: 0x0200004B RID: 75
	internal sealed class AutoIncrementBigInteger : AutoIncrementValue
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00010992 File Offset: 0x0000EB92
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0001099F File Offset: 0x0000EB9F
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (BigInteger)value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000109AD File Offset: 0x0000EBAD
		internal override Type DataType
		{
			get
			{
				return typeof(BigInteger);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000109B9 File Offset: 0x0000EBB9
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000109C1 File Offset: 0x0000EBC1
		internal override long Seed
		{
			get
			{
				return this._seed;
			}
			set
			{
				if (this._current == this._seed || this.BoundaryCheck(value))
				{
					this._current = value;
				}
				this._seed = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000109F7 File Offset: 0x0000EBF7
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00010A04 File Offset: 0x0000EC04
		internal override long Step
		{
			get
			{
				return (long)this._step;
			}
			set
			{
				if (value == 0L)
				{
					throw ExceptionBuilder.AutoIncrementSeed();
				}
				if (this._step != value)
				{
					if (this._current != this.Seed)
					{
						this._current = this._current - this._step + value;
					}
					this._step = value;
				}
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00010A69 File Offset: 0x0000EC69
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010A82 File Offset: 0x0000EC82
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = BigIntegerStorage.ConvertToBigInteger(value, formatProvider);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010A94 File Offset: 0x0000EC94
		internal override void SetCurrentAndIncrement(object value)
		{
			BigInteger bigInteger = (BigInteger)value;
			if (this.BoundaryCheck(bigInteger))
			{
				this._current = bigInteger + this._step;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010AC3 File Offset: 0x0000ECC3
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x040004CF RID: 1231
		private BigInteger _current;

		// Token: 0x040004D0 RID: 1232
		private long _seed;

		// Token: 0x040004D1 RID: 1233
		private BigInteger _step = 1;
	}
}
