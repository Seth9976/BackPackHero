using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A9 RID: 425
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/RenderBufferManager.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	public class RenderTexture : Texture
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001212 RID: 4626
		// (set) Token: 0x06001213 RID: 4627
		public override extern int width
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001214 RID: 4628
		// (set) Token: 0x06001215 RID: 4629
		public override extern int height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001216 RID: 4630
		// (set) Token: 0x06001217 RID: 4631
		public override extern TextureDimension dimension
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001218 RID: 4632
		// (set) Token: 0x06001219 RID: 4633
		[NativeProperty("ColorFormat")]
		public new extern GraphicsFormat graphicsFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600121A RID: 4634
		// (set) Token: 0x0600121B RID: 4635
		[NativeProperty("MipMap")]
		public extern bool useMipMap
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600121C RID: 4636
		[NativeProperty("SRGBReadWrite")]
		public extern bool sRGB
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600121D RID: 4637
		// (set) Token: 0x0600121E RID: 4638
		[NativeProperty("VRUsage")]
		public extern VRTextureUsage vrUsage
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600121F RID: 4639
		// (set) Token: 0x06001220 RID: 4640
		[NativeProperty("Memoryless")]
		public extern RenderTextureMemoryless memorylessMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0001857C File Offset: 0x0001677C
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x000185D2 File Offset: 0x000167D2
		public RenderTextureFormat format
		{
			get
			{
				bool flag = this.graphicsFormat > GraphicsFormat.None;
				RenderTextureFormat renderTextureFormat;
				if (flag)
				{
					renderTextureFormat = GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
				}
				else
				{
					bool flag2 = this.GetDescriptor().shadowSamplingMode != ShadowSamplingMode.None;
					if (flag2)
					{
						renderTextureFormat = RenderTextureFormat.Shadowmap;
					}
					else
					{
						renderTextureFormat = RenderTextureFormat.Depth;
					}
				}
				return renderTextureFormat;
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001223 RID: 4643
		// (set) Token: 0x06001224 RID: 4644
		public extern GraphicsFormat stencilFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001225 RID: 4645
		// (set) Token: 0x06001226 RID: 4646
		public extern GraphicsFormat depthStencilFormat
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001227 RID: 4647
		// (set) Token: 0x06001228 RID: 4648
		public extern bool autoGenerateMips
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001229 RID: 4649
		// (set) Token: 0x0600122A RID: 4650
		public extern int volumeDepth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600122B RID: 4651
		// (set) Token: 0x0600122C RID: 4652
		public extern int antiAliasing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600122D RID: 4653
		// (set) Token: 0x0600122E RID: 4654
		public extern bool bindTextureMS
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600122F RID: 4655
		// (set) Token: 0x06001230 RID: 4656
		public extern bool enableRandomWrite
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001231 RID: 4657
		// (set) Token: 0x06001232 RID: 4658
		public extern bool useDynamicScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001233 RID: 4659
		[MethodImpl(4096)]
		private extern bool GetIsPowerOfTwo();

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x000185E8 File Offset: 0x000167E8
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00004557 File Offset: 0x00002757
		public bool isPowerOfTwo
		{
			get
			{
				return this.GetIsPowerOfTwo();
			}
			set
			{
			}
		}

		// Token: 0x06001236 RID: 4662
		[FreeFunction("RenderTexture::GetActive")]
		[MethodImpl(4096)]
		private static extern RenderTexture GetActive();

		// Token: 0x06001237 RID: 4663
		[FreeFunction("RenderTextureScripting::SetActive")]
		[MethodImpl(4096)]
		private static extern void SetActive(RenderTexture rt);

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00018600 File Offset: 0x00016800
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x00018617 File Offset: 0x00016817
		public static RenderTexture active
		{
			get
			{
				return RenderTexture.GetActive();
			}
			set
			{
				RenderTexture.SetActive(value);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00018624 File Offset: 0x00016824
		[FreeFunction(Name = "RenderTextureScripting::GetColorBuffer", HasExplicitThis = true)]
		private RenderBuffer GetColorBuffer()
		{
			RenderBuffer renderBuffer;
			this.GetColorBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0001863C File Offset: 0x0001683C
		[FreeFunction(Name = "RenderTextureScripting::GetDepthBuffer", HasExplicitThis = true)]
		private RenderBuffer GetDepthBuffer()
		{
			RenderBuffer renderBuffer;
			this.GetDepthBuffer_Injected(out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x0600123C RID: 4668
		[MethodImpl(4096)]
		private extern void SetMipMapCount(int count);

		// Token: 0x0600123D RID: 4669
		[MethodImpl(4096)]
		private extern void SetShadowSamplingMode(ShadowSamplingMode samplingMode);

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00018654 File Offset: 0x00016854
		public RenderBuffer colorBuffer
		{
			get
			{
				return this.GetColorBuffer();
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0001866C File Offset: 0x0001686C
		public RenderBuffer depthBuffer
		{
			get
			{
				return this.GetDepthBuffer();
			}
		}

		// Token: 0x06001240 RID: 4672
		[MethodImpl(4096)]
		public extern IntPtr GetNativeDepthBufferPtr();

		// Token: 0x06001241 RID: 4673
		[MethodImpl(4096)]
		public extern void DiscardContents(bool discardColor, bool discardDepth);

		// Token: 0x06001242 RID: 4674
		[Obsolete("This function has no effect.", false)]
		[MethodImpl(4096)]
		public extern void MarkRestoreExpected();

		// Token: 0x06001243 RID: 4675 RVA: 0x00018684 File Offset: 0x00016884
		public void DiscardContents()
		{
			this.DiscardContents(true, true);
		}

		// Token: 0x06001244 RID: 4676
		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(4096)]
		private extern void ResolveAA();

		// Token: 0x06001245 RID: 4677
		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(4096)]
		private extern void ResolveAATo(RenderTexture rt);

		// Token: 0x06001246 RID: 4678 RVA: 0x00018690 File Offset: 0x00016890
		public void ResolveAntiAliasedSurface()
		{
			this.ResolveAA();
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0001869A File Offset: 0x0001689A
		public void ResolveAntiAliasedSurface(RenderTexture target)
		{
			this.ResolveAATo(target);
		}

		// Token: 0x06001248 RID: 4680
		[FreeFunction(Name = "RenderTextureScripting::SetGlobalShaderProperty", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetGlobalShaderProperty(string propertyName);

		// Token: 0x06001249 RID: 4681
		[MethodImpl(4096)]
		public extern bool Create();

		// Token: 0x0600124A RID: 4682
		[MethodImpl(4096)]
		public extern void Release();

		// Token: 0x0600124B RID: 4683
		[MethodImpl(4096)]
		public extern bool IsCreated();

		// Token: 0x0600124C RID: 4684
		[MethodImpl(4096)]
		public extern void GenerateMips();

		// Token: 0x0600124D RID: 4685
		[NativeThrows]
		[MethodImpl(4096)]
		public extern void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono);

		// Token: 0x0600124E RID: 4686
		[MethodImpl(4096)]
		internal extern void SetSRGBReadWrite(bool srgb);

		// Token: 0x0600124F RID: 4687
		[FreeFunction("RenderTextureScripting::Create")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] RenderTexture rt);

		// Token: 0x06001250 RID: 4688
		[FreeFunction("RenderTextureSupportsStencil")]
		[MethodImpl(4096)]
		public static extern bool SupportsStencil(RenderTexture rt);

		// Token: 0x06001251 RID: 4689 RVA: 0x000186A5 File Offset: 0x000168A5
		[NativeName("SetRenderTextureDescFromScript")]
		private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
		{
			this.SetRenderTextureDescriptor_Injected(ref desc);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000186B0 File Offset: 0x000168B0
		[NativeName("GetRenderTextureDesc")]
		private RenderTextureDescriptor GetDescriptor()
		{
			RenderTextureDescriptor renderTextureDescriptor;
			this.GetDescriptor_Injected(out renderTextureDescriptor);
			return renderTextureDescriptor;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x000186C6 File Offset: 0x000168C6
		[FreeFunction("GetRenderBufferManager().GetTextures().GetTempBuffer")]
		private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
		{
			return RenderTexture.GetTemporary_Internal_Injected(ref desc);
		}

		// Token: 0x06001254 RID: 4692
		[FreeFunction("GetRenderBufferManager().GetTextures().ReleaseTempBuffer")]
		[MethodImpl(4096)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001255 RID: 4693
		// (set) Token: 0x06001256 RID: 4694
		public extern int depth
		{
			[FreeFunction("RenderTextureScripting::GetDepth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("RenderTextureScripting::SetDepth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000186CF File Offset: 0x000168CF
		[RequiredByNativeCode]
		protected internal RenderTexture()
		{
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000186D9 File Offset: 0x000168D9
		public RenderTexture(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000186FC File Offset: 0x000168FC
		public RenderTexture(RenderTexture textureToCopy)
		{
			bool flag = textureToCopy == null;
			if (flag)
			{
				throw new ArgumentNullException("textureToCopy");
			}
			RenderTexture.ValidateRenderTextureDesc(textureToCopy.descriptor);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(textureToCopy.descriptor);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00018747 File Offset: 0x00016947
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, DefaultFormat format)
			: this(width, height, depth, SystemInfo.GetGraphicsFormat(format))
		{
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0001875B File Offset: 0x0001695B
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format)
			: this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00018770 File Offset: 0x00016970
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
				this.width = width;
				this.height = height;
				this.graphicsFormat = format;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000187E0 File Offset: 0x000169E0
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			bool flag = colorFormat != GraphicsFormat.None && !base.ValidateFormat(colorFormat, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depthStencilFormat = depthStencilFormat;
				this.graphicsFormat = colorFormat;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(colorFormat));
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0001884A File Offset: 0x00016A4A
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
			: this(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips)
		{
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0001885E File Offset: 0x00016A5E
		public RenderTexture(int width, int height, int depth, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
		{
			this.Initialize(width, height, depth, format, readWrite, Texture.GenerateAllMips);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0001887B File Offset: 0x00016A7B
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
			: this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0001888F File Offset: 0x00016A8F
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth)
			: this(width, height, depth, RenderTextureFormat.Default)
		{
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0001889D File Offset: 0x00016A9D
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, int mipCount)
		{
			this.Initialize(width, height, depth, format, RenderTextureReadWrite.Default, mipCount);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000188B8 File Offset: 0x00016AB8
		private void Initialize(int width, int height, int depth, RenderTextureFormat format, RenderTextureReadWrite readWrite, int mipCount)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depth, compatibleFormat);
			bool flag = compatibleFormat > GraphicsFormat.None;
			if (flag)
			{
				bool flag2 = !base.ValidateFormat(compatibleFormat, FormatUsage.Render);
				if (flag2)
				{
					return;
				}
			}
			RenderTexture.Internal_Create(this);
			this.width = width;
			this.height = height;
			this.depthStencilFormat = depthStencilFormatLegacy;
			this.graphicsFormat = compatibleFormat;
			this.SetMipMapCount(mipCount);
			this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(compatibleFormat));
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00018930 File Offset: 0x00016B30
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, GraphicsFormat colorFormat)
		{
			return (colorFormat == GraphicsFormat.ShadowAuto) ? GraphicsFormatUtility.GetDepthStencilFormat(depthBits, 0) : GraphicsFormatUtility.GetDepthStencilFormat(depthBits);
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0001895C File Offset: 0x00016B5C
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00018974 File Offset: 0x00016B74
		public RenderTextureDescriptor descriptor
		{
			get
			{
				return this.GetDescriptor();
			}
			set
			{
				RenderTexture.ValidateRenderTextureDesc(value);
				this.SetRenderTextureDescriptor(value);
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00018988 File Offset: 0x00016B88
		private static void ValidateRenderTextureDesc(RenderTextureDescriptor desc)
		{
			bool flag = desc.graphicsFormat == GraphicsFormat.None && desc.depthStencilFormat == GraphicsFormat.None;
			if (flag)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat and depthStencilFormat cannot both be None.");
			}
			bool flag2 = desc.graphicsFormat != GraphicsFormat.None && !SystemInfo.IsFormatSupported(desc.graphicsFormat, FormatUsage.Render);
			if (flag2)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must be a supported GraphicsFormat. " + desc.graphicsFormat.ToString() + " is not supported on this platform.", "desc.graphicsFormat");
			}
			bool flag3 = desc.depthStencilFormat != GraphicsFormat.None && !GraphicsFormatUtility.IsDepthFormat(desc.depthStencilFormat) && !GraphicsFormatUtility.IsStencilFormat(desc.depthStencilFormat);
			if (flag3)
			{
				throw new ArgumentException("RenderTextureDesc depthStencilFormat must be a supported depth/stencil GraphicsFormat. " + desc.depthStencilFormat.ToString() + " is not supported on this platform.", "desc.depthStencilFormat");
			}
			bool flag4 = desc.width <= 0;
			if (flag4)
			{
				throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
			}
			bool flag5 = desc.height <= 0;
			if (flag5)
			{
				throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
			}
			bool flag6 = desc.volumeDepth <= 0;
			if (flag6)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
			}
			bool flag7 = desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8;
			if (flag7)
			{
				throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
			}
			bool flag8 = desc.dimension == TextureDimension.CubeArray && desc.volumeDepth % 6 != 0;
			if (flag8)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be a multiple of 6 when dimension is CubeArray", "desc.volumeDepth");
			}
			bool flag9 = desc.graphicsFormat != GraphicsFormat.ShadowAuto && desc.graphicsFormat != GraphicsFormat.DepthAuto && (GraphicsFormatUtility.IsDepthFormat(desc.graphicsFormat) || GraphicsFormatUtility.IsStencilFormat(desc.graphicsFormat));
			if (flag9)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must not be a depth/stencil format. " + desc.graphicsFormat.ToString() + " is not supported.", "desc.graphicsFormat");
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00018BB8 File Offset: 0x00016DB8
		internal static GraphicsFormat GetCompatibleFormat(RenderTextureFormat renderTextureFormat, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(renderTextureFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			bool flag = graphicsFormat == compatibleFormat;
			GraphicsFormat graphicsFormat2;
			if (flag)
			{
				graphicsFormat2 = graphicsFormat;
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' is not supported. RenderTexture::GetTemporary fallbacks to {1} format on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", graphicsFormat.ToString(), compatibleFormat.ToString()));
				graphicsFormat2 = compatibleFormat;
			}
			return graphicsFormat2;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00018C14 File Offset: 0x00016E14
		public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			desc.createdFromScript = true;
			return RenderTexture.GetTemporary_Internal(desc);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00018C3C File Offset: 0x00016E3C
		private static RenderTexture GetTemporaryImpl(int width, int height, int depthBuffer, GraphicsFormat colorFormat, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false)
		{
			return RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, colorFormat, RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, colorFormat))
			{
				msaaSamples = antiAliasing,
				memoryless = memorylessMode,
				vrUsage = vrUsage,
				useDynamicScale = useDynamicScale
			});
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00018C90 File Offset: 0x00016E90
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00018CB4 File Offset: 0x00016EB4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00018CD8 File Offset: 0x00016ED8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, memorylessMode, VRTextureUsage.None, false);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00018CFC File Offset: 0x00016EFC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, antiAliasing, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00018D1C File Offset: 0x00016F1C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, format, 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00018D3C File Offset: 0x00016F3C
		public static RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00018D68 File Offset: 0x00016F68
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00018D94 File Offset: 0x00016F94
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, memorylessMode, VRTextureUsage.None, false);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00018DBC File Offset: 0x00016FBC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), antiAliasing, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00018DE4 File Offset: 0x00016FE4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, readWrite), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00018E0C File Offset: 0x0001700C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(format, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00018E34 File Offset: 0x00017034
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			return RenderTexture.GetTemporaryImpl(width, height, depthBuffer, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00018E5C File Offset: 0x0001705C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			return RenderTexture.GetTemporaryImpl(width, height, 0, RenderTexture.GetCompatibleFormat(RenderTextureFormat.Default, RenderTextureReadWrite.Default), 1, RenderTextureMemoryless.None, VRTextureUsage.None, false);
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00018E84 File Offset: 0x00017084
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00018E9F File Offset: 0x0001709F
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isCubemap
		{
			get
			{
				return this.dimension == TextureDimension.Cube;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Cube : TextureDimension.Tex2D);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00018EB0 File Offset: 0x000170B0
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00018ECB File Offset: 0x000170CB
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isVolume
		{
			get
			{
				return this.dimension == TextureDimension.Tex3D;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Tex3D : TextureDimension.Tex2D);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00018EDC File Offset: 0x000170DC
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x00004557 File Offset: 0x00002757
		[EditorBrowsable(1)]
		[Obsolete("RenderTexture.enabled is always now, no need to use it.", false)]
		public static bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00018EF0 File Offset: 0x000170F0
		[EditorBrowsable(1)]
		[Obsolete("GetTexelOffset always returns zero now, no point in using it.", false)]
		public Vector2 GetTexelOffset()
		{
			return Vector2.zero;
		}

		// Token: 0x0600127F RID: 4735
		[MethodImpl(4096)]
		private extern void GetColorBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06001280 RID: 4736
		[MethodImpl(4096)]
		private extern void GetDepthBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06001281 RID: 4737
		[MethodImpl(4096)]
		private extern void SetRenderTextureDescriptor_Injected(ref RenderTextureDescriptor desc);

		// Token: 0x06001282 RID: 4738
		[MethodImpl(4096)]
		private extern void GetDescriptor_Injected(out RenderTextureDescriptor ret);

		// Token: 0x06001283 RID: 4739
		[MethodImpl(4096)]
		private static extern RenderTexture GetTemporary_Internal_Injected(ref RenderTextureDescriptor desc);
	}
}
