using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pathfinding.Drawing
{
	// Token: 0x02000046 RID: 70
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class DrawingManager : MonoBehaviour
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000ABC3 File Offset: 0x00008DC3
		public static DrawingManager instance
		{
			get
			{
				if (DrawingManager._instance == null)
				{
					DrawingManager.Init();
				}
				return DrawingManager._instance;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public static void Init()
		{
			if (DrawingManager._instance != null)
			{
				return;
			}
			GameObject gameObject = new GameObject("RetainedGizmos")
			{
				hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset)
			};
			DrawingManager._instance = gameObject.AddComponent<DrawingManager>();
			if (Application.isPlaying)
			{
				Object.DontDestroyOnLoad(gameObject);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000AC22 File Offset: 0x00008E22
		private void RefreshRenderPipelineMode()
		{
			if (((RenderPipelineManager.currentPipeline != null) ? RenderPipelineManager.currentPipeline.GetType() : null) == typeof(UniversalRenderPipeline))
			{
				this.detectedRenderPipeline = DetectedRenderPipeline.URP;
				return;
			}
			this.detectedRenderPipeline = DetectedRenderPipeline.BuiltInOrCustom;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000AC58 File Offset: 0x00008E58
		private void OnEnable()
		{
			if (DrawingManager._instance == null)
			{
				DrawingManager._instance = this;
			}
			if (DrawingManager._instance != this)
			{
				return;
			}
			this.actuallyEnabled = true;
			if (this.gizmos == null)
			{
				this.gizmos = new DrawingData();
			}
			this.gizmos.frameRedrawScope = new RedrawScope(this.gizmos);
			Draw.builder = this.gizmos.GetBuiltInBuilder(false);
			Draw.ingame_builder = this.gizmos.GetBuiltInBuilder(true);
			this.commandBuffer = new CommandBuffer();
			this.commandBuffer.name = "ALINE Gizmos";
			Camera.onPostRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPostRender, new Camera.CameraCallback(this.PostRender));
			RenderPipelineManager.beginFrameRendering += this.BeginFrameRendering;
			RenderPipelineManager.beginCameraRendering += this.BeginCameraRendering;
			RenderPipelineManager.endCameraRendering += this.EndCameraRendering;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000AD46 File Offset: 0x00008F46
		private void BeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			this.RefreshRenderPipelineMode();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000AD46 File Offset: 0x00008F46
		private void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			this.RefreshRenderPipelineMode();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AD50 File Offset: 0x00008F50
		private void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (this.detectedRenderPipeline == DetectedRenderPipeline.URP)
			{
				UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
				if (universalAdditionalCameraData != null)
				{
					ScriptableRenderer scriptableRenderer = universalAdditionalCameraData.scriptableRenderer;
					if (this.renderPassFeature == null)
					{
						this.renderPassFeature = ScriptableObject.CreateInstance<AlineURPRenderPassFeature>();
					}
					this.renderPassFeature.AddRenderPasses(scriptableRenderer);
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		private void OnDisable()
		{
			if (!this.actuallyEnabled)
			{
				return;
			}
			this.actuallyEnabled = false;
			this.commandBuffer.Dispose();
			this.commandBuffer = null;
			Camera.onPostRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPostRender, new Camera.CameraCallback(this.PostRender));
			RenderPipelineManager.beginFrameRendering -= this.BeginFrameRendering;
			RenderPipelineManager.beginCameraRendering -= this.BeginCameraRendering;
			RenderPipelineManager.endCameraRendering -= this.EndCameraRendering;
			if (this.gizmos != null)
			{
				Draw.builder.DiscardAndDisposeInternal();
				Draw.ingame_builder.DiscardAndDisposeInternal();
				this.gizmos.ClearData();
			}
			if (this.renderPassFeature != null)
			{
				Object.DestroyImmediate(this.renderPassFeature);
				this.renderPassFeature = null;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000AE6D File Offset: 0x0000906D
		private void OnEditorUpdate()
		{
			this.framePassed = true;
			this.CleanupIfNoCameraRendered();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000AE7C File Offset: 0x0000907C
		private void Update()
		{
			if (this.actuallyEnabled)
			{
				this.CleanupIfNoCameraRendered();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000AE8C File Offset: 0x0000908C
		private void CleanupIfNoCameraRendered()
		{
			if (Time.frameCount > this.lastFrameCount + 1)
			{
				this.CheckFrameTicking();
				this.gizmos.PostRenderCleanup();
			}
			if (Time.realtimeSinceStartup - this.lastFrameTime > 10f)
			{
				Draw.builder.DiscardAndDisposeInternal();
				Draw.ingame_builder.DiscardAndDisposeInternal();
				Draw.builder = this.gizmos.GetBuiltInBuilder(false);
				Draw.ingame_builder = this.gizmos.GetBuiltInBuilder(true);
				this.lastFrameTime = Time.realtimeSinceStartup;
				DrawingManager.RemoveDestroyedGizmoDrawers();
			}
			if (this.lastFilterFrame - Time.frameCount > 5)
			{
				this.lastFilterFrame = Time.frameCount;
				DrawingManager.RemoveDestroyedGizmoDrawers();
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000AF34 File Offset: 0x00009134
		internal void ExecuteCustomRenderPass(ScriptableRenderContext context, Camera camera)
		{
			this.commandBuffer.Clear();
			this.SubmitFrame(camera, new DrawingData.CommandBufferWrapper
			{
				cmd = this.commandBuffer
			}, true);
			context.ExecuteCommandBuffer(this.commandBuffer);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000AF77 File Offset: 0x00009177
		internal void ExecuteCustomRenderGraphPass(DrawingData.CommandBufferWrapper cmd, Camera camera)
		{
			this.SubmitFrame(camera, cmd, true);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000AF82 File Offset: 0x00009182
		private void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (this.detectedRenderPipeline == DetectedRenderPipeline.BuiltInOrCustom)
			{
				this.ExecuteCustomRenderPass(context, camera);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000AF94 File Offset: 0x00009194
		private void PostRender(Camera camera)
		{
			this.commandBuffer.Clear();
			this.SubmitFrame(camera, new DrawingData.CommandBufferWrapper
			{
				cmd = this.commandBuffer
			}, false);
			Graphics.ExecuteCommandBuffer(this.commandBuffer);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000AFD8 File Offset: 0x000091D8
		private void CheckFrameTicking()
		{
			if (Time.frameCount != this.lastFrameCount)
			{
				this.framePassed = true;
				this.lastFrameCount = Time.frameCount;
				this.lastFrameTime = Time.realtimeSinceStartup;
				this.previousFrameRedrawScope = this.gizmos.frameRedrawScope;
				this.gizmos.frameRedrawScope = new RedrawScope(this.gizmos);
				Draw.builder.DisposeInternal();
				Draw.ingame_builder.DisposeInternal();
				Draw.builder = this.gizmos.GetBuiltInBuilder(false);
				Draw.ingame_builder = this.gizmos.GetBuiltInBuilder(true);
			}
			else if (this.framePassed && Application.isPlaying)
			{
				this.previousFrameRedrawScope.Draw();
			}
			if (this.framePassed)
			{
				this.gizmos.TickFramePreRender();
				this.framePassed = false;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B0A4 File Offset: 0x000092A4
		internal void SubmitFrame(Camera camera, DrawingData.CommandBufferWrapper cmd, bool usingRenderPipeline)
		{
			bool flag = false;
			bool flag2 = DrawingManager.allowRenderToRenderTextures || DrawingManager.drawToAllCameras || camera.targetTexture == null || flag;
			this.CheckFrameTicking();
			this.Submit(camera, cmd, usingRenderPipeline, flag2);
			this.gizmos.PostRenderCleanup();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B0EE File Offset: 0x000092EE
		private bool ShouldDrawGizmos(Object obj)
		{
			return true;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B0F4 File Offset: 0x000092F4
		private static void RemoveDestroyedGizmoDrawers()
		{
			int num = 0;
			for (int i = 0; i < DrawingManager.gizmoDrawers.Count; i++)
			{
				IDrawGizmos drawGizmos = DrawingManager.gizmoDrawers[i];
				if (drawGizmos as MonoBehaviour)
				{
					DrawingManager.gizmoDrawers[num] = drawGizmos;
					num++;
				}
			}
			DrawingManager.gizmoDrawers.RemoveRange(num, DrawingManager.gizmoDrawers.Count - num);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B158 File Offset: 0x00009358
		private void Submit(Camera camera, DrawingData.CommandBufferWrapper cmd, bool usingRenderPipeline, bool allowCameraDefault)
		{
			bool flag = false;
			Draw.builder.DisposeInternal();
			Draw.ingame_builder.DisposeInternal();
			this.gizmos.Render(camera, flag, cmd, allowCameraDefault);
			Draw.builder = this.gizmos.GetBuiltInBuilder(false);
			Draw.ingame_builder = this.gizmos.GetBuiltInBuilder(true);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public static void Register(IDrawGizmos item)
		{
			Type type = item.GetType();
			bool flag;
			if (!DrawingManager.gizmoDrawerTypes.TryGetValue(type, out flag))
			{
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
				MethodInfo methodInfo;
				if ((methodInfo = type.GetMethod("DrawGizmos", bindingFlags)) == null)
				{
					methodInfo = type.GetMethod("Pathfinding.Drawing.IDrawGizmos.DrawGizmos", bindingFlags) ?? type.GetMethod("Drawing.IDrawGizmos.DrawGizmos", bindingFlags);
				}
				MethodInfo methodInfo2 = methodInfo;
				if (methodInfo2 == null)
				{
					throw new Exception("Could not find the DrawGizmos method in type " + type.Name);
				}
				flag = methodInfo2.DeclaringType != typeof(MonoBehaviourGizmos);
				DrawingManager.gizmoDrawerTypes[type] = flag;
			}
			if (!flag)
			{
				return;
			}
			DrawingManager.gizmoDrawers.Add(item);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B251 File Offset: 0x00009451
		public static CommandBuilder GetBuilder(bool renderInGame = false)
		{
			return DrawingManager.instance.gizmos.GetBuilder(renderInGame);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B263 File Offset: 0x00009463
		public static CommandBuilder GetBuilder(RedrawScope redrawScope, bool renderInGame = false)
		{
			return DrawingManager.instance.gizmos.GetBuilder(redrawScope, renderInGame);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B276 File Offset: 0x00009476
		public static CommandBuilder GetBuilder(DrawingData.Hasher hasher, RedrawScope redrawScope = default(RedrawScope), bool renderInGame = false)
		{
			return DrawingManager.instance.gizmos.GetBuilder(hasher, redrawScope, renderInGame);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B28C File Offset: 0x0000948C
		public static RedrawScope GetRedrawScope(GameObject associatedGameObject = null)
		{
			RedrawScope redrawScope = new RedrawScope(DrawingManager.instance.gizmos);
			redrawScope.DrawUntilDispose(associatedGameObject);
			return redrawScope;
		}

		// Token: 0x040000FC RID: 252
		public DrawingData gizmos;

		// Token: 0x040000FD RID: 253
		private static List<IDrawGizmos> gizmoDrawers = new List<IDrawGizmos>();

		// Token: 0x040000FE RID: 254
		private static Dictionary<Type, bool> gizmoDrawerTypes = new Dictionary<Type, bool>();

		// Token: 0x040000FF RID: 255
		private static DrawingManager _instance;

		// Token: 0x04000100 RID: 256
		private bool framePassed;

		// Token: 0x04000101 RID: 257
		private int lastFrameCount = int.MinValue;

		// Token: 0x04000102 RID: 258
		private float lastFrameTime = float.PositiveInfinity;

		// Token: 0x04000103 RID: 259
		private int lastFilterFrame;

		// Token: 0x04000104 RID: 260
		[SerializeField]
		private bool actuallyEnabled;

		// Token: 0x04000105 RID: 261
		private RedrawScope previousFrameRedrawScope;

		// Token: 0x04000106 RID: 262
		public static bool allowRenderToRenderTextures = false;

		// Token: 0x04000107 RID: 263
		public static bool drawToAllCameras = false;

		// Token: 0x04000108 RID: 264
		public static float lineWidthMultiplier = 1f;

		// Token: 0x04000109 RID: 265
		private CommandBuffer commandBuffer;

		// Token: 0x0400010A RID: 266
		[NonSerialized]
		private DetectedRenderPipeline detectedRenderPipeline;

		// Token: 0x0400010B RID: 267
		private HashSet<ScriptableRenderer> scriptableRenderersWithPass = new HashSet<ScriptableRenderer>();

		// Token: 0x0400010C RID: 268
		private AlineURPRenderPassFeature renderPassFeature;

		// Token: 0x0400010D RID: 269
		private static readonly ProfilerMarker MarkerALINE = new ProfilerMarker("ALINE");

		// Token: 0x0400010E RID: 270
		private static readonly ProfilerMarker MarkerCommandBuffer = new ProfilerMarker("Executing command buffer");

		// Token: 0x0400010F RID: 271
		private static readonly ProfilerMarker MarkerFrameTick = new ProfilerMarker("Frame Tick");

		// Token: 0x04000110 RID: 272
		private static readonly ProfilerMarker MarkerFilterDestroyedObjects = new ProfilerMarker("Filter destroyed objects");

		// Token: 0x04000111 RID: 273
		internal static readonly ProfilerMarker MarkerRefreshSelectionCache = new ProfilerMarker("Refresh Selection Cache");

		// Token: 0x04000112 RID: 274
		private static readonly ProfilerMarker MarkerGizmosAllowed = new ProfilerMarker("GizmosAllowed");

		// Token: 0x04000113 RID: 275
		private static readonly ProfilerMarker MarkerDrawGizmos = new ProfilerMarker("DrawGizmos");

		// Token: 0x04000114 RID: 276
		private static readonly ProfilerMarker MarkerSubmitGizmos = new ProfilerMarker("Submit Gizmos");

		// Token: 0x04000115 RID: 277
		private const float NO_DRAWING_TIMEOUT_SECS = 10f;

		// Token: 0x04000116 RID: 278
		private readonly Dictionary<Type, bool> typeToGizmosEnabled = new Dictionary<Type, bool>();
	}
}
