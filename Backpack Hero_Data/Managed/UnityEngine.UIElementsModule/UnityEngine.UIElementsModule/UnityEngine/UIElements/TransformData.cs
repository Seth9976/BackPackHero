using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000299 RID: 665
	internal struct TransformData : IStyleDataGroup<TransformData>, IEquatable<TransformData>
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x0005CC04 File Offset: 0x0005AE04
		public TransformData Copy()
		{
			return this;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005CC1C File Offset: 0x0005AE1C
		public void CopyFrom(ref TransformData other)
		{
			this = other;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005CC2C File Offset: 0x0005AE2C
		public static bool operator ==(TransformData lhs, TransformData rhs)
		{
			return lhs.rotate == rhs.rotate && lhs.scale == rhs.scale && lhs.transformOrigin == rhs.transformOrigin && lhs.translate == rhs.translate;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005CC8C File Offset: 0x0005AE8C
		public static bool operator !=(TransformData lhs, TransformData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005CCA8 File Offset: 0x0005AEA8
		public bool Equals(TransformData other)
		{
			return other == this;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005CCC8 File Offset: 0x0005AEC8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransformData && this.Equals((TransformData)obj);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005CD00 File Offset: 0x0005AF00
		public override int GetHashCode()
		{
			int num = this.rotate.GetHashCode();
			num = (num * 397) ^ this.scale.GetHashCode();
			num = (num * 397) ^ this.transformOrigin.GetHashCode();
			return (num * 397) ^ this.translate.GetHashCode();
		}

		// Token: 0x04000969 RID: 2409
		public Rotate rotate;

		// Token: 0x0400096A RID: 2410
		public Scale scale;

		// Token: 0x0400096B RID: 2411
		public TransformOrigin transformOrigin;

		// Token: 0x0400096C RID: 2412
		public Translate translate;
	}
}
