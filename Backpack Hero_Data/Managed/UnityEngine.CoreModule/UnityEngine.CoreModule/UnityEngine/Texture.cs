using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A1 RID: 417
	[NativeHeader("Runtime/Streaming/TextureStreamingManager.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[UsedByNativeCode]
	public class Texture : Object
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		protected Texture()
		{
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060010B7 RID: 4279
		// (set) Token: 0x060010B8 RID: 4280
		[NativeProperty("GlobalMasterTextureLimit")]
		public static extern int masterTextureLimit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060010B9 RID: 4281
		public extern int mipmapCount
		{
			[NativeName("GetMipmapCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060010BA RID: 4282
		// (set) Token: 0x060010BB RID: 4283
		[NativeProperty("AnisoLimit")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060010BC RID: 4284
		[NativeName("SetGlobalAnisoLimits")]
		[MethodImpl(4096)]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00016068 File Offset: 0x00014268
		public virtual GraphicsFormat graphicsFormat
		{
			get
			{
				return GraphicsFormatUtility.GetFormat(this);
			}
		}

		// Token: 0x060010BE RID: 4286
		[MethodImpl(4096)]
		private extern int GetDataWidth();

		// Token: 0x060010BF RID: 4287
		[MethodImpl(4096)]
		private extern int GetDataHeight();

		// Token: 0x060010C0 RID: 4288
		[MethodImpl(4096)]
		private extern TextureDimension GetDimension();

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00016080 File Offset: 0x00014280
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00016098 File Offset: 0x00014298
		public virtual int width
		{
			get
			{
				return this.GetDataWidth();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000160A0 File Offset: 0x000142A0
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00016098 File Offset: 0x00014298
		public virtual int height
		{
			get
			{
				return this.GetDataHeight();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x000160B8 File Offset: 0x000142B8
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00016098 File Offset: 0x00014298
		public virtual TextureDimension dimension
		{
			get
			{
				return this.GetDimension();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060010C7 RID: 4295
		public virtual extern bool isReadable
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060010C8 RID: 4296
		// (set) Token: 0x060010C9 RID: 4297
		public extern TextureWrapMode wrapMode
		{
			[NativeName("GetWrapModeU")]
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060010CA RID: 4298
		// (set) Token: 0x060010CB RID: 4299
		public extern TextureWrapMode wrapModeU
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060010CC RID: 4300
		// (set) Token: 0x060010CD RID: 4301
		public extern TextureWrapMode wrapModeV
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060010CE RID: 4302
		// (set) Token: 0x060010CF RID: 4303
		public extern TextureWrapMode wrapModeW
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060010D0 RID: 4304
		// (set) Token: 0x060010D1 RID: 4305
		public extern FilterMode filterMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060010D2 RID: 4306
		// (set) Token: 0x060010D3 RID: 4307
		public extern int anisoLevel
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060010D4 RID: 4308
		// (set) Token: 0x060010D5 RID: 4309
		public extern float mipMapBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000160D0 File Offset: 0x000142D0
		public Vector2 texelSize
		{
			[NativeName("GetTexelSize")]
			get
			{
				Vector2 vector;
				this.get_texelSize_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x060010D7 RID: 4311
		[MethodImpl(4096)]
		public extern IntPtr GetNativeTexturePtr();

		// Token: 0x060010D8 RID: 4312 RVA: 0x000160E8 File Offset: 0x000142E8
		[Obsolete("Use GetNativeTexturePtr instead.", false)]
		public int GetNativeTextureID()
		{
			return (int)this.GetNativeTexturePtr();
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060010D9 RID: 4313
		public extern uint updateCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060010DA RID: 4314
		[MethodImpl(4096)]
		public extern void IncrementUpdateCount();

		// Token: 0x060010DB RID: 4315
		[NativeMethod("GetActiveTextureColorSpace")]
		[MethodImpl(4096)]
		private extern int Internal_GetActiveTextureColorSpace();

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00016108 File Offset: 0x00014308
		internal ColorSpace activeTextureColorSpace
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements" })]
			get
			{
				return (this.Internal_GetActiveTextureColorSpace() == 0) ? ColorSpace.Linear : ColorSpace.Gamma;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060010DD RID: 4317
		public static extern ulong totalTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTotalTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060010DE RID: 4318
		public static extern ulong desiredTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetDesiredTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060010DF RID: 4319
		public static extern ulong targetTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetTargetTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060010E0 RID: 4320
		public static extern ulong currentTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetCurrentTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060010E1 RID: 4321
		public static extern ulong nonStreamingTextureMemory
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureMemory")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060010E2 RID: 4322
		public static extern ulong streamingMipmapUploadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingMipmapUploadCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060010E3 RID: 4323
		public static extern ulong streamingRendererCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingRendererCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060010E4 RID: 4324
		public static extern ulong streamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060010E5 RID: 4325
		public static extern ulong nonStreamingTextureCount
		{
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060010E6 RID: 4326
		public static extern ulong streamingTexturePendingLoadCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTexturePendingLoadCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060010E7 RID: 4327
		public static extern ulong streamingTextureLoadingCount
		{
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureLoadingCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060010E8 RID: 4328
		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugProperties")]
		[MethodImpl(4096)]
		public static extern void SetStreamingTextureMaterialDebugProperties();

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060010E9 RID: 4329
		// (set) Token: 0x060010EA RID: 4330
		public static extern bool streamingTextureForceLoadAll
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetForceLoadAll")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetForceLoadAll")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060010EB RID: 4331
		// (set) Token: 0x060010EC RID: 4332
		public static extern bool streamingTextureDiscardUnusedMips
		{
			[FreeFunction(Name = "GetTextureStreamingManager().GetDiscardUnusedMips")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "GetTextureStreamingManager().SetDiscardUnusedMips")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060010ED RID: 4333
		// (set) Token: 0x060010EE RID: 4334
		public static extern bool allowThreadedTextureCreation
		{
			[FreeFunction(Name = "Texture2DScripting::IsCreateTextureThreadedEnabled")]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "Texture2DScripting::EnableCreateTextureThreaded")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060010EF RID: 4335
		[MethodImpl(4096)]
		internal extern ulong GetPixelDataSize(int mipLevel, int element = 0);

		// Token: 0x060010F0 RID: 4336
		[MethodImpl(4096)]
		internal extern ulong GetPixelDataOffset(int mipLevel, int element = 0);

		// Token: 0x060010F1 RID: 4337 RVA: 0x00016128 File Offset: 0x00014328
		internal bool ValidateFormat(RenderTextureFormat format)
		{
			bool flag = SystemInfo.SupportsRenderTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Debug.LogError(string.Format("RenderTexture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsRenderTextureFormat' C# API to check format support.", format.ToString()), this);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0001616C File Offset: 0x0001436C
		internal bool ValidateFormat(TextureFormat format)
		{
			bool flag = SystemInfo.SupportsTextureFormat(format);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = GraphicsFormatUtility.IsCompressedTextureFormat(format) && GraphicsFormatUtility.CanDecompressFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
				if (flag3)
				{
					Debug.LogWarning(string.Format("'{0}' is not supported on this platform. Decompressing texture. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = true;
				}
				else
				{
					Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported on this platform. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", format.ToString()), this);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000161EC File Offset: 0x000143EC
		internal bool ValidateFormat(GraphicsFormat format, FormatUsage usage)
		{
			bool flag = usage != FormatUsage.Render && (format == GraphicsFormat.ShadowAuto || format == GraphicsFormat.DepthAuto);
			bool flag2;
			if (flag)
			{
				Debug.LogWarning(string.Format("'{0}' is not allowed because it is an auto format and not an exact format. Use GraphicsFormatUtility.GetDepthStencilFormat to get an exact depth/stencil format.", format.ToString()), this);
				flag2 = false;
			}
			else
			{
				bool flag3 = SystemInfo.IsFormatSupported(format, usage);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					Debug.LogError(string.Format("Texture creation failed. '{0}' is not supported for {1} usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", format.ToString(), usage.ToString()), this);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0001627C File Offset: 0x0001447C
		internal UnityException CreateNonReadableException(Texture t)
		{
			return new UnityException(string.Format("Texture '{0}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.", t.name));
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000162A4 File Offset: 0x000144A4
		internal UnityException CreateNativeArrayLengthOverflowException()
		{
			return new UnityException("Failed to create NativeArray, length exceeds the allowed maximum of Int32.MaxValue. Use a larger type as template argument to reduce the array length.");
		}

		// Token: 0x060010F7 RID: 4343
		[MethodImpl(4096)]
		private extern void get_texelSize_Injected(out Vector2 ret);

		// Token: 0x040005B6 RID: 1462
		public static readonly int GenerateAllMips = -1;
	}
}
