using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200012F RID: 303
	internal struct TessLink
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0003CD4C File Offset: 0x0003AF4C
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

		// Token: 0x0600091A RID: 2330 RVA: 0x0003CDA8 File Offset: 0x0003AFA8
		internal static void DestroyLink(TessLink link)
		{
			link.ranks.Dispose();
			link.roots.Dispose();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0003CDC4 File Offset: 0x0003AFC4
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

		// Token: 0x0600091C RID: 2332 RVA: 0x0003CE20 File Offset: 0x0003B020
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

		// Token: 0x04000853 RID: 2131
		internal NativeArray<int> roots;

		// Token: 0x04000854 RID: 2132
		internal NativeArray<int> ranks;
	}
}
