using System;

namespace System.Data.Odbc
{
	/// <summary>Specifies the data type of a field, property, for use in an <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AC RID: 684
	public enum OdbcType
	{
		/// <summary>Exact numeric value with precision 19 (if signed) or 20 (if unsigned) and scale 0 (signed: –2[63] &lt;= n &lt;= 2[63] – 1, unsigned:0 &lt;= n &lt;= 2[64] – 1) (SQL_BIGINT). This maps to <see cref="T:System.Int64" />.</summary>
		// Token: 0x040015F3 RID: 5619
		BigInt = 1,
		/// <summary>A stream of binary data (SQL_BINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040015F4 RID: 5620
		Binary,
		/// <summary>Single bit binary data (SQL_BIT). This maps to <see cref="T:System.Boolean" />.</summary>
		// Token: 0x040015F5 RID: 5621
		Bit,
		/// <summary>A fixed-length character string (SQL_CHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040015F6 RID: 5622
		Char,
		/// <summary>Date data in the format yyyymmddhhmmss (SQL_TYPE_TIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040015F7 RID: 5623
		DateTime,
		/// <summary>Signed, exact, numeric value with a precision of at least p and scale s, where 1 &lt;= p &lt;= 15 and s &lt;= p. The maximum precision is driver-specific (SQL_DECIMAL). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040015F8 RID: 5624
		Decimal,
		/// <summary>Signed, exact, numeric value with a precision p and scale s, where 1 &lt;= p &lt;= 15, and s &lt;= p (SQL_NUMERIC). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040015F9 RID: 5625
		Numeric,
		/// <summary>Signed, approximate, numeric value with a binary precision 53 (zero or absolute value 10[–308] to 10[308]) (SQL_DOUBLE). This maps to <see cref="T:System.Double" />.</summary>
		// Token: 0x040015FA RID: 5626
		Double,
		/// <summary>Variable length binary data. Maximum length is data source–dependent (SQL_LONGVARBINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040015FB RID: 5627
		Image,
		/// <summary>Exact numeric value with precision 10 and scale 0 (signed: –2[31] &lt;= n &lt;= 2[31] – 1, unsigned:0 &lt;= n &lt;= 2[32] – 1) (SQL_INTEGER). This maps to <see cref="T:System.Int32" />.</summary>
		// Token: 0x040015FC RID: 5628
		Int,
		/// <summary>Unicode character string of fixed string length (SQL_WCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040015FD RID: 5629
		NChar,
		/// <summary>Unicode variable-length character data. Maximum length is data source–dependent. (SQL_WLONGVARCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040015FE RID: 5630
		NText,
		/// <summary>A variable-length stream of Unicode characters (SQL_WVARCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040015FF RID: 5631
		NVarChar,
		/// <summary>Signed, approximate, numeric value with a binary precision 24 (zero or absolute value 10[–38] to 10[38]).(SQL_REAL). This maps to <see cref="T:System.Single" />.</summary>
		// Token: 0x04001600 RID: 5632
		Real,
		/// <summary>A fixed-length GUID (SQL_GUID). This maps to <see cref="T:System.Guid" />.</summary>
		// Token: 0x04001601 RID: 5633
		UniqueIdentifier,
		/// <summary>Data and time data in the format yyyymmddhhmmss (SQL_TYPE_TIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04001602 RID: 5634
		SmallDateTime,
		/// <summary>Exact numeric value with precision 5 and scale 0 (signed: –32,768 &lt;= n &lt;= 32,767, unsigned: 0 &lt;= n &lt;= 65,535) (SQL_SMALLINT). This maps to <see cref="T:System.Int16" />.</summary>
		// Token: 0x04001603 RID: 5635
		SmallInt,
		/// <summary>Variable length character data. Maximum length is data source–dependent (SQL_LONGVARCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04001604 RID: 5636
		Text,
		/// <summary>A stream of binary data (SQL_BINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04001605 RID: 5637
		Timestamp,
		/// <summary>Exact numeric value with precision 3 and scale 0 (signed: –128 &lt;= n &lt;= 127, unsigned:0 &lt;= n &lt;= 255)(SQL_TINYINT). This maps to <see cref="T:System.Byte" />.</summary>
		// Token: 0x04001606 RID: 5638
		TinyInt,
		/// <summary>Variable length binary. The maximum is set by the user (SQL_VARBINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04001607 RID: 5639
		VarBinary,
		/// <summary>A variable-length stream character string (SQL_CHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04001608 RID: 5640
		VarChar,
		/// <summary>Date data in the format yyyymmdd (SQL_TYPE_DATE). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04001609 RID: 5641
		Date,
		/// <summary>Date data in the format hhmmss (SQL_TYPE_TIMES). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x0400160A RID: 5642
		Time
	}
}
