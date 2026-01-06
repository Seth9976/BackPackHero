using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000327 RID: 807
	internal class VectorImageManager : IDisposable
	{
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0006E664 File Offset: 0x0006C864
		public Texture2D atlas
		{
			get
			{
				GradientSettingsAtlas gradientSettingsAtlas = this.m_GradientSettingsAtlas;
				return (gradientSettingsAtlas != null) ? gradientSettingsAtlas.atlas : null;
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0006E688 File Offset: 0x0006C888
		public VectorImageManager(AtlasBase atlas)
		{
			VectorImageManager.instances.Add(this);
			this.m_Atlas = atlas;
			this.m_Registered = new Dictionary<VectorImage, VectorImageRenderInfo>(32);
			this.m_RenderInfoPool = new VectorImageRenderInfoPool();
			this.m_GradientRemapPool = new GradientRemapPool();
			this.m_GradientSettingsAtlas = new GradientSettingsAtlas(4096);
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0006E6E3 File Offset: 0x0006C8E3
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x0006E6EB File Offset: 0x0006C8EB
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001A02 RID: 6658 RVA: 0x0006E6F4 File Offset: 0x0006C8F4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0006E708 File Offset: 0x0006C908
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Registered.Clear();
					this.m_RenderInfoPool.Clear();
					this.m_GradientRemapPool.Clear();
					this.m_GradientSettingsAtlas.Dispose();
					VectorImageManager.instances.Remove(this);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0006E770 File Offset: 0x0006C970
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Registered.Clear();
				this.m_RenderInfoPool.Clear();
				this.m_GradientRemapPool.Clear();
				this.m_GradientSettingsAtlas.Reset();
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0006E7C4 File Offset: 0x0006C9C4
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_GradientSettingsAtlas.Commit();
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0006E7F4 File Offset: 0x0006C9F4
		public GradientRemap AddUser(VectorImage vi, VisualElement context)
		{
			bool disposed = this.disposed;
			GradientRemap gradientRemap;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				gradientRemap = null;
			}
			else
			{
				bool flag = vi == null;
				if (flag)
				{
					gradientRemap = null;
				}
				else
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, ref vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount++;
					}
					else
					{
						vectorImageRenderInfo = this.Register(vi, context);
					}
					gradientRemap = vectorImageRenderInfo.firstGradientRemap;
				}
			}
			return gradientRemap;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0006E860 File Offset: 0x0006CA60
		public void RemoveUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = vi == null;
				if (!flag)
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, ref vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount--;
						bool flag3 = vectorImageRenderInfo.useCount == 0;
						if (flag3)
						{
							this.Unregister(vi, vectorImageRenderInfo);
						}
					}
				}
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0006E8CC File Offset: 0x0006CACC
		private VectorImageRenderInfo Register(VectorImage vi, VisualElement context)
		{
			VectorImageManager.s_MarkerRegister.Begin();
			VectorImageRenderInfo vectorImageRenderInfo = this.m_RenderInfoPool.Get();
			vectorImageRenderInfo.useCount = 1;
			this.m_Registered[vi] = vectorImageRenderInfo;
			GradientSettings[] settings = vi.settings;
			bool flag = settings != null && settings.Length != 0;
			if (flag)
			{
				int num = vi.settings.Length;
				Alloc alloc = this.m_GradientSettingsAtlas.Add(num);
				bool flag2 = alloc.size > 0U;
				if (flag2)
				{
					TextureId textureId;
					RectInt rectInt;
					bool flag3 = this.m_Atlas.TryGetAtlas(context, vi.atlas, out textureId, out rectInt);
					if (flag3)
					{
						GradientRemap gradientRemap = null;
						for (int i = 0; i < num; i++)
						{
							GradientRemap gradientRemap2 = this.m_GradientRemapPool.Get();
							bool flag4 = i > 0;
							if (flag4)
							{
								gradientRemap.next = gradientRemap2;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap2;
							}
							gradientRemap = gradientRemap2;
							gradientRemap2.origIndex = i;
							gradientRemap2.destIndex = (int)(alloc.start + (uint)i);
							GradientSettings gradientSettings = vi.settings[i];
							RectInt location = gradientSettings.location;
							location.x += rectInt.x;
							location.y += rectInt.y;
							gradientRemap2.location = location;
							gradientRemap2.atlas = textureId;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, vectorImageRenderInfo.firstGradientRemap);
					}
					else
					{
						GradientRemap gradientRemap3 = null;
						for (int j = 0; j < num; j++)
						{
							GradientRemap gradientRemap4 = this.m_GradientRemapPool.Get();
							bool flag5 = j > 0;
							if (flag5)
							{
								gradientRemap3.next = gradientRemap4;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap4;
							}
							gradientRemap3 = gradientRemap4;
							gradientRemap4.origIndex = j;
							gradientRemap4.destIndex = (int)(alloc.start + (uint)j);
							gradientRemap4.atlas = TextureId.invalid;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, null);
					}
				}
				else
				{
					bool flag6 = !this.m_LoggedExhaustedSettingsAtlas;
					if (flag6)
					{
						string text = "Exhausted max gradient settings (";
						string text2 = this.m_GradientSettingsAtlas.length.ToString();
						string text3 = ") for atlas: ";
						Texture2D atlas = this.m_GradientSettingsAtlas.atlas;
						Debug.LogError(text + text2 + text3 + ((atlas != null) ? atlas.name : null));
						this.m_LoggedExhaustedSettingsAtlas = true;
					}
				}
			}
			VectorImageManager.s_MarkerRegister.End();
			return vectorImageRenderInfo;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		private void Unregister(VectorImage vi, VectorImageRenderInfo renderInfo)
		{
			VectorImageManager.s_MarkerUnregister.Begin();
			bool flag = renderInfo.gradientSettingsAlloc.size > 0U;
			if (flag)
			{
				this.m_GradientSettingsAtlas.Remove(renderInfo.gradientSettingsAlloc);
			}
			GradientRemap next;
			for (GradientRemap gradientRemap = renderInfo.firstGradientRemap; gradientRemap != null; gradientRemap = next)
			{
				next = gradientRemap.next;
				this.m_GradientRemapPool.Return(gradientRemap);
			}
			this.m_Registered.Remove(vi);
			this.m_RenderInfoPool.Return(renderInfo);
			VectorImageManager.s_MarkerUnregister.End();
		}

		// Token: 0x04000BE0 RID: 3040
		public static List<VectorImageManager> instances = new List<VectorImageManager>(16);

		// Token: 0x04000BE1 RID: 3041
		private static ProfilerMarker s_MarkerRegister = new ProfilerMarker("UIR.VectorImageManager.Register");

		// Token: 0x04000BE2 RID: 3042
		private static ProfilerMarker s_MarkerUnregister = new ProfilerMarker("UIR.VectorImageManager.Unregister");

		// Token: 0x04000BE3 RID: 3043
		private readonly AtlasBase m_Atlas;

		// Token: 0x04000BE4 RID: 3044
		private Dictionary<VectorImage, VectorImageRenderInfo> m_Registered;

		// Token: 0x04000BE5 RID: 3045
		private VectorImageRenderInfoPool m_RenderInfoPool;

		// Token: 0x04000BE6 RID: 3046
		private GradientRemapPool m_GradientRemapPool;

		// Token: 0x04000BE7 RID: 3047
		private GradientSettingsAtlas m_GradientSettingsAtlas;

		// Token: 0x04000BE8 RID: 3048
		private bool m_LoggedExhaustedSettingsAtlas;
	}
}
