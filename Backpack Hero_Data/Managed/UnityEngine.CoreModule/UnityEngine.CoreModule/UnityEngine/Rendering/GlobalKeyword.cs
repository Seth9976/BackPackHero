using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200041C RID: 1052
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[UsedByNativeCode]
	public readonly struct GlobalKeyword
	{
		// Token: 0x060024A1 RID: 9377
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(4096)]
		private static extern uint GetGlobalKeywordCount();

		// Token: 0x060024A2 RID: 9378
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		[MethodImpl(4096)]
		private static extern uint GetGlobalKeywordIndex(string keyword);

		// Token: 0x060024A3 RID: 9379
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		[MethodImpl(4096)]
		private static extern void CreateGlobalKeyword(string keyword);

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0003DFD8 File Offset: 0x0003C1D8
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
		public static GlobalKeyword Create(string name)
		{
			GlobalKeyword.CreateGlobalKeyword(name);
			return new GlobalKeyword(name);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0003E010 File Offset: 0x0003C210
		public GlobalKeyword(string name)
		{
			this.m_Name = name;
			this.m_Index = GlobalKeyword.GetGlobalKeywordIndex(name);
			bool flag = this.m_Index >= GlobalKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				Debug.LogErrorFormat("Global keyword {0} doesn't exist.", new object[] { name });
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0003E05C File Offset: 0x0003C25C
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x04000D9B RID: 3483
		internal readonly string m_Name;

		// Token: 0x04000D9C RID: 3484
		internal readonly uint m_Index;
	}
}
