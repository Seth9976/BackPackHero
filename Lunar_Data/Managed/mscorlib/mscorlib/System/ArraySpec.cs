using System;
using System.Text;

namespace System
{
	// Token: 0x02000260 RID: 608
	internal class ArraySpec : ModifierSpec
	{
		// Token: 0x06001BCF RID: 7119 RVA: 0x00067BDE File Offset: 0x00065DDE
		internal ArraySpec(int dimensions, bool bound)
		{
			this.dimensions = dimensions;
			this.bound = bound;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00067BF4 File Offset: 0x00065DF4
		public Type Resolve(Type type)
		{
			if (this.bound)
			{
				return type.MakeArrayType(1);
			}
			if (this.dimensions == 1)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.dimensions);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00067C22 File Offset: 0x00065E22
		public StringBuilder Append(StringBuilder sb)
		{
			if (this.bound)
			{
				return sb.Append("[*]");
			}
			return sb.Append('[').Append(',', this.dimensions - 1).Append(']');
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00067C56 File Offset: 0x00065E56
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00067C68 File Offset: 0x00065E68
		public int Rank
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00067C70 File Offset: 0x00065E70
		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x04001993 RID: 6547
		private int dimensions;

		// Token: 0x04001994 RID: 6548
		private bool bound;
	}
}
