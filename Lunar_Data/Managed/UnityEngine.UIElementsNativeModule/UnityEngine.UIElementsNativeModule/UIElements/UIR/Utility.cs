using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/UIElementsNative/UIRendererUtility.h")]
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	internal class Utility
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00003F0C File Offset: 0x0000210C
		public static void SetVectorArray<T>(MaterialPropertyBlock props, int name, NativeSlice<T> vector4s) where T : struct
		{
			int num = vector4s.Length * vector4s.Stride / 16;
			Utility.SetVectorArray(props, name, new IntPtr(vector4s.GetUnsafePtr<T>()), num);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000175 RID: 373 RVA: 0x00003F44 File Offset: 0x00002144
		// (remove) Token: 0x06000176 RID: 374 RVA: 0x00003F78 File Offset: 0x00002178
		[field: DebuggerBrowsable(0)]
		public static event Action<bool> GraphicsResourcesRecreate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000177 RID: 375 RVA: 0x00003FAC File Offset: 0x000021AC
		// (remove) Token: 0x06000178 RID: 376 RVA: 0x00003FE0 File Offset: 0x000021E0
		[field: DebuggerBrowsable(0)]
		public static event Action EngineUpdate;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000179 RID: 377 RVA: 0x00004014 File Offset: 0x00002214
		// (remove) Token: 0x0600017A RID: 378 RVA: 0x00004048 File Offset: 0x00002248
		[field: DebuggerBrowsable(0)]
		public static event Action FlushPendingResources;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600017B RID: 379 RVA: 0x0000407C File Offset: 0x0000227C
		// (remove) Token: 0x0600017C RID: 380 RVA: 0x000040B0 File Offset: 0x000022B0
		[field: DebuggerBrowsable(0)]
		public static event Action<Camera> RegisterIntermediateRenderers;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600017D RID: 381 RVA: 0x000040E4 File Offset: 0x000022E4
		// (remove) Token: 0x0600017E RID: 382 RVA: 0x00004118 File Offset: 0x00002318
		[field: DebuggerBrowsable(0)]
		public static event Action<IntPtr> RenderNodeAdd;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600017F RID: 383 RVA: 0x0000414C File Offset: 0x0000234C
		// (remove) Token: 0x06000180 RID: 384 RVA: 0x00004180 File Offset: 0x00002380
		[field: DebuggerBrowsable(0)]
		public static event Action<IntPtr> RenderNodeExecute;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000181 RID: 385 RVA: 0x000041B4 File Offset: 0x000023B4
		// (remove) Token: 0x06000182 RID: 386 RVA: 0x000041E8 File Offset: 0x000023E8
		[field: DebuggerBrowsable(0)]
		public static event Action<IntPtr> RenderNodeCleanup;

		// Token: 0x06000183 RID: 387 RVA: 0x0000421B File Offset: 0x0000241B
		[RequiredByNativeCode]
		internal static void RaiseGraphicsResourcesRecreate(bool recreate)
		{
			Action<bool> graphicsResourcesRecreate = Utility.GraphicsResourcesRecreate;
			if (graphicsResourcesRecreate != null)
			{
				graphicsResourcesRecreate.Invoke(recreate);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004230 File Offset: 0x00002430
		[RequiredByNativeCode]
		internal static void RaiseEngineUpdate()
		{
			bool flag = Utility.EngineUpdate != null;
			if (flag)
			{
				Utility.s_MarkerRaiseEngineUpdate.Begin();
				Utility.EngineUpdate.Invoke();
				Utility.s_MarkerRaiseEngineUpdate.End();
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000426D File Offset: 0x0000246D
		[RequiredByNativeCode]
		internal static void RaiseFlushPendingResources()
		{
			Action flushPendingResources = Utility.FlushPendingResources;
			if (flushPendingResources != null)
			{
				flushPendingResources.Invoke();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004281 File Offset: 0x00002481
		[RequiredByNativeCode]
		internal static void RaiseRegisterIntermediateRenderers(Camera camera)
		{
			Action<Camera> registerIntermediateRenderers = Utility.RegisterIntermediateRenderers;
			if (registerIntermediateRenderers != null)
			{
				registerIntermediateRenderers.Invoke(camera);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004296 File Offset: 0x00002496
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeAdd(IntPtr userData)
		{
			Action<IntPtr> renderNodeAdd = Utility.RenderNodeAdd;
			if (renderNodeAdd != null)
			{
				renderNodeAdd.Invoke(userData);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000042AB File Offset: 0x000024AB
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeExecute(IntPtr userData)
		{
			Action<IntPtr> renderNodeExecute = Utility.RenderNodeExecute;
			if (renderNodeExecute != null)
			{
				renderNodeExecute.Invoke(userData);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000042C0 File Offset: 0x000024C0
		[RequiredByNativeCode]
		internal static void RaiseRenderNodeCleanup(IntPtr userData)
		{
			Action<IntPtr> renderNodeCleanup = Utility.RenderNodeCleanup;
			if (renderNodeCleanup != null)
			{
				renderNodeCleanup.Invoke(userData);
			}
		}

		// Token: 0x0600018A RID: 394
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);

		// Token: 0x0600018B RID: 395
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void FreeBuffer(IntPtr buffer);

		// Token: 0x0600018C RID: 396
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);

		// Token: 0x0600018D RID: 397
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void SetVectorArray(MaterialPropertyBlock props, int name, IntPtr vector4s, int count);

		// Token: 0x0600018E RID: 398
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes);

		// Token: 0x0600018F RID: 399 RVA: 0x000042D8 File Offset: 0x000024D8
		public static void RegisterIntermediateRenderer(Camera camera, Material material, Matrix4x4 transform, Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize)
		{
			Utility.RegisterIntermediateRenderer_Injected(camera, material, ref transform, ref aabb, renderLayer, shadowCasting, receiveShadows, sameDistanceSortPriority, sceneCullingMask, rendererCallbackFlags, userData, userDataSize);
		}

		// Token: 0x06000190 RID: 400
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);

		// Token: 0x06000191 RID: 401
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SetPropertyBlock(MaterialPropertyBlock props);

		// Token: 0x06000192 RID: 402 RVA: 0x00004300 File Offset: 0x00002500
		[ThreadSafe]
		public static void SetScissorRect(RectInt scissorRect)
		{
			Utility.SetScissorRect_Injected(ref scissorRect);
		}

		// Token: 0x06000193 RID: 403
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void DisableScissor();

		// Token: 0x06000194 RID: 404
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsScissorEnabled();

		// Token: 0x06000195 RID: 405 RVA: 0x00004309 File Offset: 0x00002509
		[ThreadSafe]
		public static IntPtr CreateStencilState(StencilState stencilState)
		{
			return Utility.CreateStencilState_Injected(ref stencilState);
		}

		// Token: 0x06000196 RID: 406
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SetStencilState(IntPtr stencilState, int stencilRef);

		// Token: 0x06000197 RID: 407
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool HasMappedBufferRange();

		// Token: 0x06000198 RID: 408
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern uint InsertCPUFence();

		// Token: 0x06000199 RID: 409
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool CPUFencePassed(uint fence);

		// Token: 0x0600019A RID: 410
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void WaitForCPUFencePassed(uint fence);

		// Token: 0x0600019B RID: 411
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SyncRenderThread();

		// Token: 0x0600019C RID: 412 RVA: 0x00004314 File Offset: 0x00002514
		[ThreadSafe]
		public static RectInt GetActiveViewport()
		{
			RectInt rectInt;
			Utility.GetActiveViewport_Injected(out rectInt);
			return rectInt;
		}

		// Token: 0x0600019D RID: 413
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void ProfileDrawChainBegin();

		// Token: 0x0600019E RID: 414
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void ProfileDrawChainEnd();

		// Token: 0x0600019F RID: 415
		[MethodImpl(4096)]
		public static extern void NotifyOfUIREvents(bool subscribe);

		// Token: 0x060001A0 RID: 416 RVA: 0x0000432C File Offset: 0x0000252C
		[ThreadSafe]
		public static Matrix4x4 GetUnityProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetUnityProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004344 File Offset: 0x00002544
		[ThreadSafe]
		public static Matrix4x4 GetDeviceProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetDeviceProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x060001A2 RID: 418
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool DebugIsMainThread();

		// Token: 0x060001A5 RID: 421
		[MethodImpl(4096)]
		private static extern void RegisterIntermediateRenderer_Injected(Camera camera, Material material, ref Matrix4x4 transform, ref Bounds aabb, int renderLayer, int shadowCasting, bool receiveShadows, int sameDistanceSortPriority, ulong sceneCullingMask, int rendererCallbackFlags, IntPtr userData, int userDataSize);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(4096)]
		private static extern void SetScissorRect_Injected(ref RectInt scissorRect);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(4096)]
		private static extern IntPtr CreateStencilState_Injected(ref StencilState stencilState);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(4096)]
		private static extern void GetActiveViewport_Injected(out RectInt ret);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(4096)]
		private static extern void GetUnityProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060001AA RID: 426
		[MethodImpl(4096)]
		private static extern void GetDeviceProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x0400007A RID: 122
		private static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

		// Token: 0x02000029 RID: 41
		[Flags]
		internal enum RendererCallbacks
		{
			// Token: 0x0400007C RID: 124
			RendererCallback_Init = 1,
			// Token: 0x0400007D RID: 125
			RendererCallback_Exec = 2,
			// Token: 0x0400007E RID: 126
			RendererCallback_Cleanup = 4
		}

		// Token: 0x0200002A RID: 42
		internal enum GPUBufferType
		{
			// Token: 0x04000080 RID: 128
			Vertex,
			// Token: 0x04000081 RID: 129
			Index
		}

		// Token: 0x0200002B RID: 43
		public class GPUBuffer<T> : IDisposable where T : struct
		{
			// Token: 0x060001AB RID: 427 RVA: 0x0000436A File Offset: 0x0000256A
			public GPUBuffer(int elementCount, Utility.GPUBufferType type)
			{
				this.elemCount = elementCount;
				this.elemStride = UnsafeUtility.SizeOf<T>();
				this.buffer = Utility.AllocateBuffer(elementCount, this.elemStride, type == Utility.GPUBufferType.Vertex);
			}

			// Token: 0x060001AC RID: 428 RVA: 0x0000439C File Offset: 0x0000259C
			public void Dispose()
			{
				Utility.FreeBuffer(this.buffer);
			}

			// Token: 0x060001AD RID: 429 RVA: 0x000043AB File Offset: 0x000025AB
			public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
			{
				Utility.UpdateBufferRanges(this.buffer, new IntPtr(ranges.GetUnsafePtr<GfxUpdateBufferRange>()), ranges.Length, rangesMin, rangesMax);
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060001AE RID: 430 RVA: 0x000043D0 File Offset: 0x000025D0
			public int ElementStride
			{
				get
				{
					return this.elemStride;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001AF RID: 431 RVA: 0x000043E8 File Offset: 0x000025E8
			public int Count
			{
				get
				{
					return this.elemCount;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004400 File Offset: 0x00002600
			internal IntPtr BufferPointer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x04000082 RID: 130
			private IntPtr buffer;

			// Token: 0x04000083 RID: 131
			private int elemCount;

			// Token: 0x04000084 RID: 132
			private int elemStride;
		}
	}
}
