using System;

namespace System.Data
{
	/// <summary>Specifies SQL Server-specific data type of a field, property, for use in a <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DF RID: 223
	public enum SqlDbType
	{
		/// <summary>
		///   <see cref="T:System.Int64" />. A 64-bit signed integer.</summary>
		// Token: 0x04000812 RID: 2066
		BigInt,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A fixed-length stream of binary data ranging between 1 and 8,000 bytes.</summary>
		// Token: 0x04000813 RID: 2067
		Binary,
		/// <summary>
		///   <see cref="T:System.Boolean" />. An unsigned numeric value that can be 0, 1, or null. </summary>
		// Token: 0x04000814 RID: 2068
		Bit,
		/// <summary>
		///   <see cref="T:System.String" />. A fixed-length stream of non-Unicode characters ranging between 1 and 8,000 characters.</summary>
		// Token: 0x04000815 RID: 2069
		Char,
		/// <summary>
		///   <see cref="T:System.DateTime" />. Date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds.</summary>
		// Token: 0x04000816 RID: 2070
		DateTime,
		/// <summary>
		///   <see cref="T:System.Decimal" />. A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1.</summary>
		// Token: 0x04000817 RID: 2071
		Decimal,
		/// <summary>
		///   <see cref="T:System.Double" />. A floating point number within the range of -1.79E +308 through 1.79E +308.</summary>
		// Token: 0x04000818 RID: 2072
		Float,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A variable-length stream of binary data ranging from 0 to 2 31 -1 (or 2,147,483,647) bytes.</summary>
		// Token: 0x04000819 RID: 2073
		Image,
		/// <summary>
		///   <see cref="T:System.Int32" />. A 32-bit signed integer.</summary>
		// Token: 0x0400081A RID: 2074
		Int,
		/// <summary>
		///   <see cref="T:System.Decimal" />. A currency value ranging from -2 63 (or -9,223,372,036,854,775,808) to 2 63 -1 (or +9,223,372,036,854,775,807) with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x0400081B RID: 2075
		Money,
		/// <summary>
		///   <see cref="T:System.String" />. A fixed-length stream of Unicode characters ranging between 1 and 4,000 characters.</summary>
		// Token: 0x0400081C RID: 2076
		NChar,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of Unicode data with a maximum length of 2 30 - 1 (or 1,073,741,823) characters.</summary>
		// Token: 0x0400081D RID: 2077
		NText,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of Unicode characters ranging between 1 and 4,000 characters. Implicit conversion fails if the string is greater than 4,000 characters. Explicitly set the object when working with strings longer than 4,000 characters. Use <see cref="F:System.Data.SqlDbType.NVarChar" /> when the database column is nvarchar(max).</summary>
		// Token: 0x0400081E RID: 2078
		NVarChar,
		/// <summary>
		///   <see cref="T:System.Single" />. A floating point number within the range of -3.40E +38 through 3.40E +38.</summary>
		// Token: 0x0400081F RID: 2079
		Real,
		/// <summary>
		///   <see cref="T:System.Guid" />. A globally unique identifier (or GUID).</summary>
		// Token: 0x04000820 RID: 2080
		UniqueIdentifier,
		/// <summary>
		///   <see cref="T:System.DateTime" />. Date and time data ranging in value from January 1, 1900 to June 6, 2079 to an accuracy of one minute.</summary>
		// Token: 0x04000821 RID: 2081
		SmallDateTime,
		/// <summary>
		///   <see cref="T:System.Int16" />. A 16-bit signed integer.</summary>
		// Token: 0x04000822 RID: 2082
		SmallInt,
		/// <summary>
		///   <see cref="T:System.Decimal" />. A currency value ranging from -214,748.3648 to +214,748.3647 with an accuracy to a ten-thousandth of a currency unit.</summary>
		// Token: 0x04000823 RID: 2083
		SmallMoney,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of non-Unicode data with a maximum length of 2 31 -1 (or 2,147,483,647) characters.</summary>
		// Token: 0x04000824 RID: 2084
		Text,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. Automatically generated binary numbers, which are guaranteed to be unique within a database. timestamp is used typically as a mechanism for version-stamping table rows. The storage size is 8 bytes.</summary>
		// Token: 0x04000825 RID: 2085
		Timestamp,
		/// <summary>
		///   <see cref="T:System.Byte" />. An 8-bit unsigned integer.</summary>
		// Token: 0x04000826 RID: 2086
		TinyInt,
		/// <summary>
		///   <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />. A variable-length stream of binary data ranging between 1 and 8,000 bytes. Implicit conversion fails if the byte array is greater than 8,000 bytes. Explicitly set the object when working with byte arrays larger than 8,000 bytes.</summary>
		// Token: 0x04000827 RID: 2087
		VarBinary,
		/// <summary>
		///   <see cref="T:System.String" />. A variable-length stream of non-Unicode characters ranging between 1 and 8,000 characters. Use <see cref="F:System.Data.SqlDbType.VarChar" /> when the database column is varchar(max).</summary>
		// Token: 0x04000828 RID: 2088
		VarChar,
		/// <summary>
		///   <see cref="T:System.Object" />. A special data type that can contain numeric, string, binary, or date data as well as the SQL Server values Empty and Null, which is assumed if no other type is declared.</summary>
		// Token: 0x04000829 RID: 2089
		Variant,
		/// <summary>An XML value. Obtain the XML as a string using the <see cref="M:System.Data.SqlClient.SqlDataReader.GetValue(System.Int32)" /> method or <see cref="P:System.Data.SqlTypes.SqlXml.Value" /> property, or as an <see cref="T:System.Xml.XmlReader" /> by calling the <see cref="M:System.Data.SqlTypes.SqlXml.CreateReader" /> method.</summary>
		// Token: 0x0400082A RID: 2090
		Xml = 25,
		/// <summary>A SQL Server user-defined type (UDT).</summary>
		// Token: 0x0400082B RID: 2091
		Udt = 29,
		/// <summary>A special data type for specifying structured data contained in table-valued parameters.</summary>
		// Token: 0x0400082C RID: 2092
		Structured,
		/// <summary>Date data ranging in value from January 1,1 AD through December 31, 9999 AD.</summary>
		// Token: 0x0400082D RID: 2093
		Date,
		/// <summary>Time data based on a 24-hour clock. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Corresponds to a SQL Server time value.</summary>
		// Token: 0x0400082E RID: 2094
		Time,
		/// <summary>Date and time data. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds.</summary>
		// Token: 0x0400082F RID: 2095
		DateTime2,
		/// <summary>Date and time data with time zone awareness. Date value range is from January 1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through +14:00. </summary>
		// Token: 0x04000830 RID: 2096
		DateTimeOffset
	}
}
