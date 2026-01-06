using System;
using Unity.Collections;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x0200001A RID: 26
	internal struct TessLink
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000059A4 File Offset: 0x00003BA4
		internal static TessLink CreateLink(int count, Allocator allocator)
		{
			TessLink tessLink = default(TessLink);
			tessLink.roots = new NativeArray<int>(count, allocator, NativeArrayOptions.ClearMemory);
			tessLink.ranks = new NativeArray<int>(count, allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < count; i++)
			{
				tessLink.roots[i] = i;
				tessLink.ranks[i] = 0;
			}
			return tessLink;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005A00 File Offset: 0x00003C00
		internal static void DestroyLink(TessLink link)
		{
			link.ranks.Dispose();
			link.roots.Dispose();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005A1C File Offset: 0x00003C1C
		internal int Find(int x)
		{
			int num = x;
			while (this.roots[x] != x)
			{
				x = this.roots[x];
			}
			while (this.roots[num] != x)
			{
				int num2 = this.roots[num];
				this.roots[num] = x;
				num = num2;
			}
			return x;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005A78 File Offset: 0x00003C78
		internal void Link(int x, int y)
		{
			int num = this.Find(x);
			int num2 = this.Find(y);
			if (num == num2)
			{
				return;
			}
			int num3 = this.ranks[num];
			int num4 = this.ranks[num2];
			if (num3 < num4)
			{
				this.roots[num] = num2;
				return;
			}
			if (num4 < num3)
			{
				this.roots[num2] = num;
				return;
			}
			this.roots[num2] = num;
			int num5 = num;
			int num6 = this.ranks[num5] + 1;
			this.ranks[num5] = num6;
		}

		// Token: 0x04000042 RID: 66
		internal NativeArray<int> roots;

		// Token: 0x04000043 RID: 67
		internal NativeArray<int> ranks;
	}
}
