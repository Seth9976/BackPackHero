using System;

namespace Pathfinding.Util
{
	// Token: 0x0200026F RID: 623
	public class Checksum
	{
		// Token: 0x06000EDB RID: 3803 RVA: 0x0005BCD4 File Offset: 0x00059ED4
		public static uint GetChecksum(byte[] arr, uint hash)
		{
			hash ^= 2166136261U;
			for (int i = 0; i < arr.Length; i++)
			{
				hash = (hash ^ (uint)arr[i]) * 16777619U;
			}
			return hash;
		}
	}
}
