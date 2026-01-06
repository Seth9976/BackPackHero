using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000145 RID: 325
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/LineRenderer.h")]
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0000F463 File Offset: 0x0000D663
		[Obsolete("Use startWidth, endWidth or widthCurve instead.", false)]
		public void SetWidth(float start, float end)
		{
			this.startWidth = start;
			this.endWidth = end;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000F476 File Offset: 0x0000D676
		[Obsolete("Use startColor, endColor or colorGradient instead.", false)]
		public void SetColors(Color start, Color end)
		{
			this.startColor = start;
			this.endColor = end;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000F489 File Offset: 0x0000D689
		[Obsolete("Use positionCount instead.", false)]
		public void SetVertexCount(int count)
		{
			this.positionCount = count;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0000F494 File Offset: 0x0000D694
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0000F489 File Offset: 0x0000D689
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
			set
			{
				this.positionCount = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000ABD RID: 2749
		// (set) Token: 0x06000ABE RID: 2750
		public extern float startWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000ABF RID: 2751
		// (set) Token: 0x06000AC0 RID: 2752
		public extern float endWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000AC1 RID: 2753
		// (set) Token: 0x06000AC2 RID: 2754
		public extern float widthMultiplier
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000AC3 RID: 2755
		// (set) Token: 0x06000AC4 RID: 2756
		public extern int numCornerVertices
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000AC5 RID: 2757
		// (set) Token: 0x06000AC6 RID: 2758
		public extern int numCapVertices
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000AC7 RID: 2759
		// (set) Token: 0x06000AC8 RID: 2760
		public extern bool useWorldSpace
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000AC9 RID: 2761
		// (set) Token: 0x06000ACA RID: 2762
		public extern bool loop
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
		public Color startColor
		{
			get
			{
				Color color;
				this.get_startColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_startColor_Injected(ref value);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		public Color endColor
		{
			get
			{
				Color color;
				this.get_endColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_endColor_Injected(ref value);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000ACF RID: 2767
		// (set) Token: 0x06000AD0 RID: 2768
		[NativeProperty("PositionsCount")]
		public extern int positionCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		public void SetPosition(int index, Vector3 position)
		{
			this.SetPosition_Injected(index, ref position);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		public Vector3 GetPosition(int index)
		{
			Vector3 vector;
			this.GetPosition_Injected(index, out vector);
			return vector;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000AD3 RID: 2771
		// (set) Token: 0x06000AD4 RID: 2772
		public extern float shadowBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AD5 RID: 2773
		// (set) Token: 0x06000AD6 RID: 2774
		public extern bool generateLightingData
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000AD7 RID: 2775
		// (set) Token: 0x06000AD8 RID: 2776
		public extern LineTextureMode textureMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000AD9 RID: 2777
		// (set) Token: 0x06000ADA RID: 2778
		public extern LineAlignment alignment
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000ADB RID: 2779
		[MethodImpl(4096)]
		public extern void Simplify(float tolerance);

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000F50F File Offset: 0x0000D70F
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x06000ADD RID: 2781
		[MethodImpl(4096)]
		public extern void BakeMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, bool useTransform = false);

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0000F520 File Offset: 0x0000D720
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0000F538 File Offset: 0x0000D738
		public AnimationCurve widthCurve
		{
			get
			{
				return this.GetWidthCurveCopy();
			}
			set
			{
				this.SetWidthCurve(value);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0000F544 File Offset: 0x0000D744
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0000F55C File Offset: 0x0000D75C
		public Gradient colorGradient
		{
			get
			{
				return this.GetColorGradientCopy();
			}
			set
			{
				this.SetColorGradient(value);
			}
		}

		// Token: 0x06000AE2 RID: 2786
		[MethodImpl(4096)]
		private extern AnimationCurve GetWidthCurveCopy();

		// Token: 0x06000AE3 RID: 2787
		[MethodImpl(4096)]
		private extern void SetWidthCurve([NotNull("ArgumentNullException")] AnimationCurve curve);

		// Token: 0x06000AE4 RID: 2788
		[MethodImpl(4096)]
		private extern Gradient GetColorGradientCopy();

		// Token: 0x06000AE5 RID: 2789
		[MethodImpl(4096)]
		private extern void SetColorGradient([NotNull("ArgumentNullException")] Gradient curve);

		// Token: 0x06000AE6 RID: 2790
		[FreeFunction(Name = "LineRendererScripting::GetPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetPositions([NotNull("ArgumentNullException")] [Out] Vector3[] positions);

		// Token: 0x06000AE7 RID: 2791
		[FreeFunction(Name = "LineRendererScripting::SetPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetPositions([NotNull("ArgumentNullException")] Vector3[] positions);

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000F567 File Offset: 0x0000D767
		public void SetPositions(NativeArray<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000F585 File Offset: 0x0000D785
		public void SetPositions(NativeSlice<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public int GetPositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		public int GetPositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000AEC RID: 2796
		[FreeFunction(Name = "LineRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetPositionsWithNativeContainer(IntPtr positions, int count);

		// Token: 0x06000AED RID: 2797
		[FreeFunction(Name = "LineRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetPositionsWithNativeContainer(IntPtr positions, int length);

		// Token: 0x06000AEF RID: 2799
		[MethodImpl(4096)]
		private extern void get_startColor_Injected(out Color ret);

		// Token: 0x06000AF0 RID: 2800
		[MethodImpl(4096)]
		private extern void set_startColor_Injected(ref Color value);

		// Token: 0x06000AF1 RID: 2801
		[MethodImpl(4096)]
		private extern void get_endColor_Injected(out Color ret);

		// Token: 0x06000AF2 RID: 2802
		[MethodImpl(4096)]
		private extern void set_endColor_Injected(ref Color value);

		// Token: 0x06000AF3 RID: 2803
		[MethodImpl(4096)]
		private extern void SetPosition_Injected(int index, ref Vector3 position);

		// Token: 0x06000AF4 RID: 2804
		[MethodImpl(4096)]
		private extern void GetPosition_Injected(int index, out Vector3 ret);
	}
}
