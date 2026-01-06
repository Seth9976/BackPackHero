using System;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x020000AD RID: 173
	public struct PathNode
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0001A9A5 File Offset: 0x00018BA5
		public static uint ReverseFractionAlongEdge(uint v)
		{
			return 15U - v;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001A9AB File Offset: 0x00018BAB
		public static uint QuantizeFractionAlongEdge(float v)
		{
			v *= 15f;
			v += 0.5f;
			return math.clamp((uint)v, 0U, 15U);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001A9C9 File Offset: 0x00018BC9
		public static float UnQuantizeFractionAlongEdge(uint v)
		{
			return v * 0.06666667f;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001A9E5 File Offset: 0x00018BE5
		public uint fractionAlongEdge
		{
			get
			{
				return (this.flags & 1006632960U) >> 26;
			}
			set
			{
				this.flags = (this.flags & 3288334335U) | ((value << 26) & 1006632960U);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001AA04 File Offset: 0x00018C04
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001AA12 File Offset: 0x00018C12
		public uint parentIndex
		{
			get
			{
				return this.flags & 67108863U;
			}
			set
			{
				this.flags = (this.flags & 4227858432U) | value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001AA28 File Offset: 0x00018C28
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001AA39 File Offset: 0x00018C39
		public bool flag1
		{
			get
			{
				return (this.flags & 1073741824U) > 0U;
			}
			set
			{
				this.flags = (this.flags & 3221225471U) | (value ? 1073741824U : 0U);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001AA59 File Offset: 0x00018C59
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001AA6A File Offset: 0x00018C6A
		public bool flag2
		{
			get
			{
				return (this.flags & 2147483648U) > 0U;
			}
			set
			{
				this.flags = (this.flags & 2147483647U) | (value ? 2147483648U : 0U);
			}
		}

		// Token: 0x04000398 RID: 920
		public ushort pathID;

		// Token: 0x04000399 RID: 921
		public ushort heapIndex;

		// Token: 0x0400039A RID: 922
		private uint flags;

		// Token: 0x0400039B RID: 923
		public static readonly PathNode Default = new PathNode
		{
			pathID = 0,
			heapIndex = ushort.MaxValue,
			flags = 0U
		};

		// Token: 0x0400039C RID: 924
		private const uint ParentIndexMask = 67108863U;

		// Token: 0x0400039D RID: 925
		private const int FractionAlongEdgeOffset = 26;

		// Token: 0x0400039E RID: 926
		private const uint FractionAlongEdgeMask = 1006632960U;

		// Token: 0x0400039F RID: 927
		public const int FractionAlongEdgeQuantization = 16;

		// Token: 0x040003A0 RID: 928
		private const int Flag1Offset = 30;

		// Token: 0x040003A1 RID: 929
		private const uint Flag1Mask = 1073741824U;

		// Token: 0x040003A2 RID: 930
		private const int Flag2Offset = 31;

		// Token: 0x040003A3 RID: 931
		private const uint Flag2Mask = 2147483648U;
	}
}
