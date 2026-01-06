using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200094B RID: 2379
	internal class ConstantHelper
	{
		// Token: 0x06005369 RID: 21353 RVA: 0x00105D9C File Offset: 0x00103F9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte b = 0;
			*(&b) = byte.MaxValue;
			return b;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x00105DB8 File Offset: 0x00103FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte b = 0;
			*(&b) = -1;
			return b;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00105DD0 File Offset: 0x00103FD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort num = 0;
			*(&num) = ushort.MaxValue;
			return num;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00105DEC File Offset: 0x00103FEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x00105E04 File Offset: 0x00104004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint num = 0U;
			*(&num) = uint.MaxValue;
			return num;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x00105E1C File Offset: 0x0010401C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int num = 0;
			*(&num) = -1;
			return num;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00105E34 File Offset: 0x00104034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong num = 0UL;
			*(&num) = ulong.MaxValue;
			return num;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00105E4C File Offset: 0x0010404C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long num = 0L;
			*(&num) = -1L;
			return num;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00105E64 File Offset: 0x00104064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float num = 0f;
			*(int*)(&num) = -1;
			return num;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00105E80 File Offset: 0x00104080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double num = 0.0;
			*(long*)(&num) = -1L;
			return num;
		}
	}
}
