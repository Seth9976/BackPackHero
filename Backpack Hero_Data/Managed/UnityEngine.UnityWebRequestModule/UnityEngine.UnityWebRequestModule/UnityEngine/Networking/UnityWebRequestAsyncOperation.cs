using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000008 RID: 8
	[UsedByNativeCode]
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h")]
	[NativeHeader("UnityWebRequestScriptingClasses.h")]
	[StructLayout(0)]
	public class UnityWebRequestAsyncOperation : AsyncOperation
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000035C4 File Offset: 0x000017C4
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000035CC File Offset: 0x000017CC
		public UnityWebRequest webRequest { get; internal set; }
	}
}
