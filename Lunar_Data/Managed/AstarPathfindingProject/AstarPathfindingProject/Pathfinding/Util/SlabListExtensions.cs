using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.Util
{
	// Token: 0x02000256 RID: 598
	public static class SlabListExtensions
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x0005840C File Offset: 0x0005660C
		public static void Remove<[IsUnmanaged] T>(this SlabAllocator<T>.List list, T value) where T : struct, ValueType, IEquatable<T>
		{
			int num = list.span.IndexOf(value);
			if (num != -1)
			{
				list.RemoveAt(num);
			}
		}
	}
}
