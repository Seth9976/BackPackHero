using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003ED RID: 1005
	[UsedByNativeCode]
	internal struct CoreCameraValues : IEquatable<CoreCameraValues>
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x000388A0 File Offset: 0x00036AA0
		public bool Equals(CoreCameraValues other)
		{
			return this.filterMode == other.filterMode && this.cullingMask == other.cullingMask && this.instanceID == other.instanceID;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000388E0 File Offset: 0x00036AE0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CoreCameraValues && this.Equals((CoreCameraValues)obj);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00038918 File Offset: 0x00036B18
		public override int GetHashCode()
		{
			int num = this.filterMode;
			num = (num * 397) ^ (int)this.cullingMask;
			return (num * 397) ^ this.instanceID;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00038954 File Offset: 0x00036B54
		public static bool operator ==(CoreCameraValues left, CoreCameraValues right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00038970 File Offset: 0x00036B70
		public static bool operator !=(CoreCameraValues left, CoreCameraValues right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000C73 RID: 3187
		private int filterMode;

		// Token: 0x04000C74 RID: 3188
		private uint cullingMask;

		// Token: 0x04000C75 RID: 3189
		private int instanceID;
	}
}
