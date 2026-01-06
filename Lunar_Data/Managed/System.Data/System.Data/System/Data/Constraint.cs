using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>Represents a constraint that can be enforced on one or more <see cref="T:System.Data.DataColumn" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000040 RID: 64
	[DefaultProperty("ConstraintName")]
	[TypeConverter(typeof(ConstraintConverter))]
	public abstract class Constraint
	{
		/// <summary>The name of a constraint in the <see cref="T:System.Data.ConstraintCollection" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Constraint" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.Constraint" /> name is a null value or empty string. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.ConstraintCollection" /> already contains a <see cref="T:System.Data.Constraint" /> with the same name (The comparison is not case-sensitive.). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		[DefaultValue("")]
		public virtual string ConstraintName
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (string.IsNullOrEmpty(value) && this.Table != null && this.InCollection)
				{
					throw ExceptionBuilder.NoConstraintName();
				}
				CultureInfo cultureInfo = ((this.Table != null) ? this.Table.Locale : CultureInfo.CurrentCulture);
				if (string.Compare(this._name, value, true, cultureInfo) != 0)
				{
					if (this.Table != null && this.InCollection)
					{
						this.Table.Constraints.RegisterName(value);
						if (this._name.Length != 0)
						{
							this.Table.Constraints.UnregisterName(this._name);
						}
					}
					this._name = value;
					return;
				}
				if (string.Compare(this._name, value, false, cultureInfo) != 0)
				{
					this._name = value;
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000D29F File Offset: 0x0000B49F
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000D2BB File Offset: 0x0000B4BB
		internal string SchemaName
		{
			get
			{
				if (!string.IsNullOrEmpty(this._schemaName))
				{
					return this._schemaName;
				}
				return this.ConstraintName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._schemaName = value;
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		internal virtual bool InCollection
		{
			get
			{
				return this._inCollection;
			}
			set
			{
				this._inCollection = value;
				this._dataSet = (value ? this.Table.DataSet : null);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the constraint applies.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> to which the constraint applies.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000271 RID: 625
		public abstract DataTable Table { get; }

		/// <summary>Gets the collection of user-defined constraint properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x06000273 RID: 627
		internal abstract bool ContainsColumn(DataColumn column);

		// Token: 0x06000274 RID: 628
		internal abstract bool CanEnableConstraint();

		// Token: 0x06000275 RID: 629
		internal abstract Constraint Clone(DataSet destination);

		// Token: 0x06000276 RID: 630
		internal abstract Constraint Clone(DataSet destination, bool ignoreNSforTableLookup);

		// Token: 0x06000277 RID: 631 RVA: 0x0000D319 File Offset: 0x0000B519
		internal void CheckConstraint()
		{
			if (!this.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintViolation(this.ConstraintName);
			}
		}

		// Token: 0x06000278 RID: 632
		internal abstract void CheckCanAddToCollection(ConstraintCollection constraint);

		// Token: 0x06000279 RID: 633
		internal abstract bool CanBeRemovedFromCollection(ConstraintCollection constraint, bool fThrowException);

		// Token: 0x0600027A RID: 634
		internal abstract void CheckConstraint(DataRow row, DataRowAction action);

		// Token: 0x0600027B RID: 635
		internal abstract void CheckState();

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		// Token: 0x0600027C RID: 636 RVA: 0x0000D330 File Offset: 0x0000B530
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to which the constraint belongs.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000D37C File Offset: 0x0000B57C
		[CLSCompliant(false)]
		protected virtual DataSet _DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		/// <summary>Sets the constraint's <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to which this constraint will belong. </param>
		// Token: 0x0600027E RID: 638 RVA: 0x0000D384 File Offset: 0x0000B584
		protected internal void SetDataSet(DataSet dataSet)
		{
			this._dataSet = dataSet;
		}

		// Token: 0x0600027F RID: 639
		internal abstract bool IsConstraintViolated();

		/// <summary>Gets the <see cref="P:System.Data.Constraint.ConstraintName" />, if there is one, as a string.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.Constraint.ConstraintName" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000280 RID: 640 RVA: 0x0000D38D File Offset: 0x0000B58D
		public override string ToString()
		{
			return this.ConstraintName;
		}

		// Token: 0x04000497 RID: 1175
		private string _schemaName = string.Empty;

		// Token: 0x04000498 RID: 1176
		private bool _inCollection;

		// Token: 0x04000499 RID: 1177
		private DataSet _dataSet;

		// Token: 0x0400049A RID: 1178
		internal string _name = string.Empty;

		// Token: 0x0400049B RID: 1179
		internal PropertyCollection _extendedProperties;
	}
}
