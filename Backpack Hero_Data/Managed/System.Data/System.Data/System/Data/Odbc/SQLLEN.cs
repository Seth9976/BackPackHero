using System;

namespace System.Data.Odbc
{
	// Token: 0x020002A9 RID: 681
	internal struct SQLLEN
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x0009210B File Offset: 0x0009030B
		internal SQLLEN(int value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00092119 File Offset: 0x00090319
		internal SQLLEN(long value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00092127 File Offset: 0x00090327
		internal SQLLEN(IntPtr value)
		{
			this._value = value;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00092130 File Offset: 0x00090330
		public static implicit operator SQLLEN(int value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00092138 File Offset: 0x00090338
		public static explicit operator SQLLEN(long value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00092140 File Offset: 0x00090340
		public static implicit operator int(SQLLEN value)
		{
			return checked((int)value._value.ToInt64());
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0009214F File Offset: 0x0009034F
		public static explicit operator long(SQLLEN value)
		{
			return value._value.ToInt64();
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0009215D File Offset: 0x0009035D
		public long ToInt64()
		{
			return this._value.ToInt64();
		}

		// Token: 0x040015EE RID: 5614
		private IntPtr _value;
	}
}
