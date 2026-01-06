using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000422 RID: 1058
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[UsedByNativeCode]
	public struct ShaderKeyword
	{
		// Token: 0x060024DA RID: 9434
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(4096)]
		internal static extern uint GetGlobalKeywordCount();

		// Token: 0x060024DB RID: 9435
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		[MethodImpl(4096)]
		internal static extern uint GetGlobalKeywordIndex(string keyword);

		// Token: 0x060024DC RID: 9436
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		[MethodImpl(4096)]
		internal static extern uint GetKeywordCount(Shader shader);

		// Token: 0x060024DD RID: 9437
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(4096)]
		internal static extern uint GetKeywordIndex(Shader shader, string keyword);

		// Token: 0x060024DE RID: 9438
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		[MethodImpl(4096)]
		internal static extern uint GetComputeShaderKeywordCount(ComputeShader shader);

		// Token: 0x060024DF RID: 9439
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		[MethodImpl(4096)]
		internal static extern uint GetComputeShaderKeywordIndex(ComputeShader shader, string keyword);

		// Token: 0x060024E0 RID: 9440
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		[MethodImpl(4096)]
		internal static extern void CreateGlobalKeyword(string keyword);

		// Token: 0x060024E1 RID: 9441
		[FreeFunction("ShaderScripting::GetKeywordType")]
		[MethodImpl(4096)]
		internal static extern ShaderKeywordType GetGlobalShaderKeywordType(uint keyword);

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0003E580 File Offset: 0x0003C780
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0003E598 File Offset: 0x0003C798
		public static ShaderKeywordType GetGlobalKeywordType(ShaderKeyword index)
		{
			bool flag = index.IsValid();
			ShaderKeywordType shaderKeywordType;
			if (flag)
			{
				shaderKeywordType = ShaderKeyword.GetGlobalShaderKeywordType(index.m_Index);
			}
			else
			{
				shaderKeywordType = ShaderKeywordType.UserDefined;
			}
			return shaderKeywordType;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0003E5C8 File Offset: 0x0003C7C8
		public ShaderKeyword(string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			bool flag = this.m_Index >= ShaderKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				ShaderKeyword.CreateGlobalKeyword(keywordName);
				this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			}
			this.m_IsValid = true;
			this.m_IsLocal = false;
			this.m_IsCompute = false;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0003E627 File Offset: 0x0003C827
		public ShaderKeyword(Shader shader, string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetKeywordIndex(shader, keywordName);
			this.m_IsValid = this.m_Index < ShaderKeyword.GetKeywordCount(shader);
			this.m_IsLocal = true;
			this.m_IsCompute = false;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0003E660 File Offset: 0x0003C860
		public ShaderKeyword(ComputeShader shader, string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetComputeShaderKeywordIndex(shader, keywordName);
			this.m_IsValid = this.m_Index < ShaderKeyword.GetComputeShaderKeywordCount(shader);
			this.m_IsLocal = true;
			this.m_IsCompute = true;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0003E69C File Offset: 0x0003C89C
		public static bool IsKeywordLocal(ShaderKeyword keyword)
		{
			return keyword.m_IsLocal;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		public bool IsValid()
		{
			return this.m_IsValid;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0003E6CC File Offset: 0x0003C8CC
		public bool IsValid(ComputeShader shader)
		{
			return this.m_IsValid;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
		public bool IsValid(Shader shader)
		{
			return this.m_IsValid;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x0003E6FC File Offset: 0x0003C8FC
		public int index
		{
			get
			{
				return (int)this.m_Index;
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0003E714 File Offset: 0x0003C914
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0003E72C File Offset: 0x0003C92C
		[Obsolete("GetKeywordType is deprecated. Only global keywords can have a type. This method always returns ShaderKeywordType.UserDefined.")]
		public static ShaderKeywordType GetKeywordType(Shader shader, ShaderKeyword index)
		{
			return ShaderKeywordType.UserDefined;
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0003E740 File Offset: 0x0003C940
		[Obsolete("GetKeywordType is deprecated. Only global keywords can have a type. This method always returns ShaderKeywordType.UserDefined.")]
		public static ShaderKeywordType GetKeywordType(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeywordType.UserDefined;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0003E754 File Offset: 0x0003C954
		[Obsolete("GetGlobalKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetGlobalKeywordName(ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0003E76C File Offset: 0x0003C96C
		[Obsolete("GetKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetKeywordName(Shader shader, ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0003E784 File Offset: 0x0003C984
		[Obsolete("GetKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetKeywordName(ComputeShader shader, ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0003E79C File Offset: 0x0003C99C
		[Obsolete("GetKeywordType is deprecated. Use ShaderKeyword.name instead.")]
		public ShaderKeywordType GetKeywordType()
		{
			return ShaderKeyword.GetGlobalKeywordType(this);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0003E7BC File Offset: 0x0003C9BC
		[Obsolete("GetKeywordName is deprecated. Use ShaderKeyword.name instead.")]
		public string GetKeywordName()
		{
			return ShaderKeyword.GetGlobalKeywordName(this);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0003E7DC File Offset: 0x0003C9DC
		[Obsolete("GetName() has been deprecated. Use ShaderKeyword.name instead.")]
		public string GetName()
		{
			return this.GetKeywordName();
		}

		// Token: 0x04000DAC RID: 3500
		internal string m_Name;

		// Token: 0x04000DAD RID: 3501
		internal uint m_Index;

		// Token: 0x04000DAE RID: 3502
		internal bool m_IsLocal;

		// Token: 0x04000DAF RID: 3503
		internal bool m_IsCompute;

		// Token: 0x04000DB0 RID: 3504
		internal bool m_IsValid;
	}
}
