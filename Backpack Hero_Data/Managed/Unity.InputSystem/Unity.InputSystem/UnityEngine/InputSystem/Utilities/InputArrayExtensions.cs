using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012E RID: 302
	internal static class InputArrayExtensions
	{
		// Token: 0x060010B5 RID: 4277 RVA: 0x0004F7EC File Offset: 0x0004D9EC
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

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004F824 File Offset: 0x0004DA24
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

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004F863 File Offset: 0x0004DA63
		public static bool ContainsReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			return array.IndexOfReference(value) != -1;
		}
	}
}
