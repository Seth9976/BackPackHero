using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200041E RID: 1054
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public readonly struct LocalKeywordSpace : IEquatable<LocalKeywordSpace>
	{
		// Token: 0x060024BE RID: 9406 RVA: 0x0003E2DA File Offset: 0x0003C4DA
		[FreeFunction("keywords::GetKeywords", HasExplicitThis = true)]
		private LocalKeyword[] GetKeywords()
		{
			return LocalKeywordSpace.GetKeywords_Injected(ref this);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0003E2E2 File Offset: 0x0003C4E2
		[FreeFunction("keywords::GetKeywordNames", HasExplicitThis = true)]
		private string[] GetKeywordNames()
		{
			return LocalKeywordSpace.GetKeywordNames_Injected(ref this);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0003E2EA File Offset: 0x0003C4EA
		[FreeFunction("keywords::GetKeywordCount", HasExplicitThis = true)]
		private uint GetKeywordCount()
		{
			return LocalKeywordSpace.GetKeywordCount_Injected(ref this);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		[FreeFunction("keywords::GetKeyword", HasExplicitThis = true)]
		private LocalKeyword GetKeyword(string name)
		{
			LocalKeyword localKeyword;
			LocalKeywordSpace.GetKeyword_Injected(ref this, name, out localKeyword);
			return localKeyword;
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0003E30C File Offset: 0x0003C50C
		public LocalKeyword[] keywords
		{
			get
			{
				return this.GetKeywords();
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x0003E324 File Offset: 0x0003C524
		public string[] keywordNames
		{
			get
			{
				return this.GetKeywordNames();
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x0003E33C File Offset: 0x0003C53C
		public uint keywordCount
		{
			get
			{
				return this.GetKeywordCount();
			}
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0003E354 File Offset: 0x0003C554
		public LocalKeyword FindKeyword(string name)
		{
			return this.GetKeyword(name);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0003E370 File Offset: 0x0003C570
		public override bool Equals(object o)
		{
			bool flag;
			if (o is LocalKeywordSpace)
			{
				LocalKeywordSpace localKeywordSpace = (LocalKeywordSpace)o;
				flag = this.Equals(localKeywordSpace);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0003E39C File Offset: 0x0003C59C
		public bool Equals(LocalKeywordSpace rhs)
		{
			return this.m_KeywordSpace == rhs.m_KeywordSpace;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0003E3C0 File Offset: 0x0003C5C0
		public static bool operator ==(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		public static bool operator !=(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0003E3F8 File Offset: 0x0003C5F8
		public override int GetHashCode()
		{
			return this.m_KeywordSpace.GetHashCode();
		}

		// Token: 0x060024CB RID: 9419
		[MethodImpl(4096)]
		private static extern LocalKeyword[] GetKeywords_Injected(ref LocalKeywordSpace _unity_self);

		// Token: 0x060024CC RID: 9420
		[MethodImpl(4096)]
		private static extern string[] GetKeywordNames_Injected(ref LocalKeywordSpace _unity_self);

		// Token: 0x060024CD RID: 9421
		[MethodImpl(4096)]
		private static extern uint GetKeywordCount_Injected(ref LocalKeywordSpace _unity_self);

		// Token: 0x060024CE RID: 9422
		[MethodImpl(4096)]
		private static extern void GetKeyword_Injected(ref LocalKeywordSpace _unity_self, string name, out LocalKeyword ret);

		// Token: 0x04000DA0 RID: 3488
		private readonly IntPtr m_KeywordSpace;
	}
}
