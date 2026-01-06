using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A2 RID: 418
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h")]
	[NativeHeader("Runtime/Graphics/Texture2D.h")]
	public sealed class Texture2D : Texture
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060010F8 RID: 4344
		public extern TextureFormat format
		{
			[NativeName("GetTextureFormat")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060010F9 RID: 4345
		// (set) Token: 0x060010FA RID: 4346
		public extern bool ignoreMipmapLimit
		{
			[NativeName("IgnoreMasterTextureLimit")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetIgnoreMasterTextureLimitAndReload")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060010FB RID: 4347
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D whiteTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060010FC RID: 4348
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D blackTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060010FD RID: 4349
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D redTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060010FE RID: 4350
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D grayTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060010FF RID: 4351
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D linearGrayTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001100 RID: 4352
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D normalTexture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001101 RID: 4353
		[MethodImpl(4096)]
		public extern void Compress(bool highQuality);

		// Token: 0x06001102 RID: 4354
		[FreeFunction("Texture2DScripting::Create")]
		[MethodImpl(4096)]
		private static extern bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x06001103 RID: 4355 RVA: 0x000162C8 File Offset: 0x000144C8
		private static void Internal_Create([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Texture2D.Internal_CreateImpl(mono, w, h, mipCount, format, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001104 RID: 4356
		public override extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001105 RID: 4357
		[NativeConditional("ENABLE_VIRTUALTEXTURING && UNITY_EDITOR")]
		[NativeName("VTOnly")]
		public extern bool vtOnly
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001106 RID: 4358
		[NativeName("Apply")]
		[MethodImpl(4096)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06001107 RID: 4359
		[NativeName("Reinitialize")]
		[MethodImpl(4096)]
		private extern bool ReinitializeImpl(int width, int height);

		// Token: 0x06001108 RID: 4360 RVA: 0x000162F7 File Offset: 0x000144F7
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			this.SetPixelImpl_Injected(image, mip, x, y, ref color);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00016308 File Offset: 0x00014508
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int mip, int x, int y)
		{
			Color color;
			this.GetPixelImpl_Injected(image, mip, x, y, out color);
			return color;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00016324 File Offset: 0x00014524
		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, int mip, float u, float v)
		{
			Color color;
			this.GetPixelBilinearImpl_Injected(image, mip, u, v, out color);
			return color;
		}

		// Token: 0x0600110B RID: 4363
		[FreeFunction(Name = "Texture2DScripting::ReinitializeWithFormat", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool ReinitializeWithFormatImpl(int width, int height, GraphicsFormat format, bool hasMipMap);

		// Token: 0x0600110C RID: 4364 RVA: 0x0001633F File Offset: 0x0001453F
		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			this.ReadPixelsImpl_Injected(ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x0600110D RID: 4365
		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame);

		// Token: 0x0600110E RID: 4366
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool LoadRawTextureDataImpl(IntPtr data, ulong size);

		// Token: 0x0600110F RID: 4367
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool LoadRawTextureDataImplArray(byte[] data);

		// Token: 0x06001110 RID: 4368
		[FreeFunction(Name = "Texture2DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06001111 RID: 4369
		[FreeFunction(Name = "Texture2DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x06001112 RID: 4370
		[MethodImpl(4096)]
		private extern IntPtr GetWritableImageData(int frame);

		// Token: 0x06001113 RID: 4371
		[MethodImpl(4096)]
		private extern ulong GetRawImageDataSize();

		// Token: 0x06001114 RID: 4372
		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		[MethodImpl(4096)]
		private static extern void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect);

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001115 RID: 4373
		internal extern bool isPreProcessed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001116 RID: 4374
		public extern bool streamingMipmaps
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001117 RID: 4375
		public extern int streamingMipmapsPriority
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001118 RID: 4376
		// (set) Token: 0x06001119 RID: 4377
		public extern int requestedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600111A RID: 4378
		// (set) Token: 0x0600111B RID: 4379
		public extern int minimumMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetMinimumMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetMinimumMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600111C RID: 4380
		// (set) Token: 0x0600111D RID: 4381
		internal extern bool loadAllMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600111E RID: 4382
		public extern int calculatedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetCalculatedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600111F RID: 4383
		public extern int desiredMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001120 RID: 4384
		public extern int loadingMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001121 RID: 4385
		public extern int loadedMipmapLevel
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001122 RID: 4386
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ClearRequestedMipmapLevel();

		// Token: 0x06001123 RID: 4387
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsRequestedMipmapLevelLoaded();

		// Token: 0x06001124 RID: 4388
		[FreeFunction(Name = "GetTextureStreamingManager().ClearMinimumMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void ClearMinimumMipmapLevel();

		// Token: 0x06001125 RID: 4389
		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		// Token: 0x06001126 RID: 4390
		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetAllPixels32(Color32[] colors, int miplevel);

		// Token: 0x06001127 RID: 4391
		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel);

		// Token: 0x06001128 RID: 4392
		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern byte[] GetRawTextureData();

		// Token: 0x06001129 RID: 4393
		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x0600112A RID: 4394 RVA: 0x00016350 File Offset: 0x00014550
		[ExcludeFromDocs]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return this.GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		// Token: 0x0600112B RID: 4395
		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

		// Token: 0x0600112C RID: 4396 RVA: 0x00016370 File Offset: 0x00014570
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		// Token: 0x0600112D RID: 4397
		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		// Token: 0x0600112E RID: 4398 RVA: 0x0001638C File Offset: 0x0001458C
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return this.PackTextures(textures, padding, maximumAtlasSize, false);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000163A8 File Offset: 0x000145A8
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return this.PackTextures(textures, padding, 2048);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000163C8 File Offset: 0x000145C8
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

		// Token: 0x06001131 RID: 4401 RVA: 0x00016434 File Offset: 0x00014634
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
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

		// Token: 0x06001132 RID: 4402 RVA: 0x00016498 File Offset: 0x00014698
		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, int mipCount, IntPtr nativeTex)
		{
			bool flag = this.ValidateFormat(format, width, height);
			if (flag)
			{
				Texture2D.Internal_Create(this, width, height, mipCount, format, flags, nativeTex);
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000164CA File Offset: 0x000146CA
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, TextureCreationFlags flags)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000164DE File Offset: 0x000146DE
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, format, flags, Texture.GenerateAllMips, IntPtr.Zero)
		{
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000164F7 File Offset: 0x000146F7
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags)
			: this(width, height, format, flags, mipCount, IntPtr.Zero)
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00016510 File Offset: 0x00014710
		internal Texture2D(int width, int height, TextureFormat textureFormat, int mipCount, bool linear, IntPtr nativeTex)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = ((mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None);
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Texture2D.Internal_Create(this, width, height, mipCount, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0001656B File Offset: 0x0001476B
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipCount, linear, IntPtr.Zero)
		{
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00016581 File Offset: 0x00014781
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipChain ? (-1) : 1, linear, IntPtr.Zero)
		{
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0001659D File Offset: 0x0001479D
		[ExcludeFromDocs]
		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain)
			: this(width, height, textureFormat, mipChain ? (-1) : 1, false, IntPtr.Zero)
		{
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000165B8 File Offset: 0x000147B8
		[ExcludeFromDocs]
		public Texture2D(int width, int height)
			: this(width, height, TextureFormat.RGBA32, Texture.GenerateAllMips, false, IntPtr.Zero)
		{
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000165D0 File Offset: 0x000147D0
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain ? (-1) : 1, linear, nativeTex);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00016610 File Offset: 0x00014810
		[ExcludeFromDocs]
		public void SetPixel(int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, 0, x, y, color);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00016640 File Offset: 0x00014840
		public void SetPixel(int x, int y, Color color, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, mipLevel, x, y, color);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00016670 File Offset: 0x00014870
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000166A4 File Offset: 0x000148A4
		[ExcludeFromDocs]
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			this.SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000166B8 File Offset: 0x000148B8
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000166FF File Offset: 0x000148FF
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			this.SetPixels(0, 0, this.width, this.height, colors, 0);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0001671C File Offset: 0x0001491C
		[ExcludeFromDocs]
		public Color GetPixel(int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, 0, x, y);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00016750 File Offset: 0x00014950
		public Color GetPixel(int x, int y, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, mipLevel, x, y);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00016784 File Offset: 0x00014984
		[ExcludeFromDocs]
		public Color GetPixelBilinear(float u, float v)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, 0, u, v);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000167B8 File Offset: 0x000149B8
		public Color GetPixelBilinear(float u, float v, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, mipLevel, u, v);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000167EC File Offset: 0x000149EC
		public void LoadRawTextureData(IntPtr data, int size)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImpl(data, (ulong)((long)size));
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00016854 File Offset: 0x00014A54
		public void LoadRawTextureData(byte[] data)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImplArray(data);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000168B0 File Offset: 0x00014AB0
		public void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !data.IsCreated || data.Length == 0;
			if (flag2)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			bool flag3 = !this.LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), (ulong)((long)data.Length * (long)UnsafeUtility.SizeOf<T>()));
			if (flag3)
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0001692C File Offset: 0x00014B2C
		public void SetPixelData<T>(T[] data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0)
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = data == null || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImplArray(data, mipLevel, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0001699C File Offset: 0x00014B9C
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = !data.IsCreated || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00016A18 File Offset: 0x00014C18
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = mipLevel < 0 || mipLevel >= base.mipmapCount;
			if (flag2)
			{
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. It needs to be in the range 0 and " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = this.GetWritableImageData(0).ToInt64() == 0L;
			if (flag3)
			{
				throw new UnityException("Texture '" + base.name + "' has no data.");
			}
			ulong pixelDataOffset = base.GetPixelDataOffset(mipLevel, 0);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, 0);
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = pixelDataSize / (ulong)((long)num);
			bool flag4 = num2 > 2147483647UL;
			if (flag4)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr intPtr;
			intPtr..ctor((long)this.GetWritableImageData(0) + (long)pixelDataOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)intPtr, (int)num2, Allocator.None);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00016B1C File Offset: 0x00014D1C
		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = this.GetRawImageDataSize() / (ulong)((long)num);
			bool flag2 = num2 > 2147483647UL;
			if (flag2)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetWritableImageData(0), (int)num2, Allocator.None);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00016B84 File Offset: 0x00014D84
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00016BB0 File Offset: 0x00014DB0
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00016BBC File Offset: 0x00014DBC
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00016BC8 File Offset: 0x00014DC8
		public bool Reinitialize(int width, int height)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeImpl(width, height);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00016BF8 File Offset: 0x00014DF8
		public bool Reinitialize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.ReinitializeWithFormatImpl(width, height, GraphicsFormatUtility.GetGraphicsFormat(format, base.activeTextureColorSpace == ColorSpace.Gamma), hasMipMap);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00016C24 File Offset: 0x00014E24
		public bool Reinitialize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeWithFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00016C58 File Offset: 0x00014E58
		[Obsolete("Texture2D.Resize(int, int) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32)", false)]
		public bool Resize(int width, int height)
		{
			return this.Reinitialize(width, height);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00016C74 File Offset: 0x00014E74
		[Obsolete("Texture2D.Resize(int, int, TextureFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, TextureFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.TextureFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00016C94 File Offset: 0x00014E94
		[Obsolete("Texture2D.Resize(int, int, GraphicsFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, GraphicsFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.Experimental.Rendering.GraphicsFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00016CB4 File Offset: 0x00014EB4
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00016CE3 File Offset: 0x00014EE3
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			this.ReadPixels(source, destX, destY, true);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00016CF4 File Offset: 0x00014EF4
		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			bool flag = sizes == null;
			if (flag)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentException("results list cannot be null");
			}
			bool flag3 = padding < 0;
			if (flag3)
			{
				throw new ArgumentException("padding can not be negative");
			}
			bool flag4 = atlasSize <= 0;
			if (flag4)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			bool flag5 = sizes.Length == 0;
			bool flag6;
			if (flag5)
			{
				flag6 = true;
			}
			else
			{
				NoAllocHelpers.EnsureListElemCount<Rect>(results, sizes.Length);
				Texture2D.GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromListT<Rect>(results));
				flag6 = results.Count != 0;
			}
			return flag6;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00016D90 File Offset: 0x00014F90
		public void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00016D9C File Offset: 0x00014F9C
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00016DBB File Offset: 0x00014FBB
		[ExcludeFromDocs]
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00016DD0 File Offset: 0x00014FD0
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00016E1C File Offset: 0x0001501C
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		// Token: 0x0600115F RID: 4447
		[MethodImpl(4096)]
		private extern void SetPixelImpl_Injected(int image, int mip, int x, int y, ref Color color);

		// Token: 0x06001160 RID: 4448
		[MethodImpl(4096)]
		private extern void GetPixelImpl_Injected(int image, int mip, int x, int y, out Color ret);

		// Token: 0x06001161 RID: 4449
		[MethodImpl(4096)]
		private extern void GetPixelBilinearImpl_Injected(int image, int mip, float u, float v, out Color ret);

		// Token: 0x06001162 RID: 4450
		[MethodImpl(4096)]
		private extern void ReadPixelsImpl_Injected(ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x040005B7 RID: 1463
		internal const int streamingMipmapsPriorityMin = -128;

		// Token: 0x040005B8 RID: 1464
		internal const int streamingMipmapsPriorityMax = 127;

		// Token: 0x020001A3 RID: 419
		[Flags]
		public enum EXRFlags
		{
			// Token: 0x040005BA RID: 1466
			None = 0,
			// Token: 0x040005BB RID: 1467
			OutputAsFloat = 1,
			// Token: 0x040005BC RID: 1468
			CompressZIP = 2,
			// Token: 0x040005BD RID: 1469
			CompressRLE = 4,
			// Token: 0x040005BE RID: 1470
			CompressPIZ = 8
		}
	}
}
