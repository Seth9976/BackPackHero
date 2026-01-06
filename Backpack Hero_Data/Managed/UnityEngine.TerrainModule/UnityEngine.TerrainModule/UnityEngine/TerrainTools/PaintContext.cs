using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.TerrainUtils;

namespace UnityEngine.TerrainTools
{
	// Token: 0x02000022 RID: 34
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public class PaintContext
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000051AC File Offset: 0x000033AC
		public Terrain originTerrain { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000051B4 File Offset: 0x000033B4
		public RectInt pixelRect { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000051BC File Offset: 0x000033BC
		public int targetTextureWidth { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000051C4 File Offset: 0x000033C4
		public int targetTextureHeight { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000051CC File Offset: 0x000033CC
		public Vector2 pixelSize { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000051D4 File Offset: 0x000033D4
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000051DC File Offset: 0x000033DC
		public RenderTexture sourceRenderTexture { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000051E5 File Offset: 0x000033E5
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000051ED File Offset: 0x000033ED
		public RenderTexture destinationRenderTexture { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000051F6 File Offset: 0x000033F6
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000051FE File Offset: 0x000033FE
		public RenderTexture oldRenderTexture { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005208 File Offset: 0x00003408
		public int terrainCount
		{
			get
			{
				return this.m_TerrainTiles.Count;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005228 File Offset: 0x00003428
		public Terrain GetTerrain(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].terrain;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000524C File Offset: 0x0000344C
		public RectInt GetClippedPixelRectInTerrainPixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedTerrainPixels;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005270 File Offset: 0x00003470
		public RectInt GetClippedPixelRectInRenderTexturePixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedPCPixels;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005293 File Offset: 0x00003493
		public float heightWorldSpaceMin
		{
			get
			{
				return this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000529B File Offset: 0x0000349B
		public float heightWorldSpaceSize
		{
			get
			{
				return this.m_HeightWorldSpaceMax - this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000052AA File Offset: 0x000034AA
		public static float kNormalizedHeightScale
		{
			get
			{
				return 0.4999771f;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001C5 RID: 453 RVA: 0x000052B4 File Offset: 0x000034B4
		// (remove) Token: 0x060001C6 RID: 454 RVA: 0x000052E8 File Offset: 0x000034E8
		[field: DebuggerBrowsable(0)]
		internal static event Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint;

		// Token: 0x060001C7 RID: 455 RVA: 0x0000531C File Offset: 0x0000351C
		internal static int ClampContextResolution(int resolution)
		{
			return Mathf.Clamp(resolution, 1, 8192);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000533C File Offset: 0x0000353C
		public PaintContext(Terrain terrain, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			this.originTerrain = terrain;
			this.pixelRect = pixelRect;
			this.targetTextureWidth = targetTextureWidth;
			this.targetTextureHeight = targetTextureHeight;
			TerrainData terrainData = terrain.terrainData;
			this.pixelSize = new Vector2(terrainData.size.x / ((float)targetTextureWidth - (sharedBoundaryTexel ? 1f : 0f)), terrainData.size.z / ((float)targetTextureHeight - (sharedBoundaryTexel ? 1f : 0f)));
			this.FindTerrainTilesUnlimited(sharedBoundaryTexel, fillOutsideTerrain);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000053CC File Offset: 0x000035CC
		public static PaintContext CreateFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int inputTextureWidth, int inputTextureHeight, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			return new PaintContext(terrain, TerrainPaintUtility.CalcPixelRectFromBounds(terrain, boundsInTerrainSpace, inputTextureWidth, inputTextureHeight, extraBorderPixels, sharedBoundaryTexel), inputTextureWidth, inputTextureHeight, sharedBoundaryTexel, fillOutsideTerrain);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000053F8 File Offset: 0x000035F8
		private void FindTerrainTilesUnlimited(bool sharedBoundaryTexel, bool fillOutsideTerrain)
		{
			float minX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)this.pixelRect.xMin;
			float minZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)this.pixelRect.yMin;
			float maxX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)(this.pixelRect.xMax - 1);
			float maxZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)(this.pixelRect.yMax - 1);
			this.m_HeightWorldSpaceMin = this.originTerrain.GetPosition().y;
			this.m_HeightWorldSpaceMax = this.m_HeightWorldSpaceMin + this.originTerrain.terrainData.size.y;
			Predicate<Terrain> predicate = delegate(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num4 = t.transform.position.x + t.terrainData.size.x;
				float num5 = t.transform.position.z + t.terrainData.size.z;
				return x <= maxX && num4 >= minX && z <= maxZ && num5 >= minZ;
			};
			TerrainMap terrainMap = TerrainMap.CreateFromConnectedNeighbors(this.originTerrain, predicate, false);
			this.m_TerrainTiles = new List<PaintContext.TerrainTile>();
			bool flag = terrainMap != null;
			if (flag)
			{
				foreach (KeyValuePair<TerrainTileCoord, Terrain> keyValuePair in terrainMap.terrainTiles)
				{
					TerrainTileCoord key = keyValuePair.Key;
					Terrain value = keyValuePair.Value;
					int num = key.tileX * (this.targetTextureWidth - (sharedBoundaryTexel ? 1 : 0));
					int num2 = key.tileZ * (this.targetTextureHeight - (sharedBoundaryTexel ? 1 : 0));
					RectInt rectInt = new RectInt(num, num2, this.targetTextureWidth, this.targetTextureHeight);
					bool flag2 = this.pixelRect.Overlaps(rectInt);
					if (flag2)
					{
						int num3 = (fillOutsideTerrain ? Mathf.Max(this.targetTextureWidth, this.targetTextureHeight) : 0);
						this.m_TerrainTiles.Add(PaintContext.TerrainTile.Make(value, num, num2, this.pixelRect, this.targetTextureWidth, this.targetTextureHeight, num3));
						this.m_HeightWorldSpaceMin = Mathf.Min(this.m_HeightWorldSpaceMin, value.GetPosition().y);
						this.m_HeightWorldSpaceMax = Mathf.Max(this.m_HeightWorldSpaceMax, value.GetPosition().y + value.terrainData.size.y);
					}
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000056B4 File Offset: 0x000038B4
		public void CreateRenderTargets(RenderTextureFormat colorFormat)
		{
			int num = PaintContext.ClampContextResolution(this.pixelRect.width);
			int num2 = PaintContext.ClampContextResolution(this.pixelRect.height);
			bool flag = num != this.pixelRect.width || num2 != this.pixelRect.height;
			if (flag)
			{
				Debug.LogWarning(string.Format("\nTERRAIN EDITOR INTERNAL ERROR: An attempt to create a PaintContext with dimensions of {0}x{1} was made,\nwhereas the maximum supported resolution is {2}. The size has been clamped to {3}.", new object[]
				{
					this.pixelRect.width,
					this.pixelRect.height,
					8192,
					8192
				}));
			}
			this.sourceRenderTexture = RenderTexture.GetTemporary(num, num2, 16, colorFormat, RenderTextureReadWrite.Linear);
			this.destinationRenderTexture = RenderTexture.GetTemporary(num, num2, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.sourceRenderTexture.wrapMode = TextureWrapMode.Clamp;
			this.sourceRenderTexture.filterMode = FilterMode.Point;
			this.oldRenderTexture = RenderTexture.active;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000057C0 File Offset: 0x000039C0
		public void Cleanup(bool restoreRenderTexture = true)
		{
			if (restoreRenderTexture)
			{
				RenderTexture.active = this.oldRenderTexture;
			}
			RenderTexture.ReleaseTemporary(this.sourceRenderTexture);
			RenderTexture.ReleaseTemporary(this.destinationRenderTexture);
			this.sourceRenderTexture = null;
			this.destinationRenderTexture = null;
			this.oldRenderTexture = null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005810 File Offset: 0x00003A10
		private void GatherInternal(Func<PaintContext.ITerrainInfo, Texture> terrainToTexture, Color defaultColor, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			RenderTexture.active = this.sourceRenderTexture;
			GL.Clear(true, true, defaultColor);
			GL.PushMatrix();
			GL.LoadPixelMatrix(0f, (float)this.pixelRect.width, 0f, (float)this.pixelRect.height);
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.gatherEnable;
				if (!flag2)
				{
					Texture texture = terrainToTexture.Invoke(terrainTile);
					bool flag3 = texture == null || !terrainTile.gatherEnable;
					if (!flag3)
					{
						bool flag4 = texture.width != this.targetTextureWidth || texture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution texture for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit.Invoke(terrainTile);
							}
							bool flag5 = !terrainTile.gatherEnable;
							if (!flag5)
							{
								FilterMode filterMode = texture.filterMode;
								texture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", texture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuadPadded(terrainTile.clippedPCPixels, terrainTile.paddedPCPixels, terrainTile.clippedTerrainPixels, terrainTile.paddedTerrainPixels, texture);
								texture.filterMode = filterMode;
								if (afterBlit != null)
								{
									afterBlit.Invoke(terrainTile);
								}
							}
						}
					}
				}
			}
			GL.PopMatrix();
			RenderTexture.active = this.oldRenderTexture;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000059C8 File Offset: 0x00003BC8
		private void ScatterInternal(Func<PaintContext.ITerrainInfo, RenderTexture> terrainToRT, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.scatterEnable;
				if (!flag2)
				{
					RenderTexture renderTexture = terrainToRT.Invoke(terrainTile);
					bool flag3 = renderTexture == null || !terrainTile.scatterEnable;
					if (!flag3)
					{
						bool flag4 = renderTexture.width != this.targetTextureWidth || renderTexture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit.Invoke(terrainTile);
							}
							bool flag5 = !terrainTile.scatterEnable;
							if (!flag5)
							{
								RenderTexture.active = renderTexture;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)renderTexture.width, 0f, (float)renderTexture.height);
								FilterMode filterMode = this.destinationRenderTexture.filterMode;
								this.destinationRenderTexture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedTerrainPixels, terrainTile.clippedPCPixels, this.destinationRenderTexture);
								this.destinationRenderTexture.filterMode = filterMode;
								GL.PopMatrix();
								if (afterBlit != null)
								{
									afterBlit.Invoke(terrainTile);
								}
							}
						}
					}
				}
			}
			RenderTexture.active = active;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005B70 File Offset: 0x00003D70
		public void Gather(Func<PaintContext.ITerrainInfo, Texture> terrainSource, Color defaultColor, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainSource != null;
			if (flag)
			{
				this.GatherInternal(terrainSource, defaultColor, "PaintContext.Gather", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005B9C File Offset: 0x00003D9C
		public void Scatter(Func<PaintContext.ITerrainInfo, RenderTexture> terrainDest, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainDest != null;
			if (flag)
			{
				this.ScatterInternal(terrainDest, "PaintContext.Scatter", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public void GatherHeightmap()
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - this.heightWorldSpaceMin) / this.heightWorldSpaceSize * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / this.heightWorldSpaceSize);
			}, null);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005C74 File Offset: 0x00003E74
		public void ScatterHeightmap(string editorUndoName)
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.ScatterInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, "PaintContext.ScatterHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action.Invoke(t, PaintContext.ToolAction.PaintHeightmap, editorUndoName);
				}
				blitMaterial.SetFloat("_Height_Offset", (this.heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", this.heightWorldSpaceSize / t.terrain.terrainData.size.y);
			}, delegate(PaintContext.ITerrainInfo t)
			{
				TerrainHeightmapSyncControl terrainHeightmapSyncControl = (t.terrain.drawInstanced ? TerrainHeightmapSyncControl.None : TerrainHeightmapSyncControl.HeightAndLod);
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, terrainHeightmapSyncControl);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHeightmap);
			});
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005D2C File Offset: 0x00003F2C
		public void GatherHoles()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.holesTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHoles", null, 0, null, null);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005D84 File Offset: 0x00003F84
		public void ScatterHoles(string editorUndoName)
		{
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action.Invoke(t, PaintContext.ToolAction.PaintHoles, editorUndoName);
				}
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHoles);
				return null;
			}, "PaintContext.ScatterHoles", null, 0, null, null);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005DBC File Offset: 0x00003FBC
		public void GatherNormals()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.normalmapTexture, new Color(0.5f, 0.5f, 0.5f, 0.5f), "PaintContext.GatherNormals", null, 0, null, null);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005E14 File Offset: 0x00004014
		private PaintContext.SplatmapUserData GetTerrainLayerUserData(PaintContext.ITerrainInfo context, TerrainLayer terrainLayer = null, bool addLayerIfDoesntExist = false)
		{
			PaintContext.SplatmapUserData splatmapUserData = context.userData as PaintContext.SplatmapUserData;
			bool flag = splatmapUserData != null;
			if (flag)
			{
				bool flag2 = terrainLayer == null || terrainLayer == splatmapUserData.terrainLayer;
				if (flag2)
				{
					return splatmapUserData;
				}
				splatmapUserData = null;
			}
			bool flag3 = splatmapUserData == null;
			if (flag3)
			{
				int num = -1;
				bool flag4 = terrainLayer != null;
				if (flag4)
				{
					num = TerrainPaintUtility.FindTerrainLayerIndex(context.terrain, terrainLayer);
					bool flag5 = num == -1 && addLayerIfDoesntExist;
					if (flag5)
					{
						Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
						if (action != null)
						{
							action.Invoke(context, PaintContext.ToolAction.AddTerrainLayer, "Adding Terrain Layer");
						}
						num = TerrainPaintUtility.AddTerrainLayer(context.terrain, terrainLayer);
					}
				}
				bool flag6 = num != -1;
				if (flag6)
				{
					splatmapUserData = new PaintContext.SplatmapUserData();
					splatmapUserData.terrainLayer = terrainLayer;
					splatmapUserData.terrainLayerIndex = num;
					splatmapUserData.mapIndex = num >> 2;
					splatmapUserData.channelIndex = num & 3;
				}
				context.userData = splatmapUserData;
			}
			return splatmapUserData;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005F08 File Offset: 0x00004108
		public void GatherAlphamap(TerrainLayer inputLayer, bool addLayerIfDoesntExist = true)
		{
			bool flag = inputLayer == null;
			if (!flag)
			{
				Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
				Vector4[] layerMasks = new Vector4[]
				{
					new Vector4(1f, 0f, 0f, 0f),
					new Vector4(0f, 1f, 0f, 0f),
					new Vector4(0f, 0f, 1f, 0f),
					new Vector4(0f, 0f, 0f, 1f)
				};
				this.GatherInternal(delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, inputLayer, addLayerIfDoesntExist);
					bool flag2 = terrainLayerUserData != null;
					Texture texture;
					if (flag2)
					{
						texture = TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex);
					}
					else
					{
						texture = null;
					}
					return texture;
				}, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherAlphamap", copyTerrainLayerMaterial, 0, delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData2 = this.GetTerrainLayerUserData(t, null, false);
					copyTerrainLayerMaterial.SetVector("_LayerMask", layerMasks[terrainLayerUserData2.channelIndex]);
				}, null);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006024 File Offset: 0x00004224
		public void ScatterAlphamap(string editorUndoName)
		{
			Vector4[] layerMasks = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			RenderTexture tempTarget = RenderTexture.GetTemporary(new RenderTextureDescriptor(this.destinationRenderTexture.width, this.destinationRenderTexture.height, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.None)
			{
				sRGB = false,
				useMipMap = false,
				autoGenerateMips = false
			});
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
				bool flag = terrainLayerUserData != null;
				if (flag)
				{
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
					if (action != null)
					{
						action.Invoke(t, PaintContext.ToolAction.PaintTexture, editorUndoName);
					}
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D texture2D = t.terrain.terrainData.alphamapTextures[mapIndex];
					this.destinationRenderTexture.filterMode = FilterMode.Point;
					this.sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						bool flag2 = i == mapIndex;
						if (!flag2)
						{
							int num = ((i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i);
							Texture2D texture2D2 = t.terrain.terrainData.alphamapTextures[num];
							bool flag3 = texture2D2.width != this.targetTextureWidth || texture2D2.height != this.targetTextureHeight;
							if (flag3)
							{
								Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)tempTarget.width, 0f, (float)tempTarget.height);
								copyTerrainLayerMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", this.sourceRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", texture2D);
								copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D2);
								copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? layerMasks[channelIndex] : Vector4.zero);
								copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", layerMasks[channelIndex]);
								copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, this.destinationRenderTexture, t.clippedTerrainPixels, texture2D2);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
							}
						}
					}
					RenderTexture.active = null;
					PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintTexture);
				}
				return null;
			}, "PaintContext.ScatterAlphamap", copyTerrainLayerMaterial, 0, null, null);
			RenderTexture.ReleaseTemporary(tempTarget);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006150 File Offset: 0x00004350
		private static void OnTerrainPainted(PaintContext.ITerrainInfo tile, PaintContext.ToolAction action)
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				bool flag = tile.terrain == PaintContext.s_PaintedTerrain[i].terrain;
				if (flag)
				{
					PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
					paintedTerrain.action |= action;
					PaintContext.s_PaintedTerrain[i] = paintedTerrain;
					return;
				}
			}
			PaintContext.s_PaintedTerrain.Add(new PaintContext.PaintedTerrain
			{
				terrain = tile.terrain,
				action = action
			});
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000061EC File Offset: 0x000043EC
		public static void ApplyDelayedActions()
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
				TerrainData terrainData = paintedTerrain.terrain.terrainData;
				bool flag = terrainData == null;
				if (!flag)
				{
					bool flag2 = (paintedTerrain.action & PaintContext.ToolAction.PaintHeightmap) > PaintContext.ToolAction.None;
					if (flag2)
					{
						terrainData.SyncHeightmap();
					}
					bool flag3 = (paintedTerrain.action & PaintContext.ToolAction.PaintHoles) > PaintContext.ToolAction.None;
					if (flag3)
					{
						terrainData.SyncTexture(TerrainData.HolesTextureName);
					}
					bool flag4 = (paintedTerrain.action & PaintContext.ToolAction.PaintTexture) > PaintContext.ToolAction.None;
					if (flag4)
					{
						terrainData.SetBaseMapDirty();
						terrainData.SyncTexture(TerrainData.AlphamapTextureName);
					}
					paintedTerrain.terrain.editorRenderFlags = TerrainRenderFlags.all;
				}
			}
			PaintContext.s_PaintedTerrain.Clear();
		}

		// Token: 0x04000084 RID: 132
		private List<PaintContext.TerrainTile> m_TerrainTiles;

		// Token: 0x04000085 RID: 133
		private float m_HeightWorldSpaceMin;

		// Token: 0x04000086 RID: 134
		private float m_HeightWorldSpaceMax;

		// Token: 0x04000088 RID: 136
		internal const int k_MinimumResolution = 1;

		// Token: 0x04000089 RID: 137
		internal const int k_MaximumResolution = 8192;

		// Token: 0x0400008A RID: 138
		private static List<PaintContext.PaintedTerrain> s_PaintedTerrain = new List<PaintContext.PaintedTerrain>();

		// Token: 0x02000023 RID: 35
		public interface ITerrainInfo
		{
			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060001DC RID: 476
			Terrain terrain { get; }

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060001DD RID: 477
			RectInt clippedTerrainPixels { get; }

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060001DE RID: 478
			RectInt clippedPCPixels { get; }

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060001DF RID: 479
			RectInt paddedTerrainPixels { get; }

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060001E0 RID: 480
			RectInt paddedPCPixels { get; }

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060001E1 RID: 481
			// (set) Token: 0x060001E2 RID: 482
			bool gatherEnable { get; set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060001E3 RID: 483
			// (set) Token: 0x060001E4 RID: 484
			bool scatterEnable { get; set; }

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060001E5 RID: 485
			// (set) Token: 0x060001E6 RID: 486
			object userData { get; set; }
		}

		// Token: 0x02000024 RID: 36
		private class TerrainTile : PaintContext.ITerrainInfo
		{
			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060001E7 RID: 487 RVA: 0x000062C8 File Offset: 0x000044C8
			Terrain PaintContext.ITerrainInfo.terrain
			{
				get
				{
					return this.terrain;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060001E8 RID: 488 RVA: 0x000062E0 File Offset: 0x000044E0
			RectInt PaintContext.ITerrainInfo.clippedTerrainPixels
			{
				get
				{
					return this.clippedTerrainPixels;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060001E9 RID: 489 RVA: 0x000062F8 File Offset: 0x000044F8
			RectInt PaintContext.ITerrainInfo.clippedPCPixels
			{
				get
				{
					return this.clippedPCPixels;
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060001EA RID: 490 RVA: 0x00006310 File Offset: 0x00004510
			RectInt PaintContext.ITerrainInfo.paddedTerrainPixels
			{
				get
				{
					return this.paddedTerrainPixels;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060001EB RID: 491 RVA: 0x00006328 File Offset: 0x00004528
			RectInt PaintContext.ITerrainInfo.paddedPCPixels
			{
				get
				{
					return this.paddedPCPixels;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060001EC RID: 492 RVA: 0x00006340 File Offset: 0x00004540
			// (set) Token: 0x060001ED RID: 493 RVA: 0x00006358 File Offset: 0x00004558
			bool PaintContext.ITerrainInfo.gatherEnable
			{
				get
				{
					return this.gatherEnable;
				}
				set
				{
					this.gatherEnable = value;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060001EE RID: 494 RVA: 0x00006364 File Offset: 0x00004564
			// (set) Token: 0x060001EF RID: 495 RVA: 0x0000637C File Offset: 0x0000457C
			bool PaintContext.ITerrainInfo.scatterEnable
			{
				get
				{
					return this.scatterEnable;
				}
				set
				{
					this.scatterEnable = value;
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006388 File Offset: 0x00004588
			// (set) Token: 0x060001F1 RID: 497 RVA: 0x000063A0 File Offset: 0x000045A0
			object PaintContext.ITerrainInfo.userData
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x000063AC File Offset: 0x000045AC
			public static PaintContext.TerrainTile Make(Terrain terrain, int tileOriginPixelsX, int tileOriginPixelsY, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, int edgePad = 0)
			{
				PaintContext.TerrainTile terrainTile = new PaintContext.TerrainTile
				{
					terrain = terrain,
					gatherEnable = true,
					scatterEnable = true,
					tileOriginPixels = new Vector2Int(tileOriginPixelsX, tileOriginPixelsY),
					clippedTerrainPixels = new RectInt
					{
						x = Mathf.Max(0, pixelRect.x - tileOriginPixelsX),
						y = Mathf.Max(0, pixelRect.y - tileOriginPixelsY),
						xMax = Mathf.Min(targetTextureWidth, pixelRect.xMax - tileOriginPixelsX),
						yMax = Mathf.Min(targetTextureHeight, pixelRect.yMax - tileOriginPixelsY)
					}
				};
				terrainTile.clippedPCPixels = new RectInt(terrainTile.clippedTerrainPixels.x + terrainTile.tileOriginPixels.x - pixelRect.x, terrainTile.clippedTerrainPixels.y + terrainTile.tileOriginPixels.y - pixelRect.y, terrainTile.clippedTerrainPixels.width, terrainTile.clippedTerrainPixels.height);
				int num = ((terrain.leftNeighbor == null) ? edgePad : 0);
				int num2 = ((terrain.rightNeighbor == null) ? edgePad : 0);
				int num3 = ((terrain.bottomNeighbor == null) ? edgePad : 0);
				int num4 = ((terrain.topNeighbor == null) ? edgePad : 0);
				terrainTile.paddedTerrainPixels = new RectInt
				{
					x = Mathf.Max(-num, pixelRect.x - tileOriginPixelsX - num),
					y = Mathf.Max(-num3, pixelRect.y - tileOriginPixelsY - num3),
					xMax = Mathf.Min(targetTextureWidth + num2, pixelRect.xMax - tileOriginPixelsX + num2),
					yMax = Mathf.Min(targetTextureHeight + num4, pixelRect.yMax - tileOriginPixelsY + num4)
				};
				terrainTile.paddedPCPixels = new RectInt(terrainTile.clippedPCPixels.min + (terrainTile.paddedTerrainPixels.min - terrainTile.clippedTerrainPixels.min), terrainTile.clippedPCPixels.size + (terrainTile.paddedTerrainPixels.size - terrainTile.clippedTerrainPixels.size));
				bool flag = terrainTile.clippedTerrainPixels.width == 0 || terrainTile.clippedTerrainPixels.height == 0;
				if (flag)
				{
					terrainTile.gatherEnable = false;
					terrainTile.scatterEnable = false;
					Debug.LogError("PaintContext.ClipTerrainTiles found 0 content rect");
				}
				return terrainTile;
			}

			// Token: 0x0400008B RID: 139
			public Terrain terrain;

			// Token: 0x0400008C RID: 140
			public Vector2Int tileOriginPixels;

			// Token: 0x0400008D RID: 141
			public RectInt clippedTerrainPixels;

			// Token: 0x0400008E RID: 142
			public RectInt clippedPCPixels;

			// Token: 0x0400008F RID: 143
			public RectInt paddedTerrainPixels;

			// Token: 0x04000090 RID: 144
			public RectInt paddedPCPixels;

			// Token: 0x04000091 RID: 145
			public object userData;

			// Token: 0x04000092 RID: 146
			public bool gatherEnable;

			// Token: 0x04000093 RID: 147
			public bool scatterEnable;
		}

		// Token: 0x02000025 RID: 37
		private class SplatmapUserData
		{
			// Token: 0x04000094 RID: 148
			public TerrainLayer terrainLayer;

			// Token: 0x04000095 RID: 149
			public int terrainLayerIndex;

			// Token: 0x04000096 RID: 150
			public int mapIndex;

			// Token: 0x04000097 RID: 151
			public int channelIndex;
		}

		// Token: 0x02000026 RID: 38
		[Flags]
		internal enum ToolAction
		{
			// Token: 0x04000099 RID: 153
			None = 0,
			// Token: 0x0400009A RID: 154
			PaintHeightmap = 1,
			// Token: 0x0400009B RID: 155
			PaintTexture = 2,
			// Token: 0x0400009C RID: 156
			PaintHoles = 4,
			// Token: 0x0400009D RID: 157
			AddTerrainLayer = 8
		}

		// Token: 0x02000027 RID: 39
		private struct PaintedTerrain
		{
			// Token: 0x0400009E RID: 158
			public Terrain terrain;

			// Token: 0x0400009F RID: 159
			public PaintContext.ToolAction action;
		}
	}
}
