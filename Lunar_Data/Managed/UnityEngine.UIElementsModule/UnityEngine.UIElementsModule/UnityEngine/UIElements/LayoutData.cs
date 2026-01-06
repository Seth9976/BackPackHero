using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000297 RID: 663
	internal struct LayoutData : IStyleDataGroup<LayoutData>, IEquatable<LayoutData>
	{
		// Token: 0x06001696 RID: 5782 RVA: 0x0005C3E8 File Offset: 0x0005A5E8
		public LayoutData Copy()
		{
			return this;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005C400 File Offset: 0x0005A600
		public void CopyFrom(ref LayoutData other)
		{
			this = other;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005C410 File Offset: 0x0005A610
		public static bool operator ==(LayoutData lhs, LayoutData rhs)
		{
			return lhs.alignContent == rhs.alignContent && lhs.alignItems == rhs.alignItems && lhs.alignSelf == rhs.alignSelf && lhs.borderBottomWidth == rhs.borderBottomWidth && lhs.borderLeftWidth == rhs.borderLeftWidth && lhs.borderRightWidth == rhs.borderRightWidth && lhs.borderTopWidth == rhs.borderTopWidth && lhs.bottom == rhs.bottom && lhs.display == rhs.display && lhs.flexBasis == rhs.flexBasis && lhs.flexDirection == rhs.flexDirection && lhs.flexGrow == rhs.flexGrow && lhs.flexShrink == rhs.flexShrink && lhs.flexWrap == rhs.flexWrap && lhs.height == rhs.height && lhs.justifyContent == rhs.justifyContent && lhs.left == rhs.left && lhs.marginBottom == rhs.marginBottom && lhs.marginLeft == rhs.marginLeft && lhs.marginRight == rhs.marginRight && lhs.marginTop == rhs.marginTop && lhs.maxHeight == rhs.maxHeight && lhs.maxWidth == rhs.maxWidth && lhs.minHeight == rhs.minHeight && lhs.minWidth == rhs.minWidth && lhs.paddingBottom == rhs.paddingBottom && lhs.paddingLeft == rhs.paddingLeft && lhs.paddingRight == rhs.paddingRight && lhs.paddingTop == rhs.paddingTop && lhs.position == rhs.position && lhs.right == rhs.right && lhs.top == rhs.top && lhs.width == rhs.width;
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005C6A0 File Offset: 0x0005A8A0
		public static bool operator !=(LayoutData lhs, LayoutData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		public bool Equals(LayoutData other)
		{
			return other == this;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005C6DC File Offset: 0x0005A8DC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LayoutData && this.Equals((LayoutData)obj);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0005C714 File Offset: 0x0005A914
		public override int GetHashCode()
		{
			int num = (int)this.alignContent;
			num = (num * 397) ^ (int)this.alignItems;
			num = (num * 397) ^ (int)this.alignSelf;
			num = (num * 397) ^ this.borderBottomWidth.GetHashCode();
			num = (num * 397) ^ this.borderLeftWidth.GetHashCode();
			num = (num * 397) ^ this.borderRightWidth.GetHashCode();
			num = (num * 397) ^ this.borderTopWidth.GetHashCode();
			num = (num * 397) ^ this.bottom.GetHashCode();
			num = (num * 397) ^ (int)this.display;
			num = (num * 397) ^ this.flexBasis.GetHashCode();
			num = (num * 397) ^ (int)this.flexDirection;
			num = (num * 397) ^ this.flexGrow.GetHashCode();
			num = (num * 397) ^ this.flexShrink.GetHashCode();
			num = (num * 397) ^ (int)this.flexWrap;
			num = (num * 397) ^ this.height.GetHashCode();
			num = (num * 397) ^ (int)this.justifyContent;
			num = (num * 397) ^ this.left.GetHashCode();
			num = (num * 397) ^ this.marginBottom.GetHashCode();
			num = (num * 397) ^ this.marginLeft.GetHashCode();
			num = (num * 397) ^ this.marginRight.GetHashCode();
			num = (num * 397) ^ this.marginTop.GetHashCode();
			num = (num * 397) ^ this.maxHeight.GetHashCode();
			num = (num * 397) ^ this.maxWidth.GetHashCode();
			num = (num * 397) ^ this.minHeight.GetHashCode();
			num = (num * 397) ^ this.minWidth.GetHashCode();
			num = (num * 397) ^ this.paddingBottom.GetHashCode();
			num = (num * 397) ^ this.paddingLeft.GetHashCode();
			num = (num * 397) ^ this.paddingRight.GetHashCode();
			num = (num * 397) ^ this.paddingTop.GetHashCode();
			num = (num * 397) ^ (int)this.position;
			num = (num * 397) ^ this.right.GetHashCode();
			num = (num * 397) ^ this.top.GetHashCode();
			return (num * 397) ^ this.width.GetHashCode();
		}

		// Token: 0x0400093E RID: 2366
		public Align alignContent;

		// Token: 0x0400093F RID: 2367
		public Align alignItems;

		// Token: 0x04000940 RID: 2368
		public Align alignSelf;

		// Token: 0x04000941 RID: 2369
		public float borderBottomWidth;

		// Token: 0x04000942 RID: 2370
		public float borderLeftWidth;

		// Token: 0x04000943 RID: 2371
		public float borderRightWidth;

		// Token: 0x04000944 RID: 2372
		public float borderTopWidth;

		// Token: 0x04000945 RID: 2373
		public Length bottom;

		// Token: 0x04000946 RID: 2374
		public DisplayStyle display;

		// Token: 0x04000947 RID: 2375
		public Length flexBasis;

		// Token: 0x04000948 RID: 2376
		public FlexDirection flexDirection;

		// Token: 0x04000949 RID: 2377
		public float flexGrow;

		// Token: 0x0400094A RID: 2378
		public float flexShrink;

		// Token: 0x0400094B RID: 2379
		public Wrap flexWrap;

		// Token: 0x0400094C RID: 2380
		public Length height;

		// Token: 0x0400094D RID: 2381
		public Justify justifyContent;

		// Token: 0x0400094E RID: 2382
		public Length left;

		// Token: 0x0400094F RID: 2383
		public Length marginBottom;

		// Token: 0x04000950 RID: 2384
		public Length marginLeft;

		// Token: 0x04000951 RID: 2385
		public Length marginRight;

		// Token: 0x04000952 RID: 2386
		public Length marginTop;

		// Token: 0x04000953 RID: 2387
		public Length maxHeight;

		// Token: 0x04000954 RID: 2388
		public Length maxWidth;

		// Token: 0x04000955 RID: 2389
		public Length minHeight;

		// Token: 0x04000956 RID: 2390
		public Length minWidth;

		// Token: 0x04000957 RID: 2391
		public Length paddingBottom;

		// Token: 0x04000958 RID: 2392
		public Length paddingLeft;

		// Token: 0x04000959 RID: 2393
		public Length paddingRight;

		// Token: 0x0400095A RID: 2394
		public Length paddingTop;

		// Token: 0x0400095B RID: 2395
		public Position position;

		// Token: 0x0400095C RID: 2396
		public Length right;

		// Token: 0x0400095D RID: 2397
		public Length top;

		// Token: 0x0400095E RID: 2398
		public Length width;
	}
}
