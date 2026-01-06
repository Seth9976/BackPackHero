using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001A8 RID: 424
	[NativeHeader("Runtime/Graphics/SparseTexture.h")]
	public sealed class SparseTexture : Texture
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001203 RID: 4611
		public extern int tileWidth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001204 RID: 4612
		public extern int tileHeight
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001205 RID: 4613
		public extern bool isCreated
		{
			[NativeName("IsInitialized")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001206 RID: 4614
		[FreeFunction(Name = "SparseTextureScripting::Create", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, GraphicsFormat format, int mipCount);

		// Token: 0x06001207 RID: 4615
		[FreeFunction(Name = "SparseTextureScripting::UpdateTile", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data);

		// Token: 0x06001208 RID: 4616
		[FreeFunction(Name = "SparseTextureScripting::UpdateTileRaw", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data);

		// Token: 0x06001209 RID: 4617 RVA: 0x0001833B File Offset: 0x0001653B
		public void UnloadTile(int tileX, int tileY, int miplevel)
		{
			this.UpdateTileRaw(tileX, tileY, miplevel, null);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0001834C File Offset: 0x0001654C
		internal bool ValidateFormat(TextureFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000183B8 File Offset: 0x000165B8
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sparse);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0001841C File Offset: 0x0001661C
		internal bool ValidateSize(int width, int height, GraphicsFormat format)
		{
			bool flag = (ulong)GraphicsFormatUtility.GetBlockSize(format) * (ulong)((long)width / (long)((ulong)GraphicsFormatUtility.GetBlockWidth(format))) * (ulong)((long)height / (long)((ulong)GraphicsFormatUtility.GetBlockHeight(format))) < 65536UL;
			bool flag2;
			if (flag)
			{
				Debug.LogError("SparseTexture creation failed. The minimum size in bytes of a SparseTexture is 64KB.", this);
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0001846C File Offset: 0x0001666C
		private static void ValidateIsNotCrunched(TextureFormat textureFormat)
		{
			bool flag = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
			if (flag)
			{
				throw new ArgumentException("Crunched SparseTexture is not supported.");
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0001848F File Offset: 0x0001668F
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, DefaultFormat format, int mipCount)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), mipCount)
		{
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000184A4 File Offset: 0x000166A4
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, GraphicsFormat format, int mipCount)
		{
			bool flag = !this.ValidateFormat(format, width, height);
			if (!flag)
			{
				bool flag2 = !this.ValidateSize(width, height, format);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, format, mipCount);
				}
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000184E9 File Offset: 0x000166E9
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount)
			: this(width, height, textureFormat, mipCount, false)
		{
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000184FC File Offset: 0x000166FC
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount, [DefaultValue("false")] bool linear)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				SparseTexture.ValidateIsNotCrunched(textureFormat);
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				bool flag2 = !SystemInfo.IsFormatSupported(graphicsFormat, FormatUsage.Sparse);
				if (flag2)
				{
					Debug.LogError(string.Format("Creation of a SparseTexture with '{0}' is not supported on this platform.", textureFormat));
				}
				else
				{
					bool flag3 = !this.ValidateSize(width, height, graphicsFormat);
					if (!flag3)
					{
						SparseTexture.Internal_Create(this, width, height, graphicsFormat, mipCount);
					}
				}
			}
		}
	}
}
