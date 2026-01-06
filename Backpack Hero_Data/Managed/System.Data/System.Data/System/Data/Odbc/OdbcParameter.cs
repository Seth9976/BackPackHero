using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.Odbc.OdbcCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataColumn" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A2 RID: 674
	public sealed class OdbcParameter : DbParameter, ICloneable, IDataParameter, IDbDataParameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class.</summary>
		// Token: 0x06001D4C RID: 7500 RVA: 0x0004F1E7 File Offset: 0x0004D3E7
		public OdbcParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and an <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="value">An <see cref="T:System.Data.Odbc.OdbcParameter" /> object. </param>
		// Token: 0x06001D4D RID: 7501 RVA: 0x00090435 File Offset: 0x0008E635
		public OdbcParameter(string name, object value)
			: this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001D4E RID: 7502 RVA: 0x0009044B File Offset: 0x0008E64B
		public OdbcParameter(string name, OdbcType type)
			: this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001D4F RID: 7503 RVA: 0x00090461 File Offset: 0x0008E661
		public OdbcParameter(string name, OdbcType type, int size)
			: this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="sourcecolumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001D50 RID: 7504 RVA: 0x0009047E File Offset: 0x0008E67E
		public OdbcParameter(string name, OdbcType type, int size, string sourcecolumn)
			: this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
			this.SourceColumn = sourcecolumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001D51 RID: 7505 RVA: 0x000904A4 File Offset: 0x0008E6A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
			: this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="P:System.Data.Odbc.OdbcParameter.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="sourceColumnNullMapping">true if the corresponding source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001D52 RID: 7506 RVA: 0x00090504 File Offset: 0x0008E704
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
			: this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00090564 File Offset: 0x0008E764
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x00090584 File Offset: 0x0008E784
		public override DbType DbType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._dbType;
				}
				return TypeMap._NVarChar._dbType;
			}
			set
			{
				if (this._typemap == null || this._typemap._dbType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromDbType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D55 RID: 7509 RVA: 0x000905B5 File Offset: 0x0008E7B5
		public override void ResetDbType()
		{
			this.ResetOdbcType();
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcType" /> value that is the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter. The default is Nchar.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x000905BD File Offset: 0x0008E7BD
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x000905DD File Offset: 0x0008E7DD
		[DefaultValue(OdbcType.NChar)]
		[DbProviderSpecificTypeProperty(true)]
		public OdbcType OdbcType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._odbcType;
				}
				return TypeMap._NVarChar._odbcType;
			}
			set
			{
				if (this._typemap == null || this._typemap._odbcType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromOdbcType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D58 RID: 7512 RVA: 0x0009060E File Offset: 0x0008E80E
		public void ResetOdbcType()
		{
			this.PropertyTypeChanging();
			this._typemap = null;
			this._userSpecifiedType = false;
		}

		// Token: 0x17000551 RID: 1361
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x00090624 File Offset: 0x0008E824
		internal bool HasChanged
		{
			set
			{
				this._hasChanged = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0009062D File Offset: 0x0008E82D
		internal bool UserSpecifiedType
		{
			get
			{
				return this._userSpecifiedType;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Odbc.OdbcParameter" />. The default is an empty string ("").</returns>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00090638 File Offset: 0x0008E838
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x00090656 File Offset: 0x0008E856
		public override string ParameterName
		{
			get
			{
				string parameterName = this._parameterName;
				if (parameterName == null)
				{
					return ADP.StrEmpty;
				}
				return parameterName;
			}
			set
			{
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
				}
			}
		}

		/// <summary>Gets or sets the number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.Odbc.OdbcParameter.Value" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00090673 File Offset: 0x0008E873
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0009067B File Offset: 0x0008E87B
		public new byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00090684 File Offset: 0x0008E884
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x000906A9 File Offset: 0x0008E8A9
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				if (b == 0)
				{
					b = this.ValuePrecision(this.Value);
				}
				return b;
			}
			set
			{
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000906C1 File Offset: 0x0008E8C1
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x000906CC File Offset: 0x0008E8CC
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x000906D4 File Offset: 0x0008E8D4
		public new byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000906E0 File Offset: 0x0008E8E0
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0009070B File Offset: 0x0008E90B
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				if (!this.ShouldSerializeScale(b))
				{
					b = this.ValueScale(this.Value);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
				}
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00090732 File Offset: 0x0008E932
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00090740 File Offset: 0x0008E940
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00090758 File Offset: 0x0008E958
		private int GetColumnSize(object value, int offset, int ordinal)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 62;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (!this._internalShouldSerializeSize && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							num = 0;
						}
						else if (value is string)
						{
							num = ((string)value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is char[])
						{
							num = ((char[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 8192);
							}
						}
						num = Math.Max(2, num);
					}
				}
			}
			return num;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0009092C File Offset: 0x0008EB2C
		private int GetValueSize(object value, int offset)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				bool flag = false;
				if (value is string)
				{
					num = ((string)value).Length - offset;
					flag = true;
				}
				else if (value is char[])
				{
					num = ((char[])value).Length - offset;
					flag = true;
				}
				else if (value is byte[])
				{
					num = ((byte[])value).Length - offset;
				}
				else
				{
					num = 0;
				}
				if (this._internalShouldSerializeSize && this._internalSize >= 0 && this._internalSize < num && this._bindtype == this._originalbindtype)
				{
					num = this._internalSize;
				}
				if (flag)
				{
					num *= 2;
				}
			}
			return num;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000909F4 File Offset: 0x0008EBF4
		private int GetParameterSize(object value, int offset, int ordinal)
		{
			int num = this._bindtype._bufferSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 518;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (num <= 0 && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							if (this._bindtype._sql_c == ODBC32.SQL_C.WCHAR)
							{
								num = 2;
							}
							else
							{
								num = 0;
							}
						}
						else if (value is string)
						{
							num = (((string)value).Length - offset) * 2 + 2;
						}
						else if (value is char[])
						{
							num = (((char[])value).Length - offset) * 2 + 2;
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
						}
						if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
						{
							num = Math.Max(num, 8192);
						}
					}
					else if (ODBC32.SQL_C.WCHAR == this._bindtype._sql_c)
					{
						if (value is string && num < ((string)value).Length && this._bindtype == this._originalbindtype)
						{
							num = ((string)value).Length;
						}
						num = num * 2 + 2;
					}
					else if (value is byte[] && num < ((byte[])value).Length && this._bindtype == this._originalbindtype)
					{
						num = ((byte[])value).Length;
					}
				}
			}
			return num;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00090B70 File Offset: 0x0008ED70
		private byte GetParameterPrecision(object value)
		{
			if (this._internalPrecision != 0 && value is decimal)
			{
				if (this._internalPrecision < 29)
				{
					if (this._internalPrecision != 0)
					{
						byte precision = ((decimal)value).Precision;
						this._internalPrecision = Math.Max(this._internalPrecision, precision);
					}
					return this._internalPrecision;
				}
				return 29;
			}
			else
			{
				if (value == null || value is decimal || Convert.IsDBNull(value))
				{
					return 28;
				}
				return 0;
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00090BE8 File Offset: 0x0008EDE8
		private byte GetParameterScale(object value)
		{
			if (!(value is decimal))
			{
				return this._internalScale;
			}
			byte b = (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			if (this._internalScale > 0 && this._internalScale < b)
			{
				return this._internalScale;
			}
			return b;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001D6D RID: 7533 RVA: 0x00090C36 File Offset: 0x0008EE36
		object ICloneable.Clone()
		{
			return new OdbcParameter(this);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00090C40 File Offset: 0x0008EE40
		private void CopyParameterInternal()
		{
			this._internalValue = this.Value;
			this._internalPrecision = (this.ShouldSerializePrecision() ? this.PrecisionInternal : this.ValuePrecision(this._internalValue));
			this._internalShouldSerializeSize = this.ShouldSerializeSize();
			this._internalSize = (this._internalShouldSerializeSize ? this.Size : this.ValueSize(this._internalValue));
			this._internalDirection = this.Direction;
			this._internalScale = (this.ShouldSerializeScale() ? this.ScaleInternal : this.ValueScale(this._internalValue));
			this._internalOffset = this.Offset;
			this._internalUserSpecifiedType = this.UserSpecifiedType;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00090CF0 File Offset: 0x0008EEF0
		private void CloneHelper(OdbcParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._userSpecifiedType = this._userSpecifiedType;
			destination._typemap = this._typemap;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00090D4C File Offset: 0x0008EF4C
		internal void ClearBinding()
		{
			if (!this._userSpecifiedType)
			{
				this._typemap = null;
			}
			this._bindtype = null;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00090D64 File Offset: 0x0008EF64
		internal void PrepareForBind(OdbcCommand command, short ordinal, ref int parameterBufferSize)
		{
			this.CopyParameterInternal();
			object obj = this.ProcessAndGetParameterValue();
			int num = this._internalOffset;
			int num2 = this._internalSize;
			if (num > 0)
			{
				if (obj is string)
				{
					if (num > ((string)obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is char[])
				{
					if (num > ((char[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is byte[])
				{
					if (num > ((byte[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else
				{
					num = 0;
				}
			}
			ODBC32.SQL_TYPE sql_TYPE = this._bindtype._sql_type;
			if (sql_TYPE - ODBC32.SQL_TYPE.WLONGVARCHAR > 2)
			{
				if (sql_TYPE != ODBC32.SQL_TYPE.BIGINT)
				{
					if (sql_TYPE - ODBC32.SQL_TYPE.NUMERIC <= 1 && (!command.Connection.IsV3Driver || !command.Connection.TestTypeSupport(ODBC32.SQL_TYPE.NUMERIC) || command.Connection.TestRestrictedSqlBindType(this._bindtype._sql_type)))
					{
						this._bindtype = TypeMap._VarChar;
						if (obj != null && !Convert.IsDBNull(obj))
						{
							obj = ((decimal)obj).ToString(CultureInfo.CurrentCulture);
							num2 = ((string)obj).Length;
							num = 0;
						}
					}
				}
				else if (!command.Connection.IsV3Driver)
				{
					this._bindtype = TypeMap._VarChar;
					if (obj != null && !Convert.IsDBNull(obj))
					{
						obj = ((long)obj).ToString(CultureInfo.CurrentCulture);
						num2 = ((string)obj).Length;
						num = 0;
					}
				}
			}
			else
			{
				if (obj is char)
				{
					obj = obj.ToString();
					num2 = ((string)obj).Length;
					num = 0;
				}
				if (!command.Connection.TestTypeSupport(this._bindtype._sql_type))
				{
					if (ODBC32.SQL_TYPE.WCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Char;
					}
					else if (ODBC32.SQL_TYPE.WVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._VarChar;
					}
					else if (ODBC32.SQL_TYPE.WLONGVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Text;
					}
				}
			}
			ODBC32.SQL_C sql_C = this._bindtype._sql_c;
			if (!command.Connection.IsV3Driver && sql_C == ODBC32.SQL_C.WCHAR)
			{
				sql_C = ODBC32.SQL_C.CHAR;
				if (obj != null && !Convert.IsDBNull(obj) && obj is string)
				{
					obj = Encoding.GetEncoding(new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo.ANSICodePage).GetBytes(obj.ToString());
					num2 = ((byte[])obj).Length;
				}
			}
			int parameterSize = this.GetParameterSize(obj, num, (int)ordinal);
			sql_TYPE = this._bindtype._sql_type;
			if (sql_TYPE != ODBC32.SQL_TYPE.WVARCHAR)
			{
				if (sql_TYPE != ODBC32.SQL_TYPE.VARBINARY)
				{
					if (sql_TYPE == ODBC32.SQL_TYPE.VARCHAR)
					{
						if (num2 > 8000)
						{
							this._bindtype = TypeMap._Text;
						}
					}
				}
				else if (num2 > 8000)
				{
					this._bindtype = TypeMap._Image;
				}
			}
			else if (num2 > 4000)
			{
				this._bindtype = TypeMap._NText;
			}
			this._prepared_Sql_C_Type = sql_C;
			this._preparedOffset = num;
			this._preparedSize = num2;
			this._preparedValue = obj;
			this._preparedBufferSize = parameterSize;
			this._preparedIntOffset = parameterBufferSize;
			this._preparedValueOffset = this._preparedIntOffset + IntPtr.Size;
			parameterBufferSize += parameterSize + IntPtr.Size;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0009107C File Offset: 0x0008F27C
		internal void Bind(OdbcStatementHandle hstmt, OdbcCommand command, short ordinal, CNativeBuffer parameterBuffer, bool allowReentrance)
		{
			ODBC32.SQL_C prepared_Sql_C_Type = this._prepared_Sql_C_Type;
			ODBC32.SQL_PARAM sql_PARAM = this.SqlDirectionFromParameterDirection();
			int preparedOffset = this._preparedOffset;
			int preparedSize = this._preparedSize;
			object obj = this._preparedValue;
			int valueSize = this.GetValueSize(obj, preparedOffset);
			int columnSize = this.GetColumnSize(obj, preparedOffset, (int)ordinal);
			byte parameterPrecision = this.GetParameterPrecision(obj);
			byte b = this.GetParameterScale(obj);
			HandleRef handleRef = parameterBuffer.PtrOffset(this._preparedValueOffset, this._preparedBufferSize);
			HandleRef handleRef2 = parameterBuffer.PtrOffset(this._preparedIntOffset, IntPtr.Size);
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				if (ODBC32.SQL_PARAM.INPUT_OUTPUT == sql_PARAM && obj is decimal && b < this._internalScale)
				{
					while (b < this._internalScale)
					{
						obj = (decimal)obj * 10m;
						b += 1;
					}
				}
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, (int)parameterPrecision, 0, parameterBuffer);
				if (ODBC32.SQL_PARAM.INPUT != sql_PARAM)
				{
					parameterBuffer.WriteInt16(this._preparedValueOffset, (short)(((int)b << 8) | (int)parameterPrecision));
				}
			}
			else
			{
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, preparedSize, preparedOffset, parameterBuffer);
			}
			if (!this._hasChanged && this._boundSqlCType == prepared_Sql_C_Type && this._boundParameterType == this._bindtype._sql_type && this._boundSize == columnSize && this._boundScale == (int)b && this._boundBuffer == handleRef.Handle && this._boundIntbuffer == handleRef2.Handle)
			{
				return;
			}
			ODBC32.RetCode retCode = hstmt.BindParameter(ordinal, (short)sql_PARAM, prepared_Sql_C_Type, this._bindtype._sql_type, (IntPtr)columnSize, (IntPtr)((int)b), handleRef, (IntPtr)this._preparedBufferSize, handleRef2);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if ("07006" == command.GetDiagSqlState())
				{
					command.Connection.FlagRestrictedSqlBindType(this._bindtype._sql_type);
					if (allowReentrance)
					{
						this.Bind(hstmt, command, ordinal, parameterBuffer, false);
						return;
					}
				}
				command.Connection.HandleError(hstmt, retCode);
			}
			this._hasChanged = false;
			this._boundSqlCType = prepared_Sql_C_Type;
			this._boundParameterType = this._bindtype._sql_type;
			this._boundSize = columnSize;
			this._boundScale = (int)b;
			this._boundBuffer = handleRef.Handle;
			this._boundIntbuffer = handleRef2.Handle;
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				OdbcDescriptorHandle descriptorHandle = command.GetDescriptorHandle(ODBC32.SQL_ATTR.APP_PARAM_DESC);
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.TYPE, (IntPtr)2);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				int num = (int)parameterPrecision;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.PRECISION, (IntPtr)num);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				num = (int)b;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.SCALE, (IntPtr)num);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				retCode = descriptorHandle.SetDescriptionField2(ordinal, ODBC32.SQL_DESC.DATA_PTR, handleRef);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00091350 File Offset: 0x0008F550
		internal void GetOutputValue(CNativeBuffer parameterBuffer)
		{
			if (this._hasChanged)
			{
				return;
			}
			if (this._bindtype != null && this._internalDirection != ParameterDirection.Input)
			{
				TypeMap bindtype = this._bindtype;
				this._bindtype = null;
				int num = (int)parameterBuffer.ReadIntPtr(this._preparedIntOffset);
				if (-1 == num)
				{
					this.Value = DBNull.Value;
					return;
				}
				if (0 <= num || num == -3)
				{
					this.Value = parameterBuffer.MarshalToManaged(this._preparedValueOffset, this._boundSqlCType, num);
					if (this._boundSqlCType == ODBC32.SQL_C.CHAR && this.Value != null && !Convert.IsDBNull(this.Value))
					{
						Encoding encoding = Encoding.GetEncoding(new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo.ANSICodePage);
						this.Value = encoding.GetString((byte[])this.Value);
					}
					if (bindtype != this._typemap && this.Value != null && !Convert.IsDBNull(this.Value) && this.Value.GetType() != this._typemap._type)
					{
						this.Value = decimal.Parse((string)this.Value, CultureInfo.CurrentCulture);
					}
				}
			}
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00091484 File Offset: 0x0008F684
		private object ProcessAndGetParameterValue()
		{
			object obj = this._internalValue;
			if (this._internalUserSpecifiedType)
			{
				if (obj != null && !Convert.IsDBNull(obj))
				{
					Type type = obj.GetType();
					if (!type.IsArray)
					{
						if (!(type != this._typemap._type))
						{
							goto IL_00CE;
						}
						try
						{
							obj = Convert.ChangeType(obj, this._typemap._type, null);
							goto IL_00CE;
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							throw ADP.ParameterConversionFailed(obj, this._typemap._type, ex);
						}
					}
					if (type == typeof(char[]))
					{
						obj = new string((char[])obj);
					}
				}
			}
			else if (this._typemap == null)
			{
				if (obj == null || Convert.IsDBNull(obj))
				{
					this._typemap = TypeMap._NVarChar;
				}
				else
				{
					Type type2 = obj.GetType();
					this._typemap = TypeMap.FromSystemType(type2);
				}
			}
			IL_00CE:
			this._originalbindtype = (this._bindtype = this._typemap);
			return obj;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00091588 File Offset: 0x0008F788
		private void PropertyChanging()
		{
			this._hasChanged = true;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00091591 File Offset: 0x0008F791
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0009159C File Offset: 0x0008F79C
		internal void SetInputValue(object value, ODBC32.SQL_C sql_c_type, int cbsize, int sizeorprecision, int offset, CNativeBuffer parameterBuffer)
		{
			if (ParameterDirection.Input != this._internalDirection && ParameterDirection.InputOutput != this._internalDirection)
			{
				this._internalValue = null;
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (value == null)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-5));
				return;
			}
			if (Convert.IsDBNull(value))
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (sql_c_type == ODBC32.SQL_C.WCHAR || sql_c_type == ODBC32.SQL_C.BINARY || sql_c_type == ODBC32.SQL_C.CHAR)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)cbsize);
			}
			else
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, IntPtr.Zero);
			}
			parameterBuffer.MarshalToNative(this._preparedValueOffset, value, sql_c_type, sizeorprecision, offset);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00091654 File Offset: 0x0008F854
		private ODBC32.SQL_PARAM SqlDirectionFromParameterDirection()
		{
			switch (this._internalDirection)
			{
			case ParameterDirection.Input:
				return ODBC32.SQL_PARAM.INPUT;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				return ODBC32.SQL_PARAM.OUTPUT;
			case ParameterDirection.InputOutput:
				return ODBC32.SQL_PARAM.INPUT_OUTPUT;
			}
			return ODBC32.SQL_PARAM.INPUT;
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00091691 File Offset: 0x0008F891
		// (set) Token: 0x06001D7A RID: 7546 RVA: 0x00091699 File Offset: 0x0008F899
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._coercedValue = null;
				this._value = value;
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000916A9 File Offset: 0x0008F8A9
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000916B2 File Offset: 0x0008F8B2
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000916BB File Offset: 0x0008F8BB
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000916C4 File Offset: 0x0008F8C4
		private OdbcParameter(OdbcParameter source)
			: this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00091704 File Offset: 0x0008F904
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0009170C File Offset: 0x0008F90C
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x00091718 File Offset: 0x0008F918
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x00091732 File Offset: 0x0008F932
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0009175C File Offset: 0x0008F95C
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x00091764 File Offset: 0x0008F964
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0009176D File Offset: 0x0008F96D
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x00091775 File Offset: 0x0008F975
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		/// <summary>Gets or sets the maximum size of the data within the column.</summary>
		/// <returns>The maximum size of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0009178C File Offset: 0x0008F98C
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x000917B1 File Offset: 0x0008F9B1
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000917D4 File Offset: 0x0008F9D4
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>The name of the source column that will be used to set the value of this parameter. The default is an empty string ("").</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x000917E0 File Offset: 0x0008F9E0
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x000917FE File Offset: 0x0008F9FE
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This lets <see cref="T:System.Data.Common.DbCommandBuilder" /> correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00091807 File Offset: 0x0008FA07
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x0009180F File Offset: 0x0008FA0F
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00091818 File Offset: 0x0008FA18
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x00091836 File Offset: 0x0008FA36
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_0032;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_0032;
				}
				this._sourceVersion = value;
				return;
				IL_0032:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00091870 File Offset: 0x0008FA70
		private void CloneHelperCore(OdbcParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000918E0 File Offset: 0x0008FAE0
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00091900 File Offset: 0x0008FB00
		internal void ResetParent()
		{
			this._parent = null;
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</returns>
		// Token: 0x06001D93 RID: 7571 RVA: 0x0006D939 File Offset: 0x0006BB39
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0009190C File Offset: 0x0008FB0C
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0006D96E File Offset: 0x0006BB6E
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00091938 File Offset: 0x0008FB38
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x040015C0 RID: 5568
		private bool _hasChanged;

		// Token: 0x040015C1 RID: 5569
		private bool _userSpecifiedType;

		// Token: 0x040015C2 RID: 5570
		private TypeMap _typemap;

		// Token: 0x040015C3 RID: 5571
		private TypeMap _bindtype;

		// Token: 0x040015C4 RID: 5572
		private string _parameterName;

		// Token: 0x040015C5 RID: 5573
		private byte _precision;

		// Token: 0x040015C6 RID: 5574
		private byte _scale;

		// Token: 0x040015C7 RID: 5575
		private bool _hasScale;

		// Token: 0x040015C8 RID: 5576
		private ODBC32.SQL_C _boundSqlCType;

		// Token: 0x040015C9 RID: 5577
		private ODBC32.SQL_TYPE _boundParameterType;

		// Token: 0x040015CA RID: 5578
		private int _boundSize;

		// Token: 0x040015CB RID: 5579
		private int _boundScale;

		// Token: 0x040015CC RID: 5580
		private IntPtr _boundBuffer;

		// Token: 0x040015CD RID: 5581
		private IntPtr _boundIntbuffer;

		// Token: 0x040015CE RID: 5582
		private TypeMap _originalbindtype;

		// Token: 0x040015CF RID: 5583
		private byte _internalPrecision;

		// Token: 0x040015D0 RID: 5584
		private bool _internalShouldSerializeSize;

		// Token: 0x040015D1 RID: 5585
		private int _internalSize;

		// Token: 0x040015D2 RID: 5586
		private ParameterDirection _internalDirection;

		// Token: 0x040015D3 RID: 5587
		private byte _internalScale;

		// Token: 0x040015D4 RID: 5588
		private int _internalOffset;

		// Token: 0x040015D5 RID: 5589
		internal bool _internalUserSpecifiedType;

		// Token: 0x040015D6 RID: 5590
		private object _internalValue;

		// Token: 0x040015D7 RID: 5591
		private int _preparedOffset;

		// Token: 0x040015D8 RID: 5592
		private int _preparedSize;

		// Token: 0x040015D9 RID: 5593
		private int _preparedBufferSize;

		// Token: 0x040015DA RID: 5594
		private object _preparedValue;

		// Token: 0x040015DB RID: 5595
		private int _preparedIntOffset;

		// Token: 0x040015DC RID: 5596
		private int _preparedValueOffset;

		// Token: 0x040015DD RID: 5597
		private ODBC32.SQL_C _prepared_Sql_C_Type;

		// Token: 0x040015DE RID: 5598
		private object _value;

		// Token: 0x040015DF RID: 5599
		private object _parent;

		// Token: 0x040015E0 RID: 5600
		private ParameterDirection _direction;

		// Token: 0x040015E1 RID: 5601
		private int _size;

		// Token: 0x040015E2 RID: 5602
		private int _offset;

		// Token: 0x040015E3 RID: 5603
		private string _sourceColumn;

		// Token: 0x040015E4 RID: 5604
		private DataRowVersion _sourceVersion;

		// Token: 0x040015E5 RID: 5605
		private bool _sourceColumnNullMapping;

		// Token: 0x040015E6 RID: 5606
		private bool _isNullable;

		// Token: 0x040015E7 RID: 5607
		private object _coercedValue;
	}
}
