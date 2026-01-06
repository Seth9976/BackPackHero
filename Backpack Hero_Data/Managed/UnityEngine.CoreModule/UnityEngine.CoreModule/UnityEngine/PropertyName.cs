using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E3 RID: 483
	[UsedByNativeCode]
	public struct PropertyName : IEquatable<PropertyName>
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x00023142 File Offset: 0x00021342
		public PropertyName(string name)
		{
			this = new PropertyName(PropertyNameUtils.PropertyNameFromString(name));
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00023152 File Offset: 0x00021352
		public PropertyName(PropertyName other)
		{
			this.id = other.id;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00023161 File Offset: 0x00021361
		public PropertyName(int id)
		{
			this.id = id;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0002316C File Offset: 0x0002136C
		public static bool IsNullOrEmpty(PropertyName prop)
		{
			return prop.id == 0;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00023188 File Offset: 0x00021388
		public static bool operator ==(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000231A8 File Offset: 0x000213A8
		public static bool operator !=(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000231CC File Offset: 0x000213CC
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000231E4 File Offset: 0x000213E4
		public override bool Equals(object other)
		{
			return other is PropertyName && this.Equals((PropertyName)other);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00023210 File Offset: 0x00021410
		public bool Equals(PropertyName other)
		{
			return this == other;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00023230 File Offset: 0x00021430
		public static implicit operator PropertyName(string name)
		{
			return new PropertyName(name);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00023248 File Offset: 0x00021448
		public static implicit operator PropertyName(int id)
		{
			return new PropertyName(id);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00023260 File Offset: 0x00021460
		public override string ToString()
		{
			return string.Format("Unknown:{0}", this.id);
		}

		// Token: 0x040007BC RID: 1980
		internal int id;
	}
}
