using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020001AC RID: 428
	public struct RenderTextureDescriptor
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0001902E File Offset: 0x0001722E
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00019036 File Offset: 0x00017236
		public int width { readonly get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0001903F File Offset: 0x0001723F
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00019047 File Offset: 0x00017247
		public int height { readonly get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00019050 File Offset: 0x00017250
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x00019058 File Offset: 0x00017258
		public int msaaSamples { readonly get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00019061 File Offset: 0x00017261
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x00019069 File Offset: 0x00017269
		public int volumeDepth { readonly get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00019072 File Offset: 0x00017272
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x0001907A File Offset: 0x0001727A
		public int mipCount { readonly get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00019084 File Offset: 0x00017284
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x0001909C File Offset: 0x0001729C
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this._graphicsFormat;
			}
			set
			{
				this._graphicsFormat = value;
				this.SetOrClearRenderTextureCreationFlag(GraphicsFormatUtility.IsSRGBFormat(value), RenderTextureCreationFlags.SRGB);
				this.depthBufferBits = this.depthBufferBits;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000190C1 File Offset: 0x000172C1
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x000190C9 File Offset: 0x000172C9
		public GraphicsFormat stencilFormat { readonly get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x000190D2 File Offset: 0x000172D2
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x000190DA File Offset: 0x000172DA
		public GraphicsFormat depthStencilFormat { readonly get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000190E4 File Offset: 0x000172E4
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00019104 File Offset: 0x00017304
		public RenderTextureFormat colorFormat
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
			}
			set
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
				this.graphicsFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00019130 File Offset: 0x00017330
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x0001914D File Offset: 0x0001734D
		public bool sRGB
		{
			get
			{
				return GraphicsFormatUtility.IsSRGBFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(this.colorFormat, value);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00019164 File Offset: 0x00017364
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00019181 File Offset: 0x00017381
		public int depthBufferBits
		{
			get
			{
				return GraphicsFormatUtility.GetDepthBits(this.depthStencilFormat);
			}
			set
			{
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(value, this.graphicsFormat);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00019197 File Offset: 0x00017397
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x0001919F File Offset: 0x0001739F
		public TextureDimension dimension { readonly get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x000191A8 File Offset: 0x000173A8
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x000191B0 File Offset: 0x000173B0
		public ShadowSamplingMode shadowSamplingMode { readonly get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000191B9 File Offset: 0x000173B9
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000191C1 File Offset: 0x000173C1
		public VRTextureUsage vrUsage { readonly get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x000191CC File Offset: 0x000173CC
		public RenderTextureCreationFlags flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x000191E4 File Offset: 0x000173E4
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x000191EC File Offset: 0x000173EC
		public RenderTextureMemoryless memoryless { readonly get; set; }

		// Token: 0x060012D1 RID: 4817 RVA: 0x000191F5 File Offset: 0x000173F5
		public RenderTextureDescriptor(int width, int height)
		{
			this = new RenderTextureDescriptor(width, height, RenderTextureFormat.Default);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00019202 File Offset: 0x00017402
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, 0);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00019210 File Offset: 0x00017410
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00019224 File Offset: 0x00017424
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00019238 File Offset: 0x00017438
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits, int mipCount)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(colorFormat, false);
			this = new RenderTextureDescriptor(width, height, SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render), depthBufferBits, mipCount);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00019268 File Offset: 0x00017468
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat);
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000192E8 File Offset: 0x000174E8
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000192FC File Offset: 0x000174FC
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = depthStencilFormat;
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00019378 File Offset: 0x00017578
		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				this._flags |= flag;
			}
			else
			{
				this._flags &= ~flag;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000193B0 File Offset: 0x000175B0
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x000193CD File Offset: 0x000175CD
		public bool useMipMap
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.MipMap) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x000193DC File Offset: 0x000175DC
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x000193F9 File Offset: 0x000175F9
		public bool autoGenerateMips
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.AutoGenerateMips) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00019408 File Offset: 0x00017608
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x00019426 File Offset: 0x00017626
		public bool enableRandomWrite
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.EnableRandomWrite) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00019434 File Offset: 0x00017634
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x00019455 File Offset: 0x00017655
		public bool bindMS
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.BindMS) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00019468 File Offset: 0x00017668
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00019486 File Offset: 0x00017686
		internal bool createdFromScript
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.CreatedFromScript) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00019494 File Offset: 0x00017694
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x000194B5 File Offset: 0x000176B5
		public bool useDynamicScale
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalable) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		// Token: 0x040005C9 RID: 1481
		private GraphicsFormat _graphicsFormat;

		// Token: 0x040005CF RID: 1487
		private RenderTextureCreationFlags _flags;
	}
}
