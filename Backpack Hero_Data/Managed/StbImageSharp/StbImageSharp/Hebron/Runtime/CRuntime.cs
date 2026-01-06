using System;
using System.Runtime.InteropServices;

namespace Hebron.Runtime
{
	// Token: 0x02000002 RID: 2
	internal static class CRuntime
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public unsafe static void* malloc(ulong size)
		{
			return CRuntime.malloc((long)size);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public unsafe static void* malloc(long size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal((int)size);
			MemoryStats.Allocated();
			return intPtr.ToPointer();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002079 File Offset: 0x00000279
		public unsafe static void free(void* a)
		{
			if (a == null)
			{
				return;
			}
			Marshal.FreeHGlobal(new IntPtr(a));
			MemoryStats.Freed();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		public unsafe static void memcpy(void* a, void* b, long size)
		{
			byte* ptr = (byte*)a;
			byte* ptr2 = (byte*)b;
			for (long num = 0L; num < size; num += 1L)
			{
				*(ptr++) = *(ptr2++);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020BF File Offset: 0x000002BF
		public unsafe static void memcpy(void* a, void* b, ulong size)
		{
			CRuntime.memcpy(a, b, (long)size);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020CC File Offset: 0x000002CC
		public unsafe static void memmove(void* a, void* b, long size)
		{
			void* ptr = null;
			try
			{
				ptr = CRuntime.malloc(size);
				CRuntime.memcpy(ptr, b, size);
				CRuntime.memcpy(a, ptr, size);
			}
			finally
			{
				if (ptr != null)
				{
					CRuntime.free(ptr);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		public unsafe static void memmove(void* a, void* b, ulong size)
		{
			CRuntime.memmove(a, b, (long)size);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public unsafe static int memcmp(void* a, void* b, long size)
		{
			int num = 0;
			byte* ptr = (byte*)a;
			byte* ptr2 = (byte*)b;
			for (long num2 = 0L; num2 < size; num2 += 1L)
			{
				if (*ptr != *ptr2)
				{
					num++;
				}
				ptr++;
				ptr2++;
			}
			return num;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002154 File Offset: 0x00000354
		public unsafe static int memcmp(void* a, void* b, ulong size)
		{
			return CRuntime.memcmp(a, b, (long)size);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		public unsafe static int memcmp(byte* a, byte[] b, ulong size)
		{
			void* ptr;
			if (b == null || b.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&b[0]);
			}
			return CRuntime.memcmp((void*)a, ptr, (long)size);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002190 File Offset: 0x00000390
		public unsafe static void memset(void* ptr, int value, long size)
		{
			byte* ptr2 = (byte*)ptr;
			byte b = (byte)value;
			for (long num = 0L; num < size; num += 1L)
			{
				*(ptr2++) = b;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B7 File Offset: 0x000003B7
		public unsafe static void memset(void* ptr, int value, ulong size)
		{
			CRuntime.memset(ptr, value, (long)size);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C1 File Offset: 0x000003C1
		public static uint _lrotl(uint x, int y)
		{
			return (x << y) | (x >> 32 - y);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D4 File Offset: 0x000003D4
		public unsafe static void* realloc(void* a, long newSize)
		{
			if (a == null)
			{
				return CRuntime.malloc(newSize);
			}
			return Marshal.ReAllocHGlobal(new IntPtr(a), new IntPtr(newSize)).ToPointer();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002206 File Offset: 0x00000406
		public unsafe static void* realloc(void* a, ulong newSize)
		{
			return CRuntime.realloc(a, (long)newSize);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220F File Offset: 0x0000040F
		public static int abs(int v)
		{
			return Math.Abs(v);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002217 File Offset: 0x00000417
		public static double pow(double a, double b)
		{
			return Math.Pow(a, b);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00000420
		public static void SetArray<T>(T[] data, T value)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = value;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002243 File Offset: 0x00000443
		public static double ldexp(double number, int exponent)
		{
			return number * Math.Pow(2.0, (double)exponent);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002258 File Offset: 0x00000458
		public unsafe static int strcmp(sbyte* src, string token)
		{
			int num = 0;
			for (int i = 0; i < token.Length; i++)
			{
				if ((char)src[i] != token.get_Chars(i))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000228C File Offset: 0x0000048C
		public unsafe static int strncmp(sbyte* src, string token, ulong size)
		{
			int num = 0;
			for (int i = 0; i < Math.Min(token.Length, (int)size); i++)
			{
				if ((char)src[i] != token.get_Chars(i))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022C8 File Offset: 0x000004C8
		public unsafe static long strtol(sbyte* start, sbyte** end, int radix)
		{
			int i = 0;
			sbyte* ptr = start;
			while (CRuntime.numbers.IndexOf((char)(*ptr)) != -1)
			{
				ptr++;
				i++;
			}
			long num = 0L;
			ptr = start;
			while (i > 0)
			{
				long num2 = (long)CRuntime.numbers.IndexOf((char)(*ptr));
				long num3 = (long)Math.Pow(10.0, (double)(i - 1));
				num += num2 * num3;
				ptr++;
				i--;
			}
			if (end != null)
			{
				*(IntPtr*)end = ptr;
			}
			return num;
		}

		// Token: 0x04000001 RID: 1
		private static readonly string numbers = "0123456789";
	}
}
