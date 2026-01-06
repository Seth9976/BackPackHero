using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002B0 RID: 688
	[NativeHeader("PlatformDependent/Win/Webcam/WebCam.h")]
	[StaticAccessor("WebCam::GetInstance()", StaticAccessorType.Dot)]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public class WebCam
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001CFB RID: 7419
		public static extern WebCamMode Mode
		{
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
			[NativeName("GetWebCamMode")]
			[MethodImpl(4096)]
			get;
		}
	}
}
