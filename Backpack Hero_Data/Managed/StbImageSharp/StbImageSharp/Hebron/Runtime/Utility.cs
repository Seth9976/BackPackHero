using System;

namespace Hebron.Runtime
{
	// Token: 0x02000004 RID: 4
	internal class Utility
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002368 File Offset: 0x00000568
		public static T[][] CreateArray<T>(int d1, int d2)
		{
			T[][] array = new T[d1][];
			for (int i = 0; i < d1; i++)
			{
				array[i] = new T[d2];
			}
			return array;
		}
	}
}
