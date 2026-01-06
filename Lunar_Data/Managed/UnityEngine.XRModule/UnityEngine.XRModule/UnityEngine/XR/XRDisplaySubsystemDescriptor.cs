using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	public class XRDisplaySubsystemDescriptor : IntegratedSubsystemDescriptor<XRDisplaySubsystem>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011B RID: 283
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyVr
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011C RID: 284
		[NativeConditional("ENABLE_XR")]
		public extern bool enableBackBufferMSAA
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600011D RID: 285
		[NativeMethod("TryGetAvailableMirrorModeCount")]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(4096)]
		public extern int GetAvailableMirrorBlitModeCount();

		// Token: 0x0600011E RID: 286
		[NativeConditional("ENABLE_XR")]
		[NativeMethod("TryGetMirrorModeByIndex")]
		[MethodImpl(4096)]
		public extern void GetMirrorBlitModeByIndex(int index, out XRMirrorViewBlitModeDesc mode);
	}
}
