using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000022 RID: 34
	internal static class SpriteLibraryUtility
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000038E0 File Offset: 0x00001AE0
		internal static int Convert32BitTo30BitHash(int input)
		{
			return SpriteLibraryUtility.PreserveFirst30Bits(input);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038E8 File Offset: 0x00001AE8
		private static int Bit30Hash_GetStringHash(string value)
		{
			return SpriteLibraryUtility.PreserveFirst30Bits(Animator.StringToHash(value));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000038F5 File Offset: 0x00001AF5
		private static int PreserveFirst30Bits(int input)
		{
			return input & 1073741823;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003900 File Offset: 0x00001B00
		internal static long GenerateHash()
		{
			return DateTime.Now.Ticks;
		}

		// Token: 0x04000043 RID: 67
		internal static Func<string, int> GetStringHash = new Func<string, int>(SpriteLibraryUtility.Bit30Hash_GetStringHash);
	}
}
