using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012E RID: 302
	internal static class InputArrayExtensions
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x0004F644 File Offset: 0x0004D844
		public static int IndexOfReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			for (int i = 0; i < array.length; i++)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004F67C File Offset: 0x0004D87C
		public static bool Contains<TValue>(this InlinedArray<TValue> array, TValue value)
		{
			for (int i = 0; i < array.length; i++)
			{
				TValue tvalue = array[i];
				if (tvalue.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0004F6BB File Offset: 0x0004D8BB
		public static bool ContainsReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			return array.IndexOfReference(value) != -1;
		}
	}
}
