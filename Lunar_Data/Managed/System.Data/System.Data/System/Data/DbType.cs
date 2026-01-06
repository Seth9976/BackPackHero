using System;

namespace System.Data
{
	/// <summary>Specifies the data type of a field, a property, or a Parameter object of a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200008B RID: 139
	public enum DbType
	{
		/// <summary>A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters.</summary>
		// Token: 0x04000619 RID: 1561
		AnsiString,
		/// <summary>A variable-length stream of binary data ranging between 1 and 8,000 bytes.</summary>
		// Token: 0x0400061A RID: 1562
		Binary,
		/// <summary>An 8-bit unsigned integer ranging in value from 0 to 255.</summary>
		// Token: 0x0400061B RID: 1563
		Byte,
		/// <summary>A simple type representing Boolean values of true or false.</summary>
		// Token: 0x0400061C RID: 1564
		Boolean,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x0400061D RID: 1565
		Currency,
		/// <summary>A type representing a date value.</summary>
		// Token: 0x0400061E RID: 1566
		Date,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x0400061F RID: 1567
		DateTime,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x04000620 RID: 1568
		Decimal,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x04000621 RID: 1569
		Double,
		/// <summary>A globally unique identifier (or GUID).</summary>
		// Token: 0x04000622 RID: 1570
		Guid,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x04000623 RID: 1571
		Int16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x04000624 RID: 1572
		Int32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x04000625 RID: 1573
		Int64,
		/// <summary>A general type representing any reference or value type not explicitly represented by another DbType value.</summary>
		// Token: 0x04000626 RID: 1574
		Object,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x04000627 RID: 1575
		SByte,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x04000628 RID: 1576
		Single,
		/// <summary>A type representing Unicode character strings.</summary>
		// Token: 0x04000629 RID: 1577
		String,
		/// <summary>A type representing a SQL Server DateTime value. If you want to use a SQL Server time value, use <see cref="F:System.Data.SqlDbType.Time" />.</summary>
		// Token: 0x0400062A RID: 1578
		Time,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x0400062B RID: 1579
		UInt16,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x0400062C RID: 1580
		UInt32,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x0400062D RID: 1581
		UInt64,
		/// <summary>A variable-length numeric value.</summary>
		// Token: 0x0400062E RID: 1582
		VarNumeric,
		/// <summary>A fixed-length stream of non-Unicode characters.</summary>
		// Token: 0x0400062F RID: 1583
		AnsiStringFixedLength,
		/// <summary>A fixed-length string of Unicode characters.</summary>
		// Token: 0x04000630 RID: 1584
		StringFixedLength,
		/// <summary>A parsed representation of an XML document or fragment.</summary>
		// Token: 0x04000631 RID: 1585
		Xml = 25,
		/// <summary>Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.</summary>
		// Token: 0x04000632 RID: 1586
		DateTime2,
		/// <summary>Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through +14:00. </summary>
		// Token: 0x04000633 RID: 1587
		DateTimeOffset
	}
}
