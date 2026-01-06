using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x02000348 RID: 840
	internal class UIRStylePainter : IStylePainter, IDisposable
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x0007806C File Offset: 0x0007626C
		private MeshWriteData GetPooledMeshWriteData()
		{
			bool flag = this.m_NextMeshWriteDataPoolItem == this.m_MeshWriteDataPool.Count;
			if (flag)
			{
				this.m_MeshWriteDataPool.Add(new MeshWriteData());
			}
			List<MeshWriteData> meshWriteDataPool = this.m_MeshWriteDataPool;
			int nextMeshWriteDataPoolItem = this.m_NextMeshWriteDataPoolItem;
			this.m_NextMeshWriteDataPoolItem = nextMeshWriteDataPoolItem + 1;
			return meshWriteDataPool[nextMeshWriteDataPoolItem];
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000780C4 File Offset: 0x000762C4
		private MeshWriteData AllocRawVertsIndices(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			this.m_CurrentEntry.vertices = this.m_VertsPool.Alloc(vertexCount);
			this.m_CurrentEntry.indices = this.m_IndicesPool.Alloc(indexCount);
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices);
			return pooledMeshWriteData;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0007812C File Offset: 0x0007632C
		private MeshWriteData AllocThroughDrawMesh(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			return this.DrawMesh((int)vertexCount, (int)indexCount, allocatorData.texture, allocatorData.material, allocatorData.flags);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00078158 File Offset: 0x00076358
		private MeshWriteData AllocThroughDrawGradients(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData)
		{
			return this.AddGradientsEntry((int)vertexCount, (int)indexCount, allocatorData.svgTexture, allocatorData.material, allocatorData.flags);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00078184 File Offset: 0x00076384
		public UIRStylePainter(RenderChain renderChain)
		{
			this.m_Owner = renderChain;
			this.meshGenerationContext = new MeshGenerationContext(this);
			this.m_Atlas = renderChain.atlas;
			this.m_VectorImageManager = renderChain.vectorImageManager;
			this.m_AllocRawVertsIndicesDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocRawVertsIndices);
			this.m_AllocThroughDrawMeshDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocThroughDrawMesh);
			this.m_AllocThroughDrawGradientsDelegate = new MeshBuilder.AllocMeshData.Allocator(this.AllocThroughDrawGradients);
			int num = 32;
			this.m_MeshWriteDataPool = new List<MeshWriteData>(num);
			for (int i = 0; i < num; i++)
			{
				this.m_MeshWriteDataPool.Add(new MeshWriteData());
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00078267 File Offset: 0x00076467
		public MeshGenerationContext meshGenerationContext { get; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0007826F File Offset: 0x0007646F
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x00078277 File Offset: 0x00076477
		public VisualElement currentElement { get; private set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00078280 File Offset: 0x00076480
		public List<UIRStylePainter.Entry> entries
		{
			get
			{
				return this.m_Entries;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00078298 File Offset: 0x00076498
		public UIRStylePainter.ClosingInfo closingInfo
		{
			get
			{
				return this.m_ClosingInfo;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x000782B0 File Offset: 0x000764B0
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x000782B8 File Offset: 0x000764B8
		public int totalVertices { get; private set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x000782C1 File Offset: 0x000764C1
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x000782C9 File Offset: 0x000764C9
		public int totalIndices { get; private set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x000782D2 File Offset: 0x000764D2
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x000782DA File Offset: 0x000764DA
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000782E3 File Offset: 0x000764E3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000782F8 File Offset: 0x000764F8
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_IndicesPool.Dispose();
					this.m_VertsPool.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0007833C File Offset: 0x0007653C
		public void Begin(VisualElement ve)
		{
			this.currentElement = ve;
			this.m_NextMeshWriteDataPoolItem = 0;
			this.m_SVGBackgroundEntryIndex = -1;
			this.currentElement.renderChainData.usesLegacyText = (this.currentElement.renderChainData.disableNudging = false);
			this.currentElement.renderChainData.displacementUVStart = (this.currentElement.renderChainData.displacementUVEnd = 0);
			this.m_MaskDepth = 0;
			this.m_StencilRef = 0;
			VisualElement parent = this.currentElement.hierarchy.parent;
			bool flag = parent != null;
			if (flag)
			{
				this.m_MaskDepth = parent.renderChainData.childrenMaskDepth;
				this.m_StencilRef = parent.renderChainData.childrenStencilRef;
			}
			bool flag2 = (this.currentElement.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			bool flag3 = flag2;
			if (flag3)
			{
				RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
				renderChainCommand.owner = this.currentElement;
				renderChainCommand.type = CommandType.PushView;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.popViewMatrix = true);
			}
			bool flag4 = parent != null;
			if (flag4)
			{
				this.m_ClipRectID = (flag2 ? UIRVEShaderInfoAllocator.infiniteClipRect : parent.renderChainData.clipRectID);
			}
			else
			{
				this.m_ClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
			}
			bool flag5 = ve.subRenderTargetMode > VisualElement.RenderTargetMode.None;
			if (flag5)
			{
				RenderChainCommand renderChainCommand2 = this.m_Owner.AllocCommand();
				renderChainCommand2.owner = this.currentElement;
				renderChainCommand2.type = CommandType.PushRenderTexture;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand2
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.blitAndPopRenderTexture = true);
				bool flag6 = this.m_MaskDepth > 0 || this.m_StencilRef > 0;
				if (flag6)
				{
					Debug.LogError("The RenderTargetMode feature must not be used within a stencil mask.");
				}
			}
			bool flag7 = ve.defaultMaterial != null;
			if (flag7)
			{
				RenderChainCommand renderChainCommand3 = this.m_Owner.AllocCommand();
				renderChainCommand3.owner = this.currentElement;
				renderChainCommand3.type = CommandType.PushDefaultMaterial;
				renderChainCommand3.state.material = ve.defaultMaterial;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand3
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.PopDefaultMaterial = true);
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000785B7 File Offset: 0x000767B7
		public void LandClipUnregisterMeshDrawCommand(RenderChainCommand cmd)
		{
			Debug.Assert(this.m_ClosingInfo.needsClosing);
			this.m_ClosingInfo.clipUnregisterDrawCommand = cmd;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000785D7 File Offset: 0x000767D7
		public void LandClipRegisterMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, int indexOffset)
		{
			Debug.Assert(this.m_ClosingInfo.needsClosing);
			this.m_ClosingInfo.clipperRegisterVertices = vertices;
			this.m_ClosingInfo.clipperRegisterIndices = indices;
			this.m_ClosingInfo.clipperRegisterIndexOffset = indexOffset;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00078610 File Offset: 0x00076810
		public MeshWriteData AddGradientsEntry(int vertexCount, int indexCount, TextureId texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			bool flag = vertexCount == 0 || indexCount == 0;
			MeshWriteData meshWriteData;
			if (flag)
			{
				pooledMeshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
				meshWriteData = pooledMeshWriteData;
			}
			else
			{
				this.m_CurrentEntry = new UIRStylePainter.Entry
				{
					vertices = this.m_VertsPool.Alloc((uint)vertexCount),
					indices = this.m_IndicesPool.Alloc((uint)indexCount),
					material = material,
					texture = texture,
					clipRectID = this.m_ClipRectID,
					stencilRef = this.m_StencilRef,
					maskDepth = this.m_MaskDepth,
					addFlags = VertexFlags.IsSvgGradients
				};
				Debug.Assert(this.m_CurrentEntry.vertices.Length == vertexCount);
				Debug.Assert(this.m_CurrentEntry.indices.Length == indexCount);
				pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices, new Rect(0f, 0f, 1f, 1f));
				this.m_Entries.Add(this.m_CurrentEntry);
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
				meshWriteData = pooledMeshWriteData;
			}
			return meshWriteData;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00078794 File Offset: 0x00076994
		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			MeshWriteData pooledMeshWriteData = this.GetPooledMeshWriteData();
			bool flag = vertexCount == 0 || indexCount == 0;
			MeshWriteData meshWriteData;
			if (flag)
			{
				pooledMeshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
				meshWriteData = pooledMeshWriteData;
			}
			else
			{
				this.m_CurrentEntry = new UIRStylePainter.Entry
				{
					vertices = this.m_VertsPool.Alloc((uint)vertexCount),
					indices = this.m_IndicesPool.Alloc((uint)indexCount),
					material = material,
					uvIsDisplacement = ((flags & MeshGenerationContext.MeshFlags.UVisDisplacement) == MeshGenerationContext.MeshFlags.UVisDisplacement),
					clipRectID = this.m_ClipRectID,
					stencilRef = this.m_StencilRef,
					maskDepth = this.m_MaskDepth,
					addFlags = VertexFlags.IsSolid
				};
				Debug.Assert(this.m_CurrentEntry.vertices.Length == vertexCount);
				Debug.Assert(this.m_CurrentEntry.indices.Length == indexCount);
				Rect rect = new Rect(0f, 0f, 1f, 1f);
				bool flag2 = texture != null;
				if (flag2)
				{
					TextureId textureId;
					RectInt rectInt;
					bool flag3 = (flags & MeshGenerationContext.MeshFlags.SkipDynamicAtlas) != MeshGenerationContext.MeshFlags.SkipDynamicAtlas && this.m_Atlas != null && this.m_Atlas.TryGetAtlas(this.currentElement, texture as Texture2D, out textureId, out rectInt);
					if (flag3)
					{
						this.m_CurrentEntry.addFlags = VertexFlags.IsDynamic;
						rect = new Rect((float)rectInt.x, (float)rectInt.y, (float)rectInt.width, (float)rectInt.height);
						this.m_CurrentEntry.texture = textureId;
						this.m_Owner.AppendTexture(this.currentElement, texture, textureId, true);
					}
					else
					{
						TextureId textureId2 = TextureRegistry.instance.Acquire(texture);
						this.m_CurrentEntry.addFlags = VertexFlags.IsTextured;
						this.m_CurrentEntry.texture = textureId2;
						this.m_Owner.AppendTexture(this.currentElement, texture, textureId2, false);
					}
				}
				pooledMeshWriteData.Reset(this.m_CurrentEntry.vertices, this.m_CurrentEntry.indices, rect);
				this.m_Entries.Add(this.m_CurrentEntry);
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
				meshWriteData = pooledMeshWriteData;
			}
			return meshWriteData;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00078A00 File Offset: 0x00076C00
		public void DrawText(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			bool flag = !TextUtilities.IsFontAssigned(textParams);
			if (!flag)
			{
				bool flag2 = handle.IsLegacy();
				if (flag2)
				{
					this.DrawTextNative(textParams, handle, pixelsPerPoint);
				}
				else
				{
					this.DrawTextCore(textParams, handle, pixelsPerPoint);
				}
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00078A40 File Offset: 0x00076C40
		internal void DrawTextNative(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			float num = TextUtilities.ComputeTextScaling(this.currentElement.worldTransform, pixelsPerPoint);
			using (NativeArray<TextVertex> vertices = ((TextNativeHandle)handle).GetVertices(textParams, num))
			{
				bool flag = vertices.Length == 0;
				if (!flag)
				{
					TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, num);
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					this.m_CurrentEntry.isTextEntry = true;
					this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
					this.m_CurrentEntry.stencilRef = this.m_StencilRef;
					this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
					MeshBuilder.MakeText(vertices, offset, new MeshBuilder.AllocMeshData
					{
						alloc = this.m_AllocRawVertsIndicesDelegate
					});
					this.m_CurrentEntry.font = textParams.font.material.mainTexture;
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_CurrentEntry = default(UIRStylePainter.Entry);
					this.currentElement.renderChainData.usesLegacyText = true;
					this.currentElement.renderChainData.disableNudging = true;
				}
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00078BC4 File Offset: 0x00076DC4
		internal void DrawTextCore(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			TextInfo textInfo = handle.Update(textParams, pixelsPerPoint);
			for (int i = 0; i < textInfo.materialCount; i++)
			{
				bool flag = textInfo.meshInfo[i].vertexCount == 0;
				if (!flag)
				{
					this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
					this.m_CurrentEntry.stencilRef = this.m_StencilRef;
					this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
					bool flag2 = textInfo.meshInfo[i].material.name.Contains("Sprite");
					if (flag2)
					{
						Texture mainTexture = textInfo.meshInfo[i].material.mainTexture;
						TextureId textureId = TextureRegistry.instance.Acquire(mainTexture);
						this.m_CurrentEntry.texture = textureId;
						this.m_Owner.AppendTexture(this.currentElement, mainTexture, textureId, false);
						MeshBuilder.MakeText(textInfo.meshInfo[i], textParams.rect.min, new MeshBuilder.AllocMeshData
						{
							alloc = this.m_AllocRawVertsIndicesDelegate
						}, VertexFlags.IsTextured, false);
					}
					else
					{
						this.m_CurrentEntry.isTextEntry = true;
						this.m_CurrentEntry.fontTexSDFScale = textInfo.meshInfo[i].material.GetFloat(TextShaderUtilities.ID_GradientScale);
						this.m_CurrentEntry.font = textInfo.meshInfo[i].material.mainTexture;
						bool flag3 = RenderEvents.NeedsColorID(this.currentElement);
						MeshBuilder.MakeText(textInfo.meshInfo[i], textParams.rect.min, new MeshBuilder.AllocMeshData
						{
							alloc = this.m_AllocRawVertsIndicesDelegate
						}, VertexFlags.IsText, flag3);
					}
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_CurrentEntry = default(UIRStylePainter.Entry);
				}
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00078DF0 File Offset: 0x00076FF0
		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			bool flag = rectParams.rect.width < 1E-30f || rectParams.rect.height < 1E-30f;
			if (!flag)
			{
				MeshBuilder.AllocMeshData allocMeshData = new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocThroughDrawMeshDelegate,
					texture = rectParams.texture,
					material = rectParams.material,
					flags = rectParams.meshFlags
				};
				bool flag2 = rectParams.vectorImage != null;
				if (flag2)
				{
					this.DrawVectorImage(rectParams);
				}
				else
				{
					bool flag3 = rectParams.sprite != null;
					if (flag3)
					{
						this.DrawSprite(rectParams);
					}
					else
					{
						bool flag4 = rectParams.texture != null;
						if (flag4)
						{
							MeshBuilder.MakeTexturedRect(rectParams, 0f, allocMeshData, rectParams.colorPage);
						}
						else
						{
							MeshBuilder.MakeSolidRect(rectParams, 0f, allocMeshData);
						}
					}
				}
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00078ED8 File Offset: 0x000770D8
		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
			MeshBuilder.MakeBorder(borderParams, 0f, new MeshBuilder.AllocMeshData
			{
				alloc = this.m_AllocThroughDrawMeshDelegate,
				material = borderParams.material,
				texture = null
			});
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00078F20 File Offset: 0x00077120
		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
			RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
			renderChainCommand.type = (cullingEnabled ? CommandType.ImmediateCull : CommandType.Immediate);
			renderChainCommand.owner = this.currentElement;
			renderChainCommand.callback = callback;
			this.m_Entries.Add(new UIRStylePainter.Entry
			{
				customCommand = renderChainCommand
			});
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00078F78 File Offset: 0x00077178
		public VisualElement visualElement
		{
			get
			{
				return this.currentElement;
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00078F90 File Offset: 0x00077190
		public unsafe void DrawVisualElementBackground()
		{
			bool flag = this.currentElement.layout.width <= 1E-30f || this.currentElement.layout.height <= 1E-30f;
			if (!flag)
			{
				ComputedStyle computedStyle = *this.currentElement.computedStyle;
				bool flag2 = computedStyle.backgroundColor != Color.clear;
				if (flag2)
				{
					MeshGenerationContextUtils.RectangleParams rectangleParams = new MeshGenerationContextUtils.RectangleParams
					{
						rect = this.currentElement.rect,
						color = computedStyle.backgroundColor,
						colorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.backgroundColorID),
						playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
					};
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out rectangleParams.topLeftRadius, out rectangleParams.bottomLeftRadius, out rectangleParams.topRightRadius, out rectangleParams.bottomRightRadius);
					MeshGenerationContextUtils.AdjustBackgroundSizeForBorders(this.currentElement, ref rectangleParams.rect);
					this.DrawRectangle(rectangleParams);
				}
				Vector4 vector = new Vector4((float)computedStyle.unitySliceLeft, (float)computedStyle.unitySliceTop, (float)computedStyle.unitySliceRight, (float)computedStyle.unitySliceBottom);
				MeshGenerationContextUtils.RectangleParams rectangleParams2 = default(MeshGenerationContextUtils.RectangleParams);
				MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out rectangleParams2.topLeftRadius, out rectangleParams2.bottomLeftRadius, out rectangleParams2.topRightRadius, out rectangleParams2.bottomRightRadius);
				Background backgroundImage = computedStyle.backgroundImage;
				bool flag3 = backgroundImage.texture != null || backgroundImage.sprite != null || backgroundImage.vectorImage != null || backgroundImage.renderTexture != null;
				if (flag3)
				{
					MeshGenerationContextUtils.RectangleParams rectangleParams3 = default(MeshGenerationContextUtils.RectangleParams);
					float num = 1f;
					bool flag4 = backgroundImage.texture != null;
					if (flag4)
					{
						rectangleParams3 = MeshGenerationContextUtils.RectangleParams.MakeTextured(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.texture, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType);
					}
					else
					{
						bool flag5 = backgroundImage.sprite != null;
						if (flag5)
						{
							rectangleParams3 = MeshGenerationContextUtils.RectangleParams.MakeSprite(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.sprite, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType, rectangleParams2.HasRadius(Tessellation.kEpsilon), ref vector);
							num *= UIElementsUtility.PixelsPerUnitScaleForElement(this.visualElement, backgroundImage.sprite);
						}
						else
						{
							bool flag6 = backgroundImage.renderTexture != null;
							if (flag6)
							{
								rectangleParams3 = MeshGenerationContextUtils.RectangleParams.MakeTextured(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.renderTexture, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType);
							}
							else
							{
								bool flag7 = backgroundImage.vectorImage != null;
								if (flag7)
								{
									rectangleParams3 = MeshGenerationContextUtils.RectangleParams.MakeVectorTextured(this.currentElement.rect, new Rect(0f, 0f, 1f, 1f), backgroundImage.vectorImage, computedStyle.unityBackgroundScaleMode, this.currentElement.panel.contextType);
								}
							}
						}
					}
					rectangleParams3.topLeftRadius = rectangleParams2.topLeftRadius;
					rectangleParams3.topRightRadius = rectangleParams2.topRightRadius;
					rectangleParams3.bottomRightRadius = rectangleParams2.bottomRightRadius;
					rectangleParams3.bottomLeftRadius = rectangleParams2.bottomLeftRadius;
					bool flag8 = vector != Vector4.zero;
					if (flag8)
					{
						rectangleParams3.leftSlice = Mathf.RoundToInt(vector.x);
						rectangleParams3.topSlice = Mathf.RoundToInt(vector.y);
						rectangleParams3.rightSlice = Mathf.RoundToInt(vector.z);
						rectangleParams3.bottomSlice = Mathf.RoundToInt(vector.w);
					}
					rectangleParams3.color = computedStyle.unityBackgroundImageTintColor;
					rectangleParams3.colorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.tintColorID);
					rectangleParams3.sliceScale = num;
					MeshGenerationContextUtils.AdjustBackgroundSizeForBorders(this.currentElement, ref rectangleParams3.rect);
					this.DrawRectangle(rectangleParams3);
				}
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00079410 File Offset: 0x00077610
		public void DrawVisualElementBorder()
		{
			bool flag = this.currentElement.layout.width >= 1E-30f && this.currentElement.layout.height >= 1E-30f;
			if (flag)
			{
				IResolvedStyle resolvedStyle = this.currentElement.resolvedStyle;
				bool flag2 = (resolvedStyle.borderLeftColor != Color.clear && resolvedStyle.borderLeftWidth > 0f) || (resolvedStyle.borderTopColor != Color.clear && resolvedStyle.borderTopWidth > 0f) || (resolvedStyle.borderRightColor != Color.clear && resolvedStyle.borderRightWidth > 0f) || (resolvedStyle.borderBottomColor != Color.clear && resolvedStyle.borderBottomWidth > 0f);
				if (flag2)
				{
					MeshGenerationContextUtils.BorderParams borderParams = new MeshGenerationContextUtils.BorderParams
					{
						rect = this.currentElement.rect,
						leftColor = resolvedStyle.borderLeftColor,
						topColor = resolvedStyle.borderTopColor,
						rightColor = resolvedStyle.borderRightColor,
						bottomColor = resolvedStyle.borderBottomColor,
						leftWidth = resolvedStyle.borderLeftWidth,
						topWidth = resolvedStyle.borderTopWidth,
						rightWidth = resolvedStyle.borderRightWidth,
						bottomWidth = resolvedStyle.borderBottomWidth,
						leftColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderLeftColorID),
						topColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderTopColorID),
						rightColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderRightColorID),
						bottomColorPage = ColorPage.Init(this.m_Owner, this.currentElement.renderChainData.borderBottomColorID),
						playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
					};
					MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out borderParams.topLeftRadius, out borderParams.bottomLeftRadius, out borderParams.topRightRadius, out borderParams.bottomRightRadius);
					this.DrawBorder(borderParams);
				}
			}
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00079660 File Offset: 0x00077860
		public void ApplyVisualElementClipping()
		{
			bool flag = this.currentElement.renderChainData.clipMethod == ClipMethod.Scissor;
			if (flag)
			{
				RenderChainCommand renderChainCommand = this.m_Owner.AllocCommand();
				renderChainCommand.type = CommandType.PushScissor;
				renderChainCommand.owner = this.currentElement;
				this.m_Entries.Add(new UIRStylePainter.Entry
				{
					customCommand = renderChainCommand
				});
				this.m_ClosingInfo.needsClosing = (this.m_ClosingInfo.popScissorClip = true);
			}
			else
			{
				bool flag2 = this.currentElement.renderChainData.clipMethod == ClipMethod.Stencil;
				if (flag2)
				{
					bool flag3 = this.m_MaskDepth > this.m_StencilRef;
					if (flag3)
					{
						this.m_StencilRef++;
						Debug.Assert(this.m_MaskDepth == this.m_StencilRef);
					}
					this.m_ClosingInfo.maskStencilRef = this.m_StencilRef;
					bool flag4 = UIRUtility.IsVectorImageBackground(this.currentElement);
					if (flag4)
					{
						this.GenerateStencilClipEntryForSVGBackground();
					}
					else
					{
						this.GenerateStencilClipEntryForRoundedRectBackground();
					}
					this.m_MaskDepth++;
				}
			}
			this.m_ClipRectID = this.currentElement.renderChainData.clipRectID;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0007978C File Offset: 0x0007798C
		private ushort[] AdjustSpriteWinding(Vector2[] vertices, ushort[] indices)
		{
			ushort[] array = new ushort[indices.Length];
			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3 vector = vertices[(int)indices[i]];
				Vector3 vector2 = vertices[(int)indices[i + 1]];
				Vector3 vector3 = vertices[(int)indices[i + 2]];
				Vector3 normalized = (vector2 - vector).normalized;
				Vector3 normalized2 = (vector3 - vector).normalized;
				Vector3 vector4 = Vector3.Cross(normalized, normalized2);
				bool flag = vector4.z >= 0f;
				if (flag)
				{
					array[i] = indices[i + 1];
					array[i + 1] = indices[i];
					array[i + 2] = indices[i + 2];
				}
				else
				{
					array[i] = indices[i];
					array[i + 1] = indices[i + 1];
					array[i + 2] = indices[i + 2];
				}
			}
			return array;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0007987C File Offset: 0x00077A7C
		public void DrawSprite(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			Sprite sprite = rectParams.sprite;
			bool flag = sprite.texture == null || sprite.triangles.Length == 0;
			if (!flag)
			{
				MeshBuilder.AllocMeshData allocMeshData = new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocThroughDrawMeshDelegate,
					texture = sprite.texture,
					flags = rectParams.meshFlags
				};
				Vector2[] vertices = sprite.vertices;
				ushort[] triangles = sprite.triangles;
				Vector2[] uv = sprite.uv;
				int num = sprite.vertices.Length;
				Vertex[] array = new Vertex[num];
				ushort[] array2 = this.AdjustSpriteWinding(vertices, triangles);
				MeshWriteData meshWriteData = allocMeshData.Allocate((uint)array.Length, (uint)array2.Length);
				Rect uvRegion = meshWriteData.uvRegion;
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = vertices[i];
					vector -= rectParams.spriteGeomRect.position;
					vector /= rectParams.spriteGeomRect.size;
					vector.y = 1f - vector.y;
					vector *= rectParams.rect.size;
					vector += rectParams.rect.position;
					Vector2 vector2 = uv[i];
					vector2 *= uvRegion.size;
					vector2 += uvRegion.position;
					array[i] = new Vertex
					{
						position = new Vector3(vector.x, vector.y, Vertex.nearZ),
						tint = rectParams.color,
						uv = vector2
					};
				}
				meshWriteData.SetAllVertices(array);
				meshWriteData.SetAllIndices(array2);
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00079A54 File Offset: 0x00077C54
		public void DrawVectorImage(MeshGenerationContextUtils.RectangleParams rectParams)
		{
			VectorImage vectorImage = rectParams.vectorImage;
			Debug.Assert(vectorImage != null);
			int num = 0;
			MeshBuilder.AllocMeshData allocMeshData = default(MeshBuilder.AllocMeshData);
			bool flag = vectorImage.atlas != null && this.m_VectorImageManager != null;
			if (flag)
			{
				GradientRemap gradientRemap = this.m_VectorImageManager.AddUser(vectorImage, this.currentElement);
				num = gradientRemap.destIndex;
				bool flag2 = gradientRemap.atlas != TextureId.invalid;
				if (flag2)
				{
					allocMeshData.svgTexture = gradientRemap.atlas;
				}
				else
				{
					allocMeshData.svgTexture = TextureRegistry.instance.Acquire(vectorImage.atlas);
					this.m_Owner.AppendTexture(this.currentElement, vectorImage.atlas, allocMeshData.svgTexture, false);
				}
				allocMeshData.alloc = this.m_AllocThroughDrawGradientsDelegate;
			}
			else
			{
				allocMeshData.alloc = this.m_AllocThroughDrawMeshDelegate;
			}
			int count = this.m_Entries.Count;
			int num2;
			int num3;
			MeshBuilder.MakeVectorGraphics(rectParams, num, allocMeshData, out num2, out num3);
			Debug.Assert(count <= this.m_Entries.Count + 1);
			bool flag3 = count != this.m_Entries.Count;
			if (flag3)
			{
				this.m_SVGBackgroundEntryIndex = this.m_Entries.Count - 1;
				bool flag4 = num2 != 0 && num3 != 0;
				if (flag4)
				{
					UIRStylePainter.Entry entry = this.m_Entries[this.m_SVGBackgroundEntryIndex];
					entry.vertices = entry.vertices.Slice(0, num2);
					entry.indices = entry.indices.Slice(0, num3);
					this.m_Entries[this.m_SVGBackgroundEntryIndex] = entry;
				}
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00079C04 File Offset: 0x00077E04
		internal void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.ValidateMeshWriteData();
				this.m_Entries.Clear();
				this.m_VertsPool.SessionDone();
				this.m_IndicesPool.SessionDone();
				this.m_ClosingInfo = default(UIRStylePainter.ClosingInfo);
				this.m_NextMeshWriteDataPoolItem = 0;
				this.currentElement = null;
				this.totalVertices = (this.totalIndices = 0);
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00079C80 File Offset: 0x00077E80
		private void ValidateMeshWriteData()
		{
			for (int i = 0; i < this.m_NextMeshWriteDataPoolItem; i++)
			{
				MeshWriteData meshWriteData = this.m_MeshWriteDataPool[i];
				bool flag = meshWriteData.vertexCount > 0 && meshWriteData.currentVertex < meshWriteData.vertexCount;
				if (flag)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Not enough vertices written in generateVisualContent callback (asked for ",
						meshWriteData.vertexCount.ToString(),
						" but only wrote ",
						meshWriteData.currentVertex.ToString(),
						")"
					}));
					Vertex vertex = meshWriteData.m_Vertices[0];
					while (meshWriteData.currentVertex < meshWriteData.vertexCount)
					{
						meshWriteData.SetNextVertex(vertex);
					}
				}
				bool flag2 = meshWriteData.indexCount > 0 && meshWriteData.currentIndex < meshWriteData.indexCount;
				if (flag2)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Not enough indices written in generateVisualContent callback (asked for ",
						meshWriteData.indexCount.ToString(),
						" but only wrote ",
						meshWriteData.currentIndex.ToString(),
						")"
					}));
					while (meshWriteData.currentIndex < meshWriteData.indexCount)
					{
						meshWriteData.SetNextIndex(0);
					}
				}
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00079DD8 File Offset: 0x00077FD8
		private void GenerateStencilClipEntryForRoundedRectBackground()
		{
			bool flag = this.currentElement.layout.width <= 1E-30f || this.currentElement.layout.height <= 1E-30f;
			if (!flag)
			{
				IResolvedStyle resolvedStyle = this.currentElement.resolvedStyle;
				Vector2 vector;
				Vector2 vector2;
				Vector2 vector3;
				Vector2 vector4;
				MeshGenerationContextUtils.GetVisualElementRadii(this.currentElement, out vector, out vector2, out vector3, out vector4);
				float borderTopWidth = resolvedStyle.borderTopWidth;
				float borderLeftWidth = resolvedStyle.borderLeftWidth;
				float borderBottomWidth = resolvedStyle.borderBottomWidth;
				float borderRightWidth = resolvedStyle.borderRightWidth;
				MeshGenerationContextUtils.RectangleParams rectangleParams = new MeshGenerationContextUtils.RectangleParams
				{
					rect = this.currentElement.rect,
					color = Color.white,
					topLeftRadius = Vector2.Max(Vector2.zero, vector - new Vector2(borderLeftWidth, borderTopWidth)),
					topRightRadius = Vector2.Max(Vector2.zero, vector3 - new Vector2(borderRightWidth, borderTopWidth)),
					bottomLeftRadius = Vector2.Max(Vector2.zero, vector2 - new Vector2(borderLeftWidth, borderBottomWidth)),
					bottomRightRadius = Vector2.Max(Vector2.zero, vector4 - new Vector2(borderRightWidth, borderBottomWidth)),
					playmodeTintColor = ((this.currentElement.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white)
				};
				rectangleParams.rect.x = rectangleParams.rect.x + borderLeftWidth;
				rectangleParams.rect.y = rectangleParams.rect.y + borderTopWidth;
				rectangleParams.rect.width = rectangleParams.rect.width - (borderLeftWidth + borderRightWidth);
				rectangleParams.rect.height = rectangleParams.rect.height - (borderTopWidth + borderBottomWidth);
				bool flag2 = this.currentElement.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
				if (flag2)
				{
					rectangleParams.rect.x = rectangleParams.rect.x + resolvedStyle.paddingLeft;
					rectangleParams.rect.y = rectangleParams.rect.y + resolvedStyle.paddingTop;
					rectangleParams.rect.width = rectangleParams.rect.width - (resolvedStyle.paddingLeft + resolvedStyle.paddingRight);
					rectangleParams.rect.height = rectangleParams.rect.height - (resolvedStyle.paddingTop + resolvedStyle.paddingBottom);
				}
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.stencilRef = this.m_StencilRef;
				this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
				this.m_CurrentEntry.isClipRegisterEntry = true;
				MeshBuilder.MakeSolidRect(rectangleParams, 1f, new MeshBuilder.AllocMeshData
				{
					alloc = this.m_AllocRawVertsIndicesDelegate
				});
				bool flag3 = this.m_CurrentEntry.vertices.Length > 0 && this.m_CurrentEntry.indices.Length > 0;
				if (flag3)
				{
					this.m_Entries.Add(this.m_CurrentEntry);
					this.totalVertices += this.m_CurrentEntry.vertices.Length;
					this.totalIndices += this.m_CurrentEntry.indices.Length;
					this.m_ClosingInfo.needsClosing = true;
				}
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0007A128 File Offset: 0x00078328
		private void GenerateStencilClipEntryForSVGBackground()
		{
			bool flag = this.m_SVGBackgroundEntryIndex == -1;
			if (!flag)
			{
				UIRStylePainter.Entry entry = this.m_Entries[this.m_SVGBackgroundEntryIndex];
				Debug.Assert(entry.vertices.Length > 0);
				Debug.Assert(entry.indices.Length > 0);
				this.m_CurrentEntry.vertices = entry.vertices;
				this.m_CurrentEntry.indices = entry.indices;
				this.m_CurrentEntry.uvIsDisplacement = entry.uvIsDisplacement;
				this.m_CurrentEntry.clipRectID = this.m_ClipRectID;
				this.m_CurrentEntry.stencilRef = this.m_StencilRef;
				this.m_CurrentEntry.maskDepth = this.m_MaskDepth;
				this.m_CurrentEntry.isClipRegisterEntry = true;
				this.m_ClosingInfo.needsClosing = true;
				int length = this.m_CurrentEntry.vertices.Length;
				NativeSlice<Vertex> nativeSlice = this.m_VertsPool.Alloc((uint)length);
				for (int i = 0; i < length; i++)
				{
					Vertex vertex = this.m_CurrentEntry.vertices[i];
					vertex.position.z = 1f;
					nativeSlice[i] = vertex;
				}
				this.m_CurrentEntry.vertices = nativeSlice;
				this.totalVertices += this.m_CurrentEntry.vertices.Length;
				this.totalIndices += this.m_CurrentEntry.indices.Length;
				this.m_Entries.Add(this.m_CurrentEntry);
				this.m_CurrentEntry = default(UIRStylePainter.Entry);
			}
		}

		// Token: 0x04000CDA RID: 3290
		private RenderChain m_Owner;

		// Token: 0x04000CDB RID: 3291
		private List<UIRStylePainter.Entry> m_Entries = new List<UIRStylePainter.Entry>();

		// Token: 0x04000CDC RID: 3292
		private AtlasBase m_Atlas;

		// Token: 0x04000CDD RID: 3293
		private VectorImageManager m_VectorImageManager;

		// Token: 0x04000CDE RID: 3294
		private UIRStylePainter.Entry m_CurrentEntry;

		// Token: 0x04000CDF RID: 3295
		private UIRStylePainter.ClosingInfo m_ClosingInfo;

		// Token: 0x04000CE0 RID: 3296
		private int m_MaskDepth;

		// Token: 0x04000CE1 RID: 3297
		private int m_StencilRef;

		// Token: 0x04000CE2 RID: 3298
		private BMPAlloc m_ClipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;

		// Token: 0x04000CE3 RID: 3299
		private int m_SVGBackgroundEntryIndex = -1;

		// Token: 0x04000CE4 RID: 3300
		private UIRStylePainter.TempDataAlloc<Vertex> m_VertsPool = new UIRStylePainter.TempDataAlloc<Vertex>(8192);

		// Token: 0x04000CE5 RID: 3301
		private UIRStylePainter.TempDataAlloc<ushort> m_IndicesPool = new UIRStylePainter.TempDataAlloc<ushort>(16384);

		// Token: 0x04000CE6 RID: 3302
		private List<MeshWriteData> m_MeshWriteDataPool;

		// Token: 0x04000CE7 RID: 3303
		private int m_NextMeshWriteDataPoolItem;

		// Token: 0x04000CE8 RID: 3304
		private MeshBuilder.AllocMeshData.Allocator m_AllocRawVertsIndicesDelegate;

		// Token: 0x04000CE9 RID: 3305
		private MeshBuilder.AllocMeshData.Allocator m_AllocThroughDrawMeshDelegate;

		// Token: 0x04000CEA RID: 3306
		private MeshBuilder.AllocMeshData.Allocator m_AllocThroughDrawGradientsDelegate;

		// Token: 0x02000349 RID: 841
		internal struct Entry
		{
			// Token: 0x04000CF0 RID: 3312
			public NativeSlice<Vertex> vertices;

			// Token: 0x04000CF1 RID: 3313
			public NativeSlice<ushort> indices;

			// Token: 0x04000CF2 RID: 3314
			public Material material;

			// Token: 0x04000CF3 RID: 3315
			public Texture custom;

			// Token: 0x04000CF4 RID: 3316
			public Texture font;

			// Token: 0x04000CF5 RID: 3317
			public float fontTexSDFScale;

			// Token: 0x04000CF6 RID: 3318
			public TextureId texture;

			// Token: 0x04000CF7 RID: 3319
			public RenderChainCommand customCommand;

			// Token: 0x04000CF8 RID: 3320
			public BMPAlloc clipRectID;

			// Token: 0x04000CF9 RID: 3321
			public VertexFlags addFlags;

			// Token: 0x04000CFA RID: 3322
			public bool uvIsDisplacement;

			// Token: 0x04000CFB RID: 3323
			public bool isTextEntry;

			// Token: 0x04000CFC RID: 3324
			public bool isClipRegisterEntry;

			// Token: 0x04000CFD RID: 3325
			public int stencilRef;

			// Token: 0x04000CFE RID: 3326
			public int maskDepth;
		}

		// Token: 0x0200034A RID: 842
		internal struct ClosingInfo
		{
			// Token: 0x04000CFF RID: 3327
			public bool needsClosing;

			// Token: 0x04000D00 RID: 3328
			public bool popViewMatrix;

			// Token: 0x04000D01 RID: 3329
			public bool popScissorClip;

			// Token: 0x04000D02 RID: 3330
			public bool blitAndPopRenderTexture;

			// Token: 0x04000D03 RID: 3331
			public bool PopDefaultMaterial;

			// Token: 0x04000D04 RID: 3332
			public RenderChainCommand clipUnregisterDrawCommand;

			// Token: 0x04000D05 RID: 3333
			public NativeSlice<Vertex> clipperRegisterVertices;

			// Token: 0x04000D06 RID: 3334
			public NativeSlice<ushort> clipperRegisterIndices;

			// Token: 0x04000D07 RID: 3335
			public int clipperRegisterIndexOffset;

			// Token: 0x04000D08 RID: 3336
			public int maskStencilRef;
		}

		// Token: 0x0200034B RID: 843
		internal struct TempDataAlloc<T> : IDisposable where T : struct
		{
			// Token: 0x06001AFC RID: 6908 RVA: 0x0007A2CF File Offset: 0x000784CF
			public TempDataAlloc(int maxPoolElems)
			{
				this.maxPoolElemCount = maxPoolElems;
				this.pool = default(NativeArray<T>);
				this.excess = new List<NativeArray<T>>();
				this.takenFromPool = 0U;
			}

			// Token: 0x06001AFD RID: 6909 RVA: 0x0007A2F8 File Offset: 0x000784F8
			public void Dispose()
			{
				foreach (NativeArray<T> nativeArray in this.excess)
				{
					nativeArray.Dispose();
				}
				this.excess.Clear();
				bool isCreated = this.pool.IsCreated;
				if (isCreated)
				{
					this.pool.Dispose();
				}
			}

			// Token: 0x06001AFE RID: 6910 RVA: 0x0007A378 File Offset: 0x00078578
			internal NativeSlice<T> Alloc(uint count)
			{
				bool flag = (ulong)(this.takenFromPool + count) <= (ulong)((long)this.pool.Length);
				NativeSlice<T> nativeSlice2;
				if (flag)
				{
					NativeSlice<T> nativeSlice = this.pool.Slice((int)this.takenFromPool, (int)count);
					this.takenFromPool += count;
					nativeSlice2 = nativeSlice;
				}
				else
				{
					NativeArray<T> nativeArray = new NativeArray<T>((int)count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
					this.excess.Add(nativeArray);
					nativeSlice2 = nativeArray;
				}
				return nativeSlice2;
			}

			// Token: 0x06001AFF RID: 6911 RVA: 0x0007A3EC File Offset: 0x000785EC
			internal void SessionDone()
			{
				int num = this.pool.Length;
				foreach (NativeArray<T> nativeArray in this.excess)
				{
					bool flag = nativeArray.Length < this.maxPoolElemCount;
					if (flag)
					{
						num += nativeArray.Length;
					}
					nativeArray.Dispose();
				}
				this.excess.Clear();
				bool flag2 = num > this.pool.Length;
				if (flag2)
				{
					bool isCreated = this.pool.IsCreated;
					if (isCreated)
					{
						this.pool.Dispose();
					}
					this.pool = new NativeArray<T>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				this.takenFromPool = 0U;
			}

			// Token: 0x04000D09 RID: 3337
			private int maxPoolElemCount;

			// Token: 0x04000D0A RID: 3338
			private NativeArray<T> pool;

			// Token: 0x04000D0B RID: 3339
			private List<NativeArray<T>> excess;

			// Token: 0x04000D0C RID: 3340
			private uint takenFromPool;
		}
	}
}
