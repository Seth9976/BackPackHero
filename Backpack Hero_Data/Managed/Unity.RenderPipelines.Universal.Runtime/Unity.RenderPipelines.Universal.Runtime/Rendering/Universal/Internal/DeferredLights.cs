using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000100 RID: 256
	internal class DeferredLights
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0002B267 File Offset: 0x00029467
		internal int GBufferAlbedoIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0002B26A File Offset: 0x0002946A
		internal int GBufferSpecularMetallicIndex
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0002B26D File Offset: 0x0002946D
		internal int GBufferNormalSmoothnessIndex
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0002B270 File Offset: 0x00029470
		internal int GBufferLightingIndex
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0002B273 File Offset: 0x00029473
		internal int GbufferDepthIndex
		{
			get
			{
				if (!this.UseRenderPass)
				{
					return -1;
				}
				return this.GBufferLightingIndex + 1;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0002B287 File Offset: 0x00029487
		internal int GBufferShadowMask
		{
			get
			{
				if (!this.UseShadowMask)
				{
					return -1;
				}
				return this.GBufferLightingIndex + (this.UseRenderPass ? 1 : 0) + 1;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0002B2A8 File Offset: 0x000294A8
		internal int GBufferRenderingLayers
		{
			get
			{
				if (!this.UseRenderingLayers)
				{
					return -1;
				}
				return this.GBufferLightingIndex + (this.UseRenderPass ? 1 : 0) + (this.UseShadowMask ? 1 : 0) + 1;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0002B2D6 File Offset: 0x000294D6
		internal int GBufferSliceCount
		{
			get
			{
				return 4 + (this.UseRenderPass ? 1 : 0) + (this.UseShadowMask ? 1 : 0) + (this.UseRenderingLayers ? 1 : 0);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002B300 File Offset: 0x00029500
		internal GraphicsFormat GetGBufferFormat(int index)
		{
			if (index == this.GBufferAlbedoIndex)
			{
				if (QualitySettings.activeColorSpace != ColorSpace.Linear)
				{
					return GraphicsFormat.R8G8B8A8_UNorm;
				}
				return GraphicsFormat.R8G8B8A8_SRGB;
			}
			else
			{
				if (index == this.GBufferSpecularMetallicIndex)
				{
					return GraphicsFormat.R8G8B8A8_UNorm;
				}
				if (index == this.GBufferNormalSmoothnessIndex)
				{
					if (!this.AccurateGbufferNormals)
					{
						return GraphicsFormat.R8G8B8A8_SNorm;
					}
					return GraphicsFormat.R8G8B8A8_UNorm;
				}
				else
				{
					if (index == this.GBufferLightingIndex)
					{
						return GraphicsFormat.None;
					}
					if (index == this.GbufferDepthIndex)
					{
						return GraphicsFormat.R32_SFloat;
					}
					if (index == this.GBufferShadowMask)
					{
						return GraphicsFormat.R8G8B8A8_UNorm;
					}
					if (index == this.GBufferRenderingLayers)
					{
						return GraphicsFormat.R8_UNorm;
					}
					return GraphicsFormat.None;
				}
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0002B371 File Offset: 0x00029571
		internal bool UseShadowMask
		{
			get
			{
				return this.MixedLightingSetup > MixedLightingSetup.None;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0002B37C File Offset: 0x0002957C
		internal bool UseRenderingLayers
		{
			get
			{
				return UniversalRenderPipeline.asset.supportsLightLayers;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0002B388 File Offset: 0x00029588
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0002B390 File Offset: 0x00029590
		internal bool UseRenderPass { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0002B399 File Offset: 0x00029599
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0002B3A1 File Offset: 0x000295A1
		internal bool HasDepthPrepass { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0002B3AA File Offset: 0x000295AA
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0002B3B2 File Offset: 0x000295B2
		internal bool HasNormalPrepass { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0002B3BB File Offset: 0x000295BB
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0002B3C3 File Offset: 0x000295C3
		internal bool IsOverlay { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0002B3CC File Offset: 0x000295CC
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0002B3D4 File Offset: 0x000295D4
		internal bool AccurateGbufferNormals
		{
			get
			{
				return this.m_AccurateGbufferNormals;
			}
			set
			{
				this.m_AccurateGbufferNormals = value || !RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R8G8B8A8_SNorm, FormatUsage.Render);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0002B3ED File Offset: 0x000295ED
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0002B3F5 File Offset: 0x000295F5
		internal bool TiledDeferredShading { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0002B3FE File Offset: 0x000295FE
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0002B406 File Offset: 0x00029606
		internal MixedLightingSetup MixedLightingSetup { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002B40F File Offset: 0x0002960F
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0002B417 File Offset: 0x00029617
		internal bool UseJobSystem { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002B420 File Offset: 0x00029620
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0002B428 File Offset: 0x00029628
		internal int RenderWidth { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0002B431 File Offset: 0x00029631
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0002B439 File Offset: 0x00029639
		internal int RenderHeight { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0002B442 File Offset: 0x00029642
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0002B44A File Offset: 0x0002964A
		internal RenderTargetHandle[] GbufferAttachments { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0002B453 File Offset: 0x00029653
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0002B45B File Offset: 0x0002965B
		internal RenderTargetIdentifier[] DeferredInputAttachments { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0002B464 File Offset: 0x00029664
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0002B46C File Offset: 0x0002966C
		internal bool[] DeferredInputIsTransient { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0002B475 File Offset: 0x00029675
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0002B47D File Offset: 0x0002967D
		internal RenderTargetHandle DepthAttachment { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0002B486 File Offset: 0x00029686
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0002B48E File Offset: 0x0002968E
		internal RenderTargetHandle DepthCopyTexture { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0002B497 File Offset: 0x00029697
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0002B49F File Offset: 0x0002969F
		internal RenderTargetHandle DepthInfoTexture { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0002B4A8 File Offset: 0x000296A8
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0002B4B0 File Offset: 0x000296B0
		internal RenderTargetHandle TileDepthInfoTexture { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0002B4B9 File Offset: 0x000296B9
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0002B4C1 File Offset: 0x000296C1
		internal RenderTargetIdentifier[] GbufferAttachmentIdentifiers { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0002B4CA File Offset: 0x000296CA
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0002B4D2 File Offset: 0x000296D2
		internal GraphicsFormat[] GbufferFormats { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0002B4DB File Offset: 0x000296DB
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0002B4E3 File Offset: 0x000296E3
		internal RenderTargetIdentifier DepthAttachmentIdentifier { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0002B4EC File Offset: 0x000296EC
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0002B4F4 File Offset: 0x000296F4
		internal RenderTargetIdentifier DepthCopyTextureIdentifier { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0002B4FD File Offset: 0x000296FD
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0002B505 File Offset: 0x00029705
		internal RenderTargetIdentifier DepthInfoTextureIdentifier { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0002B50E File Offset: 0x0002970E
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0002B516 File Offset: 0x00029716
		internal RenderTargetIdentifier TileDepthInfoTextureIdentifier { get; set; }

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002B520 File Offset: 0x00029720
		internal DeferredLights(DeferredLights.InitParams initParams, bool useNativeRenderPass = false)
		{
			DeferredConfig.IsOpenGL = SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3;
			DeferredConfig.IsDX10 = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11 && SystemInfo.graphicsShaderLevel <= 40;
			this.m_TileDepthInfoMaterial = initParams.tileDepthInfoMaterial;
			this.m_TileDeferredMaterial = initParams.tileDeferredMaterial;
			this.m_StencilDeferredMaterial = initParams.stencilDeferredMaterial;
			this.m_TileDeferredPasses = new int[DeferredLights.k_TileDeferredPassNames.Length];
			this.InitTileDeferredMaterial();
			this.m_StencilDeferredPasses = new int[DeferredLights.k_StencilDeferredPassNames.Length];
			this.InitStencilDeferredMaterial();
			this.m_MaxDepthRangePerBatch = (DeferredConfig.UseCBufferForDepthRange ? 65536 : 131072) / 4;
			this.m_MaxTilesPerBatch = (DeferredConfig.UseCBufferForTileList ? 65536 : 131072) / Marshal.SizeOf(typeof(TileData));
			this.m_MaxPunctualLightPerBatch = (DeferredConfig.UseCBufferForLightData ? 65536 : 131072) / Marshal.SizeOf(typeof(PunctualLightData));
			this.m_MaxRelLightIndicesPerBatch = (DeferredConfig.UseCBufferForLightList ? 65536 : 131072) / 4;
			this.m_Tilers = new DeferredTiler[3];
			this.m_TileDataCapacities = new int[3];
			for (int i = 0; i < 3; i++)
			{
				int num = (int)Mathf.Pow(4f, (float)i);
				this.m_Tilers[i] = new DeferredTiler(16 * num, 16 * num, 32 * num * num, i);
				this.m_TileDataCapacities[i] = 0;
			}
			this.AccurateGbufferNormals = true;
			this.TiledDeferredShading = true;
			this.UseJobSystem = true;
			this.m_HasTileVisLights = false;
			this.UseRenderPass = useNativeRenderPass;
			this.m_LightCookieManager = initParams.lightCookieManager;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002B723 File Offset: 0x00029923
		internal ref DeferredTiler GetTiler(int i)
		{
			return ref this.m_Tilers[i];
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002B734 File Offset: 0x00029934
		internal void SetupLights(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			DeferredShaderData.instance.ResetBuffers();
			Camera camera = renderingData.cameraData.camera;
			this.RenderWidth = (camera.allowDynamicResolution ? Mathf.CeilToInt(ScalableBufferManager.widthScaleFactor * (float)renderingData.cameraData.cameraTargetDescriptor.width) : renderingData.cameraData.cameraTargetDescriptor.width);
			this.RenderHeight = (camera.allowDynamicResolution ? Mathf.CeilToInt(ScalableBufferManager.heightScaleFactor * (float)renderingData.cameraData.cameraTargetDescriptor.height) : renderingData.cameraData.cameraTargetDescriptor.height);
			if (this.TiledDeferredShading)
			{
				if (this.m_CachedRenderWidth != this.RenderWidth || this.m_CachedRenderHeight != this.RenderHeight || this.m_CachedProjectionMatrix != renderingData.cameraData.camera.projectionMatrix)
				{
					this.m_CachedRenderWidth = this.RenderWidth;
					this.m_CachedRenderHeight = this.RenderHeight;
					this.m_CachedProjectionMatrix = renderingData.cameraData.camera.projectionMatrix;
					for (int i = 0; i < this.m_Tilers.Length; i++)
					{
						this.m_Tilers[i].PrecomputeTiles(renderingData.cameraData.camera.projectionMatrix, renderingData.cameraData.camera.orthographic, this.m_CachedRenderWidth, this.m_CachedRenderHeight);
					}
				}
				for (int j = 0; j < this.m_Tilers.Length; j++)
				{
					this.m_Tilers[j].Setup(this.m_TileDataCapacities[j]);
				}
			}
			NativeArray<DeferredTiler.PrePunctualLight> nativeArray;
			this.PrecomputeLights(out nativeArray, out this.m_stencilVisLights, out this.m_stencilVisLightOffsets, ref renderingData.lightData.visibleLights, renderingData.lightData.additionalLightsCount != 0 || renderingData.lightData.mainLightIndex >= 0, renderingData.cameraData.camera.worldToCameraMatrix, renderingData.cameraData.camera.orthographic, renderingData.cameraData.camera.nearClipPlane);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, DeferredLights.m_ProfilingSetupLightConstants))
			{
				this.SetupShaderLightConstants(commandBuffer, ref renderingData);
				bool supportsMixedLighting = renderingData.lightData.supportsMixedLighting;
				CoreUtils.SetKeyword(commandBuffer, "_GBUFFER_NORMALS_OCT", this.AccurateGbufferNormals);
				bool flag = supportsMixedLighting && this.MixedLightingSetup == MixedLightingSetup.ShadowMask;
				bool flag2 = flag && QualitySettings.shadowmaskMode == ShadowmaskMode.Shadowmask;
				bool flag3 = supportsMixedLighting && this.MixedLightingSetup == MixedLightingSetup.Subtractive;
				CoreUtils.SetKeyword(commandBuffer, "LIGHTMAP_SHADOW_MIXING", flag3 || flag2);
				CoreUtils.SetKeyword(commandBuffer, "SHADOWS_SHADOWMASK", flag);
				CoreUtils.SetKeyword(commandBuffer, "_MIXED_LIGHTING_SUBTRACTIVE", flag3);
				CoreUtils.SetKeyword(commandBuffer, "_RENDER_PASS_ENABLED", this.UseRenderPass && renderingData.cameraData.cameraType == CameraType.Game);
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
			if (this.TiledDeferredShading)
			{
				this.SortLights(ref nativeArray);
				NativeArray<ushort> nativeArray2 = new NativeArray<ushort>(nativeArray.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				for (int k = 0; k < nativeArray.Length; k++)
				{
					nativeArray2[k] = (ushort)k;
				}
				NativeArray<uint> nativeArray3 = new NativeArray<uint>(2, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				nativeArray3[0] = 0U;
				nativeArray3[1] = (uint)nativeArray.Length;
				ref DeferredTiler ptr = ref this.m_Tilers[this.m_Tilers.Length - 1];
				if (this.m_Tilers.Length != 1)
				{
					NativeArray<JobHandle> nativeArray4 = default(NativeArray<JobHandle>);
					int num = 0;
					int num2 = 0;
					if (this.UseJobSystem)
					{
						int num3 = 1;
						for (int l = this.m_Tilers.Length - 1; l > 0; l--)
						{
							ref DeferredTiler ptr2 = ref this.m_Tilers[l];
							num3 += ptr2.TileXCount * ptr2.TileYCount;
						}
						nativeArray4 = new NativeArray<JobHandle>(num3, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
					}
					DeferredLights.CullLightsJob cullLightsJob = new DeferredLights.CullLightsJob
					{
						tiler = ptr,
						prePunctualLights = nativeArray,
						coarseTiles = nativeArray2,
						coarseTileHeaders = nativeArray3,
						coarseHeaderOffset = 0,
						istart = 0,
						iend = ptr.TileXCount,
						jstart = 0,
						jend = ptr.TileYCount
					};
					if (this.UseJobSystem)
					{
						nativeArray4[num2++] = cullLightsJob.Schedule(default(JobHandle));
						JobHandle.ScheduleBatchedJobs();
					}
					else
					{
						cullLightsJob.Execute();
					}
					for (int m = this.m_Tilers.Length - 1; m > 0; m--)
					{
						ref DeferredTiler ptr3 = ref this.m_Tilers[m - 1];
						ref DeferredTiler ptr4 = ref this.m_Tilers[m];
						int tileXCount = ptr3.TileXCount;
						int tileYCount = ptr3.TileYCount;
						int tileXCount2 = ptr4.TileXCount;
						int tileYCount2 = ptr4.TileYCount;
						int num4 = ((m == this.m_Tilers.Length - 1) ? tileXCount2 : 4);
						int num5 = ((m == this.m_Tilers.Length - 1) ? tileYCount2 : 4);
						int num6 = (tileXCount2 + num4 - 1) / num4;
						int num7 = (tileYCount2 + num5 - 1) / num5;
						NativeArray<ushort> tiles = ptr4.Tiles;
						NativeArray<uint> tileHeaders = ptr4.TileHeaders;
						int num8 = ptr4.TilePixelWidth / ptr3.TilePixelWidth;
						int num9 = ptr4.TilePixelHeight / ptr3.TilePixelHeight;
						for (int n = 0; n < tileYCount2; n++)
						{
							for (int num10 = 0; num10 < tileXCount2; num10++)
							{
								int num11 = num10 * num8;
								int num12 = n * num9;
								int num13 = Mathf.Min(num11 + num8, tileXCount);
								int num14 = Mathf.Min(num12 + num9, tileYCount);
								int tileHeaderOffset = ptr4.GetTileHeaderOffset(num10, n);
								DeferredLights.CullLightsJob cullLightsJob2 = new DeferredLights.CullLightsJob
								{
									tiler = this.m_Tilers[m - 1],
									prePunctualLights = nativeArray,
									coarseTiles = tiles,
									coarseTileHeaders = tileHeaders,
									coarseHeaderOffset = tileHeaderOffset,
									istart = num11,
									iend = num13,
									jstart = num12,
									jend = num14
								};
								if (this.UseJobSystem)
								{
									nativeArray4[num2++] = cullLightsJob2.Schedule(nativeArray4[num + num10 / num4 + n / num5 * num6]);
								}
								else
								{
									cullLightsJob2.Execute();
								}
							}
						}
						num += num6 * num7;
					}
					if (this.UseJobSystem)
					{
						JobHandle.CompleteAll(nativeArray4);
						nativeArray4.Dispose();
					}
				}
				else
				{
					ptr.CullFinalLights(ref nativeArray, ref nativeArray2, 0, nativeArray.Length, 0, ptr.TileXCount, 0, ptr.TileYCount);
				}
				nativeArray2.Dispose();
				nativeArray3.Dispose();
			}
			if (nativeArray.IsCreated)
			{
				nativeArray.Dispose();
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002BDF0 File Offset: 0x00029FF0
		internal void ResolveMixedLightingMode(ref RenderingData renderingData)
		{
			this.MixedLightingSetup = MixedLightingSetup.None;
			if (renderingData.lightData.supportsMixedLighting)
			{
				NativeArray<VisibleLight> visibleLights = renderingData.lightData.visibleLights;
				int num = 0;
				while (num < renderingData.lightData.visibleLights.Length && this.MixedLightingSetup == MixedLightingSetup.None)
				{
					Light light = visibleLights[num].light;
					if (light != null && light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed && light.shadows != LightShadows.None)
					{
						MixedLightingMode mixedLightingMode = light.bakingOutput.mixedLightingMode;
						if (mixedLightingMode != MixedLightingMode.Subtractive)
						{
							if (mixedLightingMode == MixedLightingMode.Shadowmask)
							{
								this.MixedLightingSetup = MixedLightingSetup.ShadowMask;
							}
						}
						else
						{
							this.MixedLightingSetup = MixedLightingSetup.Subtractive;
						}
					}
					num++;
				}
			}
			this.CreateGbufferAttachments();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002BEA2 File Offset: 0x0002A0A2
		internal void DisableFramebufferFetchInput()
		{
			this.UseRenderPass = false;
			this.CreateGbufferAttachments();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0002BEB4 File Offset: 0x0002A0B4
		internal void CreateGbufferAttachments()
		{
			int gbufferSliceCount = this.GBufferSliceCount;
			if (this.GbufferAttachments == null || this.GbufferAttachments.Length != gbufferSliceCount)
			{
				this.GbufferAttachments = new RenderTargetHandle[gbufferSliceCount];
				for (int i = 0; i < gbufferSliceCount; i++)
				{
					this.GbufferAttachments[i].Init(DeferredLights.k_GBufferNames[i]);
				}
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0002BF0B File Offset: 0x0002A10B
		internal bool IsRuntimeSupportedThisFrame()
		{
			return this.GBufferSliceCount <= SystemInfo.supportedRenderTargetCount && !DeferredConfig.IsOpenGL && !DeferredConfig.IsDX10;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002BF2C File Offset: 0x0002A12C
		public void Setup(ref RenderingData renderingData, AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass, bool hasDepthPrepass, bool hasNormalPrepass, RenderTargetHandle depthCopyTexture, RenderTargetHandle depthInfoTexture, RenderTargetHandle tileDepthInfoTexture, RenderTargetHandle depthAttachment, RenderTargetHandle colorAttachment)
		{
			this.m_AdditionalLightsShadowCasterPass = additionalLightsShadowCasterPass;
			this.HasDepthPrepass = hasDepthPrepass;
			this.HasNormalPrepass = hasNormalPrepass;
			this.DepthCopyTexture = depthCopyTexture;
			this.DepthInfoTexture = depthInfoTexture;
			this.TileDepthInfoTexture = tileDepthInfoTexture;
			this.GbufferAttachments[this.GBufferLightingIndex] = colorAttachment;
			this.DepthAttachment = depthAttachment;
			this.DepthCopyTextureIdentifier = this.DepthCopyTexture.Identifier();
			this.DepthInfoTextureIdentifier = this.DepthInfoTexture.Identifier();
			this.TileDepthInfoTextureIdentifier = this.TileDepthInfoTexture.Identifier();
			if (this.GbufferAttachmentIdentifiers == null || this.GbufferAttachmentIdentifiers.Length != this.GbufferAttachments.Length)
			{
				this.GbufferAttachmentIdentifiers = new RenderTargetIdentifier[this.GbufferAttachments.Length];
				this.GbufferFormats = new GraphicsFormat[this.GbufferAttachments.Length];
			}
			for (int i = 0; i < this.GbufferAttachments.Length; i++)
			{
				this.GbufferAttachmentIdentifiers[i] = this.GbufferAttachments[i].Identifier();
				this.GbufferFormats[i] = this.GetGBufferFormat(i);
			}
			if (this.DeferredInputAttachments == null && this.UseRenderPass && this.GbufferAttachments.Length >= 5)
			{
				this.DeferredInputAttachments = new RenderTargetIdentifier[]
				{
					this.GbufferAttachmentIdentifiers[0],
					this.GbufferAttachmentIdentifiers[1],
					this.GbufferAttachmentIdentifiers[2],
					this.GbufferAttachmentIdentifiers[4]
				};
				this.DeferredInputIsTransient = new bool[] { true, true, true, false };
			}
			this.DepthAttachmentIdentifier = depthAttachment.Identifier();
			if (renderingData.cameraData.xr.enabled)
			{
				this.DepthCopyTextureIdentifier = new RenderTargetIdentifier(this.DepthCopyTextureIdentifier, 0, CubemapFace.Unknown, -1);
				this.DepthInfoTextureIdentifier = new RenderTargetIdentifier(this.DepthInfoTextureIdentifier, 0, CubemapFace.Unknown, -1);
				this.TileDepthInfoTextureIdentifier = new RenderTargetIdentifier(this.TileDepthInfoTextureIdentifier, 0, CubemapFace.Unknown, -1);
				for (int j = 0; j < this.GbufferAttachmentIdentifiers.Length; j++)
				{
					this.GbufferAttachmentIdentifiers[j] = new RenderTargetIdentifier(this.GbufferAttachmentIdentifiers[j], 0, CubemapFace.Unknown, -1);
				}
				this.DepthAttachmentIdentifier = new RenderTargetIdentifier(this.DepthAttachmentIdentifier, 0, CubemapFace.Unknown, -1);
			}
			this.m_HasTileVisLights = this.TiledDeferredShading && this.CheckHasTileLights(ref renderingData.lightData.visibleLights);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0002C194 File Offset: 0x0002A394
		public void OnCameraCleanup(CommandBuffer cmd)
		{
			CoreUtils.SetKeyword(cmd, "_GBUFFER_NORMALS_OCT", false);
			for (int i = 0; i < this.m_Tilers.Length; i++)
			{
				this.m_TileDataCapacities[i] = math.max(this.m_TileDataCapacities[i], this.m_Tilers[i].TileDataCapacity);
				this.m_Tilers[i].OnCameraCleanup();
			}
			if (this.m_stencilVisLights.IsCreated)
			{
				this.m_stencilVisLights.Dispose();
			}
			if (this.m_stencilVisLightOffsets.IsCreated)
			{
				this.m_stencilVisLightOffsets.Dispose();
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002C228 File Offset: 0x0002A428
		internal static StencilState OverwriteStencil(StencilState s, int stencilWriteMask)
		{
			if (!s.enabled)
			{
				return new StencilState(true, 0, (byte)stencilWriteMask, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep);
			}
			CompareFunction compareFunction = ((s.compareFunctionFront != CompareFunction.Disabled) ? s.compareFunctionFront : CompareFunction.Always);
			CompareFunction compareFunction2 = ((s.compareFunctionBack != CompareFunction.Disabled) ? s.compareFunctionBack : CompareFunction.Always);
			StencilOp passOperationFront = s.passOperationFront;
			StencilOp failOperationFront = s.failOperationFront;
			StencilOp zFailOperationFront = s.zFailOperationFront;
			StencilOp passOperationBack = s.passOperationBack;
			StencilOp failOperationBack = s.failOperationBack;
			StencilOp zFailOperationBack = s.zFailOperationBack;
			return new StencilState(true, s.readMask & 15, (byte)((int)s.writeMask | stencilWriteMask), compareFunction, passOperationFront, failOperationFront, zFailOperationFront, compareFunction2, passOperationBack, failOperationBack, zFailOperationBack);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002C2D4 File Offset: 0x0002A4D4
		internal static RenderStateBlock OverwriteStencil(RenderStateBlock block, int stencilWriteMask, int stencilRef)
		{
			if (!block.stencilState.enabled)
			{
				block.stencilState = new StencilState(true, 0, (byte)stencilWriteMask, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep);
			}
			else
			{
				StencilState stencilState = block.stencilState;
				CompareFunction compareFunction = ((stencilState.compareFunctionFront != CompareFunction.Disabled) ? stencilState.compareFunctionFront : CompareFunction.Always);
				CompareFunction compareFunction2 = ((stencilState.compareFunctionBack != CompareFunction.Disabled) ? stencilState.compareFunctionBack : CompareFunction.Always);
				StencilOp passOperationFront = stencilState.passOperationFront;
				StencilOp failOperationFront = stencilState.failOperationFront;
				StencilOp zFailOperationFront = stencilState.zFailOperationFront;
				StencilOp passOperationBack = stencilState.passOperationBack;
				StencilOp failOperationBack = stencilState.failOperationBack;
				StencilOp zFailOperationBack = stencilState.zFailOperationBack;
				block.stencilState = new StencilState(true, stencilState.readMask & 15, (byte)((int)stencilState.writeMask | stencilWriteMask), compareFunction, passOperationFront, failOperationFront, zFailOperationFront, compareFunction2, passOperationBack, failOperationBack, zFailOperationBack);
			}
			block.mask |= RenderStateMask.Stencil;
			block.stencilReference = (block.stencilReference & 15) | stencilRef;
			return block;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002C3C8 File Offset: 0x0002A5C8
		internal bool HasTileLights()
		{
			return this.m_HasTileVisLights;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
		internal bool HasTileDepthRangeExtraPass()
		{
			DeferredTiler[] tilers = this.m_Tilers;
			int num = 0;
			int tilePixelWidth = tilers[num].TilePixelWidth;
			int tilePixelHeight = tilers[num].TilePixelHeight;
			Mathf.Log((float)Mathf.Min(tilePixelWidth, tilePixelHeight), 2f);
			return false;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002C40C File Offset: 0x0002A60C
		internal void ExecuteTileDepthInfoPass(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_TileDepthInfoMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_TileDepthInfoMaterial,
					base.GetType().Name
				});
				return;
			}
			uint num = (uint)((int)Mathf.FloatToHalf(-2f) | ((int)Mathf.FloatToHalf(-1f) << 16));
			ref DeferredTiler ptr = ref this.m_Tilers[0];
			int tileXCount = ptr.TileXCount;
			int tileYCount = ptr.TileYCount;
			int tilePixelWidth = ptr.TilePixelWidth;
			int tilePixelHeight = ptr.TilePixelHeight;
			int num2 = (int)Mathf.Log((float)Mathf.Min(tilePixelWidth, tilePixelHeight), 2f);
			int num3 = num2;
			int num4 = num2 - num3;
			int num5 = 1 << num3;
			int num6 = this.RenderWidth + num5 - 1 >> num3;
			int renderHeight = this.RenderHeight;
			NativeArray<ushort> tiles = ptr.Tiles;
			NativeArray<uint> tileHeaders = ptr.TileHeaders;
			NativeArray<uint> nativeArray = new NativeArray<uint>(this.m_MaxDepthRangePerBatch, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, DeferredLights.m_ProfilingTileDepthInfo))
			{
				RenderTargetIdentifier depthAttachmentIdentifier = this.DepthAttachmentIdentifier;
				RenderTargetIdentifier renderTargetIdentifier = ((num2 == num3) ? this.TileDepthInfoTextureIdentifier : this.DepthInfoTextureIdentifier);
				commandBuffer.SetGlobalTexture(DeferredLights.ShaderConstants._DepthTex, depthAttachmentIdentifier);
				commandBuffer.SetGlobalVector(DeferredLights.ShaderConstants._DepthTexSize, new Vector4((float)this.RenderWidth, (float)this.RenderHeight, 1f / (float)this.RenderWidth, 1f / (float)this.RenderHeight));
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._DownsamplingWidth, tilePixelWidth);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._DownsamplingHeight, tilePixelHeight);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._SourceShiftX, num3);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._SourceShiftY, num3);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._TileShiftX, num4);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._TileShiftY, num4);
				Matrix4x4 projectionMatrix = renderingData.cameraData.camera.projectionMatrix;
				Matrix4x4 matrix4x = Matrix4x4.Inverse(new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 0.5f, 0f), new Vector4(0f, 0f, 0.5f, 1f)) * projectionMatrix);
				commandBuffer.SetGlobalVector(DeferredLights.ShaderConstants._unproject0, matrix4x.GetRow(2));
				commandBuffer.SetGlobalVector(DeferredLights.ShaderConstants._unproject1, matrix4x.GetRow(3));
				string text = null;
				if (tilePixelWidth == tilePixelHeight)
				{
					if (num3 == 1)
					{
						text = "DOWNSAMPLING_SIZE_2";
					}
					else if (num3 == 2)
					{
						text = "DOWNSAMPLING_SIZE_4";
					}
					else if (num3 == 3)
					{
						text = "DOWNSAMPLING_SIZE_8";
					}
					else if (num3 == 4)
					{
						text = "DOWNSAMPLING_SIZE_16";
					}
				}
				if (text != null)
				{
					commandBuffer.EnableShaderKeyword(text);
				}
				int i = 0;
				int num7 = (DeferredConfig.UseCBufferForDepthRange ? 65536 : 131072) / (tileXCount * 4);
				while (i < tileYCount)
				{
					int num8 = Mathf.Min(tileYCount, i + num7);
					for (int j = i; j < num8; j++)
					{
						for (int k = 0; k < tileXCount; k++)
						{
							int tileHeaderOffset = ptr.GetTileHeaderOffset(k, j);
							uint num9 = ((tileHeaders[tileHeaderOffset + 1] == 0U) ? num : tileHeaders[tileHeaderOffset + 2]);
							nativeArray[k + (j - i) * tileXCount] = num9;
						}
					}
					ComputeBuffer computeBuffer = DeferredShaderData.instance.ReserveBuffer<uint>(this.m_MaxDepthRangePerBatch, DeferredConfig.UseCBufferForDepthRange);
					computeBuffer.SetData<uint>(nativeArray, 0, 0, nativeArray.Length);
					if (DeferredConfig.UseCBufferForDepthRange)
					{
						commandBuffer.SetGlobalConstantBuffer(computeBuffer, DeferredLights.ShaderConstants.UDepthRanges, 0, this.m_MaxDepthRangePerBatch * 4);
					}
					else
					{
						commandBuffer.SetGlobalBuffer(DeferredLights.ShaderConstants._DepthRanges, computeBuffer);
					}
					commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._tileXCount, tileXCount);
					commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._DepthRangeOffset, i * tileXCount);
					commandBuffer.EnableScissorRect(new Rect(0f, (float)(i << num4), (float)num6, (float)(num8 - i << num4)));
					commandBuffer.Blit(depthAttachmentIdentifier, renderTargetIdentifier, this.m_TileDepthInfoMaterial, 0);
					i = num8;
				}
				commandBuffer.DisableScissorRect();
				if (text != null)
				{
					commandBuffer.DisableShaderKeyword(text);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
			nativeArray.Dispose();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002C860 File Offset: 0x0002AA60
		internal void ExecuteDownsampleBitmaskPass(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_TileDepthInfoMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_TileDepthInfoMaterial,
					base.GetType().Name
				});
				return;
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, DeferredLights.m_ProfilingTileDepthInfo))
			{
				RenderTargetIdentifier depthInfoTextureIdentifier = this.DepthInfoTextureIdentifier;
				RenderTargetIdentifier tileDepthInfoTextureIdentifier = this.TileDepthInfoTextureIdentifier;
				DeferredTiler[] tilers = this.m_Tilers;
				int num = 0;
				int tilePixelWidth = tilers[num].TilePixelWidth;
				int tilePixelHeight = tilers[num].TilePixelHeight;
				int num2 = (int)Mathf.Log((float)tilePixelWidth, 2f);
				int num3 = (int)Mathf.Log((float)tilePixelHeight, 2f);
				int num4 = -1;
				int num5 = num2 - num4;
				int num6 = num3 - num4;
				commandBuffer.SetGlobalTexture(DeferredLights.ShaderConstants._BitmaskTex, depthInfoTextureIdentifier);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._DownsamplingWidth, tilePixelWidth);
				commandBuffer.SetGlobalInt(DeferredLights.ShaderConstants._DownsamplingHeight, tilePixelHeight);
				int minValue = int.MinValue;
				int num7 = this.RenderWidth + minValue - 1 >> 31;
				int num8 = this.RenderHeight + minValue - 1 >> 31;
				commandBuffer.SetGlobalVector("_BitmaskTexSize", new Vector4((float)num7, (float)num8, 1f / (float)num7, 1f / (float)num8));
				string text = null;
				if (num5 == 1 && num6 == 1)
				{
					text = "DOWNSAMPLING_SIZE_2";
				}
				else if (num5 == 2 && num6 == 2)
				{
					text = "DOWNSAMPLING_SIZE_4";
				}
				else if (num5 == 3 && num6 == 3)
				{
					text = "DOWNSAMPLING_SIZE_8";
				}
				if (text != null)
				{
					commandBuffer.EnableShaderKeyword(text);
				}
				commandBuffer.Blit(depthInfoTextureIdentifier, tileDepthInfoTextureIdentifier, this.m_TileDepthInfoMaterial, 1);
				if (text != null)
				{
					commandBuffer.DisableShaderKeyword(text);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002CA1C File Offset: 0x0002AC1C
		internal void ClearStencilPartial(CommandBuffer cmd)
		{
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerClearStencilPartialPass))
			{
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[5]);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002CA90 File Offset: 0x0002AC90
		internal void ExecuteDeferredPass(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_StencilDeferredPasses[0] < 0)
			{
				this.InitStencilDeferredMaterial();
			}
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(commandBuffer, DeferredLights.m_ProfilingDeferredPass))
			{
				CoreUtils.SetKeyword(commandBuffer, "_DEFERRED_MIXED_LIGHTING", this.UseShadowMask);
				this.SetupMatrixConstants(commandBuffer, ref renderingData);
				if (!this.HasStencilLightsOfType(LightType.Directional))
				{
					this.RenderSSAOBeforeShading(commandBuffer, ref renderingData);
				}
				this.RenderStencilLights(context, commandBuffer, ref renderingData);
				this.RenderTileLights(context, commandBuffer, ref renderingData);
				CoreUtils.SetKeyword(commandBuffer, "_DEFERRED_MIXED_LIGHTING", false);
				this.RenderFog(context, commandBuffer, ref renderingData);
			}
			CoreUtils.SetKeyword(commandBuffer, "_ADDITIONAL_LIGHT_SHADOWS", renderingData.shadowData.isKeywordAdditionalLightShadowsEnabled);
			CoreUtils.SetKeyword(commandBuffer, "_SHADOWS_SOFT", renderingData.shadowData.isKeywordSoftShadowsEnabled);
			CoreUtils.SetKeyword(commandBuffer, "_LIGHT_COOKIES", this.m_LightCookieManager.IsKeywordLightCookieEnabled);
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002CB84 File Offset: 0x0002AD84
		private void SetupShaderLightConstants(CommandBuffer cmd, ref RenderingData renderingData)
		{
			this.SetupMainLightConstants(cmd, ref renderingData.lightData);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002CB94 File Offset: 0x0002AD94
		private void SetupMainLightConstants(CommandBuffer cmd, ref LightData lightData)
		{
			if (lightData.mainLightIndex < 0)
			{
				return;
			}
			Vector4 vector;
			Vector4 vector2;
			Vector4 vector3;
			Vector4 vector4;
			Vector4 vector5;
			UniversalRenderPipeline.InitializeLightConstants_Common(lightData.visibleLights, lightData.mainLightIndex, out vector, out vector2, out vector3, out vector4, out vector5);
			uint lightLayerMask = (uint)lightData.visibleLights[lightData.mainLightIndex].light.GetUniversalAdditionalLightData().lightLayerMask;
			cmd.SetGlobalVector(DeferredLights.ShaderConstants._MainLightPosition, vector);
			cmd.SetGlobalVector(DeferredLights.ShaderConstants._MainLightColor, vector2);
			cmd.SetGlobalInt(DeferredLights.ShaderConstants._MainLightLayerMask, (int)lightLayerMask);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002CC14 File Offset: 0x0002AE14
		private void SetupMatrixConstants(CommandBuffer cmd, ref RenderingData renderingData)
		{
			ref CameraData ptr = ref renderingData.cameraData;
			int num = ((ptr.xr.enabled && ptr.xr.singlePassEnabled) ? 2 : 1);
			Matrix4x4[] screenToWorld = this.m_ScreenToWorld;
			for (int i = 0; i < num; i++)
			{
				Matrix4x4 projectionMatrix = ptr.GetProjectionMatrix(i);
				Matrix4x4 viewMatrix = ptr.GetViewMatrix(i);
				Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(projectionMatrix, false);
				Matrix4x4 matrix4x = new Matrix4x4(new Vector4(0.5f * (float)this.RenderWidth, 0f, 0f, 0f), new Vector4(0f, 0.5f * (float)this.RenderHeight, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0.5f * (float)this.RenderWidth, 0.5f * (float)this.RenderHeight, 0f, 1f));
				Matrix4x4 identity = Matrix4x4.identity;
				if (DeferredConfig.IsOpenGL)
				{
					identity = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 0.5f, 0f), new Vector4(0f, 0f, 0.5f, 1f));
				}
				screenToWorld[i] = Matrix4x4.Inverse(matrix4x * identity * gpuprojectionMatrix * viewMatrix);
			}
			cmd.SetGlobalMatrixArray(DeferredLights.ShaderConstants._ScreenToWorld, screenToWorld);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002CDAC File Offset: 0x0002AFAC
		private void SortLights(ref NativeArray<DeferredTiler.PrePunctualLight> prePunctualLights)
		{
			DeferredTiler.PrePunctualLight[] array = prePunctualLights.ToArray();
			Array.Sort<DeferredTiler.PrePunctualLight>(array, new SortPrePunctualLight());
			prePunctualLights.CopyFrom(array);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		private bool CheckHasTileLights(ref NativeArray<VisibleLight> visibleLights)
		{
			for (int i = 0; i < visibleLights.Length; i++)
			{
				if (this.IsTileLight(visibleLights[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002CE04 File Offset: 0x0002B004
		private void PrecomputeLights(out NativeArray<DeferredTiler.PrePunctualLight> prePunctualLights, out NativeArray<ushort> stencilVisLights, out NativeArray<ushort> stencilVisLightOffsets, ref NativeArray<VisibleLight> visibleLights, bool hasAdditionalLights, Matrix4x4 view, bool isOrthographic, float zNear)
		{
			if (!hasAdditionalLights)
			{
				prePunctualLights = new NativeArray<DeferredTiler.PrePunctualLight>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				stencilVisLights = new NativeArray<ushort>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				stencilVisLightOffsets = new NativeArray<ushort>(5, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < 5; i++)
				{
					stencilVisLightOffsets[i] = DeferredLights.k_InvalidLightOffset;
				}
				return;
			}
			NativeArray<int> nativeArray = new NativeArray<int>(5, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> nativeArray2 = new NativeArray<int>(5, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<int> nativeArray3 = new NativeArray<int>(5, Allocator.Temp, NativeArrayOptions.ClearMemory);
			stencilVisLightOffsets = new NativeArray<ushort>(5, Allocator.Temp, NativeArrayOptions.ClearMemory);
			ushort num = 0;
			while ((int)num < visibleLights.Length)
			{
				VisibleLight visibleLight = visibleLights[(int)num];
				if (this.TiledDeferredShading && this.IsTileLight(visibleLight))
				{
					int num2 = (int)visibleLight.lightType;
					int num3 = nativeArray[num2] + 1;
					nativeArray[num2] = num3;
				}
				else
				{
					int num3 = (int)visibleLight.lightType;
					ushort num4 = stencilVisLightOffsets[num3] + 1;
					stencilVisLightOffsets[num3] = num4;
				}
				num += 1;
			}
			int num5 = nativeArray[2] + nativeArray[0];
			int num6 = (int)(stencilVisLightOffsets[0] + stencilVisLightOffsets[1] + stencilVisLightOffsets[2]);
			prePunctualLights = new NativeArray<DeferredTiler.PrePunctualLight>(num5, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			stencilVisLights = new NativeArray<ushort>(num6, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			int j = 0;
			int num7 = 0;
			while (j < nativeArray.Length)
			{
				int num8 = nativeArray[j];
				nativeArray[j] = num7;
				num7 += num8;
				j++;
			}
			int k = 0;
			int num9 = 0;
			while (k < stencilVisLightOffsets.Length)
			{
				if (stencilVisLightOffsets[k] == 0)
				{
					stencilVisLightOffsets[k] = DeferredLights.k_InvalidLightOffset;
				}
				else
				{
					int num10 = (int)stencilVisLightOffsets[k];
					stencilVisLightOffsets[k] = (ushort)num9;
					num9 += num10;
				}
				k++;
			}
			ushort num11 = 0;
			while ((int)num11 < visibleLights.Length)
			{
				VisibleLight visibleLight2 = visibleLights[(int)num11];
				if (this.TiledDeferredShading && this.IsTileLight(visibleLight2))
				{
					DeferredTiler.PrePunctualLight prePunctualLight;
					prePunctualLight.posVS = view.MultiplyPoint(visibleLight2.localToWorldMatrix.GetColumn(3));
					prePunctualLight.radius = visibleLight2.range;
					prePunctualLight.minDist = math.max(0f, math.length(prePunctualLight.posVS) - prePunctualLight.radius);
					prePunctualLight.screenPos = new Vector2(prePunctualLight.posVS.x, prePunctualLight.posVS.y);
					if (!isOrthographic && prePunctualLight.posVS.z <= zNear)
					{
						prePunctualLight.screenPos *= -zNear / prePunctualLight.posVS.z;
					}
					prePunctualLight.visLightIndex = num11;
					int num3 = (int)visibleLight2.lightType;
					int num2 = nativeArray2[num3];
					nativeArray2[num3] = num2 + 1;
					int num12 = num2;
					prePunctualLights[nativeArray[(int)visibleLight2.lightType] + num12] = prePunctualLight;
				}
				else
				{
					int num2 = (int)visibleLight2.lightType;
					int num3 = nativeArray3[num2];
					nativeArray3[num2] = num3 + 1;
					int num13 = num3;
					stencilVisLights[(int)stencilVisLightOffsets[(int)visibleLight2.lightType] + num13] = num11;
				}
				num11 += 1;
			}
			nativeArray.Dispose();
			nativeArray2.Dispose();
			nativeArray3.Dispose();
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002D160 File Offset: 0x0002B360
		private void RenderTileLights(ScriptableRenderContext context, CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (!this.m_HasTileVisLights)
			{
				return;
			}
			if (this.m_TileDeferredMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_TileDeferredMaterial,
					base.GetType().Name
				});
				return;
			}
			if (this.m_TileDeferredPasses[0] < 0)
			{
				this.InitTileDeferredMaterial();
			}
			DeferredLights.DrawCall[] array = new DeferredLights.DrawCall[256];
			int num = 0;
			ref DeferredTiler ptr = ref this.m_Tilers[0];
			int num2 = 16;
			int num3 = num2 >> 4;
			int num4 = Marshal.SizeOf(typeof(PunctualLightData));
			int num5 = num4 >> 4;
			int tileXCount = ptr.TileXCount;
			int tileYCount = ptr.TileYCount;
			int maxLightPerTile = ptr.MaxLightPerTile;
			NativeArray<ushort> tiles = ptr.Tiles;
			NativeArray<uint> tileHeaders = ptr.TileHeaders;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			ComputeBuffer computeBuffer = DeferredShaderData.instance.ReserveBuffer<TileData>(this.m_MaxTilesPerBatch, DeferredConfig.UseCBufferForTileList);
			ComputeBuffer computeBuffer2 = DeferredShaderData.instance.ReserveBuffer<PunctualLightData>(this.m_MaxPunctualLightPerBatch, DeferredConfig.UseCBufferForLightData);
			ComputeBuffer computeBuffer3 = DeferredShaderData.instance.ReserveBuffer<uint>(this.m_MaxRelLightIndicesPerBatch, DeferredConfig.UseCBufferForLightList);
			NativeArray<uint4> nativeArray = new NativeArray<uint4>(this.m_MaxTilesPerBatch * num3, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<uint4> nativeArray2 = new NativeArray<uint4>(this.m_MaxPunctualLightPerBatch * num5, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<uint> nativeArray3 = new NativeArray<uint>(this.m_MaxRelLightIndicesPerBatch, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<ushort> nativeArray4 = new NativeArray<ushort>(maxLightPerTile, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<ushort> nativeArray5 = new NativeArray<ushort>(renderingData.lightData.visibleLights.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			BitArray bitArray = new BitArray(renderingData.lightData.visibleLights.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < tileYCount; i++)
			{
				for (int j = 0; j < tileXCount; j++)
				{
					int num10;
					int num11;
					ptr.GetTileOffsetAndCount(j, i, out num10, out num11);
					if (num11 != 0)
					{
						int num12 = this.TrimLights(ref nativeArray4, ref tiles, num10, num11, ref bitArray);
						bool flag = num7 == this.m_MaxTilesPerBatch;
						bool flag2 = num8 + num12 > this.m_MaxPunctualLightPerBatch;
						bool flag3 = num9 + num11 > this.m_MaxRelLightIndicesPerBatch;
						if (flag || flag2 || flag3)
						{
							array[num++] = new DeferredLights.DrawCall
							{
								tileList = computeBuffer,
								punctualLightBuffer = computeBuffer2,
								relLightList = computeBuffer3,
								tileListSize = num7 * num2,
								punctualLightBufferSize = num8 * num4,
								relLightListSize = DeferredLights.Align(num9, 4) * 4,
								instanceOffset = num6,
								instanceCount = num7 - num6
							};
							if (flag)
							{
								computeBuffer.SetData<uint4>(nativeArray, 0, 0, nativeArray.Length);
								computeBuffer = DeferredShaderData.instance.ReserveBuffer<TileData>(this.m_MaxTilesPerBatch, DeferredConfig.UseCBufferForTileList);
								num7 = 0;
							}
							if (flag2)
							{
								computeBuffer2.SetData<uint4>(nativeArray2, 0, 0, nativeArray2.Length);
								computeBuffer2 = DeferredShaderData.instance.ReserveBuffer<PunctualLightData>(this.m_MaxPunctualLightPerBatch, DeferredConfig.UseCBufferForLightData);
								num8 = 0;
								num12 = num11;
								for (int k = 0; k < num11; k++)
								{
									nativeArray4[k] = tiles[num10 + k];
								}
								bitArray.Clear();
							}
							if (flag3)
							{
								computeBuffer3.SetData<uint>(nativeArray3, 0, 0, nativeArray3.Length);
								computeBuffer3 = DeferredShaderData.instance.ReserveBuffer<uint>(this.m_MaxRelLightIndicesPerBatch, DeferredConfig.UseCBufferForLightList);
								num9 = 0;
							}
							num6 = num7;
						}
						int tileHeaderOffset = ptr.GetTileHeaderOffset(j, i);
						uint num13 = tileHeaders[tileHeaderOffset + 3];
						this.StoreTileData(ref nativeArray, num7, DeferredLights.PackTileID((uint)j, (uint)i), num13, (ushort)num9, (ushort)num11);
						num7++;
						for (int l = 0; l < num12; l++)
						{
							int num14 = (int)nativeArray4[l];
							this.StorePunctualLightData(ref nativeArray2, num8, ref renderingData.lightData.visibleLights, num14);
							nativeArray5[num14] = (ushort)num8;
							num8++;
							bitArray.Set(num14, true);
						}
						for (int m = 0; m < num11; m++)
						{
							ushort num15 = tiles[num10 + m];
							ushort num16 = tiles[num10 + num11 + m];
							ushort num17 = nativeArray5[(int)num15];
							nativeArray3[num9++] = (uint)((int)num17 | ((int)num16 << 16));
						}
					}
				}
			}
			int num18 = num7 - num6;
			if (num18 > 0)
			{
				computeBuffer.SetData<uint4>(nativeArray, 0, 0, nativeArray.Length);
				computeBuffer2.SetData<uint4>(nativeArray2, 0, 0, nativeArray2.Length);
				computeBuffer3.SetData<uint>(nativeArray3, 0, 0, nativeArray3.Length);
				array[num++] = new DeferredLights.DrawCall
				{
					tileList = computeBuffer,
					punctualLightBuffer = computeBuffer2,
					relLightList = computeBuffer3,
					tileListSize = num7 * num2,
					punctualLightBufferSize = num8 * num4,
					relLightListSize = DeferredLights.Align(num9, 4) * 4,
					instanceOffset = num6,
					instanceCount = num18
				};
			}
			nativeArray.Dispose();
			nativeArray2.Dispose();
			nativeArray3.Dispose();
			nativeArray4.Dispose();
			nativeArray5.Dispose();
			bitArray.Dispose();
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerDeferredTiledPass))
			{
				MeshTopology meshTopology = MeshTopology.Triangles;
				int num19 = 6;
				int tilePixelWidth = this.m_Tilers[0].TilePixelWidth;
				int tilePixelHeight = this.m_Tilers[0].TilePixelHeight;
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._TilePixelWidth, tilePixelWidth);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._TilePixelHeight, tilePixelHeight);
				cmd.SetGlobalTexture(this.TileDepthInfoTexture.id, this.TileDepthInfoTextureIdentifier);
				for (int n = 0; n < num; n++)
				{
					DeferredLights.DrawCall drawCall = array[n];
					if (DeferredConfig.UseCBufferForTileList)
					{
						cmd.SetGlobalConstantBuffer(drawCall.tileList, DeferredLights.ShaderConstants.UTileList, 0, drawCall.tileListSize);
					}
					else
					{
						cmd.SetGlobalBuffer(DeferredLights.ShaderConstants._TileList, drawCall.tileList);
					}
					if (DeferredConfig.UseCBufferForLightData)
					{
						cmd.SetGlobalConstantBuffer(drawCall.punctualLightBuffer, DeferredLights.ShaderConstants.UPunctualLightBuffer, 0, drawCall.punctualLightBufferSize);
					}
					else
					{
						cmd.SetGlobalBuffer(DeferredLights.ShaderConstants._PunctualLightBuffer, drawCall.punctualLightBuffer);
					}
					if (DeferredConfig.UseCBufferForLightList)
					{
						cmd.SetGlobalConstantBuffer(drawCall.relLightList, DeferredLights.ShaderConstants.URelLightList, 0, drawCall.relLightListSize);
					}
					else
					{
						cmd.SetGlobalBuffer(DeferredLights.ShaderConstants._RelLightList, drawCall.relLightList);
					}
					cmd.SetGlobalInt(DeferredLights.ShaderConstants._InstanceOffset, drawCall.instanceOffset);
					cmd.DrawProcedural(Matrix4x4.identity, this.m_TileDeferredMaterial, this.m_TileDeferredPasses[0], meshTopology, num19, drawCall.instanceCount);
					cmd.DrawProcedural(Matrix4x4.identity, this.m_TileDeferredMaterial, this.m_TileDeferredPasses[1], meshTopology, num19, drawCall.instanceCount);
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002D804 File Offset: 0x0002BA04
		private bool HasStencilLightsOfType(LightType type)
		{
			return this.m_stencilVisLightOffsets[(int)type] != DeferredLights.k_InvalidLightOffset;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002D81C File Offset: 0x0002BA1C
		private void RenderStencilLights(ScriptableRenderContext context, CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (this.m_stencilVisLights.Length == 0)
			{
				return;
			}
			if (this.m_StencilDeferredMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_StencilDeferredMaterial,
					base.GetType().Name
				});
				return;
			}
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerDeferredStencilPass))
			{
				NativeArray<VisibleLight> visibleLights = renderingData.lightData.visibleLights;
				if (this.HasStencilLightsOfType(LightType.Directional))
				{
					this.RenderStencilDirectionalLights(cmd, ref renderingData, visibleLights, renderingData.lightData.mainLightIndex);
				}
				if (this.HasStencilLightsOfType(LightType.Point))
				{
					this.RenderStencilPointLights(cmd, ref renderingData, visibleLights);
				}
				if (this.HasStencilLightsOfType(LightType.Spot))
				{
					this.RenderStencilSpotLights(cmd, ref renderingData, visibleLights);
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002D8E8 File Offset: 0x0002BAE8
		private void RenderStencilDirectionalLights(CommandBuffer cmd, ref RenderingData renderingData, NativeArray<VisibleLight> visibleLights, int mainLightIndex)
		{
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			cmd.EnableShaderKeyword("_DIRECTIONAL");
			bool flag = true;
			for (int i = (int)this.m_stencilVisLightOffsets[1]; i < this.m_stencilVisLights.Length; i++)
			{
				ushort num = this.m_stencilVisLights[i];
				VisibleLight visibleLight = visibleLights[(int)num];
				if (visibleLight.lightType != LightType.Directional)
				{
					break;
				}
				Vector4 vector;
				Vector4 vector2;
				Vector4 vector3;
				Vector4 vector4;
				Vector4 vector5;
				UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, (int)num, out vector, out vector2, out vector3, out vector4, out vector5);
				int num2 = 0;
				if (visibleLight.light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
				{
					num2 |= 4;
				}
				uint lightLayerMask = (uint)visibleLight.light.GetUniversalAdditionalLightData().lightLayerMask;
				bool flag2;
				if ((int)num == mainLightIndex)
				{
					flag2 = visibleLight.light && visibleLight.light.shadows > LightShadows.None;
					CoreUtils.SetKeyword(cmd, "_ADDITIONAL_LIGHT_SHADOWS", false);
				}
				else
				{
					int num3 = ((this.m_AdditionalLightsShadowCasterPass != null) ? this.m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex((int)num) : (-1));
					flag2 = visibleLight.light && visibleLight.light.shadows != LightShadows.None && num3 >= 0;
					CoreUtils.SetKeyword(cmd, "_ADDITIONAL_LIGHT_SHADOWS", flag2);
					cmd.SetGlobalInt(DeferredLights.ShaderConstants._ShadowLightIndex, num3);
				}
				bool flag3 = flag2 && renderingData.shadowData.supportsSoftShadows && visibleLight.light.shadows == LightShadows.Soft;
				CoreUtils.SetKeyword(cmd, "_SHADOWS_SOFT", flag3);
				CoreUtils.SetKeyword(cmd, "_DEFERRED_FIRST_LIGHT", flag);
				CoreUtils.SetKeyword(cmd, "_DEFERRED_MAIN_LIGHT", (int)num == mainLightIndex);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightColor, vector2);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightDirection, vector);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightFlags, num2);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightLayerMask, (int)lightLayerMask);
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[3]);
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[4]);
				flag = false;
			}
			cmd.DisableShaderKeyword("_DIRECTIONAL");
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002DB08 File Offset: 0x0002BD08
		private void RenderStencilPointLights(CommandBuffer cmd, ref RenderingData renderingData, NativeArray<VisibleLight> visibleLights)
		{
			if (this.m_SphereMesh == null)
			{
				this.m_SphereMesh = DeferredLights.CreateSphereMesh();
			}
			cmd.EnableShaderKeyword("_POINT");
			for (int i = (int)this.m_stencilVisLightOffsets[2]; i < this.m_stencilVisLights.Length; i++)
			{
				ushort num = this.m_stencilVisLights[i];
				VisibleLight visibleLight = visibleLights[(int)num];
				if (visibleLight.lightType != LightType.Point)
				{
					break;
				}
				Vector3 vector = visibleLight.localToWorldMatrix.GetColumn(3);
				Matrix4x4 matrix4x = new Matrix4x4(new Vector4(visibleLight.range, 0f, 0f, 0f), new Vector4(0f, visibleLight.range, 0f, 0f), new Vector4(0f, 0f, visibleLight.range, 0f), new Vector4(vector.x, vector.y, vector.z, 1f));
				Vector4 vector2;
				Vector4 vector3;
				Vector4 vector4;
				Vector4 vector5;
				Vector4 vector6;
				UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, (int)num, out vector2, out vector3, out vector4, out vector5, out vector6);
				uint lightLayerMask = (uint)visibleLight.light.GetUniversalAdditionalLightData().lightLayerMask;
				int num2 = 0;
				if (visibleLight.light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
				{
					num2 |= 4;
				}
				int num3 = ((this.m_AdditionalLightsShadowCasterPass != null) ? this.m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex((int)num) : (-1));
				bool flag = visibleLight.light && visibleLight.light.shadows != LightShadows.None && num3 >= 0;
				bool flag2 = flag && renderingData.shadowData.supportsSoftShadows && visibleLight.light.shadows == LightShadows.Soft;
				CoreUtils.SetKeyword(cmd, "_ADDITIONAL_LIGHT_SHADOWS", flag);
				CoreUtils.SetKeyword(cmd, "_SHADOWS_SOFT", flag2);
				int lightCookieShaderDataIndex = this.m_LightCookieManager.GetLightCookieShaderDataIndex((int)num);
				CoreUtils.SetKeyword(cmd, "_LIGHT_COOKIES", lightCookieShaderDataIndex >= 0);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightPosWS, vector2);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightColor, vector3);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightAttenuation, vector4);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightOcclusionProbInfo, vector6);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightFlags, num2);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._ShadowLightIndex, num3);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightLayerMask, (int)lightLayerMask);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._CookieLightIndex, lightCookieShaderDataIndex);
				cmd.DrawMesh(this.m_SphereMesh, matrix4x, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[0]);
				cmd.DrawMesh(this.m_SphereMesh, matrix4x, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[1]);
				cmd.DrawMesh(this.m_SphereMesh, matrix4x, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[2]);
			}
			cmd.DisableShaderKeyword("_POINT");
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002DDBC File Offset: 0x0002BFBC
		private void RenderStencilSpotLights(CommandBuffer cmd, ref RenderingData renderingData, NativeArray<VisibleLight> visibleLights)
		{
			if (this.m_HemisphereMesh == null)
			{
				this.m_HemisphereMesh = DeferredLights.CreateHemisphereMesh();
			}
			cmd.EnableShaderKeyword("_SPOT");
			for (int i = (int)this.m_stencilVisLightOffsets[0]; i < this.m_stencilVisLights.Length; i++)
			{
				ushort num = this.m_stencilVisLights[i];
				VisibleLight visibleLight = visibleLights[(int)num];
				if (visibleLight.lightType != LightType.Spot)
				{
					break;
				}
				float num2 = 0.017453292f * visibleLight.spotAngle * 0.5f;
				float num3 = Mathf.Cos(num2);
				float num4 = Mathf.Sin(num2);
				float num5 = Mathf.Lerp(1f, DeferredLights.kStencilShapeGuard, num4);
				Vector4 vector;
				Vector4 vector2;
				Vector4 vector3;
				Vector4 vector4;
				Vector4 vector5;
				UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, (int)num, out vector, out vector2, out vector3, out vector4, out vector5);
				uint lightLayerMask = (uint)visibleLight.light.GetUniversalAdditionalLightData().lightLayerMask;
				int num6 = 0;
				if (visibleLight.light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
				{
					num6 |= 4;
				}
				int num7 = ((this.m_AdditionalLightsShadowCasterPass != null) ? this.m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex((int)num) : (-1));
				bool flag = visibleLight.light && visibleLight.light.shadows != LightShadows.None && num7 >= 0;
				bool flag2 = flag && renderingData.shadowData.supportsSoftShadows && visibleLight.light.shadows == LightShadows.Soft;
				CoreUtils.SetKeyword(cmd, "_ADDITIONAL_LIGHT_SHADOWS", flag);
				CoreUtils.SetKeyword(cmd, "_SHADOWS_SOFT", flag2);
				int lightCookieShaderDataIndex = this.m_LightCookieManager.GetLightCookieShaderDataIndex((int)num);
				CoreUtils.SetKeyword(cmd, "_LIGHT_COOKIES", lightCookieShaderDataIndex >= 0);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._SpotLightScale, new Vector4(num4, num4, 1f - num3, visibleLight.range));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._SpotLightBias, new Vector4(0f, 0f, num3, 0f));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._SpotLightGuard, new Vector4(num5, num5, num5, num3 * visibleLight.range));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightPosWS, vector);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightColor, vector2);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightAttenuation, vector3);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightDirection, new Vector3(vector4.x, vector4.y, vector4.z));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightOcclusionProbInfo, vector5);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightFlags, num6);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._ShadowLightIndex, num7);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightLayerMask, (int)lightLayerMask);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._CookieLightIndex, lightCookieShaderDataIndex);
				cmd.DrawMesh(this.m_HemisphereMesh, visibleLight.localToWorldMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[0]);
				cmd.DrawMesh(this.m_HemisphereMesh, visibleLight.localToWorldMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[1]);
				cmd.DrawMesh(this.m_HemisphereMesh, visibleLight.localToWorldMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[2]);
			}
			cmd.DisableShaderKeyword("_SPOT");
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002E0B1 File Offset: 0x0002C2B1
		private void RenderSSAOBeforeShading(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[7]);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002E0EC File Offset: 0x0002C2EC
		private void RenderFog(ScriptableRenderContext context, CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (!RenderSettings.fog || renderingData.cameraData.camera.orthographic)
			{
				return;
			}
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerDeferredFogPass))
			{
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[6]);
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002E17C File Offset: 0x0002C37C
		private int TrimLights(ref NativeArray<ushort> trimmedLights, ref NativeArray<ushort> tiles, int offset, int lightCount, ref BitArray usedLights)
		{
			int num = 0;
			for (int i = 0; i < lightCount; i++)
			{
				ushort num2 = tiles[offset + i];
				if (!usedLights.IsSet((int)num2))
				{
					trimmedLights[num++] = num2;
				}
			}
			return num;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002E1BC File Offset: 0x0002C3BC
		private void StorePunctualLightData(ref NativeArray<uint4> punctualLightBuffer, int storeIndex, ref NativeArray<VisibleLight> visibleLights, int index)
		{
			int num = 0;
			if (visibleLights[index].light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
			{
				num |= 4;
			}
			Vector4 vector;
			Vector4 vector2;
			Vector4 vector3;
			Vector4 vector4;
			Vector4 vector5;
			UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, index, out vector, out vector2, out vector3, out vector4, out vector5);
			uint lightLayerMask = (uint)visibleLights[index].light.GetUniversalAdditionalLightData().lightLayerMask;
			punctualLightBuffer[storeIndex * 6] = new uint4(DeferredLights.FloatToUInt(vector.x), DeferredLights.FloatToUInt(vector.y), DeferredLights.FloatToUInt(vector.z), DeferredLights.FloatToUInt(visibleLights[index].range * visibleLights[index].range));
			punctualLightBuffer[storeIndex * 6 + 1] = new uint4(DeferredLights.FloatToUInt(vector2.x), DeferredLights.FloatToUInt(vector2.y), DeferredLights.FloatToUInt(vector2.z), 0U);
			punctualLightBuffer[storeIndex * 6 + 2] = new uint4(DeferredLights.FloatToUInt(vector3.x), DeferredLights.FloatToUInt(vector3.y), DeferredLights.FloatToUInt(vector3.z), DeferredLights.FloatToUInt(vector3.w));
			punctualLightBuffer[storeIndex * 6 + 3] = new uint4(DeferredLights.FloatToUInt(vector4.x), DeferredLights.FloatToUInt(vector4.y), DeferredLights.FloatToUInt(vector4.z), (uint)num);
			punctualLightBuffer[storeIndex * 6 + 4] = new uint4(DeferredLights.FloatToUInt(vector5.x), DeferredLights.FloatToUInt(vector5.y), DeferredLights.FloatToUInt(vector5.z), DeferredLights.FloatToUInt(vector5.w));
			punctualLightBuffer[storeIndex * 6 + 5] = new uint4(lightLayerMask, 0U, 0U, 0U);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002E370 File Offset: 0x0002C570
		private void StoreTileData(ref NativeArray<uint4> tileList, int storeIndex, uint tileID, uint listBitMask, ushort relLightOffset, ushort lightCount)
		{
			tileList[storeIndex] = new uint4
			{
				x = tileID,
				y = listBitMask,
				z = (uint)((int)relLightOffset | ((int)lightCount << 16)),
				w = 0U
			};
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002E3B8 File Offset: 0x0002C5B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsTileLight(VisibleLight visibleLight)
		{
			return (visibleLight.lightType == LightType.Point && (visibleLight.light == null || visibleLight.light.shadows == LightShadows.None)) || (visibleLight.lightType == LightType.Spot && (visibleLight.light == null || visibleLight.light.shadows == LightShadows.None));
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002E41C File Offset: 0x0002C61C
		private void InitTileDeferredMaterial()
		{
			if (this.m_TileDeferredMaterial == null)
			{
				return;
			}
			for (int i = 0; i < DeferredLights.k_TileDeferredPassNames.Length; i++)
			{
				this.m_TileDeferredPasses[i] = this.m_TileDeferredMaterial.FindPass(DeferredLights.k_TileDeferredPassNames[i]);
			}
			this.m_TileDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitStencilRef, 32f);
			this.m_TileDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitStencilReadMask, 96f);
			this.m_TileDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitStencilWriteMask, 0f);
			this.m_TileDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitStencilRef, 64f);
			this.m_TileDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitStencilReadMask, 96f);
			this.m_TileDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitStencilWriteMask, 0f);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
		private void InitStencilDeferredMaterial()
		{
			if (this.m_StencilDeferredMaterial == null)
			{
				return;
			}
			for (int i = 0; i < DeferredLights.k_StencilDeferredPassNames.Length; i++)
			{
				this.m_StencilDeferredPasses[i] = this.m_StencilDeferredMaterial.FindPass(DeferredLights.k_StencilDeferredPassNames[i]);
			}
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._StencilRef, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._StencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._StencilWriteMask, 16f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitPunctualStencilRef, 48f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitPunctualStencilReadMask, 112f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitPunctualStencilWriteMask, 16f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitPunctualStencilRef, 80f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitPunctualStencilReadMask, 112f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitPunctualStencilWriteMask, 16f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitDirStencilRef, 32f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitDirStencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitDirStencilWriteMask, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitDirStencilRef, 64f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitDirStencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitDirStencilWriteMask, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._ClearStencilRef, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._ClearStencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._ClearStencilWriteMask, 96f);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
		private static Mesh CreateSphereMesh()
		{
			Vector3[] array = new Vector3[]
			{
				new Vector3(0f, 0f, -1.07f),
				new Vector3(0.174f, -0.535f, -0.91f),
				new Vector3(-0.455f, -0.331f, -0.91f),
				new Vector3(0.562f, 0f, -0.91f),
				new Vector3(-0.455f, 0.331f, -0.91f),
				new Vector3(0.174f, 0.535f, -0.91f),
				new Vector3(-0.281f, -0.865f, -0.562f),
				new Vector3(0.736f, -0.535f, -0.562f),
				new Vector3(0.296f, -0.91f, -0.468f),
				new Vector3(-0.91f, 0f, -0.562f),
				new Vector3(-0.774f, -0.562f, -0.478f),
				new Vector3(0f, -1.07f, 0f),
				new Vector3(-0.629f, -0.865f, 0f),
				new Vector3(0.629f, -0.865f, 0f),
				new Vector3(-1.017f, -0.331f, 0f),
				new Vector3(0.957f, 0f, -0.478f),
				new Vector3(0.736f, 0.535f, -0.562f),
				new Vector3(1.017f, -0.331f, 0f),
				new Vector3(1.017f, 0.331f, 0f),
				new Vector3(-0.296f, -0.91f, 0.478f),
				new Vector3(0.281f, -0.865f, 0.562f),
				new Vector3(0.774f, -0.562f, 0.478f),
				new Vector3(-0.736f, -0.535f, 0.562f),
				new Vector3(0.91f, 0f, 0.562f),
				new Vector3(0.455f, -0.331f, 0.91f),
				new Vector3(-0.174f, -0.535f, 0.91f),
				new Vector3(0.629f, 0.865f, 0f),
				new Vector3(0.774f, 0.562f, 0.478f),
				new Vector3(0.455f, 0.331f, 0.91f),
				new Vector3(0f, 0f, 1.07f),
				new Vector3(-0.562f, 0f, 0.91f),
				new Vector3(-0.957f, 0f, 0.478f),
				new Vector3(0.281f, 0.865f, 0.562f),
				new Vector3(-0.174f, 0.535f, 0.91f),
				new Vector3(0.296f, 0.91f, -0.478f),
				new Vector3(-1.017f, 0.331f, 0f),
				new Vector3(-0.736f, 0.535f, 0.562f),
				new Vector3(-0.296f, 0.91f, 0.478f),
				new Vector3(0f, 1.07f, 0f),
				new Vector3(-0.281f, 0.865f, -0.562f),
				new Vector3(-0.774f, 0.562f, -0.478f),
				new Vector3(-0.629f, 0.865f, 0f)
			};
			int[] array2 = new int[]
			{
				0, 1, 2, 0, 3, 1, 2, 4, 0, 0,
				5, 3, 0, 4, 5, 1, 6, 2, 3, 7,
				1, 1, 8, 6, 1, 7, 8, 9, 4, 2,
				2, 6, 10, 10, 9, 2, 8, 11, 6, 6,
				12, 10, 11, 12, 6, 7, 13, 8, 8, 13,
				11, 10, 14, 9, 10, 12, 14, 3, 15, 7,
				5, 16, 3, 3, 16, 15, 15, 17, 7, 17,
				13, 7, 16, 18, 15, 15, 18, 17, 11, 19,
				12, 13, 20, 11, 11, 20, 19, 17, 21, 13,
				13, 21, 20, 12, 19, 22, 12, 22, 14, 17,
				23, 21, 18, 23, 17, 21, 24, 20, 23, 24,
				21, 20, 25, 19, 19, 25, 22, 24, 25, 20,
				26, 18, 16, 18, 27, 23, 26, 27, 18, 28,
				24, 23, 27, 28, 23, 24, 29, 25, 28, 29,
				24, 25, 30, 22, 25, 29, 30, 14, 22, 31,
				22, 30, 31, 32, 28, 27, 26, 32, 27, 33,
				29, 28, 30, 29, 33, 33, 28, 32, 34, 26,
				16, 5, 34, 16, 14, 31, 35, 14, 35, 9,
				31, 30, 36, 30, 33, 36, 35, 31, 36, 37,
				33, 32, 36, 33, 37, 38, 32, 26, 34, 38,
				26, 38, 37, 32, 5, 39, 34, 39, 38, 34,
				4, 39, 5, 9, 40, 4, 9, 35, 40, 4,
				40, 39, 35, 36, 41, 41, 36, 37, 41, 37,
				38, 40, 35, 41, 40, 41, 39, 41, 38, 39
			};
			return new Mesh
			{
				indexFormat = IndexFormat.UInt16,
				vertices = array,
				triangles = array2
			};
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002EB7C File Offset: 0x0002CD7C
		private static Mesh CreateHemisphereMesh()
		{
			Vector3[] array = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(0.92388f, 0.382683f, 0f),
				new Vector3(0.707107f, 0.707107f, 0f),
				new Vector3(0.382683f, 0.92388f, 0f),
				new Vector3(-0f, 1f, 0f),
				new Vector3(-0.382684f, 0.92388f, 0f),
				new Vector3(-0.707107f, 0.707107f, 0f),
				new Vector3(-0.92388f, 0.382683f, 0f),
				new Vector3(-1f, -0f, 0f),
				new Vector3(-0.92388f, -0.382683f, 0f),
				new Vector3(-0.707107f, -0.707107f, 0f),
				new Vector3(-0.382683f, -0.92388f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0.382684f, -0.923879f, 0f),
				new Vector3(0.707107f, -0.707107f, 0f),
				new Vector3(0.92388f, -0.382683f, 0f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0.707107f, 0f, 0.707107f),
				new Vector3(0f, -0.707107f, 0.707107f),
				new Vector3(0f, 0.707107f, 0.707107f),
				new Vector3(-0.707107f, 0f, 0.707107f),
				new Vector3(0.816497f, -0.408248f, 0.408248f),
				new Vector3(0.408248f, -0.408248f, 0.816497f),
				new Vector3(0.408248f, -0.816497f, 0.408248f),
				new Vector3(0.408248f, 0.816497f, 0.408248f),
				new Vector3(0.408248f, 0.408248f, 0.816497f),
				new Vector3(0.816497f, 0.408248f, 0.408248f),
				new Vector3(-0.816497f, 0.408248f, 0.408248f),
				new Vector3(-0.408248f, 0.408248f, 0.816497f),
				new Vector3(-0.408248f, 0.816497f, 0.408248f),
				new Vector3(-0.408248f, -0.816497f, 0.408248f),
				new Vector3(-0.408248f, -0.408248f, 0.816497f),
				new Vector3(-0.816497f, -0.408248f, 0.408248f),
				new Vector3(0f, -0.92388f, 0.382683f),
				new Vector3(0.92388f, 0f, 0.382683f),
				new Vector3(0f, -0.382683f, 0.92388f),
				new Vector3(0.382683f, 0f, 0.92388f),
				new Vector3(0f, 0.92388f, 0.382683f),
				new Vector3(0f, 0.382683f, 0.92388f),
				new Vector3(-0.92388f, 0f, 0.382683f),
				new Vector3(-0.382683f, 0f, 0.92388f)
			};
			int[] array2 = new int[]
			{
				0, 2, 1, 0, 3, 2, 0, 4, 3, 0,
				5, 4, 0, 6, 5, 0, 7, 6, 0, 8,
				7, 0, 9, 8, 0, 10, 9, 0, 11, 10,
				0, 12, 11, 0, 13, 12, 0, 14, 13, 0,
				15, 14, 0, 16, 15, 0, 1, 16, 22, 23,
				24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
				14, 24, 34, 35, 22, 16, 36, 23, 37, 2,
				27, 35, 38, 25, 4, 37, 26, 39, 6, 30,
				38, 40, 28, 8, 39, 29, 41, 10, 33, 40,
				34, 31, 12, 41, 32, 36, 15, 22, 24, 18,
				23, 22, 19, 24, 23, 3, 25, 27, 20, 26,
				25, 18, 27, 26, 7, 28, 30, 21, 29, 28,
				20, 30, 29, 11, 31, 33, 19, 32, 31, 21,
				33, 32, 13, 14, 34, 15, 24, 14, 19, 34,
				24, 1, 35, 16, 18, 22, 35, 15, 16, 22,
				17, 36, 37, 19, 23, 36, 18, 37, 23, 1,
				2, 35, 3, 27, 2, 18, 35, 27, 5, 38,
				4, 20, 25, 38, 3, 4, 25, 17, 37, 39,
				18, 26, 37, 20, 39, 26, 5, 6, 38, 7,
				30, 6, 20, 38, 30, 9, 40, 8, 21, 28,
				40, 7, 8, 28, 17, 39, 41, 20, 29, 39,
				21, 41, 29, 9, 10, 40, 11, 33, 10, 21,
				40, 33, 13, 34, 12, 19, 31, 34, 11, 12,
				31, 17, 41, 36, 21, 32, 41, 19, 36, 32
			};
			return new Mesh
			{
				indexFormat = IndexFormat.UInt16,
				vertices = array,
				triangles = array2
			};
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002F050 File Offset: 0x0002D250
		private static Mesh CreateFullscreenMesh()
		{
			Vector3[] array = new Vector3[]
			{
				new Vector3(-1f, 1f, 0f),
				new Vector3(-1f, -3f, 0f),
				new Vector3(3f, 1f, 0f)
			};
			int[] array2 = new int[] { 0, 1, 2 };
			return new Mesh
			{
				indexFormat = IndexFormat.UInt16,
				vertices = array,
				triangles = array2
			};
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002F0DE File Offset: 0x0002D2DE
		private static int Align(int s, int alignment)
		{
			return (s + alignment - 1) / alignment * alignment;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002F0E9 File Offset: 0x0002D2E9
		private static uint PackTileID(uint i, uint j)
		{
			return i | (j << 16);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		private static uint FloatToUInt(float val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			return (uint)((int)bytes[0] | ((int)bytes[1] << 8) | ((int)bytes[2] << 16) | ((int)bytes[3] << 24));
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002F120 File Offset: 0x0002D320
		private static uint Half2ToUInt(float x, float y)
		{
			uint num = (uint)Mathf.FloatToHalf(x);
			uint num2 = (uint)Mathf.FloatToHalf(y);
			return num | (num2 << 16);
		}

		// Token: 0x040006F1 RID: 1777
		private static readonly string[] k_GBufferNames = new string[] { "_GBuffer0", "_GBuffer1", "_GBuffer2", "_GBuffer3", "_GBuffer4", "_GBuffer5", "_GBuffer6" };

		// Token: 0x040006F2 RID: 1778
		private static readonly string[] k_TileDeferredPassNames = new string[] { "Tiled Deferred Punctual Light (Lit)", "Tiled Deferred Punctual Light (SimpleLit)" };

		// Token: 0x040006F3 RID: 1779
		private static readonly string[] k_StencilDeferredPassNames = new string[] { "Stencil Volume", "Deferred Punctual Light (Lit)", "Deferred Punctual Light (SimpleLit)", "Deferred Directional Light (Lit)", "Deferred Directional Light (SimpleLit)", "ClearStencilPartial", "Fog", "SSAOOnly" };

		// Token: 0x040006F4 RID: 1780
		private static readonly ushort k_InvalidLightOffset = ushort.MaxValue;

		// Token: 0x040006F5 RID: 1781
		private static readonly string k_SetupLights = "SetupLights";

		// Token: 0x040006F6 RID: 1782
		private static readonly string k_DeferredPass = "Deferred Pass";

		// Token: 0x040006F7 RID: 1783
		private static readonly string k_TileDepthInfo = "Tile Depth Info";

		// Token: 0x040006F8 RID: 1784
		private static readonly string k_DeferredTiledPass = "Deferred Shading (Tile-Based)";

		// Token: 0x040006F9 RID: 1785
		private static readonly string k_DeferredStencilPass = "Deferred Shading (Stencil)";

		// Token: 0x040006FA RID: 1786
		private static readonly string k_DeferredFogPass = "Deferred Fog";

		// Token: 0x040006FB RID: 1787
		private static readonly string k_ClearStencilPartial = "Clear Stencil Partial";

		// Token: 0x040006FC RID: 1788
		private static readonly string k_SetupLightConstants = "Setup Light Constants";

		// Token: 0x040006FD RID: 1789
		private static readonly float kStencilShapeGuard = 1.06067f;

		// Token: 0x040006FE RID: 1790
		private static readonly ProfilingSampler m_ProfilingSetupLights = new ProfilingSampler(DeferredLights.k_SetupLights);

		// Token: 0x040006FF RID: 1791
		private static readonly ProfilingSampler m_ProfilingDeferredPass = new ProfilingSampler(DeferredLights.k_DeferredPass);

		// Token: 0x04000700 RID: 1792
		private static readonly ProfilingSampler m_ProfilingTileDepthInfo = new ProfilingSampler(DeferredLights.k_TileDepthInfo);

		// Token: 0x04000701 RID: 1793
		private static readonly ProfilingSampler m_ProfilingSetupLightConstants = new ProfilingSampler(DeferredLights.k_SetupLightConstants);

		// Token: 0x04000706 RID: 1798
		private bool m_AccurateGbufferNormals;

		// Token: 0x04000719 RID: 1817
		private int m_CachedRenderWidth;

		// Token: 0x0400071A RID: 1818
		private int m_CachedRenderHeight;

		// Token: 0x0400071B RID: 1819
		private Matrix4x4 m_CachedProjectionMatrix;

		// Token: 0x0400071C RID: 1820
		private DeferredTiler[] m_Tilers;

		// Token: 0x0400071D RID: 1821
		private int[] m_TileDataCapacities;

		// Token: 0x0400071E RID: 1822
		private bool m_HasTileVisLights;

		// Token: 0x0400071F RID: 1823
		private NativeArray<ushort> m_stencilVisLights;

		// Token: 0x04000720 RID: 1824
		private NativeArray<ushort> m_stencilVisLightOffsets;

		// Token: 0x04000721 RID: 1825
		private AdditionalLightsShadowCasterPass m_AdditionalLightsShadowCasterPass;

		// Token: 0x04000722 RID: 1826
		private Mesh m_SphereMesh;

		// Token: 0x04000723 RID: 1827
		private Mesh m_HemisphereMesh;

		// Token: 0x04000724 RID: 1828
		private Mesh m_FullscreenMesh;

		// Token: 0x04000725 RID: 1829
		private int m_MaxDepthRangePerBatch;

		// Token: 0x04000726 RID: 1830
		private int m_MaxTilesPerBatch;

		// Token: 0x04000727 RID: 1831
		private int m_MaxPunctualLightPerBatch;

		// Token: 0x04000728 RID: 1832
		private int m_MaxRelLightIndicesPerBatch;

		// Token: 0x04000729 RID: 1833
		private Material m_TileDepthInfoMaterial;

		// Token: 0x0400072A RID: 1834
		private Material m_TileDeferredMaterial;

		// Token: 0x0400072B RID: 1835
		private Material m_StencilDeferredMaterial;

		// Token: 0x0400072C RID: 1836
		private int[] m_StencilDeferredPasses;

		// Token: 0x0400072D RID: 1837
		private int[] m_TileDeferredPasses;

		// Token: 0x0400072E RID: 1838
		private Matrix4x4[] m_ScreenToWorld = new Matrix4x4[2];

		// Token: 0x0400072F RID: 1839
		private ProfilingSampler m_ProfilingSamplerDeferredTiledPass = new ProfilingSampler(DeferredLights.k_DeferredTiledPass);

		// Token: 0x04000730 RID: 1840
		private ProfilingSampler m_ProfilingSamplerDeferredStencilPass = new ProfilingSampler(DeferredLights.k_DeferredStencilPass);

		// Token: 0x04000731 RID: 1841
		private ProfilingSampler m_ProfilingSamplerDeferredFogPass = new ProfilingSampler(DeferredLights.k_DeferredFogPass);

		// Token: 0x04000732 RID: 1842
		private ProfilingSampler m_ProfilingSamplerClearStencilPartialPass = new ProfilingSampler(DeferredLights.k_ClearStencilPartial);

		// Token: 0x04000733 RID: 1843
		private LightCookieManager m_LightCookieManager;

		// Token: 0x0200019C RID: 412
		internal static class ShaderConstants
		{
			// Token: 0x04000A37 RID: 2615
			public static readonly int _LitStencilRef = Shader.PropertyToID("_LitStencilRef");

			// Token: 0x04000A38 RID: 2616
			public static readonly int _LitStencilReadMask = Shader.PropertyToID("_LitStencilReadMask");

			// Token: 0x04000A39 RID: 2617
			public static readonly int _LitStencilWriteMask = Shader.PropertyToID("_LitStencilWriteMask");

			// Token: 0x04000A3A RID: 2618
			public static readonly int _SimpleLitStencilRef = Shader.PropertyToID("_SimpleLitStencilRef");

			// Token: 0x04000A3B RID: 2619
			public static readonly int _SimpleLitStencilReadMask = Shader.PropertyToID("_SimpleLitStencilReadMask");

			// Token: 0x04000A3C RID: 2620
			public static readonly int _SimpleLitStencilWriteMask = Shader.PropertyToID("_SimpleLitStencilWriteMask");

			// Token: 0x04000A3D RID: 2621
			public static readonly int _StencilRef = Shader.PropertyToID("_StencilRef");

			// Token: 0x04000A3E RID: 2622
			public static readonly int _StencilReadMask = Shader.PropertyToID("_StencilReadMask");

			// Token: 0x04000A3F RID: 2623
			public static readonly int _StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");

			// Token: 0x04000A40 RID: 2624
			public static readonly int _LitPunctualStencilRef = Shader.PropertyToID("_LitPunctualStencilRef");

			// Token: 0x04000A41 RID: 2625
			public static readonly int _LitPunctualStencilReadMask = Shader.PropertyToID("_LitPunctualStencilReadMask");

			// Token: 0x04000A42 RID: 2626
			public static readonly int _LitPunctualStencilWriteMask = Shader.PropertyToID("_LitPunctualStencilWriteMask");

			// Token: 0x04000A43 RID: 2627
			public static readonly int _SimpleLitPunctualStencilRef = Shader.PropertyToID("_SimpleLitPunctualStencilRef");

			// Token: 0x04000A44 RID: 2628
			public static readonly int _SimpleLitPunctualStencilReadMask = Shader.PropertyToID("_SimpleLitPunctualStencilReadMask");

			// Token: 0x04000A45 RID: 2629
			public static readonly int _SimpleLitPunctualStencilWriteMask = Shader.PropertyToID("_SimpleLitPunctualStencilWriteMask");

			// Token: 0x04000A46 RID: 2630
			public static readonly int _LitDirStencilRef = Shader.PropertyToID("_LitDirStencilRef");

			// Token: 0x04000A47 RID: 2631
			public static readonly int _LitDirStencilReadMask = Shader.PropertyToID("_LitDirStencilReadMask");

			// Token: 0x04000A48 RID: 2632
			public static readonly int _LitDirStencilWriteMask = Shader.PropertyToID("_LitDirStencilWriteMask");

			// Token: 0x04000A49 RID: 2633
			public static readonly int _SimpleLitDirStencilRef = Shader.PropertyToID("_SimpleLitDirStencilRef");

			// Token: 0x04000A4A RID: 2634
			public static readonly int _SimpleLitDirStencilReadMask = Shader.PropertyToID("_SimpleLitDirStencilReadMask");

			// Token: 0x04000A4B RID: 2635
			public static readonly int _SimpleLitDirStencilWriteMask = Shader.PropertyToID("_SimpleLitDirStencilWriteMask");

			// Token: 0x04000A4C RID: 2636
			public static readonly int _ClearStencilRef = Shader.PropertyToID("_ClearStencilRef");

			// Token: 0x04000A4D RID: 2637
			public static readonly int _ClearStencilReadMask = Shader.PropertyToID("_ClearStencilReadMask");

			// Token: 0x04000A4E RID: 2638
			public static readonly int _ClearStencilWriteMask = Shader.PropertyToID("_ClearStencilWriteMask");

			// Token: 0x04000A4F RID: 2639
			public static readonly int UDepthRanges = Shader.PropertyToID("UDepthRanges");

			// Token: 0x04000A50 RID: 2640
			public static readonly int _DepthRanges = Shader.PropertyToID("_DepthRanges");

			// Token: 0x04000A51 RID: 2641
			public static readonly int _DownsamplingWidth = Shader.PropertyToID("_DownsamplingWidth");

			// Token: 0x04000A52 RID: 2642
			public static readonly int _DownsamplingHeight = Shader.PropertyToID("_DownsamplingHeight");

			// Token: 0x04000A53 RID: 2643
			public static readonly int _SourceShiftX = Shader.PropertyToID("_SourceShiftX");

			// Token: 0x04000A54 RID: 2644
			public static readonly int _SourceShiftY = Shader.PropertyToID("_SourceShiftY");

			// Token: 0x04000A55 RID: 2645
			public static readonly int _TileShiftX = Shader.PropertyToID("_TileShiftX");

			// Token: 0x04000A56 RID: 2646
			public static readonly int _TileShiftY = Shader.PropertyToID("_TileShiftY");

			// Token: 0x04000A57 RID: 2647
			public static readonly int _tileXCount = Shader.PropertyToID("_tileXCount");

			// Token: 0x04000A58 RID: 2648
			public static readonly int _DepthRangeOffset = Shader.PropertyToID("_DepthRangeOffset");

			// Token: 0x04000A59 RID: 2649
			public static readonly int _BitmaskTex = Shader.PropertyToID("_BitmaskTex");

			// Token: 0x04000A5A RID: 2650
			public static readonly int UTileList = Shader.PropertyToID("UTileList");

			// Token: 0x04000A5B RID: 2651
			public static readonly int _TileList = Shader.PropertyToID("_TileList");

			// Token: 0x04000A5C RID: 2652
			public static readonly int UPunctualLightBuffer = Shader.PropertyToID("UPunctualLightBuffer");

			// Token: 0x04000A5D RID: 2653
			public static readonly int _PunctualLightBuffer = Shader.PropertyToID("_PunctualLightBuffer");

			// Token: 0x04000A5E RID: 2654
			public static readonly int URelLightList = Shader.PropertyToID("URelLightList");

			// Token: 0x04000A5F RID: 2655
			public static readonly int _RelLightList = Shader.PropertyToID("_RelLightList");

			// Token: 0x04000A60 RID: 2656
			public static readonly int _TilePixelWidth = Shader.PropertyToID("_TilePixelWidth");

			// Token: 0x04000A61 RID: 2657
			public static readonly int _TilePixelHeight = Shader.PropertyToID("_TilePixelHeight");

			// Token: 0x04000A62 RID: 2658
			public static readonly int _InstanceOffset = Shader.PropertyToID("_InstanceOffset");

			// Token: 0x04000A63 RID: 2659
			public static readonly int _DepthTex = Shader.PropertyToID("_DepthTex");

			// Token: 0x04000A64 RID: 2660
			public static readonly int _DepthTexSize = Shader.PropertyToID("_DepthTexSize");

			// Token: 0x04000A65 RID: 2661
			public static readonly int _ScreenToWorld = Shader.PropertyToID("_ScreenToWorld");

			// Token: 0x04000A66 RID: 2662
			public static readonly int _unproject0 = Shader.PropertyToID("_unproject0");

			// Token: 0x04000A67 RID: 2663
			public static readonly int _unproject1 = Shader.PropertyToID("_unproject1");

			// Token: 0x04000A68 RID: 2664
			public static int _MainLightPosition = Shader.PropertyToID("_MainLightPosition");

			// Token: 0x04000A69 RID: 2665
			public static int _MainLightColor = Shader.PropertyToID("_MainLightColor");

			// Token: 0x04000A6A RID: 2666
			public static int _MainLightLayerMask = Shader.PropertyToID("_MainLightLayerMask");

			// Token: 0x04000A6B RID: 2667
			public static int _SpotLightScale = Shader.PropertyToID("_SpotLightScale");

			// Token: 0x04000A6C RID: 2668
			public static int _SpotLightBias = Shader.PropertyToID("_SpotLightBias");

			// Token: 0x04000A6D RID: 2669
			public static int _SpotLightGuard = Shader.PropertyToID("_SpotLightGuard");

			// Token: 0x04000A6E RID: 2670
			public static int _LightPosWS = Shader.PropertyToID("_LightPosWS");

			// Token: 0x04000A6F RID: 2671
			public static int _LightColor = Shader.PropertyToID("_LightColor");

			// Token: 0x04000A70 RID: 2672
			public static int _LightAttenuation = Shader.PropertyToID("_LightAttenuation");

			// Token: 0x04000A71 RID: 2673
			public static int _LightOcclusionProbInfo = Shader.PropertyToID("_LightOcclusionProbInfo");

			// Token: 0x04000A72 RID: 2674
			public static int _LightDirection = Shader.PropertyToID("_LightDirection");

			// Token: 0x04000A73 RID: 2675
			public static int _LightFlags = Shader.PropertyToID("_LightFlags");

			// Token: 0x04000A74 RID: 2676
			public static int _ShadowLightIndex = Shader.PropertyToID("_ShadowLightIndex");

			// Token: 0x04000A75 RID: 2677
			public static int _LightLayerMask = Shader.PropertyToID("_LightLayerMask");

			// Token: 0x04000A76 RID: 2678
			public static int _CookieLightIndex = Shader.PropertyToID("_CookieLightIndex");
		}

		// Token: 0x0200019D RID: 413
		private struct CullLightsJob : IJob
		{
			// Token: 0x06000A25 RID: 2597 RVA: 0x00042528 File Offset: 0x00040728
			public void Execute()
			{
				int num = (int)this.coarseTileHeaders[this.coarseHeaderOffset];
				int num2 = (int)this.coarseTileHeaders[this.coarseHeaderOffset + 1];
				if (this.tiler.TilerLevel != 0)
				{
					this.tiler.CullIntermediateLights(ref this.prePunctualLights, ref this.coarseTiles, num, num2, this.istart, this.iend, this.jstart, this.jend);
					return;
				}
				this.tiler.CullFinalLights(ref this.prePunctualLights, ref this.coarseTiles, num, num2, this.istart, this.iend, this.jstart, this.jend);
			}

			// Token: 0x04000A77 RID: 2679
			public DeferredTiler tiler;

			// Token: 0x04000A78 RID: 2680
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<DeferredTiler.PrePunctualLight> prePunctualLights;

			// Token: 0x04000A79 RID: 2681
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<ushort> coarseTiles;

			// Token: 0x04000A7A RID: 2682
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<uint> coarseTileHeaders;

			// Token: 0x04000A7B RID: 2683
			public int coarseHeaderOffset;

			// Token: 0x04000A7C RID: 2684
			public int istart;

			// Token: 0x04000A7D RID: 2685
			public int iend;

			// Token: 0x04000A7E RID: 2686
			public int jstart;

			// Token: 0x04000A7F RID: 2687
			public int jend;
		}

		// Token: 0x0200019E RID: 414
		private struct DrawCall
		{
			// Token: 0x04000A80 RID: 2688
			public ComputeBuffer tileList;

			// Token: 0x04000A81 RID: 2689
			public ComputeBuffer punctualLightBuffer;

			// Token: 0x04000A82 RID: 2690
			public ComputeBuffer relLightList;

			// Token: 0x04000A83 RID: 2691
			public int tileListSize;

			// Token: 0x04000A84 RID: 2692
			public int punctualLightBufferSize;

			// Token: 0x04000A85 RID: 2693
			public int relLightListSize;

			// Token: 0x04000A86 RID: 2694
			public int instanceOffset;

			// Token: 0x04000A87 RID: 2695
			public int instanceCount;
		}

		// Token: 0x0200019F RID: 415
		internal enum TileDeferredPasses
		{
			// Token: 0x04000A89 RID: 2697
			PunctualLit,
			// Token: 0x04000A8A RID: 2698
			PunctualSimpleLit
		}

		// Token: 0x020001A0 RID: 416
		internal enum StencilDeferredPasses
		{
			// Token: 0x04000A8C RID: 2700
			StencilVolume,
			// Token: 0x04000A8D RID: 2701
			PunctualLit,
			// Token: 0x04000A8E RID: 2702
			PunctualSimpleLit,
			// Token: 0x04000A8F RID: 2703
			DirectionalLit,
			// Token: 0x04000A90 RID: 2704
			DirectionalSimpleLit,
			// Token: 0x04000A91 RID: 2705
			ClearStencilPartial,
			// Token: 0x04000A92 RID: 2706
			Fog,
			// Token: 0x04000A93 RID: 2707
			SSAOOnly
		}

		// Token: 0x020001A1 RID: 417
		internal struct InitParams
		{
			// Token: 0x04000A94 RID: 2708
			public Material tileDepthInfoMaterial;

			// Token: 0x04000A95 RID: 2709
			public Material tileDeferredMaterial;

			// Token: 0x04000A96 RID: 2710
			public Material stencilDeferredMaterial;

			// Token: 0x04000A97 RID: 2711
			public LightCookieManager lightCookieManager;
		}
	}
}
