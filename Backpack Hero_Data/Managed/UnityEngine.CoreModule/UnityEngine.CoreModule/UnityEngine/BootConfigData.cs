using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000EE RID: 238
	[NativeHeader("Runtime/Export/Bootstrap/BootConfig.bindings.h")]
	internal class BootConfigData
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00006E8F File Offset: 0x0000508F
		public void AddKey(string key)
		{
			this.Append(key, null);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00006E9C File Offset: 0x0000509C
		public string Get(string key)
		{
			return this.GetValue(key, 0);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00006EB8 File Offset: 0x000050B8
		public string Get(string key, int index)
		{
			return this.GetValue(key, index);
		}

		// Token: 0x06000443 RID: 1091
		[MethodImpl(4096)]
		public extern void Append(string key, string value);

		// Token: 0x06000444 RID: 1092
		[MethodImpl(4096)]
		public extern void Set(string key, string value);

		// Token: 0x06000445 RID: 1093
		[MethodImpl(4096)]
		private extern string GetValue(string key, int index);

		// Token: 0x06000446 RID: 1094 RVA: 0x00006ED4 File Offset: 0x000050D4
		[RequiredByNativeCode]
		private static BootConfigData WrapBootConfigData(IntPtr nativeHandle)
		{
			return new BootConfigData(nativeHandle);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00006EEC File Offset: 0x000050EC
		private BootConfigData(IntPtr nativeHandle)
		{
			bool flag = nativeHandle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("native handle can not be null");
			}
			this.m_Ptr = nativeHandle;
		}

		// Token: 0x04000323 RID: 803
		private IntPtr m_Ptr;
	}
}
