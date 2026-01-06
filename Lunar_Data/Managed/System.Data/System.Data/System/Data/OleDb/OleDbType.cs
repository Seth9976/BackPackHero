using System;

namespace System.Data.OleDb
{
	/// <summary>Specifies the data type of a field, a property, for use in an <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010B RID: 267
	public enum OleDbType
	{
		/// <summary>A 64-bit signed integer (DBTYPE_I8). This maps to <see cref="T:System.Int64" />.</summary>
		// Token: 0x0400099B RID: 2459
		BigInt = 20,
		/// <summary>A stream of binary data (DBTYPE_BYTES). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x0400099C RID: 2460
		Binary = 128,
		/// <summary>A Boolean value (DBTYPE_BOOL). This maps to <see cref="T:System.Boolean" />.</summary>
		// Token: 0x0400099D RID: 2461
		Boolean = 11,
		/// <summary>A null-terminated character string of Unicode characters (DBTYPE_BSTR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x0400099E RID: 2462
		BSTR = 8,
		/// <summary>A character string (DBTYPE_STR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x0400099F RID: 2463
		Char = 129,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit (DBTYPE_CY). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040009A0 RID: 2464
		Currency = 6,
		/// <summary>Date data, stored as a double (DBTYPE_DATE). The whole portion is the number of days since December 30, 1899, and the fractional portion is a fraction of a day. This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040009A1 RID: 2465
		Date,
		/// <summary>Date data in the format yyyymmdd (DBTYPE_DBDATE). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040009A2 RID: 2466
		DBDate = 133,
		/// <summary>Time data in the format hhmmss (DBTYPE_DBTIME). This maps to <see cref="T:System.TimeSpan" />.</summary>
		// Token: 0x040009A3 RID: 2467
		DBTime,
		/// <summary>Data and time data in the format yyyymmddhhmmss (DBTYPE_DBTIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040009A4 RID: 2468
		DBTimeStamp,
		/// <summary>A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1 (DBTYPE_DECIMAL). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040009A5 RID: 2469
		Decimal = 14,
		/// <summary>A floating-point number within the range of -1.79E +308 through 1.79E +308 (DBTYPE_R8). This maps to <see cref="T:System.Double" />.</summary>
		// Token: 0x040009A6 RID: 2470
		Double = 5,
		/// <summary>No value (DBTYPE_EMPTY).</summary>
		// Token: 0x040009A7 RID: 2471
		Empty = 0,
		/// <summary>A 32-bit error code (DBTYPE_ERROR). This maps to <see cref="T:System.Exception" />.</summary>
		// Token: 0x040009A8 RID: 2472
		Error = 10,
		/// <summary>A 64-bit unsigned integer representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040009A9 RID: 2473
		Filetime = 64,
		/// <summary>A globally unique identifier (or GUID) (DBTYPE_GUID). This maps to <see cref="T:System.Guid" />.</summary>
		// Token: 0x040009AA RID: 2474
		Guid = 72,
		/// <summary>A pointer to an IDispatch interface (DBTYPE_IDISPATCH). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040009AB RID: 2475
		IDispatch = 9,
		/// <summary>A 32-bit signed integer (DBTYPE_I4). This maps to <see cref="T:System.Int32" />.</summary>
		// Token: 0x040009AC RID: 2476
		Integer = 3,
		/// <summary>A pointer to an IUnknown interface (DBTYPE_UNKNOWN). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040009AD RID: 2477
		IUnknown = 13,
		/// <summary>A long binary value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040009AE RID: 2478
		LongVarBinary = 205,
		/// <summary>A long string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040009AF RID: 2479
		LongVarChar = 201,
		/// <summary>A long null-terminated Unicode string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040009B0 RID: 2480
		LongVarWChar = 203,
		/// <summary>An exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040009B1 RID: 2481
		Numeric = 131,
		/// <summary>An automation PROPVARIANT (DBTYPE_PROP_VARIANT). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040009B2 RID: 2482
		PropVariant = 138,
		/// <summary>A floating-point number within the range of -3.40E +38 through 3.40E +38 (DBTYPE_R4). This maps to <see cref="T:System.Single" />.</summary>
		// Token: 0x040009B3 RID: 2483
		Single = 4,
		/// <summary>A 16-bit signed integer (DBTYPE_I2). This maps to <see cref="T:System.Int16" />.</summary>
		// Token: 0x040009B4 RID: 2484
		SmallInt = 2,
		/// <summary>A 8-bit signed integer (DBTYPE_I1). This maps to <see cref="T:System.SByte" />.</summary>
		// Token: 0x040009B5 RID: 2485
		TinyInt = 16,
		/// <summary>A 64-bit unsigned integer (DBTYPE_UI8). This maps to <see cref="T:System.UInt64" />.</summary>
		// Token: 0x040009B6 RID: 2486
		UnsignedBigInt = 21,
		/// <summary>A 32-bit unsigned integer (DBTYPE_UI4). This maps to <see cref="T:System.UInt32" />.</summary>
		// Token: 0x040009B7 RID: 2487
		UnsignedInt = 19,
		/// <summary>A 16-bit unsigned integer (DBTYPE_UI2). This maps to <see cref="T:System.UInt16" />.</summary>
		// Token: 0x040009B8 RID: 2488
		UnsignedSmallInt = 18,
		/// <summary>A 8-bit unsigned integer (DBTYPE_UI1). This maps to <see cref="T:System.Byte" />.</summary>
		// Token: 0x040009B9 RID: 2489
		UnsignedTinyInt = 17,
		/// <summary>A variable-length stream of binary data (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040009BA RID: 2490
		VarBinary = 204,
		/// <summary>A variable-length stream of non-Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040009BB RID: 2491
		VarChar = 200,
		/// <summary>A special data type that can contain numeric, string, binary, or date data, and also the special values Empty and Null (DBTYPE_VARIANT). This type is assumed if no other is specified. This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040009BC RID: 2492
		Variant = 12,
		/// <summary>A variable-length numeric value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040009BD RID: 2493
		VarNumeric = 139,
		/// <summary>A variable-length, null-terminated stream of Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040009BE RID: 2494
		VarWChar = 202,
		/// <summary>A null-terminated stream of Unicode characters (DBTYPE_WSTR). This maps to <see cref="T:System.String" />. </summary>
		// Token: 0x040009BF RID: 2495
		WChar = 130
	}
}
