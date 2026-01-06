using System;
using System.Collections;
using System.Security;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FE RID: 510
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x00024168 File Offset: 0x00022368
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		public unsafe static void InvokeMoveNext(IEnumerator enumerator, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			*(byte*)(void*)returnValueAddress = (enumerator.MoveNext() ? 1 : 0);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000241A4 File Offset: 0x000223A4
		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] array = null;
			bool flag = variable != null;
			if (flag)
			{
				array = new object[] { variable };
			}
			return behaviour.GetType().InvokeMember(name, 308, null, behaviour, array, null, null, null);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000241E4 File Offset: 0x000223E4
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] array = null;
			bool flag = variable != null;
			if (flag)
			{
				array = new object[] { variable };
			}
			return klass.InvokeMember(name, 312, null, null, array, null, null, null);
		}
	}
}
