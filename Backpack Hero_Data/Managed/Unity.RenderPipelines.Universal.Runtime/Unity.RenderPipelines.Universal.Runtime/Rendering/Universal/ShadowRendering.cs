using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000037 RID: 55
	internal static class ShadowRendering
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000112A3 File Offset: 0x0000F4A3
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000112AA File Offset: 0x0000F4AA
		public static uint maxTextureCount { get; private set; }

		// Token: 0x06000230 RID: 560 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public static void InitializeBudget(uint maxTextureCount)
		{
			if (ShadowRendering.m_RenderTargets == null || (long)ShadowRendering.m_RenderTargets.Length != (long)((ulong)maxTextureCount))
			{
				ShadowRendering.m_RenderTargets = new RenderTargetHandle[maxTextureCount];
				ShadowRendering.maxTextureCount = maxTextureCount;
				int num = 0;
				while ((long)num < (long)((ulong)maxTextureCount))
				{
					ShadowRendering.m_RenderTargets[num].id = Shader.PropertyToID(string.Format("ShadowTex_{0}", num));
					num++;
				}
			}
			if (ShadowRendering.m_LightInputTextures == null || (long)ShadowRendering.m_LightInputTextures.Length != (long)((ulong)maxTextureCount))
			{
				ShadowRendering.m_LightInputTextures = new RenderTargetIdentifier[maxTextureCount];
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00011338 File Offset: 0x0000F538
		private static Material[] CreateMaterials(Shader shader, int pass = 0)
		{
			Material[] array = new Material[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = CoreUtils.CreateEngineMaterial(shader);
				array[i].SetInt(ShadowRendering.k_ShadowColorMaskID, 1 << i);
				array[i].SetPass(pass);
			}
			return array;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00011380 File Offset: 0x0000F580
		private static Material GetProjectedShadowMaterial(this Renderer2DData rendererData, int colorIndex)
		{
			if (rendererData.projectedShadowMaterial == null || rendererData.projectedShadowMaterial.Length == 0 || rendererData.projectedShadowShader != rendererData.projectedShadowMaterial[0].shader)
			{
				rendererData.projectedShadowMaterial = ShadowRendering.CreateMaterials(rendererData.projectedShadowShader, 0);
			}
			return rendererData.projectedShadowMaterial[colorIndex];
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000113D4 File Offset: 0x0000F5D4
		private static Material GetStencilOnlyShadowMaterial(this Renderer2DData rendererData, int colorIndex)
		{
			if (rendererData.stencilOnlyShadowMaterial == null || rendererData.stencilOnlyShadowMaterial.Length == 0 || rendererData.projectedShadowShader != rendererData.stencilOnlyShadowMaterial[0].shader)
			{
				rendererData.stencilOnlyShadowMaterial = ShadowRendering.CreateMaterials(rendererData.projectedShadowShader, 1);
			}
			return rendererData.stencilOnlyShadowMaterial[colorIndex];
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00011428 File Offset: 0x0000F628
		private static Material GetSpriteSelfShadowMaterial(this Renderer2DData rendererData, int colorIndex)
		{
			if (rendererData.spriteSelfShadowMaterial == null || rendererData.spriteSelfShadowMaterial.Length == 0 || rendererData.spriteShadowShader != rendererData.spriteSelfShadowMaterial[0].shader)
			{
				rendererData.spriteSelfShadowMaterial = ShadowRendering.CreateMaterials(rendererData.spriteShadowShader, 0);
			}
			return rendererData.spriteSelfShadowMaterial[colorIndex];
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001147C File Offset: 0x0000F67C
		private static Material GetSpriteUnshadowMaterial(this Renderer2DData rendererData, int colorIndex)
		{
			if (rendererData.spriteUnshadowMaterial == null || rendererData.spriteUnshadowMaterial.Length == 0 || rendererData.spriteUnshadowShader != rendererData.spriteUnshadowMaterial[0].shader)
			{
				rendererData.spriteUnshadowMaterial = ShadowRendering.CreateMaterials(rendererData.spriteUnshadowShader, 0);
			}
			return rendererData.spriteUnshadowMaterial[colorIndex];
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000114D0 File Offset: 0x0000F6D0
		private static Material GetGeometryUnshadowMaterial(this Renderer2DData rendererData, int colorIndex)
		{
			if (rendererData.geometryUnshadowMaterial == null || rendererData.geometryUnshadowMaterial.Length == 0 || rendererData.geometryUnshadowShader != rendererData.geometryUnshadowMaterial[0].shader)
			{
				rendererData.geometryUnshadowMaterial = ShadowRendering.CreateMaterials(rendererData.geometryUnshadowShader, 0);
			}
			return rendererData.geometryUnshadowMaterial[colorIndex];
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00011522 File Offset: 0x0000F722
		public static void CreateShadowRenderTexture(IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmdBuffer, int shadowIndex)
		{
			ShadowRendering.CreateShadowRenderTexture(pass, ShadowRendering.m_RenderTargets[shadowIndex], renderingData, cmdBuffer);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00011538 File Offset: 0x0000F738
		public static bool PrerenderShadows(IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmdBuffer, int layerToRender, Light2D light, int shadowIndex, float shadowIntensity)
		{
			int num = shadowIndex % 4;
			int num2 = shadowIndex / 4;
			if (num == 0)
			{
				ShadowRendering.CreateShadowRenderTexture(pass, renderingData, cmdBuffer, num2);
			}
			bool flag = ShadowRendering.RenderShadows(pass, renderingData, cmdBuffer, layerToRender, light, shadowIntensity, ShadowRendering.m_RenderTargets[num2].Identifier(), num);
			if (ShadowRendering.RenderShadows(pass, renderingData, cmdBuffer, layerToRender, light, shadowIntensity, ShadowRendering.m_RenderTargets[num2].Identifier(), num))
			{
				ShadowRendering.m_LightInputTextures[num2] = ShadowRendering.m_RenderTargets[num2].Identifier();
				return flag;
			}
			ShadowRendering.m_LightInputTextures[num2] = Texture2D.blackTexture;
			return flag;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000115CC File Offset: 0x0000F7CC
		public static void SetGlobalShadowTexture(CommandBuffer cmdBuffer, Light2D light, int shadowIndex)
		{
			int num = shadowIndex % 4;
			int num2 = shadowIndex / 4;
			cmdBuffer.SetGlobalTexture("_ShadowTex", ShadowRendering.m_LightInputTextures[num2]);
			cmdBuffer.SetGlobalColor(ShadowRendering.k_ShadowColorMaskID, ShadowRendering.k_ColorLookup[num]);
			cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowIntensityID, 1f - light.shadowIntensity);
			cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowVolumeIntensityID, 1f - light.shadowVolumeIntensity);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001163B File Offset: 0x0000F83B
		public static void DisableGlobalShadowTexture(CommandBuffer cmdBuffer)
		{
			cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowIntensityID, 1f);
			cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowVolumeIntensityID, 1f);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00011660 File Offset: 0x0000F860
		private static void CreateShadowRenderTexture(IRenderPass2D pass, RenderTargetHandle rtHandle, RenderingData renderingData, CommandBuffer cmdBuffer)
		{
			float num = Mathf.Clamp(pass.rendererData.lightRenderTextureScale, 0.01f, 1f);
			int num2 = (int)((float)renderingData.cameraData.cameraTargetDescriptor.width * num);
			int num3 = (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * num);
			RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor(num2, num3);
			renderTextureDescriptor.useMipMap = false;
			renderTextureDescriptor.autoGenerateMips = false;
			renderTextureDescriptor.depthBufferBits = 24;
			renderTextureDescriptor.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
			renderTextureDescriptor.msaaSamples = 1;
			renderTextureDescriptor.dimension = TextureDimension.Tex2D;
			cmdBuffer.GetTemporaryRT(rtHandle.id, renderTextureDescriptor, FilterMode.Bilinear);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00011700 File Offset: 0x0000F900
		public static void ReleaseShadowRenderTexture(CommandBuffer cmdBuffer, int shadowIndex)
		{
			bool flag = shadowIndex % 4 != 0;
			int num = shadowIndex / 4;
			if (!flag)
			{
				cmdBuffer.ReleaseTemporaryRT(ShadowRendering.m_RenderTargets[num].id);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001172C File Offset: 0x0000F92C
		public static void SetShadowProjectionGlobals(CommandBuffer cmdBuffer, ShadowCaster2D shadowCaster)
		{
			cmdBuffer.SetGlobalVector(ShadowRendering.k_ShadowModelScaleID, shadowCaster.m_CachedLossyScale);
			cmdBuffer.SetGlobalMatrix(ShadowRendering.k_ShadowModelMatrixID, shadowCaster.m_CachedShadowMatrix);
			cmdBuffer.SetGlobalMatrix(ShadowRendering.k_ShadowModelInvMatrixID, shadowCaster.m_CachedInverseShadowMatrix);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00011768 File Offset: 0x0000F968
		public static bool RenderShadows(IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmdBuffer, int layerToRender, Light2D light, float shadowIntensity, RenderTargetIdentifier renderTexture, int colorBit)
		{
			bool flag2;
			using (new ProfilingScope(cmdBuffer, ShadowRendering.m_ProfilingSamplerShadows))
			{
				bool flag = false;
				List<ShadowCasterGroup2D> shadowCasterGroups = ShadowCasterGroup2DManager.shadowCasterGroups;
				if (shadowCasterGroups != null && shadowCasterGroups.Count > 0)
				{
					for (int i = 0; i < shadowCasterGroups.Count; i++)
					{
						List<ShadowCaster2D> shadowCasters = shadowCasterGroups[i].GetShadowCasters();
						if (shadowCasters != null)
						{
							for (int j = 0; j < shadowCasters.Count; j++)
							{
								ShadowCaster2D shadowCaster2D = shadowCasters[j];
								if (shadowCaster2D != null && shadowCaster2D.IsLit(light) && shadowCaster2D.IsShadowedLayer(layerToRender))
								{
									flag = true;
									break;
								}
							}
						}
					}
					if (flag)
					{
						cmdBuffer.SetRenderTarget(renderTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
						using (new ProfilingScope(cmdBuffer, ShadowRendering.m_ProfilingSamplerShadowColorsLookup[colorBit]))
						{
							if (colorBit == 0)
							{
								cmdBuffer.ClearRenderTarget(true, true, Color.clear);
							}
							else
							{
								cmdBuffer.ClearRenderTarget(true, false, Color.clear);
							}
							float radius = light.boundingSphere.radius;
							cmdBuffer.SetGlobalVector(ShadowRendering.k_LightPosID, light.transform.position);
							cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowRadiusID, radius);
							cmdBuffer.SetGlobalColor(ShadowRendering.k_ShadowColorMaskID, ShadowRendering.k_ColorLookup[colorBit]);
							Material geometryUnshadowMaterial = pass.rendererData.GetGeometryUnshadowMaterial(colorBit);
							Material projectedShadowMaterial = pass.rendererData.GetProjectedShadowMaterial(colorBit);
							Material spriteSelfShadowMaterial = pass.rendererData.GetSpriteSelfShadowMaterial(colorBit);
							Material spriteUnshadowMaterial = pass.rendererData.GetSpriteUnshadowMaterial(colorBit);
							pass.rendererData.GetStencilOnlyShadowMaterial(colorBit);
							for (int k = 0; k < shadowCasterGroups.Count; k++)
							{
								List<ShadowCaster2D> shadowCasters2 = shadowCasterGroups[k].GetShadowCasters();
								if (shadowCasters2 != null)
								{
									for (int l = 0; l < shadowCasters2.Count; l++)
									{
										ShadowCaster2D shadowCaster2D2 = shadowCasters2[l];
										if (shadowCaster2D2.IsLit(light) && shadowCaster2D2 != null && projectedShadowMaterial != null && shadowCaster2D2.IsShadowedLayer(layerToRender) && shadowCaster2D2.castsShadows)
										{
											ShadowRendering.SetShadowProjectionGlobals(cmdBuffer, shadowCaster2D2);
											cmdBuffer.DrawMesh(shadowCaster2D2.mesh, shadowCaster2D2.m_CachedLocalToWorldMatrix, geometryUnshadowMaterial, 0, 0);
											cmdBuffer.DrawMesh(shadowCaster2D2.mesh, shadowCaster2D2.m_CachedLocalToWorldMatrix, projectedShadowMaterial, 0, 0);
											cmdBuffer.DrawMesh(shadowCaster2D2.mesh, shadowCaster2D2.m_CachedLocalToWorldMatrix, geometryUnshadowMaterial, 0, 1);
										}
									}
									for (int m = 0; m < shadowCasters2.Count; m++)
									{
										ShadowCaster2D shadowCaster2D3 = shadowCasters2[m];
										if (shadowCaster2D3.IsLit(light) && shadowCaster2D3 != null && shadowCaster2D3.IsShadowedLayer(layerToRender))
										{
											if (shadowCaster2D3.useRendererSilhouette)
											{
												Renderer renderer = null;
												shadowCaster2D3.TryGetComponent<Renderer>(out renderer);
												if (renderer != null)
												{
													Material material = (shadowCaster2D3.selfShadows ? spriteSelfShadowMaterial : spriteUnshadowMaterial);
													if (material != null)
													{
														cmdBuffer.DrawRenderer(renderer, material);
													}
												}
											}
											else
											{
												Matrix4x4 cachedLocalToWorldMatrix = shadowCaster2D3.m_CachedLocalToWorldMatrix;
												Material material2 = (shadowCaster2D3.selfShadows ? spriteSelfShadowMaterial : spriteUnshadowMaterial);
												if (material2 != null)
												{
													cmdBuffer.DrawMesh(shadowCaster2D3.mesh, cachedLocalToWorldMatrix, material2);
												}
											}
										}
									}
									for (int n = 0; n < shadowCasters2.Count; n++)
									{
										ShadowCaster2D shadowCaster2D4 = shadowCasters2[n];
										if (shadowCaster2D4.IsLit(light) && shadowCaster2D4 != null && projectedShadowMaterial != null && shadowCaster2D4.IsShadowedLayer(layerToRender) && shadowCaster2D4.castsShadows)
										{
											ShadowRendering.SetShadowProjectionGlobals(cmdBuffer, shadowCaster2D4);
											cmdBuffer.DrawMesh(shadowCaster2D4.mesh, shadowCaster2D4.m_CachedLocalToWorldMatrix, projectedShadowMaterial, 0, 1);
										}
									}
								}
							}
						}
					}
				}
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x04000175 RID: 373
		private static readonly int k_LightPosID = Shader.PropertyToID("_LightPos");

		// Token: 0x04000176 RID: 374
		private static readonly int k_SelfShadowingID = Shader.PropertyToID("_SelfShadowing");

		// Token: 0x04000177 RID: 375
		private static readonly int k_ShadowStencilGroupID = Shader.PropertyToID("_ShadowStencilGroup");

		// Token: 0x04000178 RID: 376
		private static readonly int k_ShadowIntensityID = Shader.PropertyToID("_ShadowIntensity");

		// Token: 0x04000179 RID: 377
		private static readonly int k_ShadowVolumeIntensityID = Shader.PropertyToID("_ShadowVolumeIntensity");

		// Token: 0x0400017A RID: 378
		private static readonly int k_ShadowRadiusID = Shader.PropertyToID("_ShadowRadius");

		// Token: 0x0400017B RID: 379
		private static readonly int k_ShadowColorMaskID = Shader.PropertyToID("_ShadowColorMask");

		// Token: 0x0400017C RID: 380
		private static readonly int k_ShadowModelMatrixID = Shader.PropertyToID("_ShadowModelMatrix");

		// Token: 0x0400017D RID: 381
		private static readonly int k_ShadowModelInvMatrixID = Shader.PropertyToID("_ShadowModelInvMatrix");

		// Token: 0x0400017E RID: 382
		private static readonly int k_ShadowModelScaleID = Shader.PropertyToID("_ShadowModelScale");

		// Token: 0x0400017F RID: 383
		private static readonly ProfilingSampler m_ProfilingSamplerShadows = new ProfilingSampler("Draw 2D Shadow Texture");

		// Token: 0x04000180 RID: 384
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsA = new ProfilingSampler("Draw 2D Shadows (A)");

		// Token: 0x04000181 RID: 385
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsR = new ProfilingSampler("Draw 2D Shadows (R)");

		// Token: 0x04000182 RID: 386
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsG = new ProfilingSampler("Draw 2D Shadows (G)");

		// Token: 0x04000183 RID: 387
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsB = new ProfilingSampler("Draw 2D Shadows (B)");

		// Token: 0x04000184 RID: 388
		private static RenderTargetHandle[] m_RenderTargets = null;

		// Token: 0x04000185 RID: 389
		private static RenderTargetIdentifier[] m_LightInputTextures = null;

		// Token: 0x04000186 RID: 390
		private static readonly Color[] k_ColorLookup = new Color[]
		{
			new Color(0f, 0f, 0f, 1f),
			new Color(0f, 0f, 1f, 0f),
			new Color(0f, 1f, 0f, 0f),
			new Color(1f, 0f, 0f, 0f)
		};

		// Token: 0x04000187 RID: 391
		private static readonly ProfilingSampler[] m_ProfilingSamplerShadowColorsLookup = new ProfilingSampler[]
		{
			ShadowRendering.m_ProfilingSamplerShadowsA,
			ShadowRendering.m_ProfilingSamplerShadowsB,
			ShadowRendering.m_ProfilingSamplerShadowsG,
			ShadowRendering.m_ProfilingSamplerShadowsR
		};
	}
}
