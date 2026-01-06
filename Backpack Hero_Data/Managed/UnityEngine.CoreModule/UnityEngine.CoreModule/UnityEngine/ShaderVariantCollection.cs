using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000239 RID: 569
	public sealed class ShaderVariantCollection : Object
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600186E RID: 6254
		public extern int shaderCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600186F RID: 6255
		public extern int variantCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001870 RID: 6256
		public extern bool isWarmedUp
		{
			[NativeName("IsWarmedUp")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001871 RID: 6257
		[MethodImpl(4096)]
		private extern bool AddVariant(Shader shader, PassType passType, string[] keywords);

		// Token: 0x06001872 RID: 6258
		[MethodImpl(4096)]
		private extern bool RemoveVariant(Shader shader, PassType passType, string[] keywords);

		// Token: 0x06001873 RID: 6259
		[MethodImpl(4096)]
		private extern bool ContainsVariant(Shader shader, PassType passType, string[] keywords);

		// Token: 0x06001874 RID: 6260
		[NativeName("ClearVariants")]
		[MethodImpl(4096)]
		public extern void Clear();

		// Token: 0x06001875 RID: 6261
		[NativeName("WarmupShaders")]
		[MethodImpl(4096)]
		public extern void WarmUp();

		// Token: 0x06001876 RID: 6262
		[NativeName("CreateFromScript")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] ShaderVariantCollection svc);

		// Token: 0x06001877 RID: 6263 RVA: 0x0002778B File Offset: 0x0002598B
		public ShaderVariantCollection()
		{
			ShaderVariantCollection.Internal_Create(this);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0002779C File Offset: 0x0002599C
		public bool Add(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.AddVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000277C8 File Offset: 0x000259C8
		public bool Remove(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.RemoveVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000277F4 File Offset: 0x000259F4
		public bool Contains(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.ContainsVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x0200023A RID: 570
		public struct ShaderVariant
		{
			// Token: 0x0600187B RID: 6267
			[NativeConditional("UNITY_EDITOR")]
			[FreeFunction]
			[MethodImpl(4096)]
			private static extern string CheckShaderVariant(Shader shader, PassType passType, string[] keywords);

			// Token: 0x0600187C RID: 6268 RVA: 0x0002781E File Offset: 0x00025A1E
			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
			}

			// Token: 0x04000838 RID: 2104
			public Shader shader;

			// Token: 0x04000839 RID: 2105
			public PassType passType;

			// Token: 0x0400083A RID: 2106
			public string[] keywords;
		}
	}
}
