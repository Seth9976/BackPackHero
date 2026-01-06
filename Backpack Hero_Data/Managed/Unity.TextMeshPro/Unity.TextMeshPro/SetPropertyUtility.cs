using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000040 RID: 64
	internal static class SetPropertyUtility
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00022A04 File Offset: 0x00020C04
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00022A53 File Offset: 0x00020C53
		public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00022A6E File Offset: 0x00020C6E
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00022A90 File Offset: 0x00020C90
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
