using System;

namespace Pathfinding.Util
{
	// Token: 0x020000C9 RID: 201
	public class Checksum
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0003A600 File Offset: 0x00038800
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
