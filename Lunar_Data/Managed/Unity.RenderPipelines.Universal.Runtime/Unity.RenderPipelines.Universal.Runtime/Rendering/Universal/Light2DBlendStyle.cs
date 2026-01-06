using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000022 RID: 34
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	[Serializable]
	public struct Light2DBlendStyle
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000C01C File Offset: 0x0000A21C
		internal Vector2 blendFactors
		{
			get
			{
				Vector2 vector = default(Vector2);
				switch (this.blendMode)
				{
				case Light2DBlendStyle.BlendMode.Additive:
					vector.x = 0f;
					vector.y = 1f;
					break;
				case Light2DBlendStyle.BlendMode.Multiply:
					vector.x = 1f;
					vector.y = 0f;
					break;
				case Light2DBlendStyle.BlendMode.Subtractive:
					vector.x = 0f;
					vector.y = -1f;
					break;
				default:
					vector.x = 1f;
					vector.y = 0f;
					break;
				}
				return vector;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		internal Light2DBlendStyle.MaskChannelFilter maskTextureChannelFilter
		{
			get
			{
				switch (this.maskTextureChannel)
				{
				case Light2DBlendStyle.TextureChannel.R:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.G:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.B:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.A:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 0f, 1f), new Vector4(0f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusR:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(1f, 0f, 0f, 0f), new Vector4(1f, 0f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusG:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusB:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 1f, 0f));
				case Light2DBlendStyle.TextureChannel.OneMinusA:
					return new Light2DBlendStyle.MaskChannelFilter(new Vector4(0f, 0f, 0f, 1f), new Vector4(0f, 0f, 0f, 1f));
				}
				return new Light2DBlendStyle.MaskChannelFilter(Vector4.zero, Vector4.zero);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000C2CE File Offset: 0x0000A4CE
		internal bool isDirty { readonly get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000C2DF File Offset: 0x0000A4DF
		internal bool hasRenderTarget { readonly get; set; }

		// Token: 0x040000CD RID: 205
		public string name;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		internal Light2DBlendStyle.TextureChannel maskTextureChannel;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		internal Light2DBlendStyle.BlendMode blendMode;

		// Token: 0x040000D2 RID: 210
		internal RenderTargetHandle renderTargetHandle;

		// Token: 0x0200013F RID: 319
		internal enum TextureChannel
		{
			// Token: 0x0400089B RID: 2203
			None,
			// Token: 0x0400089C RID: 2204
			R,
			// Token: 0x0400089D RID: 2205
			G,
			// Token: 0x0400089E RID: 2206
			B,
			// Token: 0x0400089F RID: 2207
			A,
			// Token: 0x040008A0 RID: 2208
			OneMinusR,
			// Token: 0x040008A1 RID: 2209
			OneMinusG,
			// Token: 0x040008A2 RID: 2210
			OneMinusB,
			// Token: 0x040008A3 RID: 2211
			OneMinusA
		}

		// Token: 0x02000140 RID: 320
		internal struct MaskChannelFilter
		{
			// Token: 0x1700021D RID: 541
			// (get) Token: 0x0600093F RID: 2367 RVA: 0x0003E35D File Offset: 0x0003C55D
			// (set) Token: 0x06000940 RID: 2368 RVA: 0x0003E365 File Offset: 0x0003C565
			public Vector4 mask { readonly get; private set; }

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06000941 RID: 2369 RVA: 0x0003E36E File Offset: 0x0003C56E
			// (set) Token: 0x06000942 RID: 2370 RVA: 0x0003E376 File Offset: 0x0003C576
			public Vector4 inverted { readonly get; private set; }

			// Token: 0x06000943 RID: 2371 RVA: 0x0003E37F File Offset: 0x0003C57F
			public MaskChannelFilter(Vector4 m, Vector4 i)
			{
				this.mask = m;
				this.inverted = i;
			}
		}

		// Token: 0x02000141 RID: 321
		internal enum BlendMode
		{
			// Token: 0x040008A7 RID: 2215
			Additive,
			// Token: 0x040008A8 RID: 2216
			Multiply,
			// Token: 0x040008A9 RID: 2217
			Subtractive
		}

		// Token: 0x02000142 RID: 322
		[Serializable]
		internal struct BlendFactors
		{
			// Token: 0x040008AA RID: 2218
			public float multiplicative;

			// Token: 0x040008AB RID: 2219
			public float additive;
		}
	}
}
