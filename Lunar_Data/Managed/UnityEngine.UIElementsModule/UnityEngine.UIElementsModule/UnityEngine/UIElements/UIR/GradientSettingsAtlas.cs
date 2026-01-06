using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x020002FD RID: 765
	internal class GradientSettingsAtlas : IDisposable
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x00064144 File Offset: 0x00062344
		internal int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0006415C File Offset: 0x0006235C
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x00064164 File Offset: 0x00062364
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001916 RID: 6422 RVA: 0x0006416D File Offset: 0x0006236D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00064180 File Offset: 0x00062380
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_Atlas);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000641B7 File Offset: 0x000623B7
		public GradientSettingsAtlas(int length = 4096)
		{
			this.m_Length = length;
			this.m_ElemWidth = 3;
			this.Reset();
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000641D8 File Offset: 0x000623D8
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator = new BestFitAllocator((uint)this.m_Length);
				UIRUtility.Destroy(this.m_Atlas);
				this.m_RawAtlas = default(GradientSettingsAtlas.RawTexture);
				this.MustCommit = false;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0006422C File Offset: 0x0006242C
		public Texture2D atlas
		{
			get
			{
				return this.m_Atlas;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00064244 File Offset: 0x00062444
		public Alloc Add(int count)
		{
			Debug.Assert(count > 0);
			bool disposed = this.disposed;
			Alloc alloc;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Alloc);
			}
			else
			{
				Alloc alloc2 = this.m_Allocator.Allocate((uint)count);
				alloc = alloc2;
			}
			return alloc;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0006428C File Offset: 0x0006248C
		public void Remove(Alloc alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000642BC File Offset: 0x000624BC
		public void Write(Alloc alloc, GradientSettings[] settings, GradientRemap remap)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_RawAtlas.rgba == null;
				if (flag)
				{
					this.m_RawAtlas = new GradientSettingsAtlas.RawTexture
					{
						rgba = new Color32[this.m_ElemWidth * this.m_Length],
						width = this.m_ElemWidth,
						height = this.m_Length
					};
					int num = this.m_ElemWidth * this.m_Length;
					for (int i = 0; i < num; i++)
					{
						this.m_RawAtlas.rgba[i] = Color.black;
					}
				}
				GradientSettingsAtlas.s_MarkerWrite.Begin();
				int num2 = (int)alloc.start;
				int j = 0;
				int num3 = settings.Length;
				while (j < num3)
				{
					int num4 = 0;
					GradientSettings gradientSettings = settings[j];
					Debug.Assert(remap == null || num2 == remap.destIndex);
					bool flag2 = gradientSettings.gradientType == GradientType.Radial;
					if (flag2)
					{
						Vector2 vector = gradientSettings.radialFocus;
						vector += Vector2.one;
						vector /= 2f;
						vector.y = 1f - vector.y;
						this.m_RawAtlas.WriteRawFloat4Packed(0.003921569f, (float)gradientSettings.addressMode / 255f, vector.x, vector.y, num4++, num2);
					}
					else
					{
						bool flag3 = gradientSettings.gradientType == GradientType.Linear;
						if (flag3)
						{
							this.m_RawAtlas.WriteRawFloat4Packed(0f, (float)gradientSettings.addressMode / 255f, 0f, 0f, num4++, num2);
						}
					}
					Vector2Int vector2Int = new Vector2Int(gradientSettings.location.x, gradientSettings.location.y);
					Vector2 vector2 = new Vector2((float)(gradientSettings.location.width - 1), (float)(gradientSettings.location.height - 1));
					bool flag4 = remap != null;
					if (flag4)
					{
						vector2Int = new Vector2Int(remap.location.x, remap.location.y);
						vector2 = new Vector2((float)(remap.location.width - 1), (float)(remap.location.height - 1));
					}
					this.m_RawAtlas.WriteRawInt2Packed(vector2Int.x, vector2Int.y, num4++, num2);
					this.m_RawAtlas.WriteRawInt2Packed((int)vector2.x, (int)vector2.y, num4++, num2);
					remap = ((remap != null) ? remap.next : null);
					num2++;
					j++;
				}
				this.MustCommit = true;
				GradientSettingsAtlas.s_MarkerWrite.End();
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x00064593 File Offset: 0x00062793
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x0006459B File Offset: 0x0006279B
		public bool MustCommit { get; private set; }

		// Token: 0x06001920 RID: 6432 RVA: 0x000645A4 File Offset: 0x000627A4
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.MustCommit;
				if (!flag)
				{
					this.PrepareAtlas();
					GradientSettingsAtlas.s_MarkerCommit.Begin();
					this.m_Atlas.SetPixels32(this.m_RawAtlas.rgba);
					this.m_Atlas.Apply();
					GradientSettingsAtlas.s_MarkerCommit.End();
					this.MustCommit = false;
				}
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00064620 File Offset: 0x00062820
		private void PrepareAtlas()
		{
			bool flag = this.m_Atlas != null;
			if (!flag)
			{
				this.m_Atlas = new Texture2D(this.m_ElemWidth, this.m_Length, TextureFormat.ARGB32, 0, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "GradientSettings " + GradientSettingsAtlas.s_TextureCounter++.ToString(),
					filterMode = FilterMode.Point
				};
			}
		}

		// Token: 0x04000ABF RID: 2751
		private static ProfilerMarker s_MarkerWrite = new ProfilerMarker("UIR.GradientSettingsAtlas.Write");

		// Token: 0x04000AC0 RID: 2752
		private static ProfilerMarker s_MarkerCommit = new ProfilerMarker("UIR.GradientSettingsAtlas.Commit");

		// Token: 0x04000AC1 RID: 2753
		private readonly int m_Length;

		// Token: 0x04000AC2 RID: 2754
		private readonly int m_ElemWidth;

		// Token: 0x04000AC3 RID: 2755
		private BestFitAllocator m_Allocator;

		// Token: 0x04000AC4 RID: 2756
		private Texture2D m_Atlas;

		// Token: 0x04000AC5 RID: 2757
		private GradientSettingsAtlas.RawTexture m_RawAtlas;

		// Token: 0x04000AC6 RID: 2758
		private static int s_TextureCounter;

		// Token: 0x020002FE RID: 766
		private struct RawTexture
		{
			// Token: 0x06001923 RID: 6435 RVA: 0x000646B4 File Offset: 0x000628B4
			public void WriteRawInt2Packed(int v0, int v1, int destX, int destY)
			{
				byte b = (byte)(v0 / 255);
				byte b2 = (byte)(v0 - (int)(b * byte.MaxValue));
				byte b3 = (byte)(v1 / 255);
				byte b4 = (byte)(v1 - (int)(b3 * byte.MaxValue));
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, b2, b3, b4);
			}

			// Token: 0x06001924 RID: 6436 RVA: 0x00064710 File Offset: 0x00062910
			public void WriteRawFloat4Packed(float f0, float f1, float f2, float f3, int destX, int destY)
			{
				byte b = (byte)(f0 * 255f + 0.5f);
				byte b2 = (byte)(f1 * 255f + 0.5f);
				byte b3 = (byte)(f2 * 255f + 0.5f);
				byte b4 = (byte)(f3 * 255f + 0.5f);
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, b2, b3, b4);
			}

			// Token: 0x04000AC9 RID: 2761
			public Color32[] rgba;

			// Token: 0x04000ACA RID: 2762
			public int width;

			// Token: 0x04000ACB RID: 2763
			public int height;
		}
	}
}
