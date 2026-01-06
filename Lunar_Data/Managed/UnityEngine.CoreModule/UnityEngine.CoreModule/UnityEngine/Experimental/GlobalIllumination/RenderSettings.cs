using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000465 RID: 1125
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	public class RenderSettings
	{
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060027DA RID: 10202
		// (set) Token: 0x060027DB RID: 10203
		public static extern bool useRadianceAmbientProbe
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
