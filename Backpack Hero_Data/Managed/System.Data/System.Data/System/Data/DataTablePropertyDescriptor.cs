using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200007D RID: 125
	internal sealed class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0002684A File Offset: 0x00024A4A
		public DataTable Table { get; }

		// Token: 0x06000883 RID: 2179 RVA: 0x00026852 File Offset: 0x00024A52
		internal DataTablePropertyDescriptor(DataTable dataTable)
			: base(dataTable.TableName, null)
		{
			this.Table = dataTable;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00026868 File Offset: 0x00024A68
		public override bool Equals(object other)
		{
			return other is DataTablePropertyDescriptor && ((DataTablePropertyDescriptor)other).Table == this.Table;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00026887 File Offset: 0x00024A87
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00026894 File Offset: 0x00024A94
		public override object GetValue(object component)
		{
			return ((DataViewManagerListItemTypeDescriptor)component).GetDataView(this.Table);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void ResetValue(object component)
		{
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
