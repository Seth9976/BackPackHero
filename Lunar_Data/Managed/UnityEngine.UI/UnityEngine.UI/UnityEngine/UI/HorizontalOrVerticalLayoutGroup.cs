using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001E RID: 30
	[ExecuteAlways]
	public abstract class HorizontalOrVerticalLayoutGroup : LayoutGroup
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000DD71 File Offset: 0x0000BF71
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000DD79 File Offset: 0x0000BF79
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000DD88 File Offset: 0x0000BF88
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000DD9F File Offset: 0x0000BF9F
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000DDA7 File Offset: 0x0000BFA7
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000DDB6 File Offset: 0x0000BFB6
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000DDBE File Offset: 0x0000BFBE
		public bool childControlWidth
		{
			get
			{
				return this.m_ChildControlWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlWidth, value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000DDD5 File Offset: 0x0000BFD5
		public bool childControlHeight
		{
			get
			{
				return this.m_ChildControlHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlHeight, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public bool childScaleWidth
		{
			get
			{
				return this.m_ChildScaleWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleWidth, value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000DDFB File Offset: 0x0000BFFB
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000DE03 File Offset: 0x0000C003
		public bool childScaleHeight
		{
			get
			{
				return this.m_ChildScaleHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleHeight, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000DE12 File Offset: 0x0000C012
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000DE1A File Offset: 0x0000C01A
		public bool reverseArrangement
		{
			get
			{
				return this.m_ReverseArrangement;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ReverseArrangement, value);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DE2C File Offset: 0x0000C02C
		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float num = (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
			bool flag = ((axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight);
			bool flag2 = ((axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight);
			bool flag3 = ((axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight);
			float num2 = num;
			float num3 = num;
			float num4 = 0f;
			bool flag4 = isVertical ^ (axis == 1);
			int count = base.rectChildren.Count;
			for (int i = 0; i < count; i++)
			{
				RectTransform rectTransform = base.rectChildren[i];
				float num5;
				float num6;
				float num7;
				this.GetChildSizes(rectTransform, axis, flag, flag3, out num5, out num6, out num7);
				if (flag2)
				{
					float num8 = rectTransform.localScale[axis];
					num5 *= num8;
					num6 *= num8;
					num7 *= num8;
				}
				if (flag4)
				{
					num2 = Mathf.Max(num5 + num, num2);
					num3 = Mathf.Max(num6 + num, num3);
					num4 = Mathf.Max(num7, num4);
				}
				else
				{
					num2 += num5 + this.spacing;
					num3 += num6 + this.spacing;
					num4 += num7;
				}
			}
			if (!flag4 && base.rectChildren.Count > 0)
			{
				num2 -= this.spacing;
				num3 -= this.spacing;
			}
			num3 = Mathf.Max(num2, num3);
			base.SetLayoutInputForAxis(num2, num3, num4, axis);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			float num = base.rectTransform.rect.size[axis];
			bool flag = ((axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight);
			bool flag2 = ((axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight);
			bool flag3 = ((axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight);
			float alignmentOnAxis = base.GetAlignmentOnAxis(axis);
			bool flag4 = isVertical ^ (axis == 1);
			int num2 = (this.m_ReverseArrangement ? (base.rectChildren.Count - 1) : 0);
			int num3 = (this.m_ReverseArrangement ? 0 : base.rectChildren.Count);
			int num4 = (this.m_ReverseArrangement ? (-1) : 1);
			if (flag4)
			{
				float num5 = num - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
				int num6 = num2;
				while (this.m_ReverseArrangement ? (num6 >= num3) : (num6 < num3))
				{
					RectTransform rectTransform = base.rectChildren[num6];
					float num7;
					float num8;
					float num9;
					this.GetChildSizes(rectTransform, axis, flag, flag3, out num7, out num8, out num9);
					float num10 = (flag2 ? rectTransform.localScale[axis] : 1f);
					float num11 = Mathf.Clamp(num5, num7, (num9 > 0f) ? num : num8);
					float startOffset = base.GetStartOffset(axis, num11 * num10);
					if (flag)
					{
						base.SetChildAlongAxisWithScale(rectTransform, axis, startOffset, num11, num10);
					}
					else
					{
						float num12 = (num11 - rectTransform.sizeDelta[axis]) * alignmentOnAxis;
						base.SetChildAlongAxisWithScale(rectTransform, axis, startOffset + num12, num10);
					}
					num6 += num4;
				}
				return;
			}
			float num13 = (float)((axis == 0) ? base.padding.left : base.padding.top);
			float num14 = 0f;
			float num15 = num - base.GetTotalPreferredSize(axis);
			if (num15 > 0f)
			{
				if (base.GetTotalFlexibleSize(axis) == 0f)
				{
					num13 = base.GetStartOffset(axis, base.GetTotalPreferredSize(axis) - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical));
				}
				else if (base.GetTotalFlexibleSize(axis) > 0f)
				{
					num14 = num15 / base.GetTotalFlexibleSize(axis);
				}
			}
			float num16 = 0f;
			if (base.GetTotalMinSize(axis) != base.GetTotalPreferredSize(axis))
			{
				num16 = Mathf.Clamp01((num - base.GetTotalMinSize(axis)) / (base.GetTotalPreferredSize(axis) - base.GetTotalMinSize(axis)));
			}
			int num17 = num2;
			while (this.m_ReverseArrangement ? (num17 >= num3) : (num17 < num3))
			{
				RectTransform rectTransform2 = base.rectChildren[num17];
				float num18;
				float num19;
				float num20;
				this.GetChildSizes(rectTransform2, axis, flag, flag3, out num18, out num19, out num20);
				float num21 = (flag2 ? rectTransform2.localScale[axis] : 1f);
				float num22 = Mathf.Lerp(num18, num19, num16);
				num22 += num20 * num14;
				if (flag)
				{
					base.SetChildAlongAxisWithScale(rectTransform2, axis, num13, num22, num21);
				}
				else
				{
					float num23 = (num22 - rectTransform2.sizeDelta[axis]) * alignmentOnAxis;
					base.SetChildAlongAxisWithScale(rectTransform2, axis, num13 + num23, num21);
				}
				num13 += num22 * num21 + this.spacing;
				num17 += num4;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		private void GetChildSizes(RectTransform child, int axis, bool controlSize, bool childForceExpand, out float min, out float preferred, out float flexible)
		{
			if (!controlSize)
			{
				min = child.sizeDelta[axis];
				preferred = min;
				flexible = 0f;
			}
			else
			{
				min = LayoutUtility.GetMinSize(child, axis);
				preferred = LayoutUtility.GetPreferredSize(child, axis);
				flexible = LayoutUtility.GetFlexibleSize(child, axis);
			}
			if (childForceExpand)
			{
				flexible = Mathf.Max(flexible, 1f);
			}
		}

		// Token: 0x040000DB RID: 219
		[SerializeField]
		protected float m_Spacing;

		// Token: 0x040000DC RID: 220
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x040000DD RID: 221
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		protected bool m_ChildControlWidth = true;

		// Token: 0x040000DF RID: 223
		[SerializeField]
		protected bool m_ChildControlHeight = true;

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		protected bool m_ChildScaleWidth;

		// Token: 0x040000E1 RID: 225
		[SerializeField]
		protected bool m_ChildScaleHeight;

		// Token: 0x040000E2 RID: 226
		[SerializeField]
		protected bool m_ReverseArrangement;
	}
}
