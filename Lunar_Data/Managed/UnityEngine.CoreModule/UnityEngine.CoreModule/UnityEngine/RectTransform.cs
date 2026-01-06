using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000259 RID: 601
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	[NativeClass("UI::RectTransform")]
	public sealed class RectTransform : Transform
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060019F8 RID: 6648 RVA: 0x00029F34 File Offset: 0x00028134
		// (remove) Token: 0x060019F9 RID: 6649 RVA: 0x00029F68 File Offset: 0x00028168
		[field: DebuggerBrowsable(0)]
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00029F9C File Offset: 0x0002819C
		public Rect rect
		{
			get
			{
				Rect rect;
				this.get_rect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00029FB4 File Offset: 0x000281B4
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x00029FCA File Offset: 0x000281CA
		public Vector2 anchorMin
		{
			get
			{
				Vector2 vector;
				this.get_anchorMin_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchorMin_Injected(ref value);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00029FD4 File Offset: 0x000281D4
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x00029FEA File Offset: 0x000281EA
		public Vector2 anchorMax
		{
			get
			{
				Vector2 vector;
				this.get_anchorMax_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchorMax_Injected(ref value);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00029FF4 File Offset: 0x000281F4
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0002A00A File Offset: 0x0002820A
		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 vector;
				this.get_anchoredPosition_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchoredPosition_Injected(ref value);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0002A014 File Offset: 0x00028214
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0002A02A File Offset: 0x0002822A
		public Vector2 sizeDelta
		{
			get
			{
				Vector2 vector;
				this.get_sizeDelta_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_sizeDelta_Injected(ref value);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0002A034 File Offset: 0x00028234
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0002A04A File Offset: 0x0002824A
		public Vector2 pivot
		{
			get
			{
				Vector2 vector;
				this.get_pivot_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_pivot_Injected(ref value);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0002A054 File Offset: 0x00028254
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x0002A08C File Offset: 0x0002828C
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0002A0D0 File Offset: 0x000282D0
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x0002A100 File Offset: 0x00028300
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0002A16C File Offset: 0x0002836C
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x0002A1A4 File Offset: 0x000283A4
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001A0B RID: 6667
		// (set) Token: 0x06001A0C RID: 6668
		public extern Object drivenByObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			internal set;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001A0D RID: 6669
		// (set) Token: 0x06001A0E RID: 6670
		internal extern DrivenTransformProperties drivenProperties
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001A0F RID: 6671
		[NativeMethod("UpdateIfTransformDispatchIsDirty")]
		[MethodImpl(4096)]
		public extern void ForceUpdateRectTransforms();

		// Token: 0x06001A10 RID: 6672 RVA: 0x0002A210 File Offset: 0x00028410
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				Rect rect = this.rect;
				float x = rect.x;
				float y = rect.y;
				float xMax = rect.xMax;
				float yMax = rect.yMax;
				fourCornersArray[0] = new Vector3(x, y, 0f);
				fourCornersArray[1] = new Vector3(x, yMax, 0f);
				fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
				fourCornersArray[3] = new Vector3(xMax, y, 0f);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0002A2B4 File Offset: 0x000284B4
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				this.GetLocalCorners(fourCornersArray);
				Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
				for (int i = 0; i < 4; i++)
				{
					fourCornersArray[i] = localToWorldMatrix.MultiplyPoint(fourCornersArray[i]);
				}
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0002A31C File Offset: 0x0002851C
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int num = ((edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Bottom) ? 1 : 0);
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float num2 = (float)(flag ? 1 : 0);
			Vector2 vector = this.anchorMin;
			vector[num] = num2;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[num] = num2;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[num] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[num] = (flag ? (-inset - size * (1f - this.pivot[num])) : (inset + size * this.pivot[num]));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0002A3E8 File Offset: 0x000285E8
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0002A441 File Offset: 0x00028641
		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
			if (reapplyDrivenProperties != null)
			{
				reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0002A458 File Offset: 0x00028658
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 vector = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			bool flag = base.transform.parent;
			if (flag)
			{
				RectTransform component = base.transform.parent.GetComponent<RectTransform>();
				bool flag2 = component;
				if (flag2)
				{
					vector += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += vector.x;
			rect.y += vector.y;
			return rect;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0002A510 File Offset: 0x00028710
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			bool flag = !rectTransform;
			Vector2 vector;
			if (flag)
			{
				vector = Vector2.zero;
			}
			else
			{
				vector = rectTransform.rect.size;
			}
			return vector;
		}

		// Token: 0x06001A18 RID: 6680
		[MethodImpl(4096)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x06001A19 RID: 6681
		[MethodImpl(4096)]
		private extern void get_anchorMin_Injected(out Vector2 ret);

		// Token: 0x06001A1A RID: 6682
		[MethodImpl(4096)]
		private extern void set_anchorMin_Injected(ref Vector2 value);

		// Token: 0x06001A1B RID: 6683
		[MethodImpl(4096)]
		private extern void get_anchorMax_Injected(out Vector2 ret);

		// Token: 0x06001A1C RID: 6684
		[MethodImpl(4096)]
		private extern void set_anchorMax_Injected(ref Vector2 value);

		// Token: 0x06001A1D RID: 6685
		[MethodImpl(4096)]
		private extern void get_anchoredPosition_Injected(out Vector2 ret);

		// Token: 0x06001A1E RID: 6686
		[MethodImpl(4096)]
		private extern void set_anchoredPosition_Injected(ref Vector2 value);

		// Token: 0x06001A1F RID: 6687
		[MethodImpl(4096)]
		private extern void get_sizeDelta_Injected(out Vector2 ret);

		// Token: 0x06001A20 RID: 6688
		[MethodImpl(4096)]
		private extern void set_sizeDelta_Injected(ref Vector2 value);

		// Token: 0x06001A21 RID: 6689
		[MethodImpl(4096)]
		private extern void get_pivot_Injected(out Vector2 ret);

		// Token: 0x06001A22 RID: 6690
		[MethodImpl(4096)]
		private extern void set_pivot_Injected(ref Vector2 value);

		// Token: 0x0200025A RID: 602
		public enum Edge
		{
			// Token: 0x040008AB RID: 2219
			Left,
			// Token: 0x040008AC RID: 2220
			Right,
			// Token: 0x040008AD RID: 2221
			Top,
			// Token: 0x040008AE RID: 2222
			Bottom
		}

		// Token: 0x0200025B RID: 603
		public enum Axis
		{
			// Token: 0x040008B0 RID: 2224
			Horizontal,
			// Token: 0x040008B1 RID: 2225
			Vertical
		}

		// Token: 0x0200025C RID: 604
		// (Invoke) Token: 0x06001A24 RID: 6692
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
