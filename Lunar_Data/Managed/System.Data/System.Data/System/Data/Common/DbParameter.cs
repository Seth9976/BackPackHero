using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.Common.DbCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataSet" /> column. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000347 RID: 839
	public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to a valid <see cref="T:System.Data.DbType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060028A4 RID: 10404
		// (set) Token: 0x060028A5 RID: 10405
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract DbType DbType { get; set; }

		/// <summary>Resets the DbType property to its original settings.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060028A6 RID: 10406
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract void ResetDbType();

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060028A7 RID: 10407
		// (set) Token: 0x060028A8 RID: 10408
		[DefaultValue(ParameterDirection.Input)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract ParameterDirection Direction { get; set; }

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060028A9 RID: 10409
		// (set) Token: 0x060028AA RID: 10410
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool IsNullable { get; set; }

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Common.DbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Common.DbParameter" />. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060028AB RID: 10411
		// (set) Token: 0x060028AC RID: 10412
		[DefaultValue("")]
		public abstract string ParameterName { get; set; }

		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x00005AE9 File Offset: 0x00003CE9
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x000094D4 File Offset: 0x000076D4
		byte IDbDataParameter.Precision
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataParameter.Scale" />.</summary>
		/// <returns>The number of decimal places to which <see cref="T:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x00005AE9 File Offset: 0x00003CE9
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x000094D4 File Offset: 0x000076D4
		byte IDbDataParameter.Scale
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000B24FF File Offset: 0x000B06FF
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x000B2507 File Offset: 0x000B0707
		public virtual byte Precision
		{
			get
			{
				return ((IDbDataParameter)this).Precision;
			}
			set
			{
				((IDbDataParameter)this).Precision = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x000B2510 File Offset: 0x000B0710
		// (set) Token: 0x060028B4 RID: 10420 RVA: 0x000B2518 File Offset: 0x000B0718
		public virtual byte Scale
		{
			get
			{
				return ((IDbDataParameter)this).Scale;
			}
			set
			{
				((IDbDataParameter)this).Scale = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060028B5 RID: 10421
		// (set) Token: 0x060028B6 RID: 10422
		public abstract int Size { get; set; }

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060028B7 RID: 10423
		// (set) Token: 0x060028B8 RID: 10424
		[DefaultValue("")]
		public abstract string SourceColumn { get; set; }

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060028B9 RID: 10425
		// (set) Token: 0x060028BA RID: 10426
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract bool SourceColumnNullMapping { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property is not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x000B2521 File Offset: 0x000B0721
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x000094D4 File Offset: 0x000076D4
		[DefaultValue(DataRowVersion.Current)]
		public virtual DataRowVersion SourceVersion
		{
			get
			{
				return DataRowVersion.Default;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060028BD RID: 10429
		// (set) Token: 0x060028BE RID: 10430
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(null)]
		public abstract object Value { get; set; }
	}
}
