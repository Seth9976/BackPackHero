using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x0200033B RID: 827
	internal class DbConnectionStringBuilderDescriptor : PropertyDescriptor
	{
		// Token: 0x060027CC RID: 10188 RVA: 0x000AFEF0 File Offset: 0x000AE0F0
		internal DbConnectionStringBuilderDescriptor(string propertyName, Type componentType, Type propertyType, bool isReadOnly, Attribute[] attributes)
			: base(propertyName, attributes)
		{
			this.ComponentType = componentType;
			this.PropertyType = propertyType;
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000AFF11 File Offset: 0x000AE111
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000AFF19 File Offset: 0x000AE119
		internal bool RefreshOnChange { get; set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000AFF22 File Offset: 0x000AE122
		public override Type ComponentType { get; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x000AFF2A File Offset: 0x000AE12A
		public override bool IsReadOnly { get; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000AFF32 File Offset: 0x000AE132
		public override Type PropertyType { get; }

		// Token: 0x060027D2 RID: 10194 RVA: 0x000AFF3C File Offset: 0x000AE13C
		public override bool CanResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000AFF64 File Offset: 0x000AE164
		public override object GetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			object obj;
			if (dbConnectionStringBuilder != null && dbConnectionStringBuilder.TryGetValue(this.DisplayName, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000AFF90 File Offset: 0x000AE190
		public override void ResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				dbConnectionStringBuilder.Remove(this.DisplayName);
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000AFFC4 File Offset: 0x000AE1C4
		public override void SetValue(object component, object value)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				if (typeof(string) == this.PropertyType && string.Empty.Equals(value))
				{
					value = null;
				}
				dbConnectionStringBuilder[this.DisplayName] = value;
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000B0020 File Offset: 0x000AE220
		public override bool ShouldSerializeValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}
	}
}
