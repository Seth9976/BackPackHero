using System;

namespace System.Net
{
	// Token: 0x0200037A RID: 890
	internal static class RangeValidationHelpers
	{
		// Token: 0x06001D5F RID: 7519 RVA: 0x0006B274 File Offset: 0x00069474
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0006B284 File Offset: 0x00069484
		public static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}
	}
}
