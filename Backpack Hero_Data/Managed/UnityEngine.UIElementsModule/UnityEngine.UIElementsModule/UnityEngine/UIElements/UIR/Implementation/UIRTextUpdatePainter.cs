using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x0200034C RID: 844
	internal class UIRTextUpdatePainter : IStylePainter, IDisposable
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0007A4C4 File Offset: 0x000786C4
		public MeshGenerationContext meshGenerationContext { get; }

		// Token: 0x06001B01 RID: 6913 RVA: 0x0007A4CC File Offset: 0x000786CC
		public UIRTextUpdatePainter()
		{
			this.meshGenerationContext = new MeshGenerationContext(this);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0007A4E4 File Offset: 0x000786E4
		public void Begin(VisualElement ve, UIRenderDevice device)
		{
			Debug.Assert(ve.renderChainData.usesLegacyText && ve.renderChainData.textEntries.Count > 0);
			this.m_CurrentElement = ve;
			this.m_TextEntryIndex = 0;
			Alloc allocVerts = ve.renderChainData.data.allocVerts;
			NativeSlice<Vertex> nativeSlice = ve.renderChainData.data.allocPage.vertices.cpuData.Slice((int)allocVerts.start, (int)allocVerts.size);
			device.Update(ve.renderChainData.data, ve.renderChainData.data.allocVerts.size, out this.m_MeshDataVerts);
			RenderChainTextEntry renderChainTextEntry = ve.renderChainData.textEntries[0];
			bool flag = ve.renderChainData.textEntries.Count > 1 || renderChainTextEntry.vertexCount != this.m_MeshDataVerts.Length;
			if (flag)
			{
				this.m_MeshDataVerts.CopyFrom(nativeSlice);
			}
			int firstVertex = renderChainTextEntry.firstVertex;
			this.m_XFormClipPages = nativeSlice[firstVertex].xformClipPages;
			this.m_IDs = nativeSlice[firstVertex].ids;
			this.m_Flags = nativeSlice[firstVertex].flags;
			this.m_OpacityColorPages = nativeSlice[firstVertex].opacityColorPages;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0007A637 File Offset: 0x00078837
		public void End()
		{
			Debug.Assert(this.m_TextEntryIndex == this.m_CurrentElement.renderChainData.textEntries.Count);
			this.m_CurrentElement = null;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0007A664 File Offset: 0x00078864
		public void Dispose()
		{
			bool isCreated = this.m_DudVerts.IsCreated;
			if (isCreated)
			{
				this.m_DudVerts.Dispose();
			}
			bool isCreated2 = this.m_DudIndices.IsCreated;
			if (isCreated2)
			{
				this.m_DudIndices.Dispose();
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000020E6 File Offset: 0x000002E6
		public void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams)
		{
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000020E6 File Offset: 0x000002E6
		public void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams)
		{
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000020E6 File Offset: 0x000002E6
		public void DrawImmediate(Action callback, bool cullingEnabled)
		{
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0007A6A8 File Offset: 0x000788A8
		public VisualElement visualElement
		{
			get
			{
				return this.m_CurrentElement;
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0007A6C0 File Offset: 0x000788C0
		public MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			bool flag = this.m_DudVerts.Length < vertexCount;
			if (flag)
			{
				bool isCreated = this.m_DudVerts.IsCreated;
				if (isCreated)
				{
					this.m_DudVerts.Dispose();
				}
				this.m_DudVerts = new NativeArray<Vertex>(vertexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			bool flag2 = this.m_DudIndices.Length < indexCount;
			if (flag2)
			{
				bool isCreated2 = this.m_DudIndices.IsCreated;
				if (isCreated2)
				{
					this.m_DudIndices.Dispose();
				}
				this.m_DudIndices = new NativeArray<ushort>(indexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			return new MeshWriteData
			{
				m_Vertices = this.m_DudVerts.Slice(0, vertexCount),
				m_Indices = this.m_DudIndices.Slice(0, indexCount)
			};
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0007A77C File Offset: 0x0007897C
		public void DrawText(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			bool flag = !TextUtilities.IsFontAssigned(textParams);
			if (!flag)
			{
				float num = TextNative.ComputeTextScaling(this.m_CurrentElement.worldTransform, pixelsPerPoint);
				TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(textParams, num);
				using (NativeArray<TextVertex> vertices = TextNative.GetVertices(textNativeSettings))
				{
					List<RenderChainTextEntry> textEntries = this.m_CurrentElement.renderChainData.textEntries;
					int textEntryIndex = this.m_TextEntryIndex;
					this.m_TextEntryIndex = textEntryIndex + 1;
					RenderChainTextEntry renderChainTextEntry = textEntries[textEntryIndex];
					Vector2 offset = TextNative.GetOffset(textNativeSettings, textParams.rect);
					MeshBuilder.UpdateText(vertices, offset, this.m_CurrentElement.renderChainData.verticesSpace, this.m_XFormClipPages, this.m_IDs, this.m_Flags, this.m_OpacityColorPages, this.m_MeshDataVerts.Slice(renderChainTextEntry.firstVertex, renderChainTextEntry.vertexCount));
					renderChainTextEntry.command.state.font = textParams.font.material.mainTexture;
				}
			}
		}

		// Token: 0x04000D0D RID: 3341
		private VisualElement m_CurrentElement;

		// Token: 0x04000D0E RID: 3342
		private int m_TextEntryIndex;

		// Token: 0x04000D0F RID: 3343
		private NativeArray<Vertex> m_DudVerts;

		// Token: 0x04000D10 RID: 3344
		private NativeArray<ushort> m_DudIndices;

		// Token: 0x04000D11 RID: 3345
		private NativeSlice<Vertex> m_MeshDataVerts;

		// Token: 0x04000D12 RID: 3346
		private Color32 m_XFormClipPages;

		// Token: 0x04000D13 RID: 3347
		private Color32 m_IDs;

		// Token: 0x04000D14 RID: 3348
		private Color32 m_Flags;

		// Token: 0x04000D15 RID: 3349
		private Color32 m_OpacityColorPages;
	}
}
