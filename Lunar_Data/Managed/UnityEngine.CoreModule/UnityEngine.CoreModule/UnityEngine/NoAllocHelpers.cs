using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200020E RID: 526
	[NativeHeader("Runtime/Export/Scripting/NoAllocHelpers.bindings.h")]
	internal sealed class NoAllocHelpers
	{
		// Token: 0x0600173A RID: 5946 RVA: 0x000254A4 File Offset: 0x000236A4
		public static void ResizeList<T>(List<T> list, int size)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list");
			}
			bool flag2 = size < 0 || size > list.Capacity;
			if (flag2)
			{
				throw new ArgumentException("invalid size to resize.", "list");
			}
			bool flag3 = size != list.Count;
			if (flag3)
			{
				NoAllocHelpers.Internal_ResizeList(list, size);
			}
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00025504 File Offset: 0x00023704
		public static void EnsureListElemCount<T>(List<T> list, int count)
		{
			list.Clear();
			bool flag = list.Capacity < count;
			if (flag)
			{
				list.Capacity = count;
			}
			NoAllocHelpers.ResizeList<T>(list, count);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00025538 File Offset: 0x00023738
		public static int SafeLength(Array values)
		{
			return (values != null) ? values.Length : 0;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00025558 File Offset: 0x00023758
		public static int SafeLength<T>(List<T> values)
		{
			return (values != null) ? values.Count : 0;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00025578 File Offset: 0x00023778
		public static T[] ExtractArrayFromListT<T>(List<T> list)
		{
			return (T[])NoAllocHelpers.ExtractArrayFromList(list);
		}

		// Token: 0x0600173F RID: 5951
		[FreeFunction("NoAllocHelpers_Bindings::Internal_ResizeList")]
		[MethodImpl(4096)]
		internal static extern void Internal_ResizeList(object list, int size);

		// Token: 0x06001740 RID: 5952
		[FreeFunction("NoAllocHelpers_Bindings::ExtractArrayFromList")]
		[MethodImpl(4096)]
		public static extern Array ExtractArrayFromList(object list);
	}
}
