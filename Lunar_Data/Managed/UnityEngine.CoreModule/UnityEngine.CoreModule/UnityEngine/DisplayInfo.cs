using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000128 RID: 296
	[UsedByNativeCode]
	[NativeType("Runtime/Graphics/DisplayInfo.h")]
	public struct DisplayInfo : IEquatable<DisplayInfo>
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0000C488 File Offset: 0x0000A688
		[MethodImpl(256)]
		public bool Equals(DisplayInfo other)
		{
			return this.handle == other.handle && this.width == other.width && this.height == other.height && this.refreshRate.Equals(other.refreshRate) && this.workArea.Equals(other.workArea) && this.name == other.name;
		}

		// Token: 0x040003B8 RID: 952
		[RequiredMember]
		internal ulong handle;

		// Token: 0x040003B9 RID: 953
		[RequiredMember]
		public int width;

		// Token: 0x040003BA RID: 954
		[RequiredMember]
		public int height;

		// Token: 0x040003BB RID: 955
		[RequiredMember]
		public RefreshRate refreshRate;

		// Token: 0x040003BC RID: 956
		[RequiredMember]
		public RectInt workArea;

		// Token: 0x040003BD RID: 957
		[RequiredMember]
		public string name;
	}
}
