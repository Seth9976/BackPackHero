using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000193 RID: 403
	[NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h")]
	[RequiredByNativeCode]
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EC9 RID: 3785
		// (set) Token: 0x06000ECA RID: 3786
		public extern SkinQuality quality
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000ECB RID: 3787
		// (set) Token: 0x06000ECC RID: 3788
		public extern bool updateWhenOffscreen
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000ECD RID: 3789
		// (set) Token: 0x06000ECE RID: 3790
		public extern bool forceMatrixRecalculationPerRender
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000ECF RID: 3791
		// (set) Token: 0x06000ED0 RID: 3792
		public extern Transform rootBone
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000ED1 RID: 3793
		// (set) Token: 0x06000ED2 RID: 3794
		public extern Transform[] bones
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000ED3 RID: 3795
		// (set) Token: 0x06000ED4 RID: 3796
		[NativeProperty("Mesh")]
		public extern Mesh sharedMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000ED5 RID: 3797
		// (set) Token: 0x06000ED6 RID: 3798
		[NativeProperty("SkinnedMeshMotionVectors")]
		public extern bool skinnedMotionVectors
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000ED7 RID: 3799
		[MethodImpl(4096)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x06000ED8 RID: 3800
		[MethodImpl(4096)]
		public extern void SetBlendShapeWeight(int index, float value);

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00012C83 File Offset: 0x00010E83
		public void BakeMesh(Mesh mesh)
		{
			this.BakeMesh(mesh, false);
		}

		// Token: 0x06000EDA RID: 3802
		[MethodImpl(4096)]
		public extern void BakeMesh([NotNull("NullExceptionObject")] Mesh mesh, bool useScale);

		// Token: 0x06000EDB RID: 3803 RVA: 0x00012C90 File Offset: 0x00010E90
		public GraphicsBuffer GetVertexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetVertexBufferImpl();
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00012CBC File Offset: 0x00010EBC
		public GraphicsBuffer GetPreviousVertexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetPreviousVertexBufferImpl();
		}

		// Token: 0x06000EDD RID: 3805
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetVertexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern GraphicsBuffer GetVertexBufferImpl();

		// Token: 0x06000EDE RID: 3806
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetPreviousVertexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern GraphicsBuffer GetPreviousVertexBufferImpl();

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000EDF RID: 3807
		// (set) Token: 0x06000EE0 RID: 3808
		public extern GraphicsBuffer.Target vertexBufferTarget
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
