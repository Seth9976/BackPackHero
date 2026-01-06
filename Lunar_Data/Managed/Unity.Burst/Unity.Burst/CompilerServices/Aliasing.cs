using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000023 RID: 35
	public static class Aliasing
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000793E File Offset: 0x00005B3E
		public unsafe static void ExpectAliased(void* a, void* b)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007940 File Offset: 0x00005B40
		public static void ExpectAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007942 File Offset: 0x00005B42
		public unsafe static void ExpectAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007944 File Offset: 0x00005B44
		public unsafe static void ExpectAliased<A>(in A a, void* b) where A : struct
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007946 File Offset: 0x00005B46
		public unsafe static void ExpectNotAliased(void* a, void* b)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007948 File Offset: 0x00005B48
		public static void ExpectNotAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000794A File Offset: 0x00005B4A
		public unsafe static void ExpectNotAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000794C File Offset: 0x00005B4C
		public unsafe static void ExpectNotAliased<A>(in A a, void* b) where A : struct
		{
		}
	}
}
