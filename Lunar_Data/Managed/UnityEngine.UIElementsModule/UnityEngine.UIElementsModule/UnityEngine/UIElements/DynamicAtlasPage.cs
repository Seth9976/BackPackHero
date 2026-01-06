using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000254 RID: 596
	internal class DynamicAtlasPage : IDisposable
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00045583 File Offset: 0x00043783
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0004558B File Offset: 0x0004378B
		public TextureId textureId { get; private set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00045594 File Offset: 0x00043794
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0004559C File Offset: 0x0004379C
		public RenderTexture atlas { get; private set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000455A5 File Offset: 0x000437A5
		public RenderTextureFormat format { get; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x000455AD File Offset: 0x000437AD
		public FilterMode filterMode { get; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x000455B5 File Offset: 0x000437B5
		public Vector2Int minSize { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x000455BD File Offset: 0x000437BD
		public Vector2Int maxSize { get; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000455C5 File Offset: 0x000437C5
		public Vector2Int currentSize
		{
			get
			{
				return this.m_CurrentSize;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000455D0 File Offset: 0x000437D0
		public DynamicAtlasPage(RenderTextureFormat format, FilterMode filterMode, Vector2Int minSize, Vector2Int maxSize)
		{
			this.textureId = TextureRegistry.instance.AllocAndAcquireDynamic();
			this.format = format;
			this.filterMode = filterMode;
			this.minSize = minSize;
			this.maxSize = maxSize;
			this.m_Allocator = new Allocator2D(minSize, maxSize, this.m_2Padding);
			this.m_Blitter = new TextureBlitter(64);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00045642 File Offset: 0x00043842
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x0004564A File Offset: 0x0004384A
		private protected bool disposed { protected get; private set; }

		// Token: 0x060011FB RID: 4603 RVA: 0x00045653 File Offset: 0x00043853
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00045668 File Offset: 0x00043868
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.atlas != null;
					if (flag)
					{
						UIRUtility.Destroy(this.atlas);
						this.atlas = null;
					}
					bool flag2 = this.m_Allocator != null;
					if (flag2)
					{
						this.m_Allocator = null;
					}
					bool flag3 = this.m_Blitter != null;
					if (flag3)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
					bool flag4 = this.textureId != TextureId.invalid;
					if (flag4)
					{
						TextureRegistry.instance.Release(this.textureId);
						this.textureId = TextureId.invalid;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00045730 File Offset: 0x00043930
		public bool TryAdd(Texture2D image, out Allocator2D.Alloc2D alloc, out RectInt rect)
		{
			bool disposed = this.disposed;
			bool flag;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Allocator2D.Alloc2D);
				rect = default(RectInt);
				flag = false;
			}
			else
			{
				bool flag2 = !this.m_Allocator.TryAllocate(image.width + this.m_2Padding, image.height + this.m_2Padding, out alloc);
				if (flag2)
				{
					rect = default(RectInt);
					flag = false;
				}
				else
				{
					this.m_CurrentSize.x = Mathf.Max(this.m_CurrentSize.x, UIRUtility.GetNextPow2(alloc.rect.xMax));
					this.m_CurrentSize.y = Mathf.Max(this.m_CurrentSize.y, UIRUtility.GetNextPow2(alloc.rect.yMax));
					rect = new RectInt(alloc.rect.xMin + this.m_1Padding, alloc.rect.yMin + this.m_1Padding, image.width, image.height);
					this.Update(image, rect);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00045848 File Offset: 0x00043A48
		public void Update(Texture2D image, RectInt rect)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(image != null && rect.width > 0 && rect.height > 0);
				this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rect.x, rect.y), true, Color.white);
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000458CC File Offset: 0x00043ACC
		public void Remove(Allocator2D.Alloc2D alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(alloc.rect.width > 0 && alloc.rect.height > 0);
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00045924 File Offset: 0x00043B24
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.UpdateAtlasTexture();
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00045960 File Offset: 0x00043B60
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag2 = this.atlas.width != this.m_CurrentSize.x || this.atlas.height != this.m_CurrentSize.y;
				if (flag2)
				{
					RenderTexture renderTexture = this.CreateAtlasTexture();
					bool flag3 = renderTexture == null;
					if (flag3)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_CurrentSize.x,
							this.m_CurrentSize.y
						});
					}
					else
					{
						this.m_Blitter.BlitOneNow(renderTexture, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					}
					UIRUtility.Destroy(this.atlas);
					this.atlas = renderTexture;
				}
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00045A94 File Offset: 0x00043C94
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_CurrentSize.x == 0 || this.m_CurrentSize.y == 0;
			RenderTexture renderTexture;
			if (flag)
			{
				renderTexture = null;
			}
			else
			{
				renderTexture = new RenderTexture(this.m_CurrentSize.x, this.m_CurrentSize.y, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas Page " + DynamicAtlasPage.s_TextureCounter++.ToString(),
					filterMode = this.filterMode
				};
			}
			return renderTexture;
		}

		// Token: 0x04000805 RID: 2053
		private readonly int m_1Padding = 1;

		// Token: 0x04000806 RID: 2054
		private readonly int m_2Padding = 2;

		// Token: 0x04000807 RID: 2055
		private Allocator2D m_Allocator;

		// Token: 0x04000808 RID: 2056
		private TextureBlitter m_Blitter;

		// Token: 0x04000809 RID: 2057
		private Vector2Int m_CurrentSize;

		// Token: 0x0400080A RID: 2058
		private static int s_TextureCounter;
	}
}
