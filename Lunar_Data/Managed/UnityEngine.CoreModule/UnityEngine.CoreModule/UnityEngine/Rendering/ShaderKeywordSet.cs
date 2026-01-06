using System;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000423 RID: 1059
	[UsedByNativeCode]
	[NativeHeader("Editor/Src/Graphics/ShaderCompilerData.h")]
	public struct ShaderKeywordSet
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsGlobalKeywordEnabled(ShaderKeywordSet state, uint index)
		{
			return ShaderKeywordSet.IsGlobalKeywordEnabled_Injected(ref state, index);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x0003E7FE File Offset: 0x0003C9FE
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsKeywordEnabled(ShaderKeywordSet state, LocalKeywordSpace keywordSpace, uint index)
		{
			return ShaderKeywordSet.IsKeywordEnabled_Injected(ref state, ref keywordSpace, index);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0003E80A File Offset: 0x0003CA0A
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsKeywordNameEnabled(ShaderKeywordSet state, string name)
		{
			return ShaderKeywordSet.IsKeywordNameEnabled_Injected(ref state, name);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0003E814 File Offset: 0x0003CA14
		[FreeFunction("keywords::EnableKeyword")]
		private static void EnableGlobalKeyword(ShaderKeywordSet state, uint index)
		{
			ShaderKeywordSet.EnableGlobalKeyword_Injected(ref state, index);
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0003E81E File Offset: 0x0003CA1E
		[FreeFunction("keywords::EnableKeyword")]
		private static void EnableKeywordName(ShaderKeywordSet state, string name)
		{
			ShaderKeywordSet.EnableKeywordName_Injected(ref state, name);
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0003E828 File Offset: 0x0003CA28
		[FreeFunction("keywords::DisableKeyword")]
		private static void DisableGlobalKeyword(ShaderKeywordSet state, uint index)
		{
			ShaderKeywordSet.DisableGlobalKeyword_Injected(ref state, index);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0003E832 File Offset: 0x0003CA32
		[FreeFunction("keywords::DisableKeyword")]
		private static void DisableKeywordName(ShaderKeywordSet state, string name)
		{
			ShaderKeywordSet.DisableKeywordName_Injected(ref state, name);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0003E83C File Offset: 0x0003CA3C
		[FreeFunction("keywords::GetEnabledKeywords")]
		private static ShaderKeyword[] GetEnabledKeywords(ShaderKeywordSet state)
		{
			return ShaderKeywordSet.GetEnabledKeywords_Injected(ref state);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0003E848 File Offset: 0x0003CA48
		private void CheckKeywordCompatible(ShaderKeyword keyword)
		{
			bool isLocal = keyword.m_IsLocal;
			if (isLocal)
			{
				bool flag = this.m_Shader != IntPtr.Zero;
				if (flag)
				{
					Assert.IsTrue(!keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
				else
				{
					Assert.IsTrue(keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x0003E8A0 File Offset: 0x0003CAA0
		public bool IsEnabled(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			return ShaderKeywordSet.IsKeywordNameEnabled(this, keyword.m_Name);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0003E8CC File Offset: 0x0003CACC
		public bool IsEnabled(GlobalKeyword keyword)
		{
			return ShaderKeywordSet.IsGlobalKeywordEnabled(this, keyword.m_Index);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0003E8F0 File Offset: 0x0003CAF0
		public bool IsEnabled(LocalKeyword keyword)
		{
			return ShaderKeywordSet.IsKeywordEnabled(this, keyword.m_SpaceInfo, keyword.m_Index);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0003E91C File Offset: 0x0003CB1C
		public void Enable(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			bool flag = keyword.m_IsLocal || !keyword.IsValid();
			if (flag)
			{
				ShaderKeywordSet.EnableKeywordName(this, keyword.m_Name);
			}
			else
			{
				ShaderKeywordSet.EnableGlobalKeyword(this, keyword.m_Index);
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0003E974 File Offset: 0x0003CB74
		public void Disable(ShaderKeyword keyword)
		{
			bool flag = keyword.m_IsLocal || !keyword.IsValid();
			if (flag)
			{
				ShaderKeywordSet.DisableKeywordName(this, keyword.m_Name);
			}
			else
			{
				ShaderKeywordSet.DisableGlobalKeyword(this, keyword.m_Index);
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0003E9C4 File Offset: 0x0003CBC4
		public ShaderKeyword[] GetShaderKeywords()
		{
			return ShaderKeywordSet.GetEnabledKeywords(this);
		}

		// Token: 0x06002504 RID: 9476
		[MethodImpl(4096)]
		private static extern bool IsGlobalKeywordEnabled_Injected(ref ShaderKeywordSet state, uint index);

		// Token: 0x06002505 RID: 9477
		[MethodImpl(4096)]
		private static extern bool IsKeywordEnabled_Injected(ref ShaderKeywordSet state, ref LocalKeywordSpace keywordSpace, uint index);

		// Token: 0x06002506 RID: 9478
		[MethodImpl(4096)]
		private static extern bool IsKeywordNameEnabled_Injected(ref ShaderKeywordSet state, string name);

		// Token: 0x06002507 RID: 9479
		[MethodImpl(4096)]
		private static extern void EnableGlobalKeyword_Injected(ref ShaderKeywordSet state, uint index);

		// Token: 0x06002508 RID: 9480
		[MethodImpl(4096)]
		private static extern void EnableKeywordName_Injected(ref ShaderKeywordSet state, string name);

		// Token: 0x06002509 RID: 9481
		[MethodImpl(4096)]
		private static extern void DisableGlobalKeyword_Injected(ref ShaderKeywordSet state, uint index);

		// Token: 0x0600250A RID: 9482
		[MethodImpl(4096)]
		private static extern void DisableKeywordName_Injected(ref ShaderKeywordSet state, string name);

		// Token: 0x0600250B RID: 9483
		[MethodImpl(4096)]
		private static extern ShaderKeyword[] GetEnabledKeywords_Injected(ref ShaderKeywordSet state);

		// Token: 0x04000DB1 RID: 3505
		private IntPtr m_KeywordState;

		// Token: 0x04000DB2 RID: 3506
		private IntPtr m_Shader;

		// Token: 0x04000DB3 RID: 3507
		private IntPtr m_ComputeShader;

		// Token: 0x04000DB4 RID: 3508
		private ulong m_StateIndex;
	}
}
