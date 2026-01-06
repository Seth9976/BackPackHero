using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.OleDb.OleDbCommand" /> and optionally its mapping to a <see cref="T:System.Data.DataSet" /> column. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011C RID: 284
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbParameter : DbParameter, IDataParameter, IDbDataParameter, ICloneable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x000094D4 File Offset: 0x000076D4
		public override DbType DbType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return-value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x000094D4 File Offset: 0x000076D4
		public override ParameterDirection Direction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x000094D4 File Offset: 0x000076D4
		public override bool IsNullable
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x000094D4 File Offset: 0x000076D4
		public int Offset
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter. The default is <see cref="F:System.Data.OleDb.OleDbType.VarWChar" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x000094D4 File Offset: 0x000076D4
		public OleDbType OleDbType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.OleDb.OleDbParameter" />. The default is an empty string ("").</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x000094D4 File Offset: 0x000076D4
		public override string ParameterName
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x000094D4 File Offset: 0x000076D4
		public new byte Precision
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000094D4 File Offset: 0x000076D4
		public new byte Scale
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x000094D4 File Offset: 0x000076D4
		public override int Size
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x000094D4 File Offset: 0x000076D4
		public override string SourceColumn
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x000094D4 File Offset: 0x000076D4
		public override bool SourceColumnNullMapping
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x000094D4 File Offset: 0x000076D4
		public override DataRowVersion SourceVersion
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x000094D4 File Offset: 0x000076D4
		public override object Value
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class.</summary>
		// Token: 0x06000F97 RID: 3991 RVA: 0x0004F1E7 File Offset: 0x0004D3E7
		public OleDbParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000F98 RID: 3992 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public OleDbParameter(string name, OleDbType dataType)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000F99 RID: 3993 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public OleDbParameter(string name, OleDbType dataType, int size)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000F9A RID: 3994 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000F9B RID: 3995 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000F9C RID: 3996 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public OleDbParameter(string name, OleDbType dataType, int size, string srcColumn)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and the value of the new <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="value">The value of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object. </param>
		// Token: 0x06000F9D RID: 3997 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public OleDbParameter(string name, object value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F9E RID: 3998 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override void ResetDbType()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</returns>
		// Token: 0x06000F9F RID: 3999 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override string ToString()
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06000FA0 RID: 4000 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public void ResetOleDbType()
		{
			throw ADP.OleDb();
		}
	}
}
