using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040C RID: 1036
	[NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderContext.bindings.h")]
	[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
	[NativeHeader("Modules/UI/CanvasManager.h")]
	public struct ScriptableRenderContext : IEquatable<ScriptableRenderContext>
	{
		// Token: 0x06002366 RID: 9062
		[FreeFunction("ScriptableRenderContext::BeginRenderPass")]
		[MethodImpl(4096)]
		private static extern void BeginRenderPass_Internal(IntPtr self, int width, int height, int samples, IntPtr colors, int colorCount, int depthAttachmentIndex);

		// Token: 0x06002367 RID: 9063
		[FreeFunction("ScriptableRenderContext::BeginSubPass")]
		[MethodImpl(4096)]
		private static extern void BeginSubPass_Internal(IntPtr self, IntPtr colors, int colorCount, IntPtr inputs, int inputCount, bool isDepthReadOnly, bool isStencilReadOnly);

		// Token: 0x06002368 RID: 9064
		[FreeFunction("ScriptableRenderContext::EndSubPass")]
		[MethodImpl(4096)]
		private static extern void EndSubPass_Internal(IntPtr self);

		// Token: 0x06002369 RID: 9065
		[FreeFunction("ScriptableRenderContext::EndRenderPass")]
		[MethodImpl(4096)]
		private static extern void EndRenderPass_Internal(IntPtr self);

		// Token: 0x0600236A RID: 9066 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		[FreeFunction("ScriptableRenderPipeline_Bindings::Internal_Cull")]
		private static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, IntPtr results)
		{
			ScriptableRenderContext.Internal_Cull_Injected(ref parameters, ref renderLoop, results);
		}

		// Token: 0x0600236B RID: 9067
		[FreeFunction("InitializeSortSettings")]
		[MethodImpl(4096)]
		internal static extern void InitializeSortSettings(Camera camera, out SortingSettings sortingSettings);

		// Token: 0x0600236C RID: 9068 RVA: 0x0003BBE3 File Offset: 0x00039DE3
		private void Submit_Internal()
		{
			ScriptableRenderContext.Submit_Internal_Injected(ref this);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0003BBEB File Offset: 0x00039DEB
		private bool SubmitForRenderPassValidation_Internal()
		{
			return ScriptableRenderContext.SubmitForRenderPassValidation_Internal_Injected(ref this);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0003BBF3 File Offset: 0x00039DF3
		private void GetCameras_Internal(Type listType, object resultList)
		{
			ScriptableRenderContext.GetCameras_Internal_Injected(ref this, listType, resultList);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0003BC00 File Offset: 0x00039E00
		private void DrawRenderers_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			ScriptableRenderContext.DrawRenderers_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0003BC20 File Offset: 0x00039E20
		private void DrawShadows_Internal(IntPtr shadowDrawingSettings)
		{
			ScriptableRenderContext.DrawShadows_Internal_Injected(ref this, shadowDrawingSettings);
		}

		// Token: 0x06002371 RID: 9073
		[FreeFunction("PlayerEmitCanvasGeometryForCamera")]
		[MethodImpl(4096)]
		public static extern void EmitGeometryForCamera(Camera camera);

		// Token: 0x06002372 RID: 9074 RVA: 0x0003BC29 File Offset: 0x00039E29
		[NativeThrows]
		private void ExecuteCommandBuffer_Internal(CommandBuffer commandBuffer)
		{
			ScriptableRenderContext.ExecuteCommandBuffer_Internal_Injected(ref this, commandBuffer);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0003BC32 File Offset: 0x00039E32
		[NativeThrows]
		private void ExecuteCommandBufferAsync_Internal(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			ScriptableRenderContext.ExecuteCommandBufferAsync_Internal_Injected(ref this, commandBuffer, queueType);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0003BC3C File Offset: 0x00039E3C
		private void SetupCameraProperties_Internal([NotNull("NullExceptionObject")] Camera camera, bool stereoSetup, int eye)
		{
			ScriptableRenderContext.SetupCameraProperties_Internal_Injected(ref this, camera, stereoSetup, eye);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0003BC47 File Offset: 0x00039E47
		private void StereoEndRender_Internal([NotNull("NullExceptionObject")] Camera camera, int eye, bool isFinalPass)
		{
			ScriptableRenderContext.StereoEndRender_Internal_Injected(ref this, camera, eye, isFinalPass);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0003BC52 File Offset: 0x00039E52
		private void StartMultiEye_Internal([NotNull("NullExceptionObject")] Camera camera, int eye)
		{
			ScriptableRenderContext.StartMultiEye_Internal_Injected(ref this, camera, eye);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0003BC5C File Offset: 0x00039E5C
		private void StopMultiEye_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.StopMultiEye_Internal_Injected(ref this, camera);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0003BC65 File Offset: 0x00039E65
		private void DrawSkybox_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawSkybox_Internal_Injected(ref this, camera);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0003BC6E File Offset: 0x00039E6E
		private void InvokeOnRenderObjectCallback_Internal()
		{
			ScriptableRenderContext.InvokeOnRenderObjectCallback_Internal_Injected(ref this);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0003BC76 File Offset: 0x00039E76
		private void DrawGizmos_Internal([NotNull("NullExceptionObject")] Camera camera, GizmoSubset gizmoSubset)
		{
			ScriptableRenderContext.DrawGizmos_Internal_Injected(ref this, camera, gizmoSubset);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0003BC80 File Offset: 0x00039E80
		private void DrawWireOverlay_Impl([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawWireOverlay_Impl_Injected(ref this, camera);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0003BC89 File Offset: 0x00039E89
		private void DrawUIOverlay_Internal([NotNull("NullExceptionObject")] Camera camera)
		{
			ScriptableRenderContext.DrawUIOverlay_Internal_Injected(ref this, camera);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0003BC94 File Offset: 0x00039E94
		internal IntPtr Internal_GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0003BCAC File Offset: 0x00039EAC
		private RendererList CreateRendererList_Internal(IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount)
		{
			RendererList rendererList;
			ScriptableRenderContext.CreateRendererList_Internal_Injected(ref this, cullResults, ref drawingSettings, ref filteringSettings, ref tagName, isPassTagName, tagValues, stateBlocks, stateCount, out rendererList);
			return rendererList;
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0003BCCF File Offset: 0x00039ECF
		private void PrepareRendererListsAsync_Internal(object rendererLists)
		{
			ScriptableRenderContext.PrepareRendererListsAsync_Internal_Injected(ref this, rendererLists);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		private RendererListStatus QueryRendererListStatus_Internal(RendererList handle)
		{
			return ScriptableRenderContext.QueryRendererListStatus_Internal_Injected(ref this, ref handle);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0003BCE2 File Offset: 0x00039EE2
		internal ScriptableRenderContext(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0003BCEC File Offset: 0x00039EEC
		public void BeginRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			ScriptableRenderContext.BeginRenderPass_Internal(this.m_Ptr, width, height, samples, (IntPtr)attachments.GetUnsafeReadOnlyPtr<AttachmentDescriptor>(), attachments.Length, depthAttachmentIndex);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0003BD14 File Offset: 0x00039F14
		public ScopedRenderPass BeginScopedRenderPass(int width, int height, int samples, NativeArray<AttachmentDescriptor> attachments, int depthAttachmentIndex = -1)
		{
			this.BeginRenderPass(width, height, samples, attachments, depthAttachmentIndex);
			return new ScopedRenderPass(this);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0003BD3F File Offset: 0x00039F3F
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthReadOnly, isStencilReadOnly);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0003BD75 File Offset: 0x00039F75
		public void BeginSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, (IntPtr)inputs.GetUnsafeReadOnlyPtr<int>(), inputs.Length, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0003BDAA File Offset: 0x00039FAA
		public void BeginSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthReadOnly, isStencilReadOnly);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0003BDD3 File Offset: 0x00039FD3
		public void BeginSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			ScriptableRenderContext.BeginSubPass_Internal(this.m_Ptr, (IntPtr)colors.GetUnsafeReadOnlyPtr<int>(), colors.Length, IntPtr.Zero, 0, isDepthStencilReadOnly, isDepthStencilReadOnly);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0003BDFC File Offset: 0x00039FFC
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, inputs, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0003BE28 File Offset: 0x0003A028
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, NativeArray<int> inputs, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, inputs, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0003BE50 File Offset: 0x0003A050
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthReadOnly, bool isStencilReadOnly)
		{
			this.BeginSubPass(colors, isDepthReadOnly, isStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0003BE78 File Offset: 0x0003A078
		public ScopedSubPass BeginScopedSubPass(NativeArray<int> colors, bool isDepthStencilReadOnly = false)
		{
			this.BeginSubPass(colors, isDepthStencilReadOnly);
			return new ScopedSubPass(this);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0003BE9E File Offset: 0x0003A09E
		public void EndSubPass()
		{
			ScriptableRenderContext.EndSubPass_Internal(this.m_Ptr);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0003BEAD File Offset: 0x0003A0AD
		public void EndRenderPass()
		{
			ScriptableRenderContext.EndRenderPass_Internal(this.m_Ptr);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		public void Submit()
		{
			this.Submit_Internal();
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0003BEC8 File Offset: 0x0003A0C8
		public bool SubmitForRenderPassValidation()
		{
			return this.SubmitForRenderPassValidation_Internal();
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0003BEE0 File Offset: 0x0003A0E0
		internal void GetCameras(List<Camera> results)
		{
			this.GetCameras_Internal(typeof(Camera), results);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings)
		{
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, IntPtr.Zero, IntPtr.Zero, 0);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x0003BF28 File Offset: 0x0003A128
		public unsafe void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock stateBlock)
		{
			ShaderTagId shaderTagId = default(ShaderTagId);
			fixed (RenderStateBlock* ptr = &stateBlock)
			{
				RenderStateBlock* ptr2 = ptr;
				this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ShaderTagId.none, false, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)ptr2), 1);
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0003BF70 File Offset: 0x0003A170
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, NativeArray<ShaderTagId> renderTypes, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = renderTypes.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[] { "renderTypes", "stateBlocks", "renderTypes", renderTypes.Length, "stateBlocks", stateBlocks.Length }));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, ScriptableRenderContext.kRenderTypeTag, false, (IntPtr)renderTypes.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), renderTypes.Length);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0003C020 File Offset: 0x0003A220
		public void DrawRenderers(CullingResults cullingResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ShaderTagId tagName, bool isPassTagName, NativeArray<ShaderTagId> tagValues, NativeArray<RenderStateBlock> stateBlocks)
		{
			bool flag = tagValues.Length != stateBlocks.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[] { "tagValues", "stateBlocks", "tagValues", tagValues.Length, "stateBlocks", stateBlocks.Length }));
			}
			this.DrawRenderers_Internal(cullingResults.ptr, ref drawingSettings, ref filteringSettings, tagName, isPassTagName, (IntPtr)tagValues.GetUnsafeReadOnlyPtr<ShaderTagId>(), (IntPtr)stateBlocks.GetUnsafeReadOnlyPtr<RenderStateBlock>(), tagValues.Length);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0003C0CC File Offset: 0x0003A2CC
		public unsafe void DrawShadows(ref ShadowDrawingSettings settings)
		{
			fixed (ShadowDrawingSettings* ptr = &settings)
			{
				ShadowDrawingSettings* ptr2 = ptr;
				this.DrawShadows_Internal((IntPtr)((void*)ptr2));
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0003C0F4 File Offset: 0x0003A2F4
		public void ExecuteCommandBuffer(CommandBuffer commandBuffer)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBuffer_Internal(commandBuffer);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0003C13C File Offset: 0x0003A33C
		public void ExecuteCommandBufferAsync(CommandBuffer commandBuffer, ComputeQueueType queueType)
		{
			bool flag = commandBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("commandBuffer");
			}
			bool flag2 = commandBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("commandBuffer");
			}
			this.ExecuteCommandBufferAsync_Internal(commandBuffer, queueType);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0003C185 File Offset: 0x0003A385
		public void SetupCameraProperties(Camera camera, bool stereoSetup = false)
		{
			this.SetupCameraProperties(camera, stereoSetup, 0);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0003C192 File Offset: 0x0003A392
		public void SetupCameraProperties(Camera camera, bool stereoSetup, int eye)
		{
			this.SetupCameraProperties_Internal(camera, stereoSetup, eye);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0003C19F File Offset: 0x0003A39F
		public void StereoEndRender(Camera camera)
		{
			this.StereoEndRender(camera, 0, true);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		public void StereoEndRender(Camera camera, int eye)
		{
			this.StereoEndRender(camera, eye, true);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0003C1B9 File Offset: 0x0003A3B9
		public void StereoEndRender(Camera camera, int eye, bool isFinalPass)
		{
			this.StereoEndRender_Internal(camera, eye, isFinalPass);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0003C1C6 File Offset: 0x0003A3C6
		public void StartMultiEye(Camera camera)
		{
			this.StartMultiEye(camera, 0);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0003C1D2 File Offset: 0x0003A3D2
		public void StartMultiEye(Camera camera, int eye)
		{
			this.StartMultiEye_Internal(camera, eye);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0003C1DE File Offset: 0x0003A3DE
		public void StopMultiEye(Camera camera)
		{
			this.StopMultiEye_Internal(camera);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0003C1E9 File Offset: 0x0003A3E9
		public void DrawSkybox(Camera camera)
		{
			this.DrawSkybox_Internal(camera);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		public void InvokeOnRenderObjectCallback()
		{
			this.InvokeOnRenderObjectCallback_Internal();
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0003C1FE File Offset: 0x0003A3FE
		public void DrawGizmos(Camera camera, GizmoSubset gizmoSubset)
		{
			this.DrawGizmos_Internal(camera, gizmoSubset);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0003C20A File Offset: 0x0003A40A
		public void DrawWireOverlay(Camera camera)
		{
			this.DrawWireOverlay_Impl(camera);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0003C215 File Offset: 0x0003A415
		public void DrawUIOverlay(Camera camera)
		{
			this.DrawUIOverlay_Internal(camera);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0003C220 File Offset: 0x0003A420
		public unsafe CullingResults Cull(ref ScriptableCullingParameters parameters)
		{
			CullingResults cullingResults = default(CullingResults);
			ScriptableRenderContext.Internal_Cull(ref parameters, this, (IntPtr)((void*)(&cullingResults)));
			return cullingResults;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00004557 File Offset: 0x00002757
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal void Validate()
		{
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x0003C250 File Offset: 0x0003A450
		public bool Equals(ScriptableRenderContext other)
		{
			return this.m_Ptr.Equals(other.m_Ptr);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0003C278 File Offset: 0x0003A478
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableRenderContext && this.Equals((ScriptableRenderContext)obj);
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		public override int GetHashCode()
		{
			return this.m_Ptr.GetHashCode();
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0003C2D0 File Offset: 0x0003A4D0
		public static bool operator ==(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		public static bool operator !=(ScriptableRenderContext left, ScriptableRenderContext right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0003C30C File Offset: 0x0003A50C
		public unsafe RendererList CreateRendererList(RendererListDesc desc)
		{
			RendererListParams rendererListParams = RendererListParams.Create(in desc);
			bool flag = rendererListParams.stateBlock == null;
			RendererList rendererList;
			if (flag)
			{
				rendererList = this.CreateRendererList_Internal(rendererListParams.cullingResult.ptr, ref rendererListParams.drawSettings, ref rendererListParams.filteringSettings, ShaderTagId.none, false, IntPtr.Zero, IntPtr.Zero, 0);
			}
			else
			{
				ShaderTagId shaderTagId = default(ShaderTagId);
				RenderStateBlock value = rendererListParams.stateBlock.Value;
				RenderStateBlock* ptr = &value;
				rendererList = this.CreateRendererList_Internal(rendererListParams.cullingResult.ptr, ref rendererListParams.drawSettings, ref rendererListParams.filteringSettings, ShaderTagId.none, false, (IntPtr)((void*)(&shaderTagId)), (IntPtr)((void*)ptr), 1);
			}
			return rendererList;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0003C3BE File Offset: 0x0003A5BE
		public void PrepareRendererListsAsync(List<RendererList> rendererLists)
		{
			this.PrepareRendererListsAsync_Internal(rendererLists);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		public RendererListStatus QueryRendererListStatus(RendererList rendererList)
		{
			return this.QueryRendererListStatus_Internal(rendererList);
		}

		// Token: 0x060023B0 RID: 9136
		[MethodImpl(4096)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, ref ScriptableRenderContext renderLoop, IntPtr results);

		// Token: 0x060023B1 RID: 9137
		[MethodImpl(4096)]
		private static extern void Submit_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x060023B2 RID: 9138
		[MethodImpl(4096)]
		private static extern bool SubmitForRenderPassValidation_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x060023B3 RID: 9139
		[MethodImpl(4096)]
		private static extern void GetCameras_Internal_Injected(ref ScriptableRenderContext _unity_self, Type listType, object resultList);

		// Token: 0x060023B4 RID: 9140
		[MethodImpl(4096)]
		private static extern void DrawRenderers_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount);

		// Token: 0x060023B5 RID: 9141
		[MethodImpl(4096)]
		private static extern void DrawShadows_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr shadowDrawingSettings);

		// Token: 0x060023B6 RID: 9142
		[MethodImpl(4096)]
		private static extern void ExecuteCommandBuffer_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer);

		// Token: 0x060023B7 RID: 9143
		[MethodImpl(4096)]
		private static extern void ExecuteCommandBufferAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, CommandBuffer commandBuffer, ComputeQueueType queueType);

		// Token: 0x060023B8 RID: 9144
		[MethodImpl(4096)]
		private static extern void SetupCameraProperties_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, bool stereoSetup, int eye);

		// Token: 0x060023B9 RID: 9145
		[MethodImpl(4096)]
		private static extern void StereoEndRender_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye, bool isFinalPass);

		// Token: 0x060023BA RID: 9146
		[MethodImpl(4096)]
		private static extern void StartMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, int eye);

		// Token: 0x060023BB RID: 9147
		[MethodImpl(4096)]
		private static extern void StopMultiEye_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023BC RID: 9148
		[MethodImpl(4096)]
		private static extern void DrawSkybox_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023BD RID: 9149
		[MethodImpl(4096)]
		private static extern void InvokeOnRenderObjectCallback_Internal_Injected(ref ScriptableRenderContext _unity_self);

		// Token: 0x060023BE RID: 9150
		[MethodImpl(4096)]
		private static extern void DrawGizmos_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera, GizmoSubset gizmoSubset);

		// Token: 0x060023BF RID: 9151
		[MethodImpl(4096)]
		private static extern void DrawWireOverlay_Impl_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023C0 RID: 9152
		[MethodImpl(4096)]
		private static extern void DrawUIOverlay_Internal_Injected(ref ScriptableRenderContext _unity_self, Camera camera);

		// Token: 0x060023C1 RID: 9153
		[MethodImpl(4096)]
		private static extern void CreateRendererList_Internal_Injected(ref ScriptableRenderContext _unity_self, IntPtr cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref ShaderTagId tagName, bool isPassTagName, IntPtr tagValues, IntPtr stateBlocks, int stateCount, out RendererList ret);

		// Token: 0x060023C2 RID: 9154
		[MethodImpl(4096)]
		private static extern void PrepareRendererListsAsync_Internal_Injected(ref ScriptableRenderContext _unity_self, object rendererLists);

		// Token: 0x060023C3 RID: 9155
		[MethodImpl(4096)]
		private static extern RendererListStatus QueryRendererListStatus_Internal_Injected(ref ScriptableRenderContext _unity_self, ref RendererList handle);

		// Token: 0x04000D2E RID: 3374
		private static readonly ShaderTagId kRenderTypeTag = new ShaderTagId("RenderType");

		// Token: 0x04000D2F RID: 3375
		private IntPtr m_Ptr;
	}
}
