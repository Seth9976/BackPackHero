using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000022 RID: 34
	public static class Aliasing
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00007906 File Offset: 0x00005B06
		public unsafe static void ExpectAliased(void* a, void* b)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007908 File Offset: 0x00005B08
		public static void ExpectAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000790A File Offset: 0x00005B0A
		public unsafe static void ExpectAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000790C File Offset: 0x00005B0C
		public unsafe static void ExpectAliased<A>(in A a, void* b) where A : struct
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000790E File Offset: 0x00005B0E
		public unsafe static void ExpectNotAliased(void* a, void* b)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007910 File Offset: 0x00005B10
		public static void ExpectNotAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007912 File Offset: 0x00005B12
		public unsafe static void ExpectNotAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007914 File Offset: 0x00005B14
		public unsafe static void ExpectNotAliased<A>(in A a, void* b) where A : struct
		{
		}
	}
}
