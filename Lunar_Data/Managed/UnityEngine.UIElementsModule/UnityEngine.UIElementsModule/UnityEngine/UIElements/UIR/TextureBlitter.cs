using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031E RID: 798
	internal class TextureBlitter : IDisposable
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0006DC2D File Offset: 0x0006BE2D
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x0006DC35 File Offset: 0x0006BE35
		private protected bool disposed { protected get; private set; }

		// Token: 0x060019DA RID: 6618 RVA: 0x0006DC3E File Offset: 0x0006BE3E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0006DC50 File Offset: 0x0006BE50
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_BlitMaterial);
					this.m_BlitMaterial = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0006DC90 File Offset: 0x0006BE90
		static TextureBlitter()
		{
			for (int i = 0; i < 8; i++)
			{
				TextureBlitter.k_TextureIds[i] = Shader.PropertyToID("_MainTex" + i.ToString());
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0006DCE5 File Offset: 0x0006BEE5
		public TextureBlitter(int capacity = 512)
		{
			this.m_PendingBlits = new List<TextureBlitter.BlitInfo>(capacity);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0006DD08 File Offset: 0x0006BF08
		public void QueueBlit(Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_PendingBlits.Add(new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				});
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0006DD70 File Offset: 0x0006BF70
		public void BlitOneNow(RenderTexture dst, Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_SingleBlit[0] = new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				};
				this.BeginBlit(dst);
				this.DoBlit(this.m_SingleBlit, 0);
				this.EndBlit();
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0006DDF5 File Offset: 0x0006BFF5
		public int queueLength
		{
			get
			{
				return this.m_PendingBlits.Count;
			}
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0006DE04 File Offset: 0x0006C004
		public void Commit(RenderTexture dst)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_PendingBlits.Count == 0;
				if (!flag)
				{
					TextureBlitter.s_CommitSampler.Begin();
					this.BeginBlit(dst);
					for (int i = 0; i < this.m_PendingBlits.Count; i += 8)
					{
						this.DoBlit(this.m_PendingBlits, i);
					}
					this.EndBlit();
					TextureBlitter.s_CommitSampler.End();
					this.m_PendingBlits.Clear();
				}
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0006DE93 File Offset: 0x0006C093
		public void Reset()
		{
			this.m_PendingBlits.Clear();
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0006DEA4 File Offset: 0x0006C0A4
		private void BeginBlit(RenderTexture dst)
		{
			bool flag = this.m_BlitMaterial == null;
			if (flag)
			{
				Shader shader = Shader.Find(Shaders.k_AtlasBlit);
				this.m_BlitMaterial = new Material(shader);
				this.m_BlitMaterial.hideFlags |= HideFlags.DontSaveInEditor;
			}
			bool flag2 = this.m_Properties == null;
			if (flag2)
			{
				this.m_Properties = new MaterialPropertyBlock();
			}
			this.m_Viewport = Utility.GetActiveViewport();
			this.m_PrevRT = RenderTexture.active;
			GL.LoadPixelMatrix(0f, (float)dst.width, 0f, (float)dst.height);
			Graphics.SetRenderTarget(dst);
			this.m_BlitMaterial.SetPass(0);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0006DF50 File Offset: 0x0006C150
		private void DoBlit(IList<TextureBlitter.BlitInfo> blitInfos, int startIndex)
		{
			int num = Mathf.Min(blitInfos.Count - startIndex, 8);
			int num2 = startIndex + num;
			int i = startIndex;
			int num3 = 0;
			while (i < num2)
			{
				Texture src = blitInfos[i].src;
				bool flag = src != null;
				if (flag)
				{
					this.m_Properties.SetTexture(TextureBlitter.k_TextureIds[num3], src);
				}
				i++;
				num3++;
			}
			Utility.SetPropertyBlock(this.m_Properties);
			GL.Begin(7);
			int j = startIndex;
			int num4 = 0;
			while (j < num2)
			{
				TextureBlitter.BlitInfo blitInfo = blitInfos[j];
				float num5 = 1f / (float)blitInfo.src.width;
				float num6 = 1f / (float)blitInfo.src.height;
				float num7 = (float)(blitInfo.dstPos.x - blitInfo.border);
				float num8 = (float)(blitInfo.dstPos.y - blitInfo.border);
				float num9 = (float)(blitInfo.dstPos.x + blitInfo.srcRect.width + blitInfo.border);
				float num10 = (float)(blitInfo.dstPos.y + blitInfo.srcRect.height + blitInfo.border);
				float num11 = (float)(blitInfo.srcRect.x - blitInfo.border) * num5;
				float num12 = (float)(blitInfo.srcRect.y - blitInfo.border) * num6;
				float num13 = (float)(blitInfo.srcRect.xMax + blitInfo.border) * num5;
				float num14 = (float)(blitInfo.srcRect.yMax + blitInfo.border) * num6;
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num11, num12, (float)num4);
				GL.Vertex3(num7, num8, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num11, num14, (float)num4);
				GL.Vertex3(num7, num10, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num13, num14, (float)num4);
				GL.Vertex3(num9, num10, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num13, num12, (float)num4);
				GL.Vertex3(num9, num8, 0f);
				j++;
				num4++;
			}
			GL.End();
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0006E1B4 File Offset: 0x0006C3B4
		private void EndBlit()
		{
			Graphics.SetRenderTarget(this.m_PrevRT);
			GL.Viewport(new Rect((float)this.m_Viewport.x, (float)this.m_Viewport.y, (float)this.m_Viewport.width, (float)this.m_Viewport.height));
		}

		// Token: 0x04000BB9 RID: 3001
		private const int k_TextureSlotCount = 8;

		// Token: 0x04000BBA RID: 3002
		private static readonly int[] k_TextureIds = new int[8];

		// Token: 0x04000BBB RID: 3003
		private static ProfilerMarker s_CommitSampler = new ProfilerMarker("UIR.TextureBlitter.Commit");

		// Token: 0x04000BBC RID: 3004
		private TextureBlitter.BlitInfo[] m_SingleBlit = new TextureBlitter.BlitInfo[1];

		// Token: 0x04000BBD RID: 3005
		private Material m_BlitMaterial;

		// Token: 0x04000BBE RID: 3006
		private MaterialPropertyBlock m_Properties;

		// Token: 0x04000BBF RID: 3007
		private RectInt m_Viewport;

		// Token: 0x04000BC0 RID: 3008
		private RenderTexture m_PrevRT;

		// Token: 0x04000BC1 RID: 3009
		private List<TextureBlitter.BlitInfo> m_PendingBlits;

		// Token: 0x0200031F RID: 799
		private struct BlitInfo
		{
			// Token: 0x04000BC3 RID: 3011
			public Texture src;

			// Token: 0x04000BC4 RID: 3012
			public RectInt srcRect;

			// Token: 0x04000BC5 RID: 3013
			public Vector2Int dstPos;

			// Token: 0x04000BC6 RID: 3014
			public int border;

			// Token: 0x04000BC7 RID: 3015
			public Color tint;
		}
	}
}
