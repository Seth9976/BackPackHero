using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Subsystems
{
	// Token: 0x02000011 RID: 17
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystem.h")]
	[UsedByNativeCode]
	public class ExampleSubsystem : IntegratedSubsystem<ExampleSubsystemDescriptor>
	{
		// Token: 0x0600004F RID: 79
		[MethodImpl(4096)]
		public extern void PrintExample();

		// Token: 0x06000050 RID: 80
		[MethodImpl(4096)]
		public extern bool GetBool();
	}
}
