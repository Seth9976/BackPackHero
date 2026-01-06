using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014A RID: 330
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/Material.h")]
	public class Material : Object
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x00011394 File Offset: 0x0000F594
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.", false)]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x06000CFB RID: 3323
		[FreeFunction("MaterialScripting::CreateWithShader")]
		[MethodImpl(4096)]
		private static extern void CreateWithShader([Writable] Material self, [NotNull("ArgumentNullException")] Shader shader);

		// Token: 0x06000CFC RID: 3324
		[FreeFunction("MaterialScripting::CreateWithMaterial")]
		[MethodImpl(4096)]
		private static extern void CreateWithMaterial([Writable] Material self, [NotNull("ArgumentNullException")] Material source);

		// Token: 0x06000CFD RID: 3325
		[FreeFunction("MaterialScripting::CreateWithString")]
		[MethodImpl(4096)]
		private static extern void CreateWithString([Writable] Material self);

		// Token: 0x06000CFE RID: 3326 RVA: 0x000113AC File Offset: 0x0000F5AC
		public Material(Shader shader)
		{
			Material.CreateWithShader(this, shader);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000113BE File Offset: 0x0000F5BE
		[RequiredByNativeCode]
		public Material(Material source)
		{
			Material.CreateWithMaterial(this, source);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000113D0 File Offset: 0x0000F5D0
		[Obsolete("Creating materials from shader source string is no longer supported. Use Shader assets instead.", false)]
		[EditorBrowsable(1)]
		public Material(string contents)
		{
			Material.CreateWithString(this);
		}

		// Token: 0x06000D01 RID: 3329
		[MethodImpl(4096)]
		internal static extern Material GetDefaultMaterial();

		// Token: 0x06000D02 RID: 3330
		[MethodImpl(4096)]
		internal static extern Material GetDefaultParticleMaterial();

		// Token: 0x06000D03 RID: 3331
		[MethodImpl(4096)]
		internal static extern Material GetDefaultLineMaterial();

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D04 RID: 3332
		// (set) Token: 0x06000D05 RID: 3333
		public extern Shader shader
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x000113E4 File Offset: 0x0000F5E4
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00011424 File Offset: 0x0000F624
		public Color color
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Color color;
				if (flag)
				{
					color = this.GetColor(firstPropertyNameIdByAttribute);
				}
				else
				{
					color = this.GetColor("_Color");
				}
				return color;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetColor(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetColor("_Color", value);
				}
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00011464 File Offset: 0x0000F664
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public Texture mainTexture
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Texture texture;
				if (flag)
				{
					texture = this.GetTexture(firstPropertyNameIdByAttribute);
				}
				else
				{
					texture = this.GetTexture("_MainTex");
				}
				return texture;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTexture(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTexture("_MainTex", value);
				}
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x000114E4 File Offset: 0x0000F6E4
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00011524 File Offset: 0x0000F724
		public Vector2 mainTextureOffset
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureOffset(firstPropertyNameIdByAttribute);
				}
				else
				{
					vector = this.GetTextureOffset("_MainTex");
				}
				return vector;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureOffset(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureOffset("_MainTex", value);
				}
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00011564 File Offset: 0x0000F764
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x000115A4 File Offset: 0x0000F7A4
		public Vector2 mainTextureScale
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureScale(firstPropertyNameIdByAttribute);
				}
				else
				{
					vector = this.GetTextureScale("_MainTex");
				}
				return vector;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureScale(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureScale("_MainTex", value);
				}
			}
		}

		// Token: 0x06000D0E RID: 3342
		[NativeName("GetFirstPropertyNameIdByAttributeFromScript")]
		[MethodImpl(4096)]
		private extern int GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags attributeFlag);

		// Token: 0x06000D0F RID: 3343
		[NativeName("HasPropertyFromScript")]
		[MethodImpl(4096)]
		public extern bool HasProperty(int nameID);

		// Token: 0x06000D10 RID: 3344 RVA: 0x000115E4 File Offset: 0x0000F7E4
		public bool HasProperty(string name)
		{
			return this.HasProperty(Shader.PropertyToID(name));
		}

		// Token: 0x06000D11 RID: 3345
		[NativeName("HasFloatFromScript")]
		[MethodImpl(4096)]
		private extern bool HasFloatImpl(int name);

		// Token: 0x06000D12 RID: 3346 RVA: 0x00011604 File Offset: 0x0000F804
		public bool HasFloat(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00011624 File Offset: 0x0000F824
		public bool HasFloat(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00011640 File Offset: 0x0000F840
		public bool HasInt(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00011660 File Offset: 0x0000F860
		public bool HasInt(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000D16 RID: 3350
		[NativeName("HasIntegerFromScript")]
		[MethodImpl(4096)]
		private extern bool HasIntImpl(int name);

		// Token: 0x06000D17 RID: 3351 RVA: 0x0001167C File Offset: 0x0000F87C
		public bool HasInteger(string name)
		{
			return this.HasIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0001169C File Offset: 0x0000F89C
		public bool HasInteger(int nameID)
		{
			return this.HasIntImpl(nameID);
		}

		// Token: 0x06000D19 RID: 3353
		[NativeName("HasTextureFromScript")]
		[MethodImpl(4096)]
		private extern bool HasTextureImpl(int name);

		// Token: 0x06000D1A RID: 3354 RVA: 0x000116B8 File Offset: 0x0000F8B8
		public bool HasTexture(string name)
		{
			return this.HasTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public bool HasTexture(int nameID)
		{
			return this.HasTextureImpl(nameID);
		}

		// Token: 0x06000D1C RID: 3356
		[NativeName("HasMatrixFromScript")]
		[MethodImpl(4096)]
		private extern bool HasMatrixImpl(int name);

		// Token: 0x06000D1D RID: 3357 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public bool HasMatrix(string name)
		{
			return this.HasMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00011714 File Offset: 0x0000F914
		public bool HasMatrix(int nameID)
		{
			return this.HasMatrixImpl(nameID);
		}

		// Token: 0x06000D1F RID: 3359
		[NativeName("HasVectorFromScript")]
		[MethodImpl(4096)]
		private extern bool HasVectorImpl(int name);

		// Token: 0x06000D20 RID: 3360 RVA: 0x00011730 File Offset: 0x0000F930
		public bool HasVector(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00011750 File Offset: 0x0000F950
		public bool HasVector(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0001176C File Offset: 0x0000F96C
		public bool HasColor(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001178C File Offset: 0x0000F98C
		public bool HasColor(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000D24 RID: 3364
		[NativeName("HasBufferFromScript")]
		[MethodImpl(4096)]
		private extern bool HasBufferImpl(int name);

		// Token: 0x06000D25 RID: 3365 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public bool HasBuffer(string name)
		{
			return this.HasBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000117C8 File Offset: 0x0000F9C8
		public bool HasBuffer(int nameID)
		{
			return this.HasBufferImpl(nameID);
		}

		// Token: 0x06000D27 RID: 3367
		[NativeName("HasConstantBufferFromScript")]
		[MethodImpl(4096)]
		private extern bool HasConstantBufferImpl(int name);

		// Token: 0x06000D28 RID: 3368 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public bool HasConstantBuffer(string name)
		{
			return this.HasConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00011804 File Offset: 0x0000FA04
		public bool HasConstantBuffer(int nameID)
		{
			return this.HasConstantBufferImpl(nameID);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D2A RID: 3370
		// (set) Token: 0x06000D2B RID: 3371
		public extern int renderQueue
		{
			[NativeName("GetActualRenderQueue")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetCustomRenderQueue")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D2C RID: 3372
		internal extern int rawRenderQueue
		{
			[NativeName("GetCustomRenderQueue")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000D2D RID: 3373
		[MethodImpl(4096)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06000D2E RID: 3374
		[MethodImpl(4096)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x06000D2F RID: 3375
		[MethodImpl(4096)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x06000D30 RID: 3376 RVA: 0x0001181D File Offset: 0x0000FA1D
		[FreeFunction("MaterialScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			this.EnableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00011827 File Offset: 0x0000FA27
		[FreeFunction("MaterialScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			this.DisableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00011831 File Offset: 0x0000FA31
		[FreeFunction("MaterialScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword_Injected(ref keyword, value);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001183C File Offset: 0x0000FA3C
		[FreeFunction("MaterialScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled_Injected(ref keyword);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00011846 File Offset: 0x0000FA46
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00011856 File Offset: 0x0000FA56
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00011866 File Offset: 0x0000FA66
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00011878 File Offset: 0x0000FA78
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x06000D38 RID: 3384
		[FreeFunction("MaterialScripting::GetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern LocalKeyword[] GetEnabledKeywords();

		// Token: 0x06000D39 RID: 3385
		[FreeFunction("MaterialScripting::SetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetEnabledKeywords(LocalKeyword[] keywords);

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00011898 File Offset: 0x0000FA98
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public LocalKeyword[] enabledKeywords
		{
			get
			{
				return this.GetEnabledKeywords();
			}
			set
			{
				this.SetEnabledKeywords(value);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D3C RID: 3388
		// (set) Token: 0x06000D3D RID: 3389
		public extern MaterialGlobalIlluminationFlags globalIlluminationFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D3E RID: 3390
		// (set) Token: 0x06000D3F RID: 3391
		public extern bool doubleSidedGI
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D40 RID: 3392
		// (set) Token: 0x06000D41 RID: 3393
		[NativeProperty("EnableInstancingVariants")]
		public extern bool enableInstancing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D42 RID: 3394
		public extern int passCount
		{
			[NativeName("GetShader()->GetPassCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000D43 RID: 3395
		[FreeFunction("MaterialScripting::SetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetShaderPassEnabled(string passName, bool enabled);

		// Token: 0x06000D44 RID: 3396
		[FreeFunction("MaterialScripting::GetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool GetShaderPassEnabled(string passName);

		// Token: 0x06000D45 RID: 3397
		[MethodImpl(4096)]
		public extern string GetPassName(int pass);

		// Token: 0x06000D46 RID: 3398
		[MethodImpl(4096)]
		public extern int FindPass(string passName);

		// Token: 0x06000D47 RID: 3399
		[MethodImpl(4096)]
		public extern void SetOverrideTag(string tag, string val);

		// Token: 0x06000D48 RID: 3400
		[NativeName("GetTag")]
		[MethodImpl(4096)]
		private extern string GetTagImpl(string tag, bool currentSubShaderOnly, string defaultValue);

		// Token: 0x06000D49 RID: 3401 RVA: 0x000118BC File Offset: 0x0000FABC
		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this.GetTagImpl(tag, !searchFallbacks, defaultValue);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000118DC File Offset: 0x0000FADC
		public string GetTag(string tag, bool searchFallbacks)
		{
			return this.GetTagImpl(tag, !searchFallbacks, "");
		}

		// Token: 0x06000D4B RID: 3403
		[NativeThrows]
		[FreeFunction("MaterialScripting::Lerp", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Lerp(Material start, Material end, float t);

		// Token: 0x06000D4C RID: 3404
		[FreeFunction("MaterialScripting::SetPass", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool SetPass(int pass);

		// Token: 0x06000D4D RID: 3405
		[FreeFunction("MaterialScripting::CopyPropertiesFrom", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		// Token: 0x06000D4E RID: 3406
		[FreeFunction("MaterialScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern string[] GetShaderKeywords();

		// Token: 0x06000D4F RID: 3407
		[FreeFunction("MaterialScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetShaderKeywords(string[] names);

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00011900 File Offset: 0x0000FB00
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00011918 File Offset: 0x0000FB18
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x06000D52 RID: 3410
		[MethodImpl(4096)]
		public extern int ComputeCRC();

		// Token: 0x06000D53 RID: 3411
		[FreeFunction("MaterialScripting::GetTexturePropertyNames", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern string[] GetTexturePropertyNames();

		// Token: 0x06000D54 RID: 3412
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDs", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int[] GetTexturePropertyNameIDs();

		// Token: 0x06000D55 RID: 3413
		[FreeFunction("MaterialScripting::GetTexturePropertyNamesInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTexturePropertyNamesInternal(object outNames);

		// Token: 0x06000D56 RID: 3414
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDsInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void GetTexturePropertyNameIDsInternal(object outNames);

		// Token: 0x06000D57 RID: 3415 RVA: 0x00011924 File Offset: 0x0000FB24
		public void GetTexturePropertyNames(List<string> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNamesInternal(outNames);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00011950 File Offset: 0x0000FB50
		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNameIDsInternal(outNames);
		}

		// Token: 0x06000D59 RID: 3417
		[NativeName("SetIntFromScript")]
		[MethodImpl(4096)]
		private extern void SetIntImpl(int name, int value);

		// Token: 0x06000D5A RID: 3418
		[NativeName("SetFloatFromScript")]
		[MethodImpl(4096)]
		private extern void SetFloatImpl(int name, float value);

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001197A File Offset: 0x0000FB7A
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00011985 File Offset: 0x0000FB85
		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06000D5D RID: 3421
		[NativeName("SetTextureFromScript")]
		[MethodImpl(4096)]
		private extern void SetTextureImpl(int name, Texture value);

		// Token: 0x06000D5E RID: 3422
		[NativeName("SetRenderTextureFromScript")]
		[MethodImpl(4096)]
		private extern void SetRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		// Token: 0x06000D5F RID: 3423
		[NativeName("SetBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		// Token: 0x06000D60 RID: 3424
		[NativeName("SetBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x06000D61 RID: 3425
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x06000D62 RID: 3426
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(4096)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x06000D63 RID: 3427
		[NativeName("GetIntFromScript")]
		[MethodImpl(4096)]
		private extern int GetIntImpl(int name);

		// Token: 0x06000D64 RID: 3428
		[NativeName("GetFloatFromScript")]
		[MethodImpl(4096)]
		private extern float GetFloatImpl(int name);

		// Token: 0x06000D65 RID: 3429 RVA: 0x00011990 File Offset: 0x0000FB90
		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			Color color;
			this.GetColorImpl_Injected(name, out color);
			return color;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000119A8 File Offset: 0x0000FBA8
		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			this.GetMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000D67 RID: 3431
		[NativeName("GetTextureFromScript")]
		[MethodImpl(4096)]
		private extern Texture GetTextureImpl(int name);

		// Token: 0x06000D68 RID: 3432
		[FreeFunction(Name = "MaterialScripting::SetFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x06000D69 RID: 3433
		[FreeFunction(Name = "MaterialScripting::SetVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x06000D6A RID: 3434
		[FreeFunction(Name = "MaterialScripting::SetColorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetColorArrayImpl(int name, Color[] values, int count);

		// Token: 0x06000D6B RID: 3435
		[FreeFunction(Name = "MaterialScripting::SetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x06000D6C RID: 3436
		[FreeFunction(Name = "MaterialScripting::GetFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern float[] GetFloatArrayImpl(int name);

		// Token: 0x06000D6D RID: 3437
		[FreeFunction(Name = "MaterialScripting::GetVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		// Token: 0x06000D6E RID: 3438
		[FreeFunction(Name = "MaterialScripting::GetColorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Color[] GetColorArrayImpl(int name);

		// Token: 0x06000D6F RID: 3439
		[FreeFunction(Name = "MaterialScripting::GetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		// Token: 0x06000D70 RID: 3440
		[FreeFunction(Name = "MaterialScripting::GetFloatArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetFloatArrayCountImpl(int name);

		// Token: 0x06000D71 RID: 3441
		[FreeFunction(Name = "MaterialScripting::GetVectorArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetVectorArrayCountImpl(int name);

		// Token: 0x06000D72 RID: 3442
		[FreeFunction(Name = "MaterialScripting::GetColorArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetColorArrayCountImpl(int name);

		// Token: 0x06000D73 RID: 3443
		[FreeFunction(Name = "MaterialScripting::GetMatrixArrayCount", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern int GetMatrixArrayCountImpl(int name);

		// Token: 0x06000D74 RID: 3444
		[FreeFunction(Name = "MaterialScripting::ExtractFloatArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x06000D75 RID: 3445
		[FreeFunction(Name = "MaterialScripting::ExtractVectorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x06000D76 RID: 3446
		[FreeFunction(Name = "MaterialScripting::ExtractColorArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractColorArrayImpl(int name, [Out] Color[] val);

		// Token: 0x06000D77 RID: 3447
		[FreeFunction(Name = "MaterialScripting::ExtractMatrixArray", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x06000D78 RID: 3448 RVA: 0x000119C0 File Offset: 0x0000FBC0
		[NativeName("GetTextureScaleAndOffsetFromScript")]
		private Vector4 GetTextureScaleAndOffsetImpl(int name)
		{
			Vector4 vector;
			this.GetTextureScaleAndOffsetImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000119D7 File Offset: 0x0000FBD7
		[NativeName("SetTextureOffsetFromScript")]
		private void SetTextureOffsetImpl(int name, Vector2 offset)
		{
			this.SetTextureOffsetImpl_Injected(name, ref offset);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000119E2 File Offset: 0x0000FBE2
		[NativeName("SetTextureScaleFromScript")]
		private void SetTextureScaleImpl(int name, Vector2 scale)
		{
			this.SetTextureScaleImpl_Injected(name, ref scale);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000119F0 File Offset: 0x0000FBF0
		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00011A44 File Offset: 0x0000FC44
		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00011A98 File Offset: 0x0000FC98
		private void SetColorArray(int name, Color[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetColorArrayImpl(name, values, count);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00011AEC File Offset: 0x0000FCEC
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00011B40 File Offset: 0x0000FD40
		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int floatArrayCountImpl = this.GetFloatArrayCountImpl(name);
			bool flag2 = floatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, floatArrayCountImpl);
				this.ExtractFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00011B98 File Offset: 0x0000FD98
		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int vectorArrayCountImpl = this.GetVectorArrayCountImpl(name);
			bool flag2 = vectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, vectorArrayCountImpl);
				this.ExtractVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		private void ExtractColorArray(int name, List<Color> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int colorArrayCountImpl = this.GetColorArrayCountImpl(name);
			bool flag2 = colorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Color>(values, colorArrayCountImpl);
				this.ExtractColorArrayImpl(name, (Color[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00011C48 File Offset: 0x0000FE48
		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int matrixArrayCountImpl = this.GetMatrixArrayCountImpl(name);
			bool flag2 = matrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, matrixArrayCountImpl);
				this.ExtractMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00011C9D File Offset: 0x0000FE9D
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00011CAF File Offset: 0x0000FEAF
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00011CCD File Offset: 0x0000FECD
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00011CD9 File Offset: 0x0000FED9
		public void SetInteger(string name, int value)
		{
			this.SetIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00011CEA File Offset: 0x0000FEEA
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00011CF6 File Offset: 0x0000FEF6
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00011D07 File Offset: 0x0000FF07
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00011D13 File Offset: 0x0000FF13
		public void SetVector(string name, Vector4 value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00011D29 File Offset: 0x0000FF29
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00011D3A File Offset: 0x0000FF3A
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00011D4B File Offset: 0x0000FF4B
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00011D57 File Offset: 0x0000FF57
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00011D68 File Offset: 0x0000FF68
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00011D74 File Offset: 0x0000FF74
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00011D86 File Offset: 0x0000FF86
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00011D93 File Offset: 0x0000FF93
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00011DC1 File Offset: 0x0000FFC1
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00011DCD File Offset: 0x0000FFCD
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00011DE1 File Offset: 0x0000FFE1
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00011E04 File Offset: 0x00010004
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00011E13 File Offset: 0x00010013
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00011E2F File Offset: 0x0001002F
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00011E46 File Offset: 0x00010046
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00011E5A File Offset: 0x0001005A
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00011E69 File Offset: 0x00010069
		public void SetColorArray(string name, List<Color> values)
		{
			this.SetColorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00011E85 File Offset: 0x00010085
		public void SetColorArray(int nameID, List<Color> values)
		{
			this.SetColorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00011E9C File Offset: 0x0001009C
		public void SetColorArray(string name, Color[] values)
		{
			this.SetColorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00011EB0 File Offset: 0x000100B0
		public void SetColorArray(int nameID, Color[] values)
		{
			this.SetColorArray(nameID, values, values.Length);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00011EBF File Offset: 0x000100BF
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00011EDB File Offset: 0x000100DB
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00011EF2 File Offset: 0x000100F2
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00011F06 File Offset: 0x00010106
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00011F15 File Offset: 0x00010115
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00011F31 File Offset: 0x00010131
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00011F48 File Offset: 0x00010148
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00011F5C File Offset: 0x0001015C
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00011F6C File Offset: 0x0001016C
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00011F8C File Offset: 0x0001018C
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00011FA8 File Offset: 0x000101A8
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00011FC8 File Offset: 0x000101C8
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00011FE4 File Offset: 0x000101E4
		public int GetInteger(string name)
		{
			return this.GetIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00012004 File Offset: 0x00010204
		public int GetInteger(int nameID)
		{
			return this.GetIntImpl(nameID);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00012020 File Offset: 0x00010220
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00012040 File Offset: 0x00010240
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001205C File Offset: 0x0001025C
		public Vector4 GetVector(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00012080 File Offset: 0x00010280
		public Vector4 GetVector(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000120A0 File Offset: 0x000102A0
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000120C0 File Offset: 0x000102C0
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000120DC File Offset: 0x000102DC
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000120FC File Offset: 0x000102FC
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00012118 File Offset: 0x00010318
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00012138 File Offset: 0x00010338
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00012160 File Offset: 0x00010360
		public Color[] GetColorArray(string name)
		{
			return this.GetColorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00012180 File Offset: 0x00010380
		public Color[] GetColorArray(int nameID)
		{
			return (this.GetColorArrayCountImpl(nameID) != 0) ? this.GetColorArrayImpl(nameID) : null;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000121A8 File Offset: 0x000103A8
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000121C8 File Offset: 0x000103C8
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000121F0 File Offset: 0x000103F0
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00012210 File Offset: 0x00010410
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00012235 File Offset: 0x00010435
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00012246 File Offset: 0x00010446
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00012252 File Offset: 0x00010452
		public void GetColorArray(string name, List<Color> values)
		{
			this.ExtractColorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00012263 File Offset: 0x00010463
		public void GetColorArray(int nameID, List<Color> values)
		{
			this.ExtractColorArray(nameID, values);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001226F File Offset: 0x0001046F
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00012280 File Offset: 0x00010480
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001228C File Offset: 0x0001048C
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001229D File Offset: 0x0001049D
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000122A9 File Offset: 0x000104A9
		public void SetTextureOffset(string name, Vector2 value)
		{
			this.SetTextureOffsetImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000122BA File Offset: 0x000104BA
		public void SetTextureOffset(int nameID, Vector2 value)
		{
			this.SetTextureOffsetImpl(nameID, value);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000122C6 File Offset: 0x000104C6
		public void SetTextureScale(string name, Vector2 value)
		{
			this.SetTextureScaleImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000122D7 File Offset: 0x000104D7
		public void SetTextureScale(int nameID, Vector2 value)
		{
			this.SetTextureScaleImpl(nameID, value);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000122E4 File Offset: 0x000104E4
		public Vector2 GetTextureOffset(string name)
		{
			return this.GetTextureOffset(Shader.PropertyToID(name));
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00012304 File Offset: 0x00010504
		public Vector2 GetTextureOffset(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.z, textureScaleAndOffsetImpl.w);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00012330 File Offset: 0x00010530
		public Vector2 GetTextureScale(string name)
		{
			return this.GetTextureScale(Shader.PropertyToID(name));
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00012350 File Offset: 0x00010550
		public Vector2 GetTextureScale(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.x, textureScaleAndOffsetImpl.y);
		}

		// Token: 0x06000DD1 RID: 3537
		[MethodImpl(4096)]
		private extern void EnableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x06000DD2 RID: 3538
		[MethodImpl(4096)]
		private extern void DisableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x06000DD3 RID: 3539
		[MethodImpl(4096)]
		private extern void SetLocalKeyword_Injected(ref LocalKeyword keyword, bool value);

		// Token: 0x06000DD4 RID: 3540
		[MethodImpl(4096)]
		private extern bool IsLocalKeywordEnabled_Injected(ref LocalKeyword keyword);

		// Token: 0x06000DD5 RID: 3541
		[MethodImpl(4096)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		// Token: 0x06000DD6 RID: 3542
		[MethodImpl(4096)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x06000DD7 RID: 3543
		[MethodImpl(4096)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		// Token: 0x06000DD8 RID: 3544
		[MethodImpl(4096)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x06000DD9 RID: 3545
		[MethodImpl(4096)]
		private extern void GetTextureScaleAndOffsetImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000DDA RID: 3546
		[MethodImpl(4096)]
		private extern void SetTextureOffsetImpl_Injected(int name, ref Vector2 offset);

		// Token: 0x06000DDB RID: 3547
		[MethodImpl(4096)]
		private extern void SetTextureScaleImpl_Injected(int name, ref Vector2 scale);
	}
}
