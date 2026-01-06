using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000063 RID: 99
	internal sealed class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00015DA6 File Offset: 0x00013FA6
		internal DataRelationPropertyDescriptor(DataRelation dataRelation)
			: base(dataRelation.RelationName, null)
		{
			this.Relation = dataRelation;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00015DBC File Offset: 0x00013FBC
		internal DataRelation Relation { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public override bool Equals(object other)
		{
			return other is DataRelationPropertyDescriptor && ((DataRelationPropertyDescriptor)other).Relation == this.Relation;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00015DEF File Offset: 0x00013FEF
		public override int GetHashCode()
		{
			return this.Relation.GetHashCode();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00015DFC File Offset: 0x00013FFC
		public override object GetValue(object component)
		{
			return ((DataRowView)component).CreateChildView(this.Relation);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void ResetValue(object component)
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
