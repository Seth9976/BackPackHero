using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000094 RID: 148
	public class RTHandleSystem : IDisposable
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000158B5 File Offset: 0x00013AB5
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleProperties;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000158BD File Offset: 0x00013ABD
		public RTHandleSystem()
		{
			this.m_AutoSizedRTs = new HashSet<RTHandle>();
			this.m_ResizeOnDemandRTs = new HashSet<RTHandle>();
			this.m_MaxWidths = 1;
			this.m_MaxHeights = 1;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000158E9 File Offset: 0x00013AE9
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000158F4 File Offset: 0x00013AF4
		public void Initialize(int width, int height)
		{
			if (this.m_AutoSizedRTs.Count != 0)
			{
				string text = "Unreleased RTHandles:";
				foreach (RTHandle rthandle in this.m_AutoSizedRTs)
				{
					text = string.Format("{0}\n    {1}", text, rthandle.name);
				}
				Debug.LogError(string.Format("RTHandle.Initialize should only be called once before allocating any Render Texture. This may be caused by an unreleased RTHandle resource.\n{0}\n", text));
			}
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.m_HardwareDynamicResRequested = DynamicResolutionHandler.instance.RequestsHardwareDynamicResolution();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015994 File Offset: 0x00013B94
		public void Release(RTHandle rth)
		{
			if (rth != null)
			{
				rth.Release();
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001599F File Offset: 0x00013B9F
		internal void Remove(RTHandle rth)
		{
			this.m_AutoSizedRTs.Remove(rth);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000159AE File Offset: 0x00013BAE
		public void ResetReferenceSize(int width, int height)
		{
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.SetReferenceSize(width, height, true);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000159C7 File Offset: 0x00013BC7
		public void SetReferenceSize(int width, int height)
		{
			this.SetReferenceSize(width, height, false);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000159D4 File Offset: 0x00013BD4
		public void SetReferenceSize(int width, int height, bool reset)
		{
			this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
			this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
			Vector2 vector = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			width = Mathf.Max(width, 1);
			height = Mathf.Max(height, 1);
			bool flag = width > this.GetMaxWidth() || height > this.GetMaxHeight() || reset;
			if (flag)
			{
				this.Resize(width, height, flag);
			}
			this.m_RTHandleProperties.currentViewportSize = new Vector2Int(width, height);
			this.m_RTHandleProperties.currentRenderTargetSize = new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight());
			if (this.m_RTHandleProperties.previousViewportSize.x == 0)
			{
				this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
				this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
				vector = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			}
			Vector2 vector2 = this.CalculateRatioAgainstMaxSize(in this.m_RTHandleProperties.currentViewportSize);
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && this.m_HardwareDynamicResRequested)
			{
				this.m_RTHandleProperties.rtHandleScale = new Vector4(vector2.x, vector2.y, this.m_RTHandleProperties.rtHandleScale.x, this.m_RTHandleProperties.rtHandleScale.y);
				return;
			}
			Vector2 vector3 = this.m_RTHandleProperties.previousViewportSize / vector;
			this.m_RTHandleProperties.rtHandleScale = new Vector4(vector2.x, vector2.y, vector3.x, vector3.y);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015B7C File Offset: 0x00013D7C
		internal Vector2 CalculateRatioAgainstMaxSize(in Vector2Int viewportSize)
		{
			Vector2 vector = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && this.m_HardwareDynamicResRequested && viewportSize != DynamicResolutionHandler.instance.finalViewport)
			{
				Vector2 vector2 = viewportSize / DynamicResolutionHandler.instance.finalViewport;
				vector = DynamicResolutionHandler.instance.ApplyScalesOnSize(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()), vector2);
			}
			Vector2Int vector2Int = viewportSize;
			float num = (float)vector2Int.x / vector.x;
			vector2Int = viewportSize;
			return new Vector2(num, (float)vector2Int.y / vector.y);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00015C3C File Offset: 0x00013E3C
		public void SetHardwareDynamicResolutionState(bool enableHWDynamicRes)
		{
			if (enableHWDynamicRes != this.m_HardwareDynamicResRequested)
			{
				this.m_HardwareDynamicResRequested = enableHWDynamicRes;
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int num = this.m_AutoSizedRTsArray.Length;
				while (i < num)
				{
					RTHandle rthandle = this.m_AutoSizedRTsArray[i];
					RenderTexture rt = rthandle.m_RT;
					if (rt)
					{
						rt.Release();
						rt.useDynamicScale = this.m_HardwareDynamicResRequested && rthandle.m_EnableHWDynamicScale;
						rt.Create();
					}
					i++;
				}
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00015CD4 File Offset: 0x00013ED4
		internal void SwitchResizeMode(RTHandle rth, RTHandleSystem.ResizeMode mode)
		{
			if (!rth.useScaling)
			{
				return;
			}
			if (mode != RTHandleSystem.ResizeMode.Auto)
			{
				if (mode == RTHandleSystem.ResizeMode.OnDemand)
				{
					this.m_AutoSizedRTs.Remove(rth);
					this.m_ResizeOnDemandRTs.Add(rth);
					return;
				}
			}
			else
			{
				if (this.m_ResizeOnDemandRTs.Contains(rth))
				{
					this.DemandResize(rth);
				}
				this.m_ResizeOnDemandRTs.Remove(rth);
				this.m_AutoSizedRTs.Add(rth);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00015D3C File Offset: 0x00013F3C
		private void DemandResize(RTHandle rth)
		{
			RenderTexture rt = rth.m_RT;
			rth.referenceSize = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Vector2Int vector2Int = rth.GetScaledSize(rth.referenceSize);
			vector2Int = Vector2Int.Max(Vector2Int.one, vector2Int);
			if (rt.width != vector2Int.x || rt.height != vector2Int.y)
			{
				rt.Release();
				rt.width = vector2Int.x;
				rt.height = vector2Int.y;
				rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, rt.graphicsFormat, rt.dimension, rth.m_Name, rt.useMipMap, rth.m_EnableMSAA, (MSAASamples)rt.antiAliasing, rt.useDynamicScale);
				rt.Create();
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015E14 File Offset: 0x00014014
		public int GetMaxWidth()
		{
			return this.m_MaxWidths;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015E1C File Offset: 0x0001401C
		public int GetMaxHeight()
		{
			return this.m_MaxHeights;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015E24 File Offset: 0x00014024
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int num = this.m_AutoSizedRTsArray.Length;
				while (i < num)
				{
					RTHandle rthandle = this.m_AutoSizedRTsArray[i];
					this.Release(rthandle);
					i++;
				}
				this.m_AutoSizedRTs.Clear();
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_ResizeOnDemandRTs.Count);
				this.m_ResizeOnDemandRTs.CopyTo(this.m_AutoSizedRTsArray);
				int j = 0;
				int num2 = this.m_AutoSizedRTsArray.Length;
				while (j < num2)
				{
					RTHandle rthandle2 = this.m_AutoSizedRTsArray[j];
					this.Release(rthandle2);
					j++;
				}
				this.m_ResizeOnDemandRTs.Clear();
				this.m_AutoSizedRTsArray = null;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00015EF0 File Offset: 0x000140F0
		private void Resize(int width, int height, bool sizeChanged)
		{
			this.m_MaxWidths = Math.Max(width, this.m_MaxWidths);
			this.m_MaxHeights = Math.Max(height, this.m_MaxHeights);
			Vector2Int vector2Int = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int num = this.m_AutoSizedRTsArray.Length;
			while (i < num)
			{
				RTHandle rthandle = this.m_AutoSizedRTsArray[i];
				rthandle.referenceSize = vector2Int;
				RenderTexture rt = rthandle.m_RT;
				rt.Release();
				Vector2Int scaledSize = rthandle.GetScaledSize(vector2Int);
				rt.width = Mathf.Max(scaledSize.x, 1);
				rt.height = Mathf.Max(scaledSize.y, 1);
				rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, rt.graphicsFormat, rt.dimension, rthandle.m_Name, rt.useMipMap, rthandle.m_EnableMSAA, (MSAASamples)rt.antiAliasing, rt.useDynamicScale);
				rt.Create();
				i++;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00016020 File Offset: 0x00014220
		public RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return this.Alloc(width, height, wrapMode, wrapMode, wrapMode, slices, depthBufferBits, colorFormat, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001605C File Offset: 0x0001425C
		public RTHandle Alloc(int width, int height, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW = TextureWrapMode.Repeat, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			bool flag = msaaSamples != MSAASamples.None;
			if (!flag && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			RenderTexture renderTexture;
			if (isShadowMap || depthBufferBits != DepthBits.None)
			{
				RenderTextureFormat renderTextureFormat = (isShadowMap ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, renderTextureFormat, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapModeU = wrapModeU,
					wrapModeV = wrapModeV,
					wrapModeW = wrapModeW,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, renderTextureFormat, name, useMipMap, flag, msaaSamples)
				};
			}
			else
			{
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, colorFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapModeU = wrapModeU,
					wrapModeV = wrapModeV,
					wrapModeW = wrapModeW,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, colorFormat, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale)
				};
			}
			renderTexture.Create();
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(renderTexture);
			rthandle.useScaling = false;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.m_EnableMSAA = flag;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			rthandle.referenceSize = new Vector2Int(width, height);
			return rthandle;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016240 File Offset: 0x00014440
		public RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			int num = Mathf.Max(Mathf.RoundToInt(scaleFactor.x * (float)this.GetMaxWidth()), 1);
			int num2 = Mathf.Max(Mathf.RoundToInt(scaleFactor.y * (float)this.GetMaxHeight()), 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(num, num2, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
			rthandle.referenceSize = new Vector2Int(num, num2);
			rthandle.scaleFactor = scaleFactor;
			return rthandle;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000162C0 File Offset: 0x000144C0
		public RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			Vector2Int vector2Int = scaleFunc(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()));
			int num = Mathf.Max(vector2Int.x, 1);
			int num2 = Mathf.Max(vector2Int.y, 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(num, num2, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
			rthandle.referenceSize = new Vector2Int(num, num2);
			rthandle.scaleFunc = scaleFunc;
			return rthandle;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00016340 File Offset: 0x00014540
		private RTHandle AllocAutoSizedRenderTexture(int width, int height, int slices, DepthBits depthBufferBits, GraphicsFormat colorFormat, FilterMode filterMode, TextureWrapMode wrapMode, TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, MSAASamples msaaSamples, bool bindTextureMS, bool useDynamicScale, RenderTextureMemoryless memoryless, string name)
		{
			bool flag = msaaSamples != MSAASamples.None;
			if (!flag && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			if (flag && enableRandomWrite)
			{
				Debug.LogWarning("RTHandle that is MSAA-enabled cannot allocate MSAA RT with 'enableRandomWrite = true'.");
				enableRandomWrite = false;
			}
			RenderTexture renderTexture;
			if (isShadowMap || depthBufferBits != DepthBits.None)
			{
				RenderTextureFormat renderTextureFormat = (isShadowMap ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				GraphicsFormat graphicsFormat = (isShadowMap ? GraphicsFormat.None : GraphicsFormat.R8_UInt);
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, renderTextureFormat, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					stencilFormat = graphicsFormat,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, colorFormat, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale)
				};
			}
			else
			{
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, colorFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, colorFormat, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale)
				};
			}
			renderTexture.Create();
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(renderTexture);
			rthandle.m_EnableMSAA = flag;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.useScaling = true;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			this.m_AutoSizedRTs.Add(rthandle);
			return rthandle;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001652D File Offset: 0x0001472D
		public RTHandle Alloc(RenderTexture texture)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = texture.name;
			return rthandle;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00016564 File Offset: 0x00014764
		public RTHandle Alloc(Texture texture)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = texture.name;
			return rthandle;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001659B File Offset: 0x0001479B
		public RTHandle Alloc(RenderTargetIdentifier texture)
		{
			return this.Alloc(texture, "");
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000165A9 File Offset: 0x000147A9
		public RTHandle Alloc(RenderTargetIdentifier texture, string name)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = name;
			return rthandle;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000165DB File Offset: 0x000147DB
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000165E8 File Offset: 0x000147E8
		internal string DumpRTInfo()
		{
			string text = "";
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int num = this.m_AutoSizedRTsArray.Length;
			while (i < num)
			{
				RenderTexture rt = this.m_AutoSizedRTsArray[i].rt;
				text = string.Format("{0}\nRT ({1})\t Format: {2} W: {3} H {4}\n", new object[] { text, i, rt.format, rt.width, rt.height });
				i++;
			}
			return text;
		}

		// Token: 0x04000302 RID: 770
		private bool m_HardwareDynamicResRequested;

		// Token: 0x04000303 RID: 771
		private HashSet<RTHandle> m_AutoSizedRTs;

		// Token: 0x04000304 RID: 772
		private RTHandle[] m_AutoSizedRTsArray;

		// Token: 0x04000305 RID: 773
		private HashSet<RTHandle> m_ResizeOnDemandRTs;

		// Token: 0x04000306 RID: 774
		private RTHandleProperties m_RTHandleProperties;

		// Token: 0x04000307 RID: 775
		private int m_MaxWidths;

		// Token: 0x04000308 RID: 776
		private int m_MaxHeights;

		// Token: 0x0200016B RID: 363
		internal enum ResizeMode
		{
			// Token: 0x04000570 RID: 1392
			Auto,
			// Token: 0x04000571 RID: 1393
			OnDemand
		}
	}
}
