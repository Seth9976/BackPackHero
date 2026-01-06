using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x0200004F RID: 79
	internal sealed class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00011D46 File Offset: 0x0000FF46
		internal DataColumnPropertyDescriptor(DataColumn dataColumn)
			: base(dataColumn.ColumnName, null)
		{
			this.Column = dataColumn;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public override AttributeCollection Attributes
		{
			get
			{
				if (typeof(IList).IsAssignableFrom(this.PropertyType))
				{
					Attribute[] array = new Attribute[base.Attributes.Count + 1];
					base.Attributes.CopyTo(array, 0);
					array[array.Length - 1] = new ListBindableAttribute(false);
					return new AttributeCollection(array);
				}
				return base.Attributes;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00011DBA File Offset: 0x0000FFBA
		internal DataColumn Column { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011DCE File Offset: 0x0000FFCE
		public override bool IsReadOnly
		{
			get
			{
				return this.Column.ReadOnly;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00011DDB File Offset: 0x0000FFDB
		public override Type PropertyType
		{
			get
			{
				return this.Column.DataType;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		public override bool Equals(object other)
		{
			return other is DataColumnPropertyDescriptor && ((DataColumnPropertyDescriptor)other).Column == this.Column;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00011E07 File Offset: 0x00010007
		public override int GetHashCode()
		{
			return this.Column.GetHashCode();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011E14 File Offset: 0x00010014
		public override bool CanResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			if (!this.Column.IsSqlType)
			{
				return dataRowView.GetColumnValue(this.Column) != DBNull.Value;
			}
			return !DataStorage.IsObjectNull(dataRowView.GetColumnValue(this.Column));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011E60 File Offset: 0x00010060
		public override object GetValue(object component)
		{
			return ((DataRowView)component).GetColumnValue(this.Column);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011E73 File Offset: 0x00010073
		public override void ResetValue(object component)
		{
			((DataRowView)component).SetColumnValue(this.Column, DBNull.Value);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00011E8B File Offset: 0x0001008B
		public override void SetValue(object component, object value)
		{
			((DataRowView)component).SetColumnValue(this.Column, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00011EAB File Offset: 0x000100AB
		public override bool IsBrowsable
		{
			get
			{
				return this.Column.ColumnMapping != MappingType.Hidden && base.IsBrowsable;
			}
		}
	}
}
