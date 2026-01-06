using System;
using System.Data.Common;
using System.Globalization;
using System.Numerics;

namespace System.Data
{
	// Token: 0x0200004A RID: 74
	internal sealed class AutoIncrementInt64 : AutoIncrementValue
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00010841 File Offset: 0x0000EA41
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0001084E File Offset: 0x0000EA4E
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (long)value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001085C File Offset: 0x0000EA5C
		internal override Type DataType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00010868 File Offset: 0x0000EA68
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00010870 File Offset: 0x0000EA70
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0001089C File Offset: 0x0000EA9C
		// (set) Token: 0x0600035C RID: 860 RVA: 0x000108A4 File Offset: 0x0000EAA4
		internal override long Step
		{
			get
			{
				return this._step;
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

		// Token: 0x0600035D RID: 861 RVA: 0x000108E2 File Offset: 0x0000EAE2
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000108F7 File Offset: 0x0000EAF7
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = Convert.ToInt64(value, formatProvider);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00010908 File Offset: 0x0000EB08
		internal override void SetCurrentAndIncrement(object value)
		{
			long num = (long)SqlConvert.ChangeType2(value, StorageType.Int64, typeof(long), CultureInfo.InvariantCulture);
			if (this.BoundaryCheck(num))
			{
				this._current = num + this._step;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001094E File Offset: 0x0000EB4E
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x040004CC RID: 1228
		private long _current;

		// Token: 0x040004CD RID: 1229
		private long _seed;

		// Token: 0x040004CE RID: 1230
		private long _step = 1L;
	}
}
