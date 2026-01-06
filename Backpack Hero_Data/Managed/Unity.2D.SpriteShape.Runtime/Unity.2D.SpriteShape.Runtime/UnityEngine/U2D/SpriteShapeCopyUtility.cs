using System;
using Unity.Collections;

namespace UnityEngine.U2D
{
	// Token: 0x02000016 RID: 22
	internal class SpriteShapeCopyUtility<T> where T : struct
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00007168 File Offset: 0x00005368
		internal static void Copy(NativeSlice<T> dst, T[] src, int length)
		{
			NativeSlice<T> nativeSlice = new NativeSlice<T>(dst, 0, length);
			nativeSlice.CopyFrom(src);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007188 File Offset: 0x00005388
		internal static void Copy(T[] dst, NativeSlice<T> src, int length)
		{
			NativeSlice<T> nativeSlice = new NativeSlice<T>(src, 0, length);
			nativeSlice.CopyTo(dst);
		}
	}
}
